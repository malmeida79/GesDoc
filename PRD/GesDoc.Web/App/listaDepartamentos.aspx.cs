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
    public partial class listaDepartamentos : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        DepartamentosController CtrlDpto = new DepartamentosController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Lista de departamentos ::";
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
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Novo, texto: @"<span class="" glyphicon glyphicon-plus""></span> Novo Departamento");
                CarregaGrid();
            }
        }

        protected void gdvDepartamentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvDepartamentos.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvDepartamentos_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            var lista = CtrlDpto.GetAll();

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<Departamentos>(SortExp, Sortdir);

            CarregaGrid(lista);
        }

        protected void gdvDepartamentos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["departamentoEditar"] = gdvDepartamentos.Rows[e.NewEditIndex].Cells[1].Text;
            Server.Transfer("caddepartamentos.aspx");
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Session["departamentoEditar"] = string.Empty;
            Server.Transfer("caddepartamentos.aspx");
        }

        protected void gdvDepartamentos_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void CarregaGrid(List<Departamentos> lista = null)
        {
            if (lista == null)
            {
                lista = new List<Departamentos>();
                lista = CtrlDpto.GetAll();
            }

            gdvDepartamentos.Preencher<Departamentos>(lista);
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