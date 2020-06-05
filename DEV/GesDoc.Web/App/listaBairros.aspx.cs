using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Infraestructure;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GesDoc.Web.App
{
    public partial class listaBairros : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        BairroController CtrlBairro = new BairroController();
        PaisesController CtrlPais = new PaisesController();
        EstadosController CtrlEst = new EstadosController();
        CidadesController CtrlCit = new CidadesController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        ~listaBairros()
        {
            CtrlBairro = null;
            CtrlPais = null;
            CtrlEst = null;
            CtrlCit = null;
        }

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Lista de bairros ::";
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
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Novo, texto: @"<span class="" glyphicon glyphicon-plus""></span> Novo Bairro");
                
                CarregaPaises();
            }
        }

        protected void gdvBairros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvBairros.PageIndex = e.NewPageIndex;
            CarregaGrid();
        }

        protected void gdvBairros_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            List<Bairro> lista = CtrlBairro.ListarBairroPorCidade(Convert.ToInt32(cboCidade.SelectedValue));

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<Bairro>(SortExp, Sortdir);

            CarregaGrid(lista);
        }

        protected void gdvBairros_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["BairroEditar"] = gdvBairros.Rows[e.NewEditIndex].Cells[1].Text;
            Session["estadoSelecionada"] = cboEstado.SelectedValue.ToString();
            Server.Transfer("cadBairro.aspx");
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Session["BairroEditar"] = string.Empty;
            Session["estadoSelecionada"] = cboEstado.SelectedValue.ToString();
            Server.Transfer("cadBairro.aspx");
        }

        protected void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCidade.Descarregar();
            gdvBairros.Descarregar();

            if (cboEstado.SelectedIndex > 0)
            {
                CarregaCidades();
            }
        }

        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCidade.Descarregar();
            cboEstado.Descarregar();
            gdvBairros.Descarregar();

            if (cboPais.SelectedIndex > 0)
            {
                CarregaEstados();
            }
        }

        protected void cboCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCidade.SelectedIndex > 0)
            {
                CarregaGrid();
            }
            else
            {
                gdvBairros.DataSource = null;
                gdvBairros.DataBind();
            }
        }

        protected void gdvBairros_RowDataBound(object sender, GridViewRowEventArgs e)
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
            cboPais.Preencher<Pais>(CtrlPais.GetAll(), "descricaoPais", "codPais", true);
        }

        public void CarregaEstados()
        {
            cboEstado.Preencher<Estado>(CtrlEst.ListarEstadosPorPais(Convert.ToInt32(cboPais.SelectedValue.ToString())), "descricaoEstado", "codEstado", true);            
        }

        public void CarregaCidades()
        {
            cboCidade.Preencher<Cidade>(CtrlCit.ListarCidadesPorEstado(Convert.ToInt32(cboEstado.SelectedValue.ToString())), "descricaoCidade", "codCidade", true);
        }

        protected void CarregaGrid(List<Bairro> lista = null)
        {
            if (lista == null)
            {
                lista = CtrlBairro.ListarBairroPorCidade(Convert.ToInt32(cboCidade.SelectedValue));
            }

            gdvBairros.Preencher<Bairro>(lista);
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