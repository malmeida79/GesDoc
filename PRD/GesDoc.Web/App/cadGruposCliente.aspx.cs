using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class cadGruposCliente : System.Web.UI.Page
    {
        #region declaracoes 

        GruposClientes entGrupo = new GruposClientes();
        GruposClientesController CtrlGrupo = new GruposClientesController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region Eventos

        protected void btnAcao_Click(object sender, EventArgs e)
        {

            // de acordo com a ação da tela o Grupo podera
            // ser alterado ou cadastrado. Todo o controle e 
            // realizado pela sessao que apresenta o codigo
            // do Grupo.
            entGrupo.NomeGrupo = txtNomeGrupo.Text;

            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) == "Salvar")
            {
                entGrupo.CodGrupo = Convert.ToInt32(hdnCodGrupo.Value);

                if (CtrlGrupo.Alterar(entGrupo))
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
                if (CtrlGrupo.Inserir(entGrupo))
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
            Session["GrupoEditar"] = string.Empty;
            Server.Transfer("listaGruposCliente.aspx");
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            Mensagens.Confirm("Deseja realmente excluir esse grupo?");
        }

        protected void btnAcaoJQuery_click(object sender, EventArgs e)
        {
            if (CtrlGrupo.ContaUso(Convert.ToInt32(hdnCodGrupo.Value)) <= 0)
            {
                if (CtrlGrupo.Excluir(Convert.ToInt32(hdnCodGrupo.Value)))
                {
                    Mensagens.Alerta("Dados Excluidos com sucesso.");
                    Session["GrupoEditar"] = string.Empty;
                    Server.Transfer("listaGruposCliente.aspx");
                }
                else
                {
                    Mensagens.Alerta($"Falha no cadastramento dos dados:{Mensagens.MsgErro}");
                    return;
                }
            }
            else
            {
                Mensagens.Alerta("Esse grupo atualmente esta em uso, não pode ser excluido.");
                return;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Cadastro de grupos de clientes ::";
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
                CarregarTela(Session["GrupoEditar"].RecuperarValor<Int32>());
            }
        }

        #endregion

        #region Metodos

        private void CarregarTela(Int32 valorRecebido = 0)
        {
            if (valorRecebido > 0)
            {
                entGrupo.CodGrupo = valorRecebido;
                entGrupo = CtrlGrupo.Pesquisar(entGrupo);
                hdnCodGrupo.Value = entGrupo.CodGrupo.ToString();
                txtNomeGrupo.Text = entGrupo.NomeGrupo;
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
            }
            else
            {

            }
        }

        #endregion
    }
}