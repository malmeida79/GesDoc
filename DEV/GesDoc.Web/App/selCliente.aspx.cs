using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Services;
using GesDoc.Web.Infraestructure;
using System;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace GesDoc.Web.App
{
    public partial class selCliente : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Seleção de clientes ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            ButtonBar.AcaoClick += new EventHandler(btnAcao_Click);

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultCadBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Limpar, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-log-in""></span> Acessar");

                StringBuilder menu = new StringBuilder();

                menu.Append(MontaMenu.AdicionaItem("Inicial", "interna.aspx"));

                menu.Append(MontaMenu.AdicionaItem("Encerrar Sessão (Sair)", "../descarrega.aspx"));

                if (UsuarioLogado.TipoCliente)
                {
                    if ((string)Session["grupoAcesso"] != null)
                    {
                        CarregaGruposAcesso(Session["grupoAcesso"].ToString());
                    }
                    else
                    {
                        CarregaGruposAcesso();
                    }

                    if ((string)Session["clienteAcesso"] != null)
                    {
                        CarregaClientesAcesso(Session["clienteAcesso"].ToString());
                    }
                    else
                    {
                        CarregaClientesAcesso();
                    }
                }
            }
        }


        #endregion

        #region "Eventos"

        protected void cboGruposAcesso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaClientesAcesso();
        }

        protected void cboClientesAcesso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboClientesAcesso.SelectedIndex > 0)
            {
                Session["clienteAcesso"] = cboClientesAcesso.SelectedItem.Text;
                Session["codClienteAcesso"] = cboClientesAcesso.SelectedItem.Value;
                Session["CodRaizCliente"] = Convert.ToInt32(cboClientesAcesso.SelectedItem.Value);
            }
            else
            {
                Session["clienteAcesso"] = null;
                Session["codClienteAcesso"] = null;
                Session["CodRaizCliente"] = null;
            }
        }

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            if (cboClientesAcesso.SelectedIndex > 0 && cboGruposAcesso.SelectedIndex > 0)
            {
                Server.Transfer("interna.aspx");
            }
            else
            {
                if (cboGruposAcesso.SelectedIndex <= 0)
                {
                    Mensagens.Alerta("Necessário selecionar um grupo!");
                    return;
                }
                if (cboClientesAcesso.SelectedIndex <= 0)
                {
                    Mensagens.Alerta("Necessário selecionar um cliente!");
                    return;
                }
            }
        }
        #endregion

        #region "Metodos"

        public void CarregaGruposAcesso(string grupoSelecionado = null)
        {
            try
            {
                GruposClientesController CtrlGrp = new GruposClientesController();
                cboGruposAcesso.Preencher<GruposClientes>(CtrlGrp.GetAll().Where(pr => UsuarioLogado.GETCodGruposAcesso.Contains(pr.CodGrupo)).ToList(), "nomeGrupo", "codGrupo", true, grupoSelecionado);
                CtrlGrp = null;
             }
            catch (Exception)
            {
                Mensagens.Alerta("Ocorreu erro ao acessar a aplicação. O usuário não possui clientes associados.", @"http://www.radimenstein.com.br");
            }

        }

        public void CarregaClientesAcesso(string clienteSelecionado = null)
        {
            if (cboGruposAcesso.SelectedIndex > 0)
            {
                ClientesController CtrlCli = new ClientesController();
                Cliente cli = new Cliente();
                cli.CodGrupo = Convert.ToInt32(cboGruposAcesso.SelectedValue);
                Session["grupoAcesso"] = cboGruposAcesso.SelectedItem.Text;
                cboClientesAcesso.Preencher<Cliente>(CtrlCli.PesquisarLista(cli).Where(pr => UsuarioLogado.GETClientesAcesso.Contains(pr.CodCliente)).ToList(), "nomeCliente", "codCliente", true, clienteSelecionado);
                CtrlCli = null;
                cli = null;
            }
            else
            {
                Session["grupoAcesso"] = null;
                Session["clienteAcesso"] = null;
                Session["codClienteAcesso"] = null;
                cboClientesAcesso.Descarregar();
            }
        }

        #endregion
    }
}