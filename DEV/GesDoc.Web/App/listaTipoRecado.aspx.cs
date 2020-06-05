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
    public partial class listaTipoRecado : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        TipoRecadoController CtrlTipoRecado = new TipoRecadoController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Lista de tipos de recado ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            ButtonBar.NovoClick += new EventHandler(btnNovo_Click);
            ButtonBar.ExportCsvClick += new EventHandler(ExportToCsv_Click);
            ButtonBar.ExportExcelClick += new EventHandler(ExportToExcel_Click);
            ButtonBar.ExportTxtClick += new EventHandler(ExportToTxt_Click);

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultListBar(permissoes);
                ButtonBar.DisableExports(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Pesquisa, visivel: false, habilitado: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Novo, texto: @"<span class="" glyphicon glyphicon-plus""></span> Novo Tipo de Recado");
                CarregaGrid();
            }
        }

        protected void gdvTipoRecado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvTipoRecado.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvTipoRecado_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            var lista = CtrlTipoRecado.GetAll();

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<TipoRecado>(SortExp, Sortdir);

            CarregaGrid(lista);            
        }

        protected void gdvTipoRecado_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["TipoRecadoEditar"] = gdvTipoRecado.Rows[e.NewEditIndex].Cells[1].Text;
            Server.Transfer("cadTipoRecado.aspx");
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Session["TipoRecadoEditar"] = string.Empty;
            Server.Transfer("cadTipoRecado.aspx");
        }

        protected void gdvTipoRecado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    LinkButton addbutton = (LinkButton)e.Row.Cells[0].Controls[0];
                    addbutton.Text = "Selecionar";
                }
            }
        }

        protected void ExportToCsv_Click(Object sender, EventArgs e)
        {
            List<TipoRecado> lista = CtrlTipoRecado.GetAll();
            Exports.ListToCSV<TipoRecado>(lista, "TipoRecados");
        }

        protected void ExportToTxt_Click(Object sender, EventArgs e)
        {
            List<TipoRecado> lista = CtrlTipoRecado.GetAll();
            Exports.ListToTXT<TipoRecado>(lista, "TipoRecados");
        }

        protected void ExportToExcel_Click(Object sender, EventArgs e)
        {
            List<TipoRecado> lista = CtrlTipoRecado.GetAll();
            Exports.ListToExcel<TipoRecado>(lista, "TipoRecados");
        }

        #endregion

        #region "Metodos"

        protected void CarregaGrid(List<TipoRecado> lista = null)
        {

            if (lista == null)
            {
                lista = new List<TipoRecado>();
                lista = CtrlTipoRecado.GetAll();
            }

            gdvTipoRecado.Preencher<TipoRecado>(lista);
            ButtonBar.EnableExports(permissoes);
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