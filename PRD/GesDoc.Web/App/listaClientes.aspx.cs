using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class listaClientes : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        ClientesController CtrlCli = new ClientesController();
        string dadoBusca = string.Empty;
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Lista de clientes ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            ButtonBar.NovoClick += new EventHandler(btnNovo_Click);
            ButtonBar.PesquisaClick += new EventHandler(btnPesquisa_Click);
            ButtonBar.ExportCsvClick += new EventHandler(ExportToCsv_Click);
            ButtonBar.ExportExcelClick += new EventHandler(ExportToExcel_Click);
            ButtonBar.ExportTxtClick += new EventHandler(ExportToTxt_Click);

            if (!Page.IsPostBack) {
                ButtonBar.DefaultListBar(permissoes);
                ButtonBar.DisableExports(permissoes);
            }
        }

        protected void gdvClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvClientes.PageIndex = e.NewPageIndex;

            //Carrega grid conforme pesquisa
            CarregaGrid(getListaPesquisada());
        }

        protected void gdvClientes_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            // lista para saida dos dados
            List<Cliente> lista = getListaPesquisada();

            // usando MyExtensions para ordenar o grid
            lista.toSort<Cliente>(SortExp, Sortdir);

            // recarregando o grid
            CarregaGrid(lista);
        }

        protected void gdvClientes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["ClienteEditar"] = gdvClientes.Rows[e.NewEditIndex].Cells[1].Text;
            Server.Transfer("cadClientes.aspx");
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Session["ClienteEditar"] = string.Empty;
            Server.Transfer("cadClientes.aspx");
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
            rdPesquisanome.Checked = true;
            dadoBusca = string.Empty;

            // descarregando a grid
            gdvClientes.DataSource = null;
            gdvClientes.DataBind();
            return;

        }

        protected void gdvClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    LinkButton addButton = (LinkButton)e.Row.Cells[0].Controls[0];
                    addButton.Text = "Selecionar";
                }
                decimal cpfcnpj = 0;

                // Recebendo CPF/CNPJ, converto para decimal para poder aplicar a mascara
                if (decimal.TryParse(e.Row.Cells[3].Text, out cpfcnpj))
                {
                    e.Row.Cells[3].Text = cpfcnpj.ToString(@"00\.000\.000\/0000\-00");
                }
                else
                {
                    Mensagens.Alerta( "CPF ou CNPJ incorretos, impossivel preencher o Grid !");
                    return;
                }

                bool status = Convert.ToBoolean(e.Row.Cells[4].Text);

                // Tratamento para true/false sair como ativo/inativo
                if (status)
                {
                    e.Row.Cells[4].Text = "Ativo";
                }
                else
                {
                    e.Row.Cells[4].Text = "Inativo";
                }

                bool deletado = Convert.ToBoolean(e.Row.Cells[6].Text);

                // Tratamento para true/false sair como ativo/inativo
                if (deletado)
                {
                    e.Row.Cells[6].Text = "Deletado";
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                }
                else
                {
                    e.Row.Cells[6].Text = "";
                }

            }
        }

        protected void ExportToCsv_Click(Object sender, EventArgs e)
        {
            List<Cliente> lista = getListaPesquisada();
            Exports.ListToCSV<Cliente>(lista, "Clientes");
        }

        protected void ExportToTxt_Click(Object sender, EventArgs e)
        {
            List<Cliente> lista = getListaPesquisada();
            Exports.ListToTXT<Cliente>(lista, "Clientes");
        }

        protected void ExportToExcel_Click(Object sender, EventArgs e)
        {
            List<Cliente> lista = getListaPesquisada();
            Exports.ListToExcel<Cliente>(lista, "Clientes");
        }

        #endregion

        #region "Metodos"

        protected List<Cliente> getListaPesquisada()
        {
            // lista para pesquisa segundo selecao na tela
            List<Cliente> listaClientes = new List<Cliente>();
           Cliente clientePesquisa = new Cliente();
            dadoBusca = txtParPesquisa.Text;

            if (rdPesquisanome.Checked)
            {
                clientePesquisa.NomeCliente = dadoBusca;
                clientePesquisa.CpfCnpjCliente = null;
                clientePesquisa.NomeGrupo = null;
            }
            else if (rdpesquisaCnpj.Checked)
            {
                clientePesquisa.NomeCliente = null;
                clientePesquisa.CpfCnpjCliente = dadoBusca;
                clientePesquisa.NomeGrupo = null;
            }
            else if (rdpesquisaGrupo.Checked)
            {
                clientePesquisa.NomeCliente = null;
                clientePesquisa.CpfCnpjCliente = null;
                clientePesquisa.NomeGrupo = dadoBusca;
            }

            //// montagem da lista a partir da seleção
            listaClientes = CtrlCli.PesquisarLista(clientePesquisa);

            //endPesquisa = null;
            return listaClientes;
        }

        protected void CarregaGrid(List<Cliente> lista = null)
        {
            // caso seja selecionado o item selecione ... descarregar GRID
            if (string.IsNullOrEmpty(txtParPesquisa.Text))
            {
                gdvClientes.Descarregar();
                return;
            }

            gdvClientes.Preencher<Cliente>(lista);
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