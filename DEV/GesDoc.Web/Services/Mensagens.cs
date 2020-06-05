using System.Web;
using System.Web.UI;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.Services
{

    public static class Mensagens
    {
        public static string MsgErro = string.Empty;

        public static void Alerta(string mensagem, string redireciona = null)
        {
            var page = HttpContext.Current.CurrentHandler as Page;

            mensagem = mensagem.Replace(@"''", @"'").Replace(@"\\", @"\").Replace(@"""", "");
            string sMessage = $"AbreModal('{mensagem}');";

            if (!string.IsNullOrEmpty(redireciona))
            {
                sMessage = $"AbreModalRedireciona('{mensagem}','{redireciona}')";
            }

            if (mensagem.ToLower().Contains("erro") || mensagem.ToLower().Contains("exception"))

            {
                FuncoesGerais.RegistraErro("Mensagem de erro", "paginas", mensagem);
            }

            ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", sMessage, true);
        }

        public static void Confirm(string mensagem, string redireciona = null)
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            mensagem = mensagem.Replace(@"''", @"'").Replace(@"\\", @"\").Replace(@"""", "");

            string sMessage = $"AbreConfirmModal('{mensagem}');"; ;

            ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", sMessage, true);
        }
    }
}

