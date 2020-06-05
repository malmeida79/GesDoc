using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class cadEnderecoCliente : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        EnderecoCliente entEndCli = new EnderecoCliente();
        EnderecoController CtrlEnd = new EnderecoController();
        TipoEnderecoController CtrlTpEnd = new TipoEnderecoController();
        EnderecoClienteController CtrlEndCli = new EnderecoClienteController();
        Permissoes permissoes;
        string dadoBusca = string.Empty;

        ~cadEnderecoCliente()
        {
            entEndCli = null;
            CtrlEnd = null;
            CtrlEndCli = null;
            CtrlTpEnd = null;
        }

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            // recupera nome da pagina
            string pageFileName = System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath);
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            ButtonBar.AcaoClick += new EventHandler(btnAcao_Click);
            ButtonBar.CancelarClick += new EventHandler(btnCancela_Click);
            ButtonBar.PesquisaClick += new EventHandler(btnPesquisa_Click);
            ButtonBar.LimparClick += new EventHandler(btnLimpar_Click);

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultCadBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Pesquisa, visivel: permissoes.Leitura);
                // recuperando dados do cliente
                hdnCodCliente.Value = Session["CodClienteEditar"].RecuperarValor<string>();
                CarregarTela(sender, e, Session["CodEnderecoClienteEditar"].RecuperarValor<Int32>());
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

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            // verificando se existe um endereço selecionado ao menos
            if (gdvEnderecos.SelectedIndex < 0)
            {
                Mensagens.Alerta("Necessário selecionar um endereço ao menos para cadastro.");
                return;
            }


            GridViewRow linha = gdvEnderecos.Rows[gdvEnderecos.SelectedIndex];
            int codigoEndereco = Convert.ToInt32(linha.Cells[0].Text);

            // recuperando os dados da tela para salvar
            entEndCli.CodCliente = Convert.ToInt32(hdnCodCliente.Value);
            entEndCli.CodEndereco = codigoEndereco;
            entEndCli.Numero = txtNumero.Text;
            entEndCli.Complemento = txtComplemento.Text;
            entEndCli.Referencia = txtReferencia.Text;
            entEndCli.CodTipoEndereco = Convert.ToInt32(cboTipoEndereco.SelectedValue);

            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) == "Salvar")
            {
                entEndCli.CodEnderecoCliente = Convert.ToInt32(hdnCodEndCliente.Value);
                if (CtrlEndCli.Alterar(entEndCli))
                {
                    Mensagens.Alerta("Dados alterados com sucesso.");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, texto: @"<span class="" glyphicon glyphicon-arrow-left""></span> Voltar");
                    Server.Transfer("cadClientes.aspx");
                }
                else
                {
                    Mensagens.Alerta("Falha na alteração dos dados:{Tratamentos.MsgErro}");
                    return;
                }
            }
            else
            {
                if (CtrlEndCli.Inserir(entEndCli))
                {
                    Mensagens.Alerta("Dados cadastrados com sucesso.");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, texto: @"<span class="" glyphicon glyphicon-arrow-left""></span> Voltar");
                    Server.Transfer("cadClientes.aspx");
                }
                else
                {
                    Mensagens.Alerta("Falha na inclusão dos dados:{Tratamentos.MsgErro}");
                    return;
                }
            }

        }

        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            // Carregar grid com dados da pesquisa
            CarregaGrid(getListaPesquisada());
            gdvEnderecos.SelectedIndex = -1;
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            gdvEnderecos.Descarregar();
            ConsultaEnderecoPreSelecionado(sender, e);
            gdvEnderecos.SelectedIndex = 0;
        }

        protected void btnCancela_Click(object sender, EventArgs e)
        {
            Session["ClienteEditar"] = hdnCodCliente.Value;
            Server.Transfer("cadClientes.aspx");
        }

        #endregion

        #region "Metodos"

        private void CarregarTela(object sender, EventArgs e, Int32 valorRecebido = 0)
        {
            if (valorRecebido > 0)
            {
                // buscando os dados do endereço selecionado
                entEndCli = CtrlEndCli.PesquisarPorCodigoEndereco(valorRecebido);

                // preenchendo a tela
                hdnCodEndCliente.Value = valorRecebido.ToString();
                txtNumero.Text = entEndCli.Numero;
                txtComplemento.Text = entEndCli.Complemento;
                txtReferencia.Text = entEndCli.Referencia;
                CarregaTipoEndereco(entEndCli.CodTipoEndereco.ToString());
                ConsultaEnderecoPreSelecionado(sender, e, entEndCli);
                gdvEnderecos.SelectedIndex = 0;
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
            }
            else
            {
                // esta se cadastrando novo endereço então não tem 
                // endereço para restaurar.
                btnLimpar.Enabled = false;

                // Não e necessário nenhuma pré-seleção apenas carregar na tela.
                CarregaTipoEndereco();
            }
        }

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

        public void CarregaTipoEndereco(string valorSelecionado = null)
        {
            cboTipoEndereco.Preencher(CtrlTpEnd.GetAll(), "descricaoTipoEndereco", "codTipoEndereco", true, valorSelecionado);
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

        private void ConsultaEnderecoPreSelecionado(object sender, EventArgs e, EnderecoCliente enderecoSel = null)
        {
            /*
             * 
             Caso nao exista informação de endereço o mesmo então sera buscado pela primeira vez pelo
             metodo. Caso ja exista entao, ele tera condições de utilizar a informação ja carregada
             na tela.
             
             */

            if (enderecoSel == null)
            {
                enderecoSel = new EnderecoCliente();
                enderecoSel = CtrlEndCli.PesquisarPorCodigoEndereco(Convert.ToInt32(Session["CodEnderecoClienteEditar"]));
            }

            txtParPesquisa.Text = enderecoSel.CepEndereco;
            rdPesquisaCep.Checked = true;
            rdpesquisaEndereco.Checked = false;
            btnPesquisa_Click(sender, e);
            enderecoSel = null;
        }

        #endregion
    }
}