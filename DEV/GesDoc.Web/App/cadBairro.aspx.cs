using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class cadBairros : System.Web.UI.Page
    {
        #region declaracoes 

        Bairro entBairro = new Bairro();
        BairroController CtrlBair = new BairroController();
        CidadesController CtrlCid = new CidadesController();
        EstadosController CtrlEst = new EstadosController();
        PaisesController CtrlPais = new PaisesController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region Eventos

        protected void btnAcao_Click(object sender, EventArgs e)
        {

            // de acordo com a ação da tela o Bairro podera
            // ser alterado ou cadastrado. Todo o controle e 
            // realizado pela sessao que apresenta o codigo
            // do Bairro.
            entBairro.DescricaoBairro = txtNomeBairro.Text;
            entBairro.CodCidade = Convert.ToInt32(cboCidade.SelectedValue);

            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) == "Salvar")
            {
                entBairro.CodBairro = Convert.ToInt32(hdnCodBairro.Value);

                if (CtrlBair.Alterar(entBairro))
                {
                    Mensagens.Alerta("Dados alterados com sucesso.");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, texto: @"<span class="" glyphicon glyphicon-arrow-left""></span> Voltar");
                }
                else
                {
                    Mensagens.Alerta("Falha na alteração dos dados:{Tratamentos.MsgErro}");
                    return;
                }
            }
            else
            {
                if (CtrlBair.Inserir(entBairro))
                {                    
                    Mensagens.Alerta("Dados cadastrados com sucesso.");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, texto: @"<span class="" glyphicon glyphicon-arrow-left""></span> Voltar");
                }
                else
                {
                    Mensagens.Alerta("Falha no cadastramento dos dados:{Tratamentos.MsgErro}");
                    return;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Cadastro de bairros ::";
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
                CarregaPaises();
                CarregarTela(Session["BairroEditar"].RecuperarValor<Int32>());
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["BairroEditar"] = string.Empty;
            Server.Transfer("listaBairros.aspx");
        }

        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCidade.Descarregar();
            cboEstado.Descarregar();

            if (cboPais.SelectedIndex > 0)
            {
                CarregaEstados();
            }
        }

        protected void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCidade.Descarregar();

            if (cboEstado.SelectedIndex > 0)
            {
                CarregaCidades();
            }
        }

        #endregion

        #region Metodos

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
            cboCidade.Preencher<Cidade>(CtrlCid.ListarCidadesPorEstado(Convert.ToInt32(cboEstado.SelectedValue)), "descricaoCidade", "codCidade", true);
        }

        private void CarregarTela(Int32 valorRecebido = 0)
        {
            if (valorRecebido > 0)
            {
                entBairro.CodBairro = valorRecebido;
                entBairro = CtrlBair.Pesquisar(entBairro);

                hdnCodBairro.Value = entBairro.CodBairro.ToString();
                txtNomeBairro.Text = entBairro.DescricaoBairro;

                cboPais.SetSelectedValue(entBairro.CodPais.ToString());

                CarregaEstados();
                cboEstado.SetSelectedValue(entBairro.CodEstado.ToString());

                CarregaCidades();
                cboCidade.SetSelectedValue(entBairro.CodCidade.ToString());

                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
            }
            else
            {

            }
        }

        #endregion
    }
}