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
    public partial class listaAcessos : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        AcessosController CtrlAcessos = new AcessosController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Lista de acessos ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

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

        protected void gdvAcessos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvAcessos.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvAcessos_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            var lista = CtrlAcessos.GetAll();

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<Acessos>(SortExp, Sortdir);

            CarregaGrid(lista);
        }

        protected void ExportToCsv_Click(Object sender, EventArgs e)
        {
            List<Acessos> lista = lista = CtrlAcessos.GetAll();
            Exports.ListToCSV<Acessos>(lista, "Acessos");
        }

        protected void ExportToTxt_Click(Object sender, EventArgs e)
        {
            List<Acessos> lista = lista = CtrlAcessos.GetAll();
            Exports.ListToTXT<Acessos>(lista, "Acessos");
        }

        protected void ExportToExcel_Click(Object sender, EventArgs e)
        {
            List<Acessos> lista = CtrlAcessos.GetAll();
            Exports.ListToExcel<Acessos>(lista, "Acessos");
        }

        #endregion

        #region "Metodos"

        protected void CarregaGrid(List<Acessos> lista = null)
        {

            if (lista == null)
            {
                lista = new List<Acessos>();
                lista = CtrlAcessos.GetAll();
            }

            gdvAcessos.Preencher<Acessos>(lista);
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