using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class cadTipoEndereco : System.Web.UI.Page
    {
        #region declaracoes

        TipoEndereco entTipoEndereco = new TipoEndereco();
        TipoEnderecoController CtrlTipoEndereco = new TipoEnderecoController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region eventos

        protected void btnAcao_Click(object sender, EventArgs e)
        {

            // de acordo com a ação da tela o TipoEndereco podera
            // ser alterado ou cadastrado. Todo o controle e 
            // realizado pela sessao que apresenta o codigo
            // do TipoEndereco.
            entTipoEndereco.DescricaoTipoEndereco = txtNomeTipoEndereco.Text;

            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) == "Salvar")
            {
                entTipoEndereco.CodTipoEndereco = Convert.ToInt32(hdnCodTipoEndereco.Value);

                if (CtrlTipoEndereco.Alterar(entTipoEndereco))
                {
                    Mensagens.Alerta("Dados alterados com sucesso.");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, texto: @"<span class="" glyphicon glyphicon-arrow-left""></span> Voltar");
                }
                else
                {
                    Mensagens.Alerta($"Falha na alteração dos dados:{Mensagens.MsgErro}");
                    return;
                }
            }
            else
            {
                if (CtrlTipoEndereco.Inserir(entTipoEndereco))
                {
                    Mensagens.Alerta("Dados cadastrados com sucesso.");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, texto: @"<span class="" glyphicon glyphicon-arrow-left""></span> Voltar");
                }
                else
                {
                    Mensagens.Alerta($"Falha no cadastramento dos dados:{Mensagens.MsgErro}");
                    return;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Cadastro de tipos de endereço ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            ButtonBar.AcaoClick += new EventHandler(btnAcao_Click);
            ButtonBar.CancelarClick += new EventHandler(btnCancelar_Click);
            ButtonBar.AcaoJQueryClick += new EventHandler(btnAcaoJQuery_click);

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultCadBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Limpar, visivel: false);
                CarregarTela(Session["TipoEnderecoEditar"].RecuperarValor<Int32>());
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["TipoEnderecoEditar"] = string.Empty;
            Server.Transfer("listaTipoEndereco.aspx");
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            Mensagens.Confirm("Deseja realmente excluir esse tipo de endereço?");
        }

        protected void btnAcaoJQuery_click(object sender, EventArgs e)
        {
            if (CtrlTipoEndereco.ContaUso(Convert.ToInt32(hdnCodTipoEndereco.Value)) <= 0)
            {

                if (CtrlTipoEndereco.Excluir(Convert.ToInt32(hdnCodTipoEndereco.Value)))
                {
                    Mensagens.Alerta("Dados Excluidos com sucesso.");
                    Session["TipoEnderecoEditar"] = string.Empty;
                    Server.Transfer("listaTipoEndereco.aspx");
                }
                else
                {
                    Mensagens.Alerta($"Falha no cadastramento dos dados:{Mensagens.MsgErro}");
                    return;
                }
            }
            else
            {
                Mensagens.Alerta("Esse TipoEndereco atualmente esta em uso, não pode ser excluido.");
                return;
            }
        }

        #endregion

        #region metodos

        private void CarregarTela(Int32 valorRecebido = 0)
        {
            if (valorRecebido > 0)
            {
                entTipoEndereco.CodTipoEndereco = valorRecebido;
                entTipoEndereco = CtrlTipoEndereco.Pesquisar(entTipoEndereco);

                hdnCodTipoEndereco.Value = entTipoEndereco.CodTipoEndereco.ToString();
                txtNomeTipoEndereco.Text = entTipoEndereco.DescricaoTipoEndereco;

                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
            }
            else
            {

            }
        }

        #endregion
    }
}