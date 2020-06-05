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
    public partial class listaControleAcesso : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        GruposAcessoController CtrlGrupo;
        Permissoes permissoes;

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Lista de Grupos ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            CtrlGrupo = new GruposAcessoController();

            ButtonBar.ExportCsvClick += new EventHandler(ExportToCsv_Click);
            ButtonBar.ExportExcelClick += new EventHandler(ExportToExcel_Click);
            ButtonBar.ExportTxtClick += new EventHandler(ExportToTxt_Click);

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultListBar(permissoes);
                ButtonBar.DisableExports(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Pesquisa, visivel: false, habilitado: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Novo, visivel: false, habilitado: false);
                CarregaGrid();
            }
        }

        protected void gdvGrupos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvGrupos.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvGrupos_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            var lista = CtrlGrupo.GetAll();

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<GruposAcesso>(SortExp, Sortdir);

            CarregaGrid(lista);
        }

        protected void gdvGrupos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["acessoGrupoEditar"] = gdvGrupos.Rows[e.NewEditIndex].Cells[1].Text;
            Server.Transfer("cadControleAcesso.aspx");
        }

        protected void gdvGrupos_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void ExportToCsv_Click(Object sender, EventArgs e)
        {
            List<GruposAcesso> lista = CtrlGrupo.GetAll();
            Exports.ListToCSV<GruposAcesso>(lista, "GruposAcessos");
        }

        protected void ExportToTxt_Click(Object sender, EventArgs e)
        {
            List<GruposAcesso> lista = CtrlGrupo.GetAll();
            Exports.ListToTXT<GruposAcesso>(lista, "GruposAcessos");
        }

        protected void ExportToExcel_Click(Object sender, EventArgs e)
        {
            List<GruposAcesso> lista = CtrlGrupo.GetAll();
            Exports.ListToExcel<GruposAcesso>(lista, "GruposAcessos");
        }

        #endregion

        #region "Metodos"

        protected void CarregaGrid(List<GruposAcesso> lista = null)
        {
            if (lista == null)
            {
                lista = new List<GruposAcesso>();
                lista = CtrlGrupo.GetAll();
            }
            gdvGrupos.Preencher<GruposAcesso>(lista);
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