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
    public partial class admGruposUsuario : System.Web.UI.Page
    {
        #region declaracoes 

        GruposAcesso grupoAcesso = new GruposAcesso();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        GruposAcesso GrpAcc = new GruposAcesso();
        GruposAcessoController CtrlGrupoAcesso;
        GruposUsuarioAcessoController CtrlGruposUsuarioAcesso;
        Permissoes permissoes;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Gestão de grupos do usuário ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            CtrlGrupoAcesso = new GruposAcessoController();

            ButtonBar.CancelarClick += new EventHandler(btnCancelar_Click);

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultCadBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Limpar, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, visivel: false);
                CarregarTela(Session["usuarioEditar"].RecuperarValor<Int32>());
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Server.Transfer("cadUsuarios.aspx");
        }

        protected void gdvGruposAcesso_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gdvGruposAcesso.EditIndex = e.NewEditIndex;
            CarregaGrid();
        }

        protected void gdvGruposAcesso_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gdvGruposAcesso.EditIndex = -1;
            CarregaGrid();
        }

        protected void gdvGruposAcesso_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            CtrlGruposUsuarioAcesso = new GruposUsuarioAcessoController();

            // finaliza edição da linha de acesso
            gdvGruposAcesso.EditIndex = -1;

            // recupera os dados do data table para alterações
            DataTable dt = (DataTable)Session["dtAcessos"];

            // verifica o que foi alterado na tabela para executar a acao
            GridViewRow row = gdvGruposAcesso.Rows[e.RowIndex];
            var codUsuario = Convert.ToInt32(hdnCodusuario.Value);
            var codGrupo = Convert.ToInt32(dt.Rows[row.DataItemIndex]["codgrupo"]);

            dt.Rows[row.DataItemIndex]["acesso"] = ((CheckBox)(row.Cells[3].Controls[0])).Checked;

            // se marcada funcionalidade que não existia previamente, entao sera inserida conforme agora configurada;
            if (((CheckBox)(row.Cells[3].Controls[0])).Checked == true && Convert.ToBoolean(dt.Rows[row.DataItemIndex]["JahExistia"]) == false)
            {
                // Cadastrando funcionalidade nova clicada
                if (!CtrlGruposUsuarioAcesso.Inserir(codGrupo, codUsuario))
                {
                    Mensagens.Alerta(Mensagens.MsgErro);
                    return;
                }

                // apos cadastrado, passa entao a ser entendida como já existente para novas
                // edições
                dt.Rows[row.DataItemIndex]["JahExistia"] = true;
            }

            // caso removida a mesma sera excluida
            else if (((CheckBox)(row.Cells[3].Controls[0])).Checked == false)
            {
                if (!CtrlGruposUsuarioAcesso.Excluir(codGrupo, codUsuario))
                {
                    Mensagens.Alerta(Mensagens.MsgErro);
                    return;
                }

                // apos excluido deixa entao a ser considerado como já existente
                // uma vez que a pagina so controi o data table quando nao postback
                dt.Rows[row.DataItemIndex]["JahExistia"] = false;
            }

            // recarrega o grid
            CarregaGrid();

            CtrlGruposUsuarioAcesso = null;
        }

        protected void gdvGruposAcesso_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvGruposAcesso.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void btnClone_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty((string)Session["usuarioEditar"]))
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
            gdvGruposAcesso.DataSource = Session["dtAcessos"];
            gdvGruposAcesso.DataBind();
        }

        private void CarregarTela(Int32 valorRecebido = 0)
        {
            if (valorRecebido > 0)
            {
                // guarda codigo do usuario
                hdnCodusuario.Value = valorRecebido.ToString();

                // todas os grupos existentes no cadastro
                List<GruposAcesso> lstGrp = new List<GruposAcesso>();
                lstGrp = CtrlGrupoAcesso.GetAll();

                CtrlGruposUsuarioAcesso = new GruposUsuarioAcessoController();

                // lista de grupos que o usuario possui atualmente
                List<GruposUsuarioAcesso> lstAccGrp = new List<GruposUsuarioAcesso>();
                lstAccGrp = CtrlGruposUsuarioAcesso.GetGruposAcessoUsuario(valorRecebido);

                // Listagem que sera carregada, e montada para exibicao no grid
                List<GruposUsuarioAcesso> listaGruposUsuario = new List<GruposUsuarioAcesso>();

                foreach (GruposAcesso item in lstGrp)
                {
                    GruposUsuarioAcesso grupoAcessoUsuario = new GruposUsuarioAcesso();

                    // configurando acessos como padrao para false na criação para que o processo de 
                    // validação abaixo altere as permissoes conforme as mesmas estiverem cadastradas
                    grupoAcessoUsuario.CodGrupo = item.CodGrupo;
                    grupoAcessoUsuario.NomeGrupo = item.NomeGrupo;
                    grupoAcessoUsuario.InfoGrupo = item.InfoGrupo;
                    grupoAcessoUsuario.Acesso = false;

                    // se possuir acessos configurados, podemos verifcar se existe correspondente a volta do 
                    // looping
                    if (lstAccGrp != null)
                    {
                        // consultando se usuário possui o acesso da volta do looping e como esta configurado.
                        GruposAcesso validaAcesso = new GruposAcesso();
                        validaAcesso = lstAccGrp.FirstOrDefault(S => S.CodGrupo == Convert.ToInt32(item.CodGrupo));

                        // caso usuario tenha esse acesso validamos as permissoes do mesmo.
                        if (validaAcesso != null)
                        {
                            grupoAcessoUsuario.Acesso = true;
                            grupoAcessoUsuario.JahExistia = true;

                        }

                        validaAcesso = null;
                    }

                    // adiciona o acesso a listagem para saida.
                    listaGruposUsuario.Add(grupoAcessoUsuario);
                    grupoAcessoUsuario = null;
                }

                // transformando a list gerada em um datatable para facilitar a manipulação
                // da mesma no grid e posterior acesso aos dados.
                DataTable dtAcessos = Tratamentos.GetDataTableFromList(listaGruposUsuario);

                //Persist the table in the Session object.
                Session["dtAcessos"] = dtAcessos;

                //Bind data to the GridView control.
                CarregaGrid();

                CtrlGruposUsuarioAcesso = null;
            }
        }

        #endregion
    }
}