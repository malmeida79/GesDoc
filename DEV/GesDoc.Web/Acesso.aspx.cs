using GesDoc.Web.Controllers;
using GesDoc.Models;
using System;
using System.Web;
using System.Web.Services;

namespace GesDoc.Web.Scripts
{
    public partial class Acesso : System.Web.UI.Page
    {
        protected void Page_Load(object userLogin, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetAcesso(string userLogin, string userSenha)
        {
            try
            {
                UsuarioLogado logado = new UsuarioLogado();
                UsuarioController buUser = new UsuarioController();
                Usuario usuario = new Usuario();

                usuario.login = userLogin;
                usuario.senha = userSenha;

                if (string.IsNullOrEmpty(userLogin))
                {
                    return "Falha no login: Informe seu login !!!";
                }

                if (string.IsNullOrEmpty(userSenha))
                {
                    return "Falha no login: Necessário fornecer uma senha.";
                }

                UsuarioOnLineController ctrlOnLine = new UsuarioOnLineController();
                UsuarioOnLine validaLogin = ctrlOnLine.GetFromLogin(userLogin);

                if (validaLogin != null)
                {
                    ctrlOnLine.Remove(validaLogin.CodUsuario);
                }

                ctrlOnLine = null;
                validaLogin = null;

                logado = buUser.Logar(usuario);

                if (logado == null)
                {
                    return "Falha no login: Usuario ou senha incorretos.";
                }
                else
                {
                    HttpContext.Current.Session["usuarioLogado"] = logado;

                    UsuarioOnLine onLine = new UsuarioOnLine();
                    onLine.CodUsuario = logado.codUsuario;
                    onLine.USLogin = logado.login;
                    onLine.IdSessao = HttpContext.Current.Session.SessionID;
                    onLine.HorarioLogin = DateTime.Now;

                    UsuarioOnLineController ctrlCadOnLine = new UsuarioOnLineController();
                    ctrlCadOnLine.Registrer(onLine);

                    if (logado.TipoCliente)
                    {
                        return "App/selCliente.aspx";
                    }
                    else
                    {
                        return "App/interna.aspx";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Ocorreu Falha:{ex.Message.ToString()}";
            }
        }
    }
}