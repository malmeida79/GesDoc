using System.Web;

namespace GesDoc.Web.Services
{
    public static class MapeamentoPaths
    {
        /// <summary>
        /// Retorna o path do arquivo em questao relativo a aplicacao.
        /// </summary>
        /// <returns>/site/pedidos/arquivo.aspx</returns>
        public static string GetFilePath()
        {
            return HttpContext.Current.Request.FilePath;
        }

        /// <summary>
        /// Mapeia um caminho virtual para um caminho físico no servidor.
        /// </summary>
        /// <returns>Resultado: C:\inetpub\wwwroot\exemplo\</returns>
        public static string GetCaminhoFisicoRaiz()
        {
            return System.Web.Hosting.HostingEnvironment.MapPath("/");
        }

        /// <summary>
        /// Devolve a pagina atual
        /// </summary>
        /// <returns>exemplo.html</returns>
        public static string GetPaginaAtual()
        {
            return HttpContext.Current.Request.CurrentExecutionFilePath.Substring(System.Web.HttpContext.Current.Request.CurrentExecutionFilePath.LastIndexOf("/") + 1);
        }

        /// <summary>
        /// DEvolve a url da pagina atual
        /// </summary>
        /// <returns>Http://www.exemplo.com.br/deOndeVim.aspx</returns>
        public static string GetUrlPaginaAnterior()
        {
            return HttpContext.Current.Request.UrlReferrer.OriginalString;
        }

        /// <summary>
        /// Convert o caminho virtual para o fisico ex: ~/Arquivos para c:\teste\arquivos
        /// </summary>
        /// <param name="caminhoVirtual">Caminho virtual a ser convertido</param>
        /// <returns>caminho fisico retornado</returns>
        public static string ConverteVirtualParaFisico(this string caminhoVirtual)
        {
            if (caminhoVirtual.Contains("~"))
            {
                return HttpContext.Current.Request.MapPath(caminhoVirtual);
            }
            else {
                return caminhoVirtual;
            }
        }

        /// <summary>
        /// Buscando caminho virtual da aplicação
        /// </summary>
        /// <returns>Retorna o path virtual raiz da aplicação</returns>
        public static string CaminhoRaizVirtualApp()
        {
            return HttpContext.Current.Request.ApplicationPath;
        }

        /// <summary>
        /// CAminho completo url aplicação: localhost:8080
        /// </summary>
        /// <returns>caminho</returns>
        public static string CaminhoRaizHttp()
        {
            return HttpContext.Current.Request.Url.Authority;
        }        
    }
}
