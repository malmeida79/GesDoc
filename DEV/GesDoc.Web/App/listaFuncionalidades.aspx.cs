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
    public partial class listaFuncionalidades : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        FuncionalidadesController CtrlFnc = new FuncionalidadesController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Lista de funcionalidades ::";
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
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Novo, texto: @"<span class="" glyphicon glyphicon-plus""></span> Nova Funcionalidade");
                CarregaDepartamentos();
            }
        }

        protected void gdvFuncionalidades_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvFuncionalidades.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvFuncionalidades_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            List<Funcionalidades> lista = CtrlFnc.ListarPorDepartamento(Convert.ToInt32(cboDepartamento.SelectedValue));

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<Funcionalidades>(SortExp, Sortdir);

            CarregaGrid(lista);
        }

        protected void gdvFuncionalidades_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["FuncionalidadeEditar"] = gdvFuncionalidades.Rows[e.NewEditIndex].Cells[1].Text;
            Server.Transfer("cadFuncionalidades.aspx");
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Session["FuncionalidadeEditar"] = string.Empty;
            Server.Transfer("cadFuncionalidades.aspx");
        }

        protected void cboDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            gdvFuncionalidades.Descarregar();

            if (cboDepartamento.SelectedIndex > 0)
            {
                CarregaGrid();
            }
        }

        protected void gdvFuncionalidades_RowDataBound(object sender, GridViewRowEventArgs e)
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

        public void CarregaDepartamentos()
        {

            DepartamentosController buDpto = new DepartamentosController();
            List<Departamentos> listaDpto = new List<Departamentos>();

            listaDpto = buDpto.GetAll();

            cboDepartamento.DataSource = listaDpto;
            cboDepartamento.DataValueField = "codDepartamento";
            cboDepartamento.DataTextField = "descricaoDepartamento";
            cboDepartamento.DataBind();
            cboDepartamento.Items.Insert(0, "-- Selecione --");

        }

        protected void CarregaGrid(List<Funcionalidades> lista = null)
        {
            if (lista == null)
            {
                lista = CtrlFnc.ListarPorDepartamento(Convert.ToInt32(cboDepartamento.SelectedValue));
            }

            gdvFuncionalidades.Preencher<Funcionalidades>(lista);
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