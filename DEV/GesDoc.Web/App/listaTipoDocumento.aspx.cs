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
    public partial class listaTipoDocumento : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        TipoDocumentoController CtrlTipoDocumento = new TipoDocumentoController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Lista de tipos de documento ::";
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
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Novo, texto: @"<span class="" glyphicon glyphicon-plus""></span> Novo Tipo Documento");
                CarregaGrid();
            }
        }

        protected void gdvTipoDocumento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvTipoDocumento.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvTipoDocumento_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            var lista = CtrlTipoDocumento.GetAll();

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<TipoDocumento>(SortExp, Sortdir);

            CarregaGrid(lista);            
        }

        protected void gdvTipoDocumento_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["TipoDocumentoEditar"] = gdvTipoDocumento.Rows[e.NewEditIndex].Cells[1].Text;
            Server.Transfer("cadTipoDocumento.aspx");
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Session["TipoDocumentoEditar"] = string.Empty;
            Server.Transfer("cadTipoDocumento.aspx");
        }

        protected void gdvTipoDocumento_RowDataBound(object sender, GridViewRowEventArgs e)
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
            List<TipoDocumento> lista = CtrlTipoDocumento.GetAll();
            Exports.ListToCSV<TipoDocumento>(lista, "TipoDocumentos");
        }

        protected void ExportToTxt_Click(Object sender, EventArgs e)
        {
            List<TipoDocumento> lista = CtrlTipoDocumento.GetAll();
            Exports.ListToTXT<TipoDocumento>(lista, "TipoDocumentos");
        }

        protected void ExportToExcel_Click(Object sender, EventArgs e)
        {
            List<TipoDocumento> lista = CtrlTipoDocumento.GetAll();
            Exports.ListToExcel<TipoDocumento>(lista, "TipoDocumentos");
        }

        #endregion

        #region "Metodos"

        protected void CarregaGrid(List<TipoDocumento> lista = null)
        {

            if (lista == null)
            {
                lista = new List<TipoDocumento>();
                lista = CtrlTipoDocumento.GetAll();
            }

            gdvTipoDocumento.Preencher<TipoDocumento>(lista);
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