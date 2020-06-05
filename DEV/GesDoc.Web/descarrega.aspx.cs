using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Infraestructure;
using System;

namespace GesDoc.Web.App
{
    public partial class descarrega : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioLogado UsuarioLogado = new UsuarioLogado();
            UsuarioLogado = Ambiente.ValidaAcesso();

            if (UsuarioLogado != null)
            {
                UsuarioOnLineController ctrlOnLine = new UsuarioOnLineController();
                ctrlOnLine.Remove(UsuarioLogado.codUsuario);
            }

            Session.Clear();
            Session.Abandon();
            Server.Transfer("Default.aspx");
        }
    }
}