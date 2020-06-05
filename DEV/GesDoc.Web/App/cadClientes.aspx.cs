using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Models;
using GesDoc.Web.Controllers;
using GesDoc.Web.Services;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class cadClientes : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        Cliente Cli = new Cliente();
        GruposClientesController CtrlGrp = new GruposClientesController();
        ContatosController CtrlCt = new ContatosController();
        SetoresController CtrlSt = new SetoresController();
        SalasController CtrlSl = new SalasController();
        EnderecoClienteController CtrlEndCli = new EnderecoClienteController();
        ClientesController CtrlCli = new ClientesController();
        EquipamentosController CtrlEquip = new EquipamentosController();
        DocumentosController CtrlLD = new DocumentosController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;
        int codCliente;
        int codGrupo;

        ~cadClientes()
        {
            CtrlCli = null;
            CtrlGrp = null;
            CtrlEndCli = null;
            CtrlCt = null;
            CtrlSt = null;
            CtrlSl = null;
            Cli = null;
            CtrlEquip = null;
        }

        #endregion

        #region "Eventos"

        private void CarregarTela(Int32 valorRecebido = 0)
        {
            Session["quemExcluir"] = "nada";
            if (valorRecebido > 0)
            {
                // Só ativar se cliente já cadastrado pois para todas as ações
                // são necessários o codigo do cliente.
                btnAddCt.Enabled = true;
                btnAddSt.Enabled = true;
                btnAddSl.Enabled = true;
                btnAddEnd.Enabled = true;
                btnAddEquip.Enabled = true;

                // recebendo codigo do endereço e carregando a tela;
                codCliente = valorRecebido;

                // Buscando demais dados do endereço
                Cli = CtrlCli.PesquisarPorCodigo(codCliente);

                codGrupo = Cli.CodGrupo;

                // Carrega demais dados da tela com base no Cliente selecionado
                CarregaGrupos(codGrupo.ToString());
                CarregaEnderecos(codCliente);
                CarregaContatos(codCliente);
                CarregaSetores(codCliente);
                CarregaSalas(codCliente);
                CarregaEquipamentos(codCliente);

                hdnCodCliente.Value = codCliente.ToString();
                hdnCodGrupo.Value = codGrupo.ToString();
                txtNomeCliente.Text = Cli.NomeCliente;
                txtRazaoSocial.Text = Cli.RazaoSocialCliente;
                txtCpfCnpj.Text = Cli.CpfCnpjCliente;

                if (Cli.Status)
                {
                    rdAtivo.Checked = true;
                    rdInativo.Checked = false;
                }
                else
                {
                    rdAtivo.Checked = false;
                    rdInativo.Checked = true;
                }

                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
            }
            else
            {
                CarregaGrupos();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Cadastro de clientes ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            ButtonBar.AcaoClick += new EventHandler(btnAcao_Click);
            ButtonBar.CancelarClick += new EventHandler(btnCancelar_Click);
            ButtonBar.AcaoJQueryClick += new EventHandler(btnAcaoJQuery_click);

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultCadBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Limpar, visivel: false);
                Tab1.CssClass = "tabClicked";
                MainView.ActiveViewIndex = 0;
                CarregarTela(Session["ClienteEditar"].RecuperarValor<Int32>());
            }
            else
            {
                if (!string.IsNullOrEmpty(hdnCodCliente.Value))
                {
                    // somente preencher quando existirem dados.
                    codCliente = Convert.ToInt32(hdnCodCliente.Value);
                    codGrupo = Convert.ToInt32(hdnCodGrupo.Value);
                }
            }
        }

        protected void btnAcao_Click(object sender, EventArgs e)
        {

            if (!Validacoes.EstaPreenchido(txtNomeCliente.Text, 3))
            {
                Mensagens.Alerta("Necessário informar um Nome do Cliete para cadastro.");
                return;
            }

            if (!Validacoes.EstaPreenchido(txtRazaoSocial.Text, 3))
            {
                Mensagens.Alerta("Necessário informar um Razão Social válido para cadastro.");
                return;
            }

            if (!Validacoes.EstaPreenchido(txtCpfCnpj.Text))
            {
                Mensagens.Alerta("Necessário informar CPF/CNPJ válido para cadastro.");
                return;
            }

            if (txtCpfCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", "").Length < 12)
            {
                if (!Validacoes.IsCpf(txtCpfCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", "")))
                {
                    Mensagens.Alerta("Necessário informar CPF válido para cadastro.");
                    return;
                }
            }
            else
            {
                if (!Validacoes.IsCnpj(txtCpfCnpj.Text.Replace(".", "").Replace("/", "").Replace("-", "")))
                {
                    Mensagens.Alerta("Necessário informar CNPJ válido para cadastro.");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(hdnCodCliente.Value))
            {
                Cli.CodCliente = Convert.ToInt32(hdnCodCliente.Value);
            }

            Cli.NomeCliente = txtNomeCliente.Text;
            Cli.RazaoSocialCliente = txtRazaoSocial.Text;
            Cli.CpfCnpjCliente = txtCpfCnpj.Text;

            if (rdAtivo.Checked == true)
            {
                Cli.Status = true;
            }
            else
            {
                Cli.Status = false;
            }


            if (cboGrupo.SelectedIndex > -1)
            {
                Cli.CodGrupo = Convert.ToInt32(cboGrupo.SelectedValue.ToString());
            }

            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) == "Salvar")
            {
                if (CtrlCli.Alterar(Cli))
                {
                    Mensagens.Alerta("Dados alterados com sucesso.");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, texto: @"<span class="" glyphicon glyphicon-arrow-left""></span> Voltar");
                }
                else
                {
                    Mensagens.Alerta($"Falha na alteração dos dados:{Mensagens.MsgErro}");
                    return;
                }
            }
            else
            {
                if (CtrlCli.Inserir(Cli))
                {
                    Mensagens.Alerta("Dados cadastrados com sucesso.");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, texto: @"<span class="" glyphicon glyphicon-arrow-left""></span> Voltar");
                }
                else
                {
                    Mensagens.Alerta($"Falha no cadastramento dos dados:{Mensagens.MsgErro}");
                    return;
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["ClienteEditar"] = string.Empty;
            Server.Transfer("listaClientes.aspx");
        }

        #region "Tabs"

        protected void Tab1_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "tabClicked";
            Tab2.CssClass = "tabInitial";
            Tab3.CssClass = "tabInitial";
            Tab4.CssClass = "tabInitial";
            Tab5.CssClass = "tabInitial";
            MainView.ActiveViewIndex = 0;
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "tabInitial";
            Tab2.CssClass = "tabClicked";
            Tab3.CssClass = "tabInitial";
            Tab4.CssClass = "tabInitial";
            Tab5.CssClass = "tabInitial";
            MainView.ActiveViewIndex = 1;
        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "tabInitial";
            Tab2.CssClass = "tabInitial";
            Tab3.CssClass = "tabClicked";
            Tab4.CssClass = "tabInitial";
            Tab5.CssClass = "tabInitial";
            MainView.ActiveViewIndex = 2;
        }

        protected void Tab4_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "tabInitial";
            Tab2.CssClass = "tabInitial";
            Tab3.CssClass = "tabInitial";
            Tab4.CssClass = "tabClicked";
            Tab5.CssClass = "tabInitial";
            MainView.ActiveViewIndex = 3;
        }

        protected void Tab5_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "tabInitial";
            Tab2.CssClass = "tabInitial";
            Tab3.CssClass = "tabInitial";
            Tab4.CssClass = "tabInitial";
            Tab5.CssClass = "tabClicked";
            MainView.ActiveViewIndex = 4;
        }

        #endregion

        protected void gdvEnderecos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Session["quemExcluir"] = "endereco";
            Session["iditemacao"] = e.RowIndex;
            Mensagens.Confirm("Deseja realmente excluir o endereco?");
        }

        protected void gdvEquipamento_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Session["quemExcluir"] = "equipamento";
            Session["iditemacao"] = e.RowIndex;
            Mensagens.Confirm("Deseja realmente excluir o equipamento?");
        }

        protected void gdvContatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Session["quemExcluir"] = "contato";
            Session["iditemacao"] = e.RowIndex;
            Mensagens.Confirm("Deseja realmente excluir o contato?");
        }

        protected void gdvSetores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Session["quemExcluir"] = "setor";
            Session["iditemacao"] = e.RowIndex;
            Mensagens.Confirm("Deseja realmente excluir o Setor?");
        }

        protected void gdvSalas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Session["quemExcluir"] = "sala";
            Session["iditemacao"] = e.RowIndex;
            Mensagens.Confirm("Deseja realmente excluir a sala?");

        }

        protected void btnAcaoJQuery_click(object sender, EventArgs e)
        {
            string quemExcluir = Session["quemExcluir"].RecuperarValor<string>();

            switch (quemExcluir)
            {
                case "contato":
                    btnAcaoJQueryCnt_click(sender, e);
                    break;
                case "endereco":
                    btnAcaoJQueryEnd_click(sender, e);
                    break;
                case "equipamento":
                    btnAcaoJQueryEqp_click(sender, e);
                    break;
                case "setor":
                    btnAcaoJQuerySet_click(sender, e);
                    break;
                case "sala":
                    btnAcaoJQuerySla_click(sender, e);
                    break;
                default:
                    break;
            }

            Session["quemExcluir"] = "nada";
        }

        protected void btnAcaoJQuerySla_click(object sender, EventArgs e)
        {
            int rowIndex = (int)Session["iditemacao"];
            Salas slCli = new Salas();
            slCli.CodSala = Convert.ToInt32(gdvSalas.Rows[rowIndex].Cells[0].Text);
            slCli.CodCliente = Convert.ToInt32(gdvSalas.Rows[rowIndex].Cells[1].Text);
            if (CtrlSl.Excluir(slCli))
            {
                Mensagens.Alerta("Exclusão realizada com sucesso !!!");
                CarregaSalas(codCliente);
            }
            else
            {
                Mensagens.Alerta($"{Mensagens.MsgErro}");
            }
            slCli = null;
        }

        protected void btnAcaoJQueryEqp_click(object sender, EventArgs e)
        {
            int rowIndex = (int)Session["iditemacao"];
            Equipamento equipCli = new Equipamento();
            equipCli.CodEquipamento = Convert.ToInt32(gdvEquipamento.Rows[rowIndex].Cells[0].Text);
            equipCli.CodCliente = Convert.ToInt32(gdvEquipamento.Rows[rowIndex].Cells[1].Text);

            if (CtrlLD.ContaDocumentosEquipamentoCliente(codCliente, Convert.ToInt32(gdvEquipamento.Rows[rowIndex].Cells[0].Text)) > 0)
            {
                Mensagens.Alerta("Existem documentos para esse equipamento, ele não pode ser excluido.");
                return;
            }

            if (CtrlEquip.Excluir(equipCli))
            {
                Mensagens.Alerta("Exclusão realizada com sucesso !!!");
                CarregaEquipamentos(codCliente);
            }
            else
            {
                Mensagens.Alerta($"{Mensagens.MsgErro}");
            }
            equipCli = null;
        }

        protected void btnAcaoJQuerySet_click(object sender, EventArgs e)
        {
            int rowIndex = (int)Session["iditemacao"];
            Setores stCli = new Setores();
            int qtdSalas = 0;

            stCli.CodSetor = Convert.ToInt32(gdvSetores.Rows[rowIndex].Cells[0].Text);
            stCli.CodCliente = Convert.ToInt32(gdvSetores.Rows[rowIndex].Cells[1].Text);

            qtdSalas = CtrlSt.ContaSalasetor(stCli.CodSetor);

            if (qtdSalas <= 0)
            {
                if (CtrlSt.Excluir(stCli))
                {
                    Mensagens.Alerta("Exclusão realizada com sucesso !!!");
                    CarregaSetores(codCliente);
                }
                else
                {
                    Mensagens.Alerta($"Erro ao excluir:{Mensagens.MsgErro}");
                }
            }
            else
            {
                Mensagens.Alerta($"Setor possui {qtdSalas.ToString()} sala(s). Necessário remover ou alterar setor das salas para excluir esse setor.");
            }
            stCli = null;

        }

        protected void btnAcaoJQueryEnd_click(object sender, EventArgs e)
        {
            int rowIndex = (int)Session["iditemacao"];
            EnderecoCliente endCli = new EnderecoCliente();
            endCli.CodEnderecoCliente = Convert.ToInt32(gdvEnderecos.Rows[rowIndex].Cells[0].Text);
            endCli.CodCliente = Convert.ToInt32(gdvEnderecos.Rows[rowIndex].Cells[1].Text);

            if (CtrlEndCli.Excluir(endCli))
            {
                Mensagens.Alerta("Endereço excluido com sucesso.");
                CarregaEnderecos(codCliente);
            }
            else
            {
                Mensagens.Alerta($"Falha na exclusão do endereço:{Mensagens.MsgErro}");
                return;
            }

            endCli = null;
        }

        protected void btnAcaoJQueryCnt_click(object sender, EventArgs e)
        {
            int rowIndex = (int)Session["iditemacao"];
            Contatos ctCli = new Contatos();
            ctCli.CodContato = Convert.ToInt32(gdvContatos.Rows[rowIndex].Cells[0].Text);
            ctCli.CodCliente = Convert.ToInt32(gdvContatos.Rows[rowIndex].Cells[1].Text);
            if (CtrlCt.Excluir(ctCli))
            {
                Mensagens.Alerta("Exclusão realizada com sucesso !!!");
                CarregaContatos(codCliente);
            }
            else
            {
                Mensagens.Alerta($"{Mensagens.MsgErro}");
            }
            ctCli = null;
        }

        protected void gdvEnderecos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvEnderecos.PageIndex = e.NewPageIndex;
            CarregaEnderecos(codCliente);
        }

        protected void gdvContatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvContatos.PageIndex = e.NewPageIndex;
            CarregaContatos(codCliente);
        }

        protected void gdvSetores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvSetores.PageIndex = e.NewPageIndex;
            CarregaSetores(codCliente);
        }

        protected void gdvSalas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvSalas.PageIndex = e.NewPageIndex;
            CarregaSalas(codCliente);
        }

        protected void gdvEquipamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvEquipamento.PageIndex = e.NewPageIndex;
            CarregaEquipamentos(codCliente);
        }

        protected void gdvSetores_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["CodSetorClienteEditar"] = gdvSetores.Rows[e.NewEditIndex].Cells[0].Text;
            Session["CodClienteEditar"] = gdvSetores.Rows[e.NewEditIndex].Cells[1].Text;
            Server.Transfer("cadSetorCliente.aspx");
        }

        protected void gdvEnderecos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["CodEnderecoClienteEditar"] = gdvEnderecos.Rows[e.NewEditIndex].Cells[0].Text;
            Session["CodClienteEditar"] = gdvEnderecos.Rows[e.NewEditIndex].Cells[1].Text;
            Server.Transfer("cadEnderecoCliente.aspx");
        }

        protected void gdvContatos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["CodContatoEditar"] = gdvContatos.Rows[e.NewEditIndex].Cells[0].Text;
            Session["CodClienteEditar"] = gdvContatos.Rows[e.NewEditIndex].Cells[1].Text;
            Server.Transfer("cadContatoCliente.aspx");
        }

        protected void gdvSalas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["CodSalaEditar"] = gdvSalas.Rows[e.NewEditIndex].Cells[0].Text;
            Session["CodClienteEditar"] = gdvSalas.Rows[e.NewEditIndex].Cells[1].Text;
            Server.Transfer("cadSalaCliente.aspx");
        }

        protected void gdvEquipamento_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["CodEquipamentoClienteEditar"] = gdvEquipamento.Rows[e.NewEditIndex].Cells[0].Text;
            Session["CodClienteEditar"] = gdvEquipamento.Rows[e.NewEditIndex].Cells[1].Text;
            Server.Transfer("cadEquipCliente.aspx");
        }

        protected void gdvEnderecos_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            var lista = CtrlEndCli.PesquisarPorCodigoCliente(codCliente);

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<EnderecoCliente>(SortExp, Sortdir);
            CarregaEnderecos(codCliente, lista);
        }

        protected void gdvContatos_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            var lista = CtrlCt.PesquisarPorCodigoCliente(codCliente);

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<Contatos>(SortExp, Sortdir);
            CarregaContatos(codCliente, lista);
        }

        protected void gdvSetores_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            var lista = CtrlSt.PesquisarPorCodigoCliente(codCliente);

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<Setores>(SortExp, Sortdir);
            CarregaSetores(codCliente, lista);
        }

        protected void gdvSalas_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            var lista = CtrlSl.PesquisarPorCodigoCliente(codCliente);

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<Salas>(SortExp, Sortdir);
            CarregaSalas(codCliente, lista);
        }

        protected void gdvEquipamento_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            var lista = CtrlEquip.PesquisarPorCodigoCliente(codCliente);

            // usando MyExtensions para ordenar o grid
            lista = lista.toSort<Equipamento>(SortExp, Sortdir);
            CarregaEquipamentos(codCliente, lista);
        }

        protected void btnAddSl_Click(object sender, EventArgs e)
        {
            Session["CodSalaEditar"] = string.Empty;
            Session["CodClienteEditar"] = hdnCodCliente.Value;
            Server.Transfer("cadSalaCliente.aspx");
        }

        protected void btnAddSt_Click(object sender, EventArgs e)
        {
            Session["CodSetorClienteEditar"] = string.Empty;
            Session["CodClienteEditar"] = hdnCodCliente.Value;
            Server.Transfer("cadSetorCliente.aspx");
        }

        protected void btnAddCt_Click(object sender, EventArgs e)
        {
            Session["CodContatoEditar"] = string.Empty;
            Session["CodClienteEditar"] = hdnCodCliente.Value;
            Server.Transfer("cadContatoCliente.aspx");
        }

        protected void btnAddEnd_Click(object sender, EventArgs e)
        {
            Session["CodEnderecoClienteEditar"] = string.Empty;
            Session["CodClienteEditar"] = hdnCodCliente.Value;
            Server.Transfer("cadEnderecoCliente.aspx");
        }

        protected void btnAddEquip_Click(object sender, EventArgs e)
        {
            Session["CodEquipamentoClienteEditar"] = string.Empty;
            Session["CodClienteEditar"] = hdnCodCliente.Value;
            Server.Transfer("cadEquipCliente.aspx");
        }

        #endregion

        #region "Metodos internos"       

        public void CarregaGrupos(string selecionadoPais = null)
        {
            cboGrupo.Preencher<GruposClientes>(CtrlGrp.GetAll(), "nomeGrupo", "codGrupo", true, selecionadoPais);
        }

        public void CarregaEnderecos(int codCliente, List<EnderecoCliente> lista = null)
        {

            if (lista == null)
            {
                lista = CtrlEndCli.PesquisarPorCodigoCliente(codCliente);
            }

            gdvEnderecos.Columns[0].Visible = true;
            gdvEnderecos.Columns[1].Visible = true;
            gdvEnderecos.Preencher<EnderecoCliente>(lista);
            gdvEnderecos.Columns[0].Visible = false;
            gdvEnderecos.Columns[1].Visible = false;
        }

        public void CarregaContatos(int codCliente, List<Contatos> lista = null)
        {

            if (lista == null)
            {
                lista = CtrlCt.PesquisarPorCodigoCliente(codCliente);
            }


            gdvContatos.Columns[0].Visible = true;
            gdvContatos.Columns[1].Visible = true;
            gdvContatos.Preencher<Contatos>(lista);
            gdvContatos.Columns[0].Visible = false;
            gdvContatos.Columns[1].Visible = false;
        }

        public void CarregaSetores(int codCliente, List<Setores> lista = null)
        {

            if (lista == null)
            {
                lista = CtrlSt.PesquisarPorCodigoCliente(codCliente);
            }

            gdvSetores.Columns[0].Visible = true;
            gdvSetores.Columns[1].Visible = true;
            gdvSetores.Preencher<Setores>(lista);
            gdvSetores.Columns[0].Visible = false;
            gdvSetores.Columns[1].Visible = false;
        }

        public void CarregaSalas(int codCliente, List<Salas> lista = null)
        {

            if (lista == null)
            {
                lista = CtrlSl.PesquisarPorCodigoCliente(codCliente);
            }

            gdvSalas.Columns[0].Visible = true;
            gdvSalas.Columns[1].Visible = true;
            gdvSalas.Preencher<Salas>(lista);
            gdvSalas.Columns[0].Visible = false;
            gdvSalas.Columns[1].Visible = false;
        }

        public void CarregaEquipamentos(int codCliente, List<Equipamento> lista = null)
        {

            if (lista == null)
            {
                lista = CtrlEquip.PesquisarPorCodigoCliente(codCliente);
            }

            gdvEquipamento.Columns[1].Visible = true;
            gdvEquipamento.Columns[2].Visible = true;
            gdvEquipamento.Columns[4].Visible = true;
            gdvEquipamento.Preencher<Equipamento>(lista);
            gdvEquipamento.Columns[1].Visible = false;
            gdvEquipamento.Columns[2].Visible = false;
            gdvEquipamento.Columns[4].Visible = false;
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