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
    public partial class listaGruposCliente : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        GruposClientesController CtrlGrupo = new GruposClientesController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;
        
        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Lista de grupos de clientes ::";
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
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Pesquisa, visivel: false, habilitado: false);
                ButtonBar.DisableExports(permissoes);
                CarregaGrid();
            }
        }

        protected void gdvGrupo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvGrupo.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvGrupo_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            var lista = CtrlGrupo.GetAll();

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<GruposClientes>(SortExp, Sortdir);

            CarregaGrid(lista);
        }

        protected void gdvGrupo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["GrupoEditar"] = gdvGrupo.Rows[e.NewEditIndex].Cells[1].Text;
            Server.Transfer("cadGruposCliente.aspx");
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Session["GrupoEditar"] = string.Empty;
            Server.Transfer("cadGruposCliente.aspx");
        }

        protected void gdvGrupo_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void CarregaGrid(List<GruposClientes> lista = null)
        {

            if (lista == null)
            {
                lista = new List<GruposClientes>();
                lista = CtrlGrupo.GetAll();
            }

            gdvGrupo.Preencher<GruposClientes>(lista);

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