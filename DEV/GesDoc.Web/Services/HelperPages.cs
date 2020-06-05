using GesDoc.Web.Controllers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GesDoc.Web.Services
{
    public static class HelperPages
    {
        /// <summary>
        /// Devolve o help da pagina solicitada
        /// </summary>
        /// <param name="pagina">pagina para pesquisar o help</param>
        /// <param name="ehUsuario">Se usuário [true] devolve help para usuario
        /// caso não devolve para colaborador</param>
        /// <returns></returns>
        public static void SetHelp(HiddenField campoHelp, string pagina, bool ehUsuario = false)
        {
            string retorno = string.Empty;
            HelpController hlp = new HelpController();
            retorno = hlp.GetHelp(pagina, ehUsuario);

            if (string.IsNullOrEmpty(retorno))
            {
                OcultaHelp();
            }
            else {
                ExibeHelp();
            }

            campoHelp.Value = retorno;

            hlp = null;

        }

        private static void OcultaHelp()
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            string sMessage = "OcultaIconeAjuda();";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "escondeHelp", sMessage, true);
        }

        private static void ExibeHelp()
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            string sMessage = "ExibeIconeAjuda();";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "escondeHelp", sMessage, true);
        }
    }
}