using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Models;
using GesDoc.Web.Controllers;
using GesDoc.Web.Services;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class cadEquipCliente : System.Web.UI.Page
    {
        #region declaracoes 

        Equipamento equip = new Equipamento();
        EquipamentosController CtrlEquip = new EquipamentosController();
        TipoEquipamentoController CtrlTipoEquipamento = new TipoEquipamentoController();
        SalasController CtrlSala = new SalasController();
        SetoresController CtrlSet = new SetoresController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region Eventos

        protected void btnAcao_Click(object sender, EventArgs e)
        {

            // de acordo com a ação da tela o usuario podera
            // ser alterado ou cadastrado. Todo o controle e 
            // realizado pela sessao que apresenta o codigo
            // do usuario.

            equip.CodCliente = Convert.ToInt32(hdnCodCliente.Value);
            equip.CodSala = Convert.ToInt32(cboSala.SelectedValue);
            equip.CodTipoEquipamento = Convert.ToInt32(cboTipo.SelectedValue);
            equip.Marca = txtMarca.Text;
            equip.Modelo = txtModelo.Text;
            equip.NumeroSerie = txtNumSerie.Text;
            equip.NumeroPatrimonio = txtNumPat.Text;
            equip.RegistroAnvisa = txtRegAnvisa.Text;
            equip.AnoFabricacao = cboAnoFab.SelectedValue;
            equip.StatusEquip = cboStatus.SelectedValue;

            int validaequip = CtrlEquip.ValidaEquipamentoExistente(equip.CodCliente, equip.NumeroSerie);

            if (validaequip > 0)
            {
                Mensagens.Alerta("Numero de série do equipamento não pode ser duplicado.");
                return;
            }

            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) == "Salvar")
            {
                equip.CodEquipamento = Convert.ToInt32(hdnCodEquipamento.Value);

                if (CtrlEquip.Alterar(equip))
                {
                    Mensagens.Alerta("Dados alterados com sucesso.");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, texto: @"<span class="" glyphicon glyphicon-arrow-left""></span> Voltar");
                    Server.Transfer("cadClientes.aspx");
                }
                else
                {
                    Mensagens.Alerta($"Falha na alteração dos dados:{Mensagens.MsgErro}");
                    return;
                }
            }
            else
            {
                if (CtrlEquip.Inserir(equip))
                {
                    Mensagens.Alerta("Dados cadastrados com sucesso.");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, texto: @"<span class="" glyphicon glyphicon-arrow-left""></span> Voltar");
                    Server.Transfer("cadClientes.aspx");
                }
                else
                {
                    Mensagens.Alerta($"Falha no cadastramento dos dados:{Mensagens.MsgErro}");
                    return;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Cadastro de equipamentos ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );
            
            ButtonBar.AcaoClick += new EventHandler(btnAcao_Click);
            ButtonBar.CancelarClick += new EventHandler(btnCancelar_Click);

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultCadBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Limpar, visivel: false);
                // recuperando dados do cliente
                if (Session["CodClienteEditar"].RecuperarValor<Int32>() > 0)
                {
                    hdnCodCliente.Value = Session["CodClienteEditar"].RecuperarValor<string>(); ;
                    CarregaSetor();
                    CarregaTipo();
                    CarregaAno();
                    CarregaStatus();
                }

                CarregarTela(Session["CodEquipamentoClienteEditar"].RecuperarValor<Int32>());
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["ClienteEditar"] = hdnCodCliente.Value;
            Server.Transfer("cadClientes.aspx");
        }

        protected void cboSetor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSetor.SelectedIndex > 0)
            {
                CarregaSala(Convert.ToInt32(cboSetor.SelectedValue));
            }
        }

        #endregion

        #region Metodos

        private void CarregarTela(Int32 valorRecebido = 0)
        {
            if (valorRecebido > 0)
            {
                equip.CodCliente = Convert.ToInt32(hdnCodCliente.Value);
                equip.CodEquipamento = valorRecebido;
                equip = CtrlEquip.PesquisarPorCodigoEquipamento(equip.CodEquipamento);
                hdnCodEquipamento.Value = equip.CodEquipamento.ToString();
                hdnCodCliente.Value = equip.CodCliente.ToString();
                txtMarca.Text = equip.Marca;
                txtModelo.Text = equip.Modelo;
                txtNumSerie.Text = equip.NumeroSerie;
                txtNumPat.Text = equip.NumeroPatrimonio;
                txtRegAnvisa.Text = equip.RegistroAnvisa;
                cboSetor.SetSelectedValue(equip.CodSetor.ToString());
                CarregaSala(Convert.ToInt32(cboSetor.SelectedValue));
                cboSala.SetSelectedValue(equip.CodSala.ToString());
                cboTipo.SetSelectedValue(equip.CodTipoEquipamento.ToString());
                cboAnoFab.SetSelectedValue(equip.AnoFabricacao);
                cboStatus.SetSelectedValue(equip.StatusEquip);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
            }
            else
            {

            }
        }

        public void CarregaSetor()
        {
            cboSetor.Preencher<Setores>(CtrlSet.PesquisarPorCodigoCliente(Convert.ToInt32(hdnCodCliente.Value)), "descricaoSetor", "codSetor", true);
        }

        public void CarregaSala(int codSetor = 0)
        {
            if (codSetor <= 0)
            {
                cboSala.Preencher<Salas>(CtrlSala.PesquisarPorCodigoCliente(Convert.ToInt32(hdnCodCliente.Value)), "NomeSala", "CodSala", true);
            }
            else
            {
                cboSala.Preencher<Salas>(CtrlSala.PesquisarPorCodigoSetor(codSetor), "NomeSala", "CodSala", true);
            }
        }

        public void CarregaTipo()
        {
            cboTipo.Preencher<TipoEquipamento>(CtrlTipoEquipamento.GetAll(), "descricaoTipoEquipamento", "codTipoEquipamento", true);
        }

        public void CarregaAno()
        {
            cboAnoFab.Items.Clear();
            for (int i = 1950; i < 2099; i++)
            {
                cboAnoFab.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        public void CarregaStatus()
        {
            cboStatus.Items.Clear();
            cboStatus.Items.Add(new ListItem("ATIVO", "ATIVO"));
            cboStatus.Items.Add(new ListItem("DESATIVADO", "DESATIVADO"));
        }

        #endregion
    }
}