using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class listaEstados : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        EstadosController CtrlEst = new EstadosController();
        PaisesController CtrlPais = new PaisesController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        ~listaEstados()
        {
            CtrlEst = null;
            CtrlPais = null;
        }

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Lista de estados ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            ButtonBar.NovoClick += new EventHandler(btnNovo_Click);

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultListBar(permissoes);
                ButtonBar.DisableExports(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Pesquisa, visivel: false, habilitado: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Novo, texto: @"<span class="" glyphicon glyphicon-plus""></span> Novo Estado");
                CarregaPais();
            }
        }

        protected void gdvEstados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvEstados.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvEstados_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            List<Estado> lista;
            lista = CtrlEst.ListarEstadosPorPais(Convert.ToInt32(cboPais.SelectedValue));

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<Estado>(SortExp, Sortdir);

            CarregaGrid(lista);
        }

        protected void gdvEstados_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["EstadoEditar"] = gdvEstados.Rows[e.NewEditIndex].Cells[1].Text;
            Server.Transfer("cadEstados.aspx");
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Session["EstadoEditar"] = string.Empty;
            Server.Transfer("cadEstados.aspx");
        }

        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPais.SelectedIndex > 0)
            {
                CarregaGrid();
            }
            else
            {
                gdvEstados.Descarregar();
            }
        }

        protected void gdvEstados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    LinkButton addButton = (LinkButton)e.Row.Cells[0].Controls[0];
                    addButton.Text = "Selecionar";
                }
            }
        }

        #endregion

        #region "Metodos"

        public void CarregaPais()
        {
            cboPais.Preencher<Pais> (CtrlPais.GetAll(), "descricaoPais", "codPais", true);
        }

        protected void CarregaGrid(List<Estado> lista = null)
        {
            if (lista == null)
            {
                lista = CtrlEst.ListarEstadosPorPais(Convert.ToInt32(cboPais.SelectedValue));
            }

            gdvEstados.Preencher<Estado>(lista);
        }

        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }

        #endregion
    }
}