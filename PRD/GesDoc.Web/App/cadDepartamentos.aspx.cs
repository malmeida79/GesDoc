using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class cadDepartamentos : System.Web.UI.Page
    {
        #region declaracoes 

        Departamentos entDpto = new Departamentos();
        DepartamentosController CtrlDpto = new DepartamentosController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region Eventos

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            if (!Validacoes.EstaPreenchido(txtNomeDepartamento.Text, 3))
            {
                Mensagens.Alerta("Necessário informar um departamento para cadastro.");
                return;
            }

            // de acordo com a ação da tela o Departamento podera
            // ser alterado ou cadastrado. Todo o controle e 
            // realizado pela sessao que apresenta o codigo
            // do Departamento.
            entDpto.DescricaoDepartamento = txtNomeDepartamento.Text;
            entDpto.DepartamentoPadrao = chkMenuPadrao.Checked;
            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) == "Salvar")
            {
                entDpto.CodDepartamento = Convert.ToInt32(hdnCodDepartamento.Value);

                if (CtrlDpto.Alterar(entDpto))
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
                if (CtrlDpto.Inserir(entDpto))
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
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Cadastro de departamentos ::";
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
                CarregarTela(Session["DepartamentoEditar"].RecuperarValor<Int32>());
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["DepartamentoEditar"] = string.Empty;
            Server.Transfer("listaDepartamentos.aspx");
        }

        #endregion

        #region Metodos

        private void CarregarTela(Int32 valorRecebido = 0)
        {
            if (valorRecebido > 0)
            {
                entDpto.CodDepartamento = valorRecebido;
                entDpto = CtrlDpto.Pesquisar(entDpto);

                hdnCodDepartamento.Value = entDpto.CodDepartamento.ToString();
                txtNomeDepartamento.Text = entDpto.DescricaoDepartamento;
                chkMenuPadrao.Checked = entDpto.DepartamentoPadrao;

                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
            }
            else
            {

            }
        }

        #endregion
    }
}