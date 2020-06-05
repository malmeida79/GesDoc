using System;
using GesDoc.Web.Services;
using GesDoc.Web.Controllers;
using GesDoc.Models;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class alteraSenha : System.Web.UI.Page
    {

        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        UsuarioController usuario = new UsuarioController();
        Permissoes permissoes;

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Alteração de senha ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            ButtonBar.AcaoClick += new EventHandler(btnAcao_Click);

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultCadBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Limpar, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao,texto: @"<span class=""glyphicon glyphicon-ok""></span> Alterar senha");
            }

        }

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            if (!Validacoes.EstaPreenchido(txtSenha.Text))
            {
                Mensagens.Alerta("Necessário informar a senha atual para alteração.");
                return;
            }

            if (!Validacoes.EstaPreenchido(txtNovaSenha.Text))
            {
                Mensagens.Alerta("Necessário informar uma nova senha para alteração da senha!");
                return;
            }

            if (!Validacoes.EstaPreenchido(txtConfNovaSenha.Text))
            {
                Mensagens.Alerta("Necessário informar uma confirmação para nova senha para alteração da senha!");
                return;
            }

            if (txtSenha.Text != UsuarioLogado.senha)
            {
                txtSenha.Text = "";
                txtSenha.Focus();
                Mensagens.Alerta("Senha atual não confere com a senha digitada!");
                return;
            }

            if (txtConfNovaSenha.Text != txtNovaSenha.Text)
            {
                txtNovaSenha.Text = "";
                txtConfNovaSenha.Text = "";
                txtNovaSenha.Focus();
                Mensagens.Alerta("Senha atual não confere com a senha digitada!");
                return;
            }

            if (usuario.AlterarSenha(UsuarioLogado.codUsuario, txtNovaSenha.Text))
            {
                Mensagens.Alerta("Senha alterada com sucesso !");
                return;
            }
            else
            {
                Mensagens.Alerta("Ocorreu um erro ao alterar a senha!");
                return;
            }
        }
    }
}