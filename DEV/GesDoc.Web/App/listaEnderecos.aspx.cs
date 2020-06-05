using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class listaEnderecos : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        EnderecoController CtrlEnd = new EnderecoController();
        string dadoBusca = string.Empty;
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Lista de endereços ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            ButtonBar.NovoClick += new EventHandler(btnNovo_Click);
            ButtonBar.PesquisaClick += new EventHandler(btnPesquisa_Click);

            if (!Page.IsPostBack) {
                ButtonBar.DefaultListBar(permissoes);
                ButtonBar.DisableExports(permissoes);
            }
        }

        protected void gdvEnderecos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvEnderecos.PageIndex = e.NewPageIndex;

            //Carrega grid conforme pesquisa
            CarregaGrid(getListaPesquisada());
        }

        protected void gdvEnderecos_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            // lista para saida dos dados
            List<Endereco> lista = getListaPesquisada();

            // usando MyExtensions para ordenar o grid
            lista.toSort<Endereco>(SortExp, Sortdir);

            // recarregando o grid
            CarregaGrid(lista);
        }

        protected void gdvEnderecos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["EnderecoEditar"] = gdvEnderecos.Rows[e.NewEditIndex].Cells[1].Text;
            Server.Transfer("cadEnderecos.aspx");
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Session["EnderecoEditar"] = string.Empty;
            Server.Transfer("cadEnderecos.aspx");
        }

        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            // Carregar grid com dados da pesquisa
            CarregaGrid(getListaPesquisada());
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            // limpado dados de consulta e tela
            txtParPesquisa.Text = string.Empty;
            rdPesquisaCep.Checked = true;
            dadoBusca = string.Empty;

            // descarregando a grid
            gdvEnderecos.DataSource = null;
            gdvEnderecos.DataBind();
            return;

        }

        protected void gdvEnderecos_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected List<Endereco> getListaPesquisada()
        {
            // lista para pesquisa segundo selecao na tela
            List<Endereco> listaEnds = new List<Endereco>();
           Endereco endPesquisa = new Endereco();
            dadoBusca = txtParPesquisa.Text;

            if (rdPesquisaCep.Checked)
            {
                endPesquisa.CepEndereco = dadoBusca;
                endPesquisa.DescricaoEndereco = null;
            }
            else
            {
                endPesquisa.CepEndereco = null;
                endPesquisa.DescricaoEndereco = dadoBusca;
            }

            // montagem da lista a partir da seleção
            listaEnds = CtrlEnd.Pesquisar(endPesquisa);

            endPesquisa = null;
            return listaEnds;
        }

        protected void CarregaGrid(List<Endereco> lista = null)
        {
            // caso seja selecionado o item selecione ... descarregar GRID
            if (string.IsNullOrEmpty(txtParPesquisa.Text))
            {
                gdvEnderecos.Descarregar();
                return;
            }

            gdvEnderecos.Preencher<Endereco>(lista);

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