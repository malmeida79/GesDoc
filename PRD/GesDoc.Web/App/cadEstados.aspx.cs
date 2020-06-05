using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Models;
using GesDoc.Web.Controllers;
using GesDoc.Web.Services;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class cadEstados : System.Web.UI.Page
    {
        #region declaracoes 

        Estado entEstado = new Estado();
        EstadosController CtrlEst = new EstadosController();
        PaisesController CtrlPais = new PaisesController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region Eventos

        protected void btnAcao_Click(object sender, EventArgs e)
        {

            // de acordo com a ação da tela o Estado podera
            // ser alterado ou cadastrado. Todo o controle e 
            // realizado pela sessao que apresenta o codigo
            // do Estado.
            entEstado.DescricaoEstado = txtNomeEstado.Text;
            entEstado.CodPais = Convert.ToInt32(cboPais.SelectedValue);
            entEstado.UFEstado = txtUfEstado.Text;

            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) == "Salvar")
            {
                entEstado.CodEstado = Convert.ToInt32(hdnCodEstado.Value);

                if (CtrlEst.Alterar(entEstado))
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
                if (CtrlEst.Inserir(entEstado))
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
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Cadastro de estados ::";
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
                CarregaPais();
                CarregarTela(Session["EstadoEditar"].RecuperarValor<Int32>());
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["EstadoEditar"] = string.Empty;
            Server.Transfer("listaEstados.aspx");
        }

        #endregion

        #region Metodos

        public void CarregaPais()
        {
            cboPais.Preencher(CtrlPais.GetAll(), "descricaoPais", "codPais", true);
        }

        private void CarregarTela(Int32 valorRecebido = 0)
        {
            if (valorRecebido > 0)
            {
                entEstado.CodEstado = valorRecebido;
                entEstado = CtrlEst.Pesquisar(entEstado);

                hdnCodEstado.Value = entEstado.CodEstado.ToString();
                txtNomeEstado.Text = entEstado.DescricaoEstado;
                txtUfEstado.Text = entEstado.UFEstado;

                cboPais.SetSelectedValue(entEstado.CodPais.ToString());

                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
            }
            else
            {

            }
        }

        #endregion
    }
}