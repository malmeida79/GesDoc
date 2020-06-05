using System;
using System.Linq;
using System.Web;
using GesDoc.Web.Services;

namespace GesDoc.Web.App
{
    public partial class downloader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string caminhoPath = Request.QueryString["caminho"].ConverteVirtualParaFisico();

                System.IO.FileInfo arquivo = new System.IO.FileInfo(caminhoPath);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + caminhoPath.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).Last() + ";");
                HttpContext.Current.Response.AddHeader("Content-Length", arquivo.Length.ToString());
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.WriteFile(arquivo.FullName);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.Close();

            }
            catch (Exception ex)
            {
                Mensagens.Alerta($"Ocorreu erro:{ex.Message.ToString()}");
            }
        }
    }
}