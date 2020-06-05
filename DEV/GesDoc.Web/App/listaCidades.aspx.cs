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
    public partial class listaCidades : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        EstadosController CtrlEst = new EstadosController();
        CidadesController CtrlCit = new CidadesController();
        PaisesController BPais = new PaisesController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;
        
        ~listaCidades()
        {
            CtrlEst = null;
            CtrlCit = null;
            BPais = null;
        }

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Lista de cidades ::";
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
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Novo, texto: @"<span class="" glyphicon glyphicon-plus""></span> Nova Cidade");
                CarregaPaises();
            }
        }

        protected void gdvCidades_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvCidades.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvCidades_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            List<Cidade> lista = CtrlCit.ListarCidadesPorEstado(Convert.ToInt32(cboEstado.SelectedValue));

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<Cidade>(SortExp, Sortdir);

            CarregaGrid(lista);
        }

        protected void gdvCidades_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["CidadeEditar"] = gdvCidades.Rows[e.NewEditIndex].Cells[1].Text;
            Session["paisSelecinado"] = cboPais.SelectedValue.ToString();
            Server.Transfer("cadCidades.aspx");
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Session["CidadeEditar"] = string.Empty;
            Session["paisSelecinado"] = cboPais.SelectedValue.ToString();
            Session["estadoSelecinado"] = cboEstado.SelectedValue.ToString();
            Server.Transfer("cadCidades.aspx");
        }

        protected void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstado.SelectedIndex > 0)
            {
                CarregaGrid();
            }
            else
            {
                gdvCidades.Descarregar();
            }
        }

        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEstado.Descarregar();
            gdvCidades.Descarregar();

            if (cboPais.SelectedIndex > 0)
            {
                CarregaEstados();
            }
        }

        protected void gdvCidades_RowDataBound(object sender, GridViewRowEventArgs e)
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

        public void CarregaPaises()
        {
            cboPais.Preencher<Pais>(BPais.GetAll(), "descricaoPais", "codPais", true);
        }

        public void CarregaEstados()
        {
            cboEstado.Preencher<Estado>(CtrlEst.ListarEstadosPorPais(Convert.ToInt32(cboPais.SelectedValue)), "descricaoEstado", "codEstado", true);
        }

        protected void CarregaGrid(List<Cidade> lista = null)
        {
            if (lista == null)
            {
                lista = CtrlCit.ListarCidadesPorEstado(Convert.ToInt32(cboEstado.SelectedValue));
            }

            gdvCidades.Preencher<Cidade>(lista);
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