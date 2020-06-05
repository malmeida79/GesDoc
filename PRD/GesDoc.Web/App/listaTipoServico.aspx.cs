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
    public partial class listaTipoServico : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        TipoServicoController CtrlTipoServico = new TipoServicoController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Lista de tipos de serviço ::";
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
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Novo, texto: @"<span class="" glyphicon glyphicon-plus""></span> Novo tipo de Serviço");
                CarregaGrid();
            }
        }

        protected void gdvTipoServico_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvTipoServico.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvTipoServico_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            var lista = CtrlTipoServico.GetAll();

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<TipoServico>(SortExp, Sortdir);

            CarregaGrid(lista);
        }

        protected void gdvTipoServico_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["TipoServicoEditar"] = gdvTipoServico.Rows[e.NewEditIndex].Cells[1].Text;
            Server.Transfer("cadTipoServico.aspx");
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Session["TipoServicoEditar"] = string.Empty;
            Server.Transfer("cadTipoServico.aspx");
        }

        protected void gdvTipoServico_RowDataBound(object sender, GridViewRowEventArgs e)
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
            List<TipoServico> lista = CtrlTipoServico.GetAll();
            Exports.ListToCSV<TipoServico>(lista, "TipoServicos");
        }

        protected void ExportToTxt_Click(Object sender, EventArgs e)
        {
            List<TipoServico> lista = CtrlTipoServico.GetAll();
            Exports.ListToTXT<TipoServico>(lista, "TipoServicos");
        }

        protected void ExportToExcel_Click(Object sender, EventArgs e)
        {
            List<TipoServico> lista = CtrlTipoServico.GetAll();
            Exports.ListToExcel<TipoServico>(lista, "TipoServicos");
        }

        #endregion

        #region "Metodos"

        protected void CarregaGrid(List<TipoServico> lista = null)
        {

            if (lista == null)
            {
                lista = new List<TipoServico>();
                lista = CtrlTipoServico.GetAll();
            }

            gdvTipoServico.Preencher<TipoServico>(lista);
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