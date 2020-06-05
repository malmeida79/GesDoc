using System;
using GesDoc.Web.Services;
using GesDoc.Web.Controllers;
using GesDoc.Models;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class Interna : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        public bool usuarioCliente = false;
        DocumentosController CtrlDocumentos = new DocumentosController();
        RecadosController CtrlRec = new RecadosController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                hfAccordionIndex.Value = Request.Form[hfAccordionIndex.UniqueID];
            }

            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Application - 2.0 ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            usuarioCliente = UsuarioLogado.TipoCliente;

            // busca recados para os devidos fins
            List<Recados> lstrec = CtrlRec.PesquisarPorCodigoTipoRecadoAtivo(UsuarioLogado.codtipoRecado);

            string recados = string.Empty;

            foreach (var item in lstrec)
            {
                recados += item.Recado + @"<br />";
            }

            lblMsgsGeraisRad.Text = recados;

            if (!Page.IsPostBack)
            {
                HelperPages.SetHelp(
                    ((HiddenField)Master.FindControl("hfDuvidas")),
                    MapeamentoPaths.GetPaginaAtual(),
                    UsuarioLogado.TipoCliente
                    );

                if (UsuarioLogado.AssinaDocumento == true)
                {
                    CarregaGridAssina();
                }


                if (UsuarioLogado.LiberaDocumento == true)
                {
                    CarregaGridLibera();
                }
            }
        }

        protected void gdvDocumentoAssinar_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            Documentos documento = new Documentos();
            documento.Assinado = false;

            var lista = CtrlDocumentos.GET(documento);

            documento = null;

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<Documentos>(SortExp, Sortdir);

            CarregaGridAssina(lista);
        }

        protected void gdvDocumentoLiberar_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            Documentos documento = new Documentos();
            documento.Assinado = true;
            documento.Liberado = false;

            var lista = CtrlDocumentos.GET(documento);

            documento = null;

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<Documentos>(SortExp, Sortdir);

            CarregaGridLibera(lista);
        }

        protected void gdvDocumentoAssinar_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gdvDocumentoAssinar.PageIndex = e.NewPageIndex;
            CarregaGridAssina();
        }

        protected void gdvDocumentoLiberar_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            gdvDocumentoLiberar.PageIndex = e.NewPageIndex;
            CarregaGridLibera();
        }

        protected void gdvDocumentoLiberar_RowCommand(object sender, GridViewCommandEventArgs e)
        {            
            if (e.CommandName == "irpara")
            {
                int index = int.Parse((string)e.CommandArgument);

                gdvDocumentoLiberar.Columns[4].Visible = true;
                gdvDocumentoLiberar.Columns[5].Visible = true;
                gdvDocumentoLiberar.Columns[6].Visible = true;

                Session["tratamentoDireto"] = true;
                Session["ClienteEditar"] = gdvDocumentoLiberar.Rows[index].Cells[4].Text;
                Session["equipamentoDocumento"] = gdvDocumentoLiberar.Rows[index].Cells[5].Text;
                Session["tipoServicoDocumento"] = gdvDocumentoLiberar.Rows[index].Cells[6].Text;

                gdvDocumentoLiberar.Columns[4].Visible = false;
                gdvDocumentoLiberar.Columns[5].Visible = false;
                gdvDocumentoLiberar.Columns[6].Visible = false;

                Server.Transfer("admDocumentos.aspx");
            }
        }

        protected void gdvDocumentoAssinar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "irpara")
            {
                int index = int.Parse((string)e.CommandArgument);

                gdvDocumentoAssinar.Columns[4].Visible = true;
                gdvDocumentoAssinar.Columns[5].Visible = true;
                gdvDocumentoAssinar.Columns[6].Visible = true;

                Session["tratamentoDireto"] = true;
                Session["ClienteEditar"] = gdvDocumentoAssinar.Rows[index].Cells[4].Text;
                Session["equipamentoDocumento"] = gdvDocumentoAssinar.Rows[index].Cells[5].Text;
                Session["tipoServicoDocumento"] = gdvDocumentoAssinar.Rows[index].Cells[6].Text;

                gdvDocumentoAssinar.Columns[4].Visible = false;
                gdvDocumentoAssinar.Columns[5].Visible = false;
                gdvDocumentoAssinar.Columns[6].Visible = false;

                Server.Transfer("admDocumentos.aspx");
            }
        }

        #endregion

        #region "Metodos"

        protected void CarregaGridAssina(List<Documentos> listaAssina = null)
        {

            if (listaAssina == null)
            {
                listaAssina = new List<Documentos>();

                Documentos documento = new Documentos();
                documento.Assinado = false;
                listaAssina = CtrlDocumentos.GET(documento, consideraStatus: true);
                documento = null;
            }

            gdvDocumentoAssinar.Columns[4].Visible = true;
            gdvDocumentoAssinar.Columns[5].Visible = true;
            gdvDocumentoAssinar.Columns[6].Visible = true;

            gdvDocumentoAssinar.Preencher<Documentos>(listaAssina);

            gdvDocumentoAssinar.Columns[4].Visible = false;
            gdvDocumentoAssinar.Columns[5].Visible = false;
            gdvDocumentoAssinar.Columns[6].Visible = false;
        }

        protected void CarregaGridLibera(List<Documentos> listaLibera = null)
        {

            if (listaLibera == null)
            {
                listaLibera = new List<Documentos>();
                Documentos documento = new Documentos();
                documento.Assinado = true;
                documento.Liberado = false;
                listaLibera = CtrlDocumentos.GET(documento, consideraStatus: true);
                documento = null;
            }

            gdvDocumentoLiberar.Columns[4].Visible = true;
            gdvDocumentoLiberar.Columns[5].Visible = true;
            gdvDocumentoLiberar.Columns[6].Visible = true;

            gdvDocumentoLiberar.Preencher<Documentos>(listaLibera);

            gdvDocumentoLiberar.Columns[4].Visible = false;
            gdvDocumentoLiberar.Columns[5].Visible = false;
            gdvDocumentoLiberar.Columns[6].Visible = false;

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