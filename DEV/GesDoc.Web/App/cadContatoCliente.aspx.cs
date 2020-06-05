using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class cadContatoCliente : System.Web.UI.Page
    {
        #region declaracoes 

        Contatos ctc = new Contatos();
        ContatosController CtrlCT = new ContatosController();
        TipoContatoController CtrlTPCT = new TipoContatoController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region Eventos

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            if (!Validacoes.EstaPreenchido(txtNome.Text, 3))
            {
                Mensagens.Alerta("Necessário informar um nome de contato para cadastramento.");
                return;
            }

            // de acordo com a ação da tela o usuario podera
            // ser alterado ou cadastrado. Todo o controle e 
            // realizado pela sessao que apresenta o codigo
            // do usuario.
            ctc.Nome = txtNome.Text;
            ctc.CodDDD = txtDDD.Text;
            ctc.Telefone = txtTelefone.Text;
            ctc.Email = txtEmail.Text;
            ctc.Ramal = txtRamal.Text;
            ctc.CodTipoContato = Convert.ToInt32(cboTipoContato.SelectedValue);
            ctc.CodCliente = Convert.ToInt32(hdnCodCliente.Value);

            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) == "Salvar")
            {
                ctc.CodContato = Convert.ToInt32(hdnCodContato.Value);


                if (CtrlCT.Alterar(ctc))
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
                if (CtrlCT.Inserir(ctc))
                {
                    Mensagens.Alerta("Dados cadastrados com sucesso.");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, texto: @"<span class="" glyphicon glyphicon-arrow-left""></span> Voltar");
                    Server.Transfer("cadClientes.aspx");
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

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultCadBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Limpar, visivel: false);
                hdnCodCliente.Value = Session["CodClienteEditar"].RecuperarValor<string>();
                CarregarTela(Session["CodContatoEditar"].RecuperarValor<Int32>());
                if (Session["CodContatoEditar"].RecuperarValor<Int32>() > 0)
                {

                }
                else
                {

                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["ClienteEditar"] = hdnCodCliente.Value;
            Server.Transfer("cadClientes.aspx");
        }

        #endregion

        #region Metodos

        public void CarregaTipoContato(string valorSelecionado = null)
        {
            cboTipoContato.Preencher(CtrlTPCT.GetAll(), "descricaoTipoContato", "codTipoContato", true, valorSelecionado);
        }

        private void CarregarTela(Int32 valorRecebido = 0)
        {
            if (valorRecebido > 0)
            {
                ctc.CodCliente = Convert.ToInt32(hdnCodCliente.Value);
                ctc.CodContato = valorRecebido;
                ctc = CtrlCT.PesquisarPorCodigoContato(ctc.CodContato);
                CarregaTipoContato(ctc.CodTipoContato.ToString());
                hdnCodContato.Value = ctc.CodContato.ToString();
                hdnCodCliente.Value = ctc.CodCliente.ToString();
                txtNome.Text = ctc.Nome;
                txtDDD.Text = ctc.CodDDD;
                txtTelefone.Text = ctc.Telefone;
                txtEmail.Text = ctc.Email;
                txtRamal.Text = ctc.Ramal;

                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
            }
            else
            {
                CarregaTipoContato();
            }
        }

        #endregion
    }
}