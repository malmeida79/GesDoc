using GesDoc.Web.Controllers;
using GesDoc.Web.Infraestructure;
using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GesDoc.Web.App
{
    public partial class cadControleAcesso : System.Web.UI.Page
    {
        #region declaracoes 

        GruposAcesso grupoAcesso = new GruposAcesso();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        FuncionalidadesController fnc = new FuncionalidadesController();
        GruposAcessoController CtrlGrupoAcesso;
        AcessosGruposController AcessosGrupoCtrl;
        Permissoes permissoes;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Gestão de acessos á grupos ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            CtrlGrupoAcesso = new GruposAcessoController();
            AcessosGrupoCtrl = new AcessosGruposController();

            ButtonBar.CancelarClick += new EventHandler(btnCancelar_Click);

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultCadBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Limpar, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, visivel: false);
                CarregarTela(Session["acessoGrupoEditar"].RecuperarValor<Int32>());
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["acessoGrupoEditar"] = string.Empty;
            Server.Transfer("listaControleAcesso.aspx");
        }

        protected void gdvControleAcesso_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gdvControleAcesso.EditIndex = e.NewEditIndex;
            CarregaGrid();
        }

        protected void gdvControleAcesso_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gdvControleAcesso.EditIndex = -1;
            CarregaGrid();
        }

        protected void gdvControleAcesso_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            // finaliza edição da linha de acesso
            gdvControleAcesso.EditIndex = -1;

            // recupera os dados do data table para alterações
            DataTable dt = (DataTable)Session["dtAcessos"];

            // verifica o que foi alterado na tabela para executar a acao
            GridViewRow row = gdvControleAcesso.Rows[e.RowIndex];
            dt.Rows[row.DataItemIndex]["acesso"] = ((CheckBox)(row.Cells[2].Controls[0])).Checked;
            dt.Rows[row.DataItemIndex]["leitura"] = ((CheckBox)(row.Cells[3].Controls[0])).Checked;
            dt.Rows[row.DataItemIndex]["gravacao"] = ((CheckBox)(row.Cells[4].Controls[0])).Checked;
            dt.Rows[row.DataItemIndex]["excluir"] = ((CheckBox)(row.Cells[5].Controls[0])).Checked;

            // caso sejam removidas as permissoes de leitura entao sera removido o acesso
            if (((CheckBox)(row.Cells[3].Controls[0])).Checked == false)
            {
                ((CheckBox)(row.Cells[2].Controls[0])).Checked = false;
                ((CheckBox)(row.Cells[3].Controls[0])).Checked = false;
                ((CheckBox)(row.Cells[4].Controls[0])).Checked = false;
                ((CheckBox)(row.Cells[5].Controls[0])).Checked = false;
                dt.Rows[row.DataItemIndex]["acesso"] = false;
                dt.Rows[row.DataItemIndex]["leitura"] = false;
                dt.Rows[row.DataItemIndex]["gravacao"] = false;
                dt.Rows[row.DataItemIndex]["excluir"] = false;
            }

            AcessosGrupos acessoGrupo = new AcessosGrupos();
            acessoGrupo.CodGrupo = Convert.ToInt32(hdnCodusuario.Value);
            acessoGrupo.CodGrupo = Convert.ToInt32(hdnCodusuario.Value);
            acessoGrupo.CodFuncionalidade = Convert.ToInt32(dt.Rows[row.DataItemIndex]["codFuncionalidade"]);
            acessoGrupo.Leitura = ((CheckBox)(row.Cells[3].Controls[0])).Checked;
            acessoGrupo.Gravacao = ((CheckBox)(row.Cells[4].Controls[0])).Checked;
            acessoGrupo.Excluir = ((CheckBox)(row.Cells[5].Controls[0])).Checked;

            // se marcada funcionalidade que não existia previamente, entao sera inserida conforme agora configurada;
            if (((CheckBox)(row.Cells[2].Controls[0])).Checked == true && Convert.ToBoolean(dt.Rows[row.DataItemIndex]["jahExistia"]) == false)
            {
                // Cadastrando funcionalidade nova clicada
                if (!AcessosGrupoCtrl.Inserir(acessoGrupo))
                {
                    Mensagens.Alerta(Mensagens.MsgErro);
                    return;
                }

                // apos cadastrado, passa entao a ser entendida como já existente para novas
                // edições
                dt.Rows[row.DataItemIndex]["JahExistia"] = true;
            }

            // se marcada funcionalidade que já existia, entao  a mesma sera alterada conforme agora configurada;
            if (((CheckBox)(row.Cells[2].Controls[0])).Checked == true && Convert.ToBoolean(dt.Rows[row.DataItemIndex]["jahExistia"]) == true)
            {
                if (!AcessosGrupoCtrl.Alterar(acessoGrupo))
                {
                    Mensagens.Alerta(Mensagens.MsgErro);
                    return;
                }

                // mantenho como já existente
                dt.Rows[row.DataItemIndex]["JahExistia"] = true;
            }

            // caso removida a mesma sera excluida
            else if (((CheckBox)(row.Cells[2].Controls[0])).Checked == false)
            {
                if (!AcessosGrupoCtrl.Excluir(acessoGrupo))
                {
                    Mensagens.Alerta(Mensagens.MsgErro);
                    return;
                }

                // apos excluido deixa entao a ser considerado como já existente
                // uma vez que a pagina so controi o data table quando nao postback
                dt.Rows[row.DataItemIndex]["JahExistia"] = false;
            }

            acessoGrupo = null;

            // recarrega o grid
            CarregaGrid();
        }

        protected void gdvControleAcesso_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvControleAcesso.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void btnClone_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty((string)Session["acessoGrupoEditar"]))
            {
                Server.Transfer("clonadorAcessosGrupo.aspx");
            }
            else
            {
                Mensagens.Alerta("Ocorreu um erro, por algum motivo o codigo do usuário não esta carregado. Por favor acesse novamente a tela.");
                return;
            }
        }

        #endregion

        #region Metodos

        private void CarregaGrid()
        {
            gdvControleAcesso.DataSource = Session["dtAcessos"];
            gdvControleAcesso.DataBind();
        }

        private void CarregarTela(Int32 valorRecebido = 0)
        {
            if (valorRecebido > 0)
            {
                // recebe usuario selecionado
                grupoAcesso.CodGrupo = valorRecebido;

                // busca dados adicionais do usuario selecionado
                grupoAcesso = CtrlGrupoAcesso.GetGrupoCodigo(grupoAcesso);

                // preenche tela com dados do usuario selecionado
                hdnCodusuario.Value = grupoAcesso.CodGrupo.ToString();
                lblGrupo.Text = $"{grupoAcesso.NomeGrupo}";

                // todas as funcionalidaeds existentes no cadastro
                List<Funcionalidades> lstFnc = new List<Funcionalidades>();
                lstFnc = fnc.GetAll();

                // lista de acessos que o usuario possui atualmente
                List<AcessosGrupos> lstAccGrp = new List<AcessosGrupos>();
                lstAccGrp = AcessosGrupoCtrl.GetAcessosGrupo(valorRecebido);

                // Listagem que sera carregada, e montada para exibicao no grid
                List<AcessosGrupos> listaNovosAcessos = new List<AcessosGrupos>();

                foreach (Funcionalidades item in lstFnc)
                {
                    AcessosGrupos novosAcessos = new AcessosGrupos();

                    // configurando acessos como padrao para false na criação para que o processo de 
                    // validação abaixo altere as permissoes conforme as mesmas estiverem cadastradas
                    novosAcessos.CodFuncionalidade = item.CodFuncionalidade;
                    novosAcessos.DescricaoAcesso = item.DescricaoFuncionalidade;
                    novosAcessos.Leitura = false;
                    novosAcessos.Gravacao = false;
                    novosAcessos.Excluir = false;
                    novosAcessos.Acesso = false;

                    // se possuir acessos configurados, podemos verifcar se existe correspondente a volta do 
                    // looping
                    if (lstAccGrp != null)
                    {
                        // consultando se usuário possui o acesso da volta do looping e como esta configurado.
                        AcessosGrupos validaAcesso = new AcessosGrupos();
                        validaAcesso = lstAccGrp.FirstOrDefault(S => S.CodFuncionalidade == Convert.ToInt32(item.CodFuncionalidade));

                        // caso usuario tenha esse acesso validamos as permissoes do mesmo.
                        if (validaAcesso != null)
                        {
                            novosAcessos.Acesso = true;
                            novosAcessos.JahExistia = true;

                            if (validaAcesso.Leitura == true)
                            {
                                novosAcessos.Leitura = true;
                            }

                            if (validaAcesso.Gravacao == true)
                            {
                                novosAcessos.Gravacao = true;
                            }

                            if (validaAcesso.Excluir == true)
                            {
                                novosAcessos.Excluir = true;
                            }
                        }

                        validaAcesso = null;
                    }

                    // adiciona o acesso a listagem para saida.
                    listaNovosAcessos.Add(novosAcessos);
                    novosAcessos = null;
                }

                // transformando a list gerada em um datatable para facilitar a manipulação
                // da mesma no grid e posterior acesso aos dados.
                DataTable dtAcessos = Tratamentos.GetDataTableFromList(listaNovosAcessos);

                //Persist the table in the Session object.
                Session["dtAcessos"] = dtAcessos;

                //Bind data to the GridView control.
                CarregaGrid();
            }
            else
            {

            }
        }

        #endregion
    }
}