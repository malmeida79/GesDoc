using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class cadTipoRecado : System.Web.UI.Page
    {
        #region declaracoes

        TipoRecado TipoRecado = new TipoRecado();
        TipoRecadoController CtrlTipoRecado = new TipoRecadoController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region eventos 

        protected void btnAcao_Click(object sender, EventArgs e)
        {

            // de acordo com a ação da tela o TipoRecado podera
            // ser alterado ou cadastrado. Todo o controle e 
            // realizado pela sessao que apresenta o codigo
            // do TipoRecado.
            TipoRecado.DescricaoTipoRecado = txtNomeTipoRecado.Text;

            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) == "Salvar")
            {
                TipoRecado.CodTipoRecado = Convert.ToInt32(hdnCodTipoRecado.Value);

                if (CtrlTipoRecado.Alterar(TipoRecado))
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
                if (CtrlTipoRecado.Inserir(TipoRecado))
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
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Cadastro de tipos de recado ::";
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
                CarregarTela(Session["TipoRecadoEditar"].RecuperarValor<Int32>());
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["TipoRecadoEditar"] = string.Empty;
            Server.Transfer("listaTipoRecado.aspx");
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            Mensagens.Confirm("Deseja realmente excluir esse tipo de recado?");
        }

        protected void btnAcaoJQuery_click(object sender, EventArgs e)
        {
            if (CtrlTipoRecado.ContaUso(Convert.ToInt32(hdnCodTipoRecado.Value)) <= 0)
            {
                if (CtrlTipoRecado.Excluir(Convert.ToInt32(hdnCodTipoRecado.Value)))
                {
                    Mensagens.Alerta("Dados Excluidos com sucesso.");
                    Session["TipoRecadoEditar"] = string.Empty;
                    Server.Transfer("listaTipoRecado.aspx");
                }
                else
                {
                    Mensagens.Alerta($"Falha no cadastramento dos dados:{Mensagens.MsgErro}");
                    return;
                }
            }
            else
            {
                Mensagens.Alerta("Esse Tipo de Recado atualmente esta em uso, não pode ser excluido.");
                return;
            }
        }

        #endregion

        #region metodos

        private void CarregarTela(Int32 valorRecebido = 0)
        {
            if (valorRecebido > 0)
            {
                TipoRecado.CodTipoRecado = valorRecebido;
                TipoRecado = CtrlTipoRecado.Pesquisar(TipoRecado);

                hdnCodTipoRecado.Value = TipoRecado.CodTipoRecado.ToString();
                txtNomeTipoRecado.Text = TipoRecado.DescricaoTipoRecado;

                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
            }
            else
            {

            }
        }

        #endregion
    }
}