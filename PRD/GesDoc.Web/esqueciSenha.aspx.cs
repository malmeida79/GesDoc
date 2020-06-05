using System;
using GesDoc.Web.Controllers;
using GesDoc.Web.Services;
using System.Web.UI;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web
{
    public partial class esqueciSenha : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonBar.AcaoClick += new EventHandler(btnAcao_Click);

            if (!Page.IsPostBack)
            {
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, visivel: true, habilitado: true);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, visivel: false, habilitado: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, visivel: false, habilitado: true);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Limpar, visivel: false, habilitado: true);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Novo, visivel: false, habilitado: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Pesquisa, visivel: false, habilitado: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Recuperar, visivel: false, habilitado: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.ExportaCsv, visivel: false, habilitado: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.ExportaExcel, visivel: false, habilitado: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.ExportaTxt, visivel: false, habilitado: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-question-sign""></span> Lembrar senha");                
            }
        }

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            if (!Validacoes.isValidEmail(txtEmail.Text)) {
                Mensagens.Alerta("Email inválido!");
                return;
            }

            UsuarioController usuario = new UsuarioController();
            string senha = usuario.RecuperaSenha(txtEmail.Text);

            if (senha.Length > 0) {

                Emails.EnviarEmail(txtEmail.Text, "comercial@radimenstein.com.br", "Recuperação de senha", $"Sua senha para o portal raddimenstein é:{senha}.");
                Mensagens.Alerta("Um email foi enviado para você com sua senha. Caso não receba, continue sem acesso ou tenha dúvidas favor entrar em contato com nossa equipe.");
                return;
            }


        }
    }
}