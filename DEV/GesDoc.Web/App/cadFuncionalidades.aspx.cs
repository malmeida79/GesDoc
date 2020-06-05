using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class cadFuncionalidades : System.Web.UI.Page
    {
        #region declaracoes 

        Funcionalidades entFnc = new Funcionalidades();
        FuncionalidadesController CtrlFnc = new FuncionalidadesController();
        DepartamentosController CtrlDpto = new DepartamentosController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region Eventos

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            if (cboDepartamento.SelectedIndex <= 0)
            {
                Mensagens.Alerta("Necessário informar um departamento para cadastramento.");
                return;
            }

            if (!Validacoes.EstaPreenchido(txtNomeFuncionalidade.Text, 3))
            {
                Mensagens.Alerta("Necessário informar uma funcionalidade para cadastramento.");
                return;
            }

            // de acordo com a ação da tela o Funcionalidade podera
            // ser alterado ou cadastrado. Todo o controle e 
            // realizado pela sessao que apresenta o codigo
            // do Funcionalidade.
            entFnc.DescricaoFuncionalidade = txtNomeFuncionalidade.Text;
            entFnc.CodDepartamento = Convert.ToInt32(cboDepartamento.SelectedValue);
            entFnc.UrlFuncionalidade = txtUrlFuncionalidade.Text;
            entFnc.ExibeMenu = chkExibeMenu.Checked;
            entFnc.FuncionalidadePadrao = chkMenuItemPadrao.Checked;

            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) == "Salvar")
            {
                entFnc.CodFuncionalidade = Convert.ToInt32(hdnCodFuncionalidade.Value);

                if (CtrlFnc.Alterar(entFnc))
                {
                    Mensagens.Alerta("Dados alterados com sucesso.");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, texto: @"<span class="" glyphicon glyphicon-arrow-left""></span> Voltar");
                }
                else
                {
                    Mensagens.Alerta("Falha na alteração dos dados:{Tratamentos.MsgErro}");
                    return;
                }
            }
            else
            {
                if (CtrlFnc.Inserir(entFnc))
                {
                    Mensagens.Alerta("Dados cadastrados com sucesso.");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, texto: @"<span class="" glyphicon glyphicon-arrow-left""></span> Voltar");
                }
                else
                {
                    Mensagens.Alerta("Falha no cadastramento dos dados:{Tratamentos.MsgErro}");
                    return;
                }
            }
        }        

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Cadastro de funcionalidades ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            ButtonBar.AcaoClick += new EventHandler(btnAcao_Click);
            ButtonBar.CancelarClick += new EventHandler(btnCancelar_Click);

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultCadBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Limpar, visivel: false);
                CarregaDepartamentos();
                CarregarTela(Session["FuncionalidadeEditar"].RecuperarValor<Int32>());
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["FuncionalidadeEditar"] = string.Empty;
            Server.Transfer("listaFuncionalidades.aspx");
        }

        #endregion

        #region Metodos

        public void CarregaDepartamentos()
        {
            cboDepartamento.Preencher<Departamentos>(CtrlDpto.GetAll(), "descricaoDepartamento", "codDepartamento", true);
        }

        private void CarregarTela(Int32 valorRecebido = 0)
        {
            if (valorRecebido > 0)
            {
                entFnc.CodFuncionalidade = valorRecebido;
                entFnc = CtrlFnc.Pesquisar(entFnc);

                hdnCodFuncionalidade.Value = entFnc.CodFuncionalidade.ToString();
                txtNomeFuncionalidade.Text = entFnc.DescricaoFuncionalidade;
                txtUrlFuncionalidade.Text = entFnc.UrlFuncionalidade;
                chkExibeMenu.Checked = entFnc.ExibeMenu;
                chkMenuItemPadrao.Checked = entFnc.FuncionalidadePadrao;

                cboDepartamento.SetSelectedValue(entFnc.CodDepartamento.ToString());

                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
            }
            else
            {

            }
        }

        #endregion
    }
}