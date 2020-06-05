using System;
using System.Web.UI;
using GesDoc.Models;
using GesDoc.Web.Controllers;
using GesDoc.Web.Services;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class cadCidades : System.Web.UI.Page
    {
        #region declaracoes 

        Cidade Cidade = new Cidade();
        CidadesController CtrlCid = new CidadesController();
        EstadosController CtrlEst = new EstadosController();
        PaisesController CtrlPais = new PaisesController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region Eventos

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            if (cboEstado.SelectedIndex <= 0)
            {
                Mensagens.Alerta("Necessário informar um estado para cadastramento.");
                return;
            }

            if (!Validacoes.EstaPreenchido(txtNomeCidade.Text, 3))
            {
                Mensagens.Alerta("Necessário informar cidade para cadastramento.");
                return;
            }

            // de acordo com a ação da tela o Cidade podera
            // ser alterado ou cadastrado. Todo o controle e 
            // realizado pela sessao que apresenta o codigo
            // do Cidade.
            Cidade.DescricaoCidade = txtNomeCidade.Text;
            Cidade.CodEstado = Convert.ToInt32(cboEstado.SelectedValue);

            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) == "Salvar")
            {
                Cidade.CodCidade = Convert.ToInt32(hdnCodCidade.Value);

                if (CtrlCid.Alterar(Cidade))
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
                if (CtrlCid.Inserir(Cidade))
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
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Cadastro de cidades ::";
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
                CarregaPais();
                CarregarTela(Session["CidadeEditar"].RecuperarValor<Int32>());
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["CidadeEditar"] = string.Empty;
            Server.Transfer("listaCidades.aspx");
        }

        #endregion

        #region Metodos

        public void CarregaPais()
        {
            cboPais.Preencher(CtrlPais.GetAll(), "descricaoPais", "codPais", true);
        }

        public void CarregaEstados()
        {
            cboEstado.Preencher<Estado>(CtrlEst.ListarEstadosPorPais(Convert.ToInt32(cboPais.SelectedValue)), "descricaoEstado", "codEstado", true);
        }

        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboEstado.Descarregar();
            if (cboPais.SelectedIndex > 0)
            {
                CarregaEstados();
            }
        }

        private void CarregarTela(Int32 valorRecebido = 0)
        {
            if (valorRecebido > 0)
            {
                Cidade.CodCidade = valorRecebido;
                Cidade = CtrlCid.Pesquisar(Cidade);

                hdnCodCidade.Value = Cidade.CodCidade.ToString();
                txtNomeCidade.Text = Cidade.DescricaoCidade;

                cboPais.SetSelectedValue(Cidade.CodPais.ToString());

                CarregaEstados();

                cboEstado.SetSelectedValue(Cidade.CodEstado.ToString());

                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
            }
            else
            {

            }
        }

        #endregion
    }
}