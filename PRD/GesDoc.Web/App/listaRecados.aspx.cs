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
    public partial class listaRecados : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        RecadosController CtrlRec = new RecadosController();
        TipoRecadoController CtrlTpRec = new TipoRecadoController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        ~listaRecados()
        {
            CtrlRec = null;
            CtrlTpRec = null;
        }

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Lista de recados ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );
            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultListBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Pesquisa, visivel: false, habilitado: false);
                ButtonBar.DisableExports(permissoes);
                CarregaTpRec();
            }
        }

        protected void gdvRecados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvRecados.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvRecados_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            List<Recados> lista;
            lista = CtrlRec.PesquisarPorCodigoTipoRecado(Convert.ToInt32(cboTpRec.SelectedValue));

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<Recados>(SortExp, Sortdir);

            CarregaGrid(lista);
        }

        protected void gdvRecados_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["RecadoEditar"] = gdvRecados.Rows[e.NewEditIndex].Cells[1].Text;
            Server.Transfer("cadRecados.aspx");
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Session["RecadoEditar"] = string.Empty;
            Server.Transfer("cadRecados.aspx");
        }

        protected void cboTpRec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTpRec.SelectedIndex > 0)
            {
                CarregaGrid();
            }
            else
            {
                gdvRecados.Descarregar();
            }
        }

        protected void gdvRecados_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    LinkButton addButton = (LinkButton)e.Row.Cells[0].Controls[0];
                    addButton.Text = "Selecionar";
                }
                bool status = Convert.ToBoolean(e.Row.Cells[4].Text);
                if (status)
                {
                    e.Row.Cells[4].Text = "Ativo";
                }
                else
                {
                    e.Row.Cells[4].Text = "Inativo";
                }

            }
        }

        #endregion

        #region "Metodos"

        public void CarregaTpRec()
        {
            cboTpRec.Preencher<TipoRecado>(CtrlTpRec.GetAll(), "descricaotiporecado", "codtiporecado", true);
        }

        protected void CarregaGrid(List<Recados> lista = null)
        {
            if (lista == null)
            {
                lista = CtrlRec.PesquisarPorCodigoTipoRecado(Convert.ToInt32(cboTpRec.SelectedValue));
            }

            gdvRecados.Preencher<Recados>(lista);
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