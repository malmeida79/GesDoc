using System.Web;
using GesDoc.Models;
using GesDoc.Web.Services;
using System.Web.UI;

namespace GesDoc.Web.Infraestructure
{
    public class FuncoesGerais
    {

        /// <summary>
        /// Registro de erros
        /// </summary>
        /// <param name="modulo">Modulo em que ocorreu o erro</param>
        /// <param name="area">Area em que ocorreu o erro</param>
        /// <param name="erro">erro</param>
        public static void RegistraErro(string modulo, string area, string erro)
        {
            UsuarioLogado UsuarioLogado = (UsuarioLogado)HttpContext.Current.Session["UsuarioLogado"];
            Log lg = new Log();
            lg.RegistraErro(area, erro);
            lg = null;
        }

        /// <summary>
        /// Registro de historico de acoes
        /// </summary>
        /// <param name="modulo">Modulo da acao</param>
        /// <param name="tabela">Tabela da acao</param>
        /// <param name="acao">acao</param>
        /// <param name="chave">id chave da acao</param>
        public static void RegistraHistorico(string tabela, string acao, int chave)
        {
            UsuarioLogado UsuarioLogado = (UsuarioLogado)HttpContext.Current.Session["UsuarioLogado"];
            Log lg = new Log();
            lg.RegistraHistorico(acao, tabela, chave);
            lg = null;            
        }

        /// <summary>
        /// Registro de Logs
        /// </summary>
        /// <param name="msg">Mensagem a ser registrada</param>
        /// <param name="modulo">Modulo em que se deseja registrar a msg</param>
        public static void RegistraLog(string msg, string modulo = "")
        {
            UsuarioLogado UsuarioLogado = (UsuarioLogado)HttpContext.Current.Session["UsuarioLogado"];
            Log lg = new Log();
            lg.RegistraLog(msg, modulo);
            lg = null;            
        }

        /// <summary>
        /// Executa pequenos scripts (Não necessário informas as tags <script></script>
        /// </summary>
        /// <param name="JScript"></param>
        public static void ExecutaJScript(string JScript) {
            var page = HttpContext.Current.CurrentHandler as Page;
            ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", JScript, true);
        }

    }
}