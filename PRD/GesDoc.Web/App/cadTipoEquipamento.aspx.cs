using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class cadTipoEquipamento : System.Web.UI.Page
    {
        #region declaracoes 

        TipoEquipamento entTipoEquipamento = new TipoEquipamento();
        TipoEquipamentoController CtrlTipoEquipamento = new TipoEquipamentoController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region eventos

        protected void btnAcao_Click(object sender, EventArgs e)
        {

            // de acordo com a ação da tela o TipoEquipamento podera
            // ser alterado ou cadastrado. Todo o controle e 
            // realizado pela sessao que apresenta o codigo
            // do TipoEquipamento.
            entTipoEquipamento.DescricaoTipoEquipamento = txtNomeTipoEquipamento.Text;

            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) == "Salvar")
            {
                entTipoEquipamento.CodTipoEquipamento = Convert.ToInt32(hdnCodTipoEquipamento.Value);

                if (CtrlTipoEquipamento.Alterar(entTipoEquipamento))
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
                if (CtrlTipoEquipamento.Inserir(entTipoEquipamento))
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
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Cadastro de tipos de equipamentos ::";
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
                CarregarTela(Session["TipoEquipamentoEditar"].RecuperarValor<Int32>());
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["TipoEquipamentoEditar"] = string.Empty;
            Server.Transfer("listaTipoEquipamento.aspx");
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            Mensagens.Confirm("Deseja realmente excluir esse tipo de equipamento?");
        }

        protected void btnAcaoJQuery_click(object sender, EventArgs e)
        {
            if (CtrlTipoEquipamento.ContaUso(Convert.ToInt32(hdnCodTipoEquipamento.Value)) <= 0)
            {

                if (CtrlTipoEquipamento.Excluir(Convert.ToInt32(hdnCodTipoEquipamento.Value)))
                {
                    Mensagens.Alerta("Dados Excluidos com sucesso.");
                    Session["TipoEquipamentoEditar"] = string.Empty;
                    Server.Transfer("listaTipoEquipamento.aspx");
                }
                else
                {
                    Mensagens.Alerta($"Falha no cadastramento dos dados:{Mensagens.MsgErro}");
                    return;
                }
            }
            else
            {
                Mensagens.Alerta("Esse TipoEquipamento atualmente esta em uso, não pode ser excluido.");
                return;
            }
        }

        #endregion

        #region metodos

        private void CarregarTela(Int32 valorRecebido = 0)
        {
            if (valorRecebido > 0)
            {
                entTipoEquipamento.CodTipoEquipamento = valorRecebido;
                entTipoEquipamento = CtrlTipoEquipamento.Pesquisar(entTipoEquipamento);

                hdnCodTipoEquipamento.Value = entTipoEquipamento.CodTipoEquipamento.ToString();
                txtNomeTipoEquipamento.Text = entTipoEquipamento.DescricaoTipoEquipamento;


                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
            }
            else
            {

            }
        }

        #endregion
    }
}