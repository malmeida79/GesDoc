using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class cadSalaCliente : System.Web.UI.Page
    {
        #region declaracoes 

        Salas cSl = new Salas();
        SalasController CtrlSl = new SalasController();
        SetoresController CtrlST = new SetoresController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region Eventos

        protected void btnAcao_Click(object sender, EventArgs e)
        {

            if (!Validacoes.SelecionadoItem(cboSetor.SelectedIndex))
            {
                Mensagens.Alerta("Selecione um setor");
                return;
            }

            // de acordo com a ação da tela o usuario podera
            // ser alterado ou cadastrado. Todo o controle e 
            // realizado pela sessao que apresenta o codigo
            // do usuario.
            cSl.NomeSala = txtNomeSala.Text;
            cSl.ResponsavelSala = txtRespSala.Text;
            cSl.CodSetor = Convert.ToInt32(cboSetor.SelectedValue.ToString());
            cSl.CodCliente = Convert.ToInt32(hdnCodCliente.Value);

            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) == "Salvar")
            {
                cSl.CodSala = Convert.ToInt32(hdnCodSala.Value);

                if (CtrlSl.Alterar(cSl))
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
                if (CtrlSl.Inserir(cSl))
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
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Cadastro de salas ::";
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
                hdnCodCliente.Value = Session["ClienteEditar"].RecuperarValor<string>();
                CarregarTela(Session["CodSalaEditar"].RecuperarValor<Int32>());
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["ClienteEditar"] = hdnCodCliente.Value;
            Server.Transfer("cadClientes.aspx");
        }

        #endregion

        #region Metodos

        public void CarregaSetores(string valorSelecionado = null)
        {
            cboSetor.Preencher(CtrlST.PesquisarPorCodigoCliente(Convert.ToInt32(hdnCodCliente.Value)), "descricaoSetor", "codSetor", true, valorSelecionado);
        }

        private void CarregarTela(Int32 valorRecebido = 0)
        {
            if (valorRecebido > 0)
            {
                cSl.CodCliente = Convert.ToInt32(hdnCodCliente.Value);
                cSl.CodSala = valorRecebido;
                cSl = CtrlSl.PesquisarPorCodigoSala(cSl.CodSala);
                CarregaSetores(cSl.CodSetor.ToString());
                hdnCodSala.Value = cSl.CodSala.ToString();
                hdnCodCliente.Value = cSl.CodCliente.ToString();

                txtNomeSala.Text = cSl.NomeSala;
                txtRespSala.Text = cSl.ResponsavelSala;

                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
            }
            else
            {
                CarregaSetores();
            }
        }

        #endregion
    }
}