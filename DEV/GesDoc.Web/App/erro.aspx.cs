using System;
using GesDoc.Web.Services;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class erro : System.Web.UI.Page
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
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-arrow-left""></span> Voltar");
            }

            if (!string.IsNullOrEmpty(Mensagens.MsgErro))
            {
                lblPrincipal.Text = Mensagens.MsgErro;
            }
        }

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            Server.Transfer("interna.aspx");
        }
    }
}