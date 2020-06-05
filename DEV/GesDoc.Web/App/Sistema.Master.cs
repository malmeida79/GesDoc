using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Models;
using GesDoc.Web.Services;
using GesDoc.Web.Infraestructure;
using System.Text;

namespace GesDoc.Web.App
{
    public partial class Sistema : System.Web.UI.MasterPage
    {
        #region Declarações"

        UsuarioLogado UsuarioLogado = new UsuarioLogado();

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioLogado = Ambiente.ValidaAcesso();

            if (!Page.IsPostBack)
            {

                lblUsuario.Text = UsuarioLogado.TextoLabel;
                lblUsuario.ForeColor = UsuarioLogado.CorLabel;

                StringBuilder menu = new StringBuilder();

                if (!MapeamentoPaths.GetPaginaAtual().ToLower().Contains("selcliente"))
                {

                    menu.Append(MontaMenu.AdicionaItem("Inicial", "interna.aspx"));

                    List<AcessosGrupoUsuario> itensPaiMenu = UsuarioLogado.GETMenuUsuario.Where(x => x.UrlAcesso == string.Empty).ToList();

                    if (itensPaiMenu == null)
                    {
                        Mensagens.Alerta("Usuário sem acesso a nenhum item do sistema!");
                        return;
                    }
                    else
                    {
                        int seq = 1;
                        foreach (AcessosGrupoUsuario itemPai in itensPaiMenu)
                        {
                            menu.Append(MontaMenu.AdicionaPai(itemPai.DescricaoDepartamento, seq));

                            List<AcessosGrupoUsuario> itensFilhosMenu = UsuarioLogado.GETMenuUsuario.Where(x => x.DescricaoDepartamento == itemPai.DescricaoDepartamento.ToString() && x.UrlAcesso != null).ToList();
                            foreach (AcessosGrupoUsuario itemFilho in itensFilhosMenu)
                            {
                                if (itemFilho.Leitura)
                                {
                                    menu.Append(MontaMenu.AdicionaItem(itemFilho.DescricaoAcesso, itemFilho.UrlAcesso));
                                }
                            }
                            itensFilhosMenu = null;
                            menu.Append(MontaMenu.FechaItemPai());
                            seq++;
                        }
                    }

                    itensPaiMenu = null;

                    if (UsuarioLogado.TipoCliente)
                    {
                        menu.Append(MontaMenu.AdicionaItem("Trocar de Cliente", "selCliente.aspx"));
                    }
                }

                menu.Append(MontaMenu.AdicionaItem("Encerrar Sessão (Sair)", "../descarrega.aspx"));

                menuSistema.InnerHtml = menu.ToString();

            }
        }

        #endregion
    }
}