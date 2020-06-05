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
    public partial class listaTipoEndereco : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        TipoEnderecoController CtrlTipoEndereco = new TipoEnderecoController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Lista de tipos de endereço ::";
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
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Novo, texto: @"<span class="" glyphicon glyphicon-plus""></span> Novo Tipo Endereço");
                CarregaGrid();
            }
        }

        protected void gdvTipoEndereco_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvTipoEndereco.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvTipoEndereco_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            var lista = CtrlTipoEndereco.GetAll();

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<TipoEndereco>(SortExp, Sortdir);

            CarregaGrid(lista);            
        }

        protected void gdvTipoEndereco_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["TipoEnderecoEditar"] = gdvTipoEndereco.Rows[e.NewEditIndex].Cells[1].Text;
            Server.Transfer("cadTipoEndereco.aspx");
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Session["TipoEnderecoEditar"] = string.Empty;
            Server.Transfer("cadTipoEndereco.aspx");
        }

        protected void gdvTipoEndereco_RowDataBound(object sender, GridViewRowEventArgs e)
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
            List<TipoEndereco> lista = CtrlTipoEndereco.GetAll();
            Exports.ListToCSV<TipoEndereco>(lista, "TipoEnderecos");
        }

        protected void ExportToTxt_Click(Object sender, EventArgs e)
        {
            List<TipoEndereco> lista = CtrlTipoEndereco.GetAll();
            Exports.ListToTXT<TipoEndereco>(lista, "TipoEnderecos");
        }

        protected void ExportToExcel_Click(Object sender, EventArgs e)
        {
            List<TipoEndereco> lista = CtrlTipoEndereco.GetAll();
            Exports.ListToExcel<TipoEndereco>(lista, "TipoEnderecos");
        }
        #endregion

        #region "Metodos"

        protected void CarregaGrid(List<TipoEndereco> lista = null)
        {

            if (lista == null)
            {
                lista = new List<TipoEndereco>();
                lista = CtrlTipoEndereco.GetAll();
            }

            gdvTipoEndereco.Preencher<TipoEndereco>(lista);
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