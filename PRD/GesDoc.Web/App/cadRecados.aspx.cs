using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Models;
using GesDoc.Web.Controllers;
using GesDoc.Web.Services;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class cadRecados : System.Web.UI.Page
    {
        #region declaracoes 

        Recados Recados = new Recados();
        RecadosController CtrlRec = new RecadosController();
        TipoRecadoController CtrlTPRec = new TipoRecadoController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region Eventos

        protected void btnAcao_Click(object sender, EventArgs e)
        {

            // de acordo com a ação da tela o Recado podera
            // ser alterado ou cadastrado. Todo o controle e 
            // realizado pela sessao que apresenta o codigo
            // do Recado.
            Recados.Recado = txtRecado.Text;
            Recados.CodTipoRecado = Convert.ToInt32(cboTipoRecado.SelectedValue);
            Recados.CodUsuarioRecado = UsuarioLogado.codUsuario;
            Recados.Ativo = chkAtivo.Checked;

            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) == "Salvar")
            {
                Recados.CodRecado = Convert.ToInt32(hdnCodRecado.Value);

                if (CtrlRec.Alterar(Recados))
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
                if (CtrlRec.Inserir(Recados))
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

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Cadastro de recados ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );
            
            ButtonBar.AcaoClick += new EventHandler(btnAcao_Click);
            ButtonBar.CancelarClick += new EventHandler(btnCancelar_Click);
            ButtonBar.ExcluirClick += new EventHandler(btnExcluir_Click);
            ButtonBar.AcaoJQueryClick += new EventHandler(btnAcaoJQuery_click);

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultCadBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Limpar, visivel: false);
                CarregaRecados();
                CarregarTela(Session["RecadoEditar"].RecuperarValor<Int32>());
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["RecadoEditar"] = string.Empty;
            Server.Transfer("listaRecados.aspx");
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            Mensagens.Confirm("Deseja realmente excluir esse recado?");
        }

        protected void btnAcaoJQuery_click(object sender, EventArgs e)
        {
            Recados rec = new Recados();
            rec.CodRecado = Convert.ToInt32(hdnCodRecado.Value);
            if (CtrlRec.Excluir(rec))
            {
                Mensagens.Alerta("Dados excluidos com sucesso.");
                Session["RecadoEditar"] = string.Empty;
                Server.Transfer("listaRecados.aspx");
            }
            else
            {
                Mensagens.Alerta($"Falha na exclusão do recado.{Mensagens.MsgErro}");
                return;
            }
        }

        #endregion

        #region Metodos

        public void CarregaRecados()
        {
            cboTipoRecado.Preencher(CtrlTPRec.GetAll(), "descricaotipoRecado", "codTipoRecado", true);
        }

        private void CarregarTela(Int32 valorRecebido = 0)
        {
            if (valorRecebido > 0)
            {
                Recados.CodRecado = valorRecebido;
                Recados = CtrlRec.Pesquisar(Recados.CodRecado);

                hdnCodRecado.Value = Recados.CodRecado.ToString();
                txtRecado.Text = Recados.Recado;
                chkAtivo.Checked = Recados.Ativo;

                cboTipoRecado.SetSelectedValue(Recados.CodTipoRecado.ToString());

                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
            }
            else
            {

            }
        }

        #endregion
    }
}