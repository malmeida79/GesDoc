using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Models;
using GesDoc.Web.Controllers;
using GesDoc.Web.Services;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class cadSetorCliente : System.Web.UI.Page
    {
        #region declaracoes 

        Setores str = new Setores();
        SetoresController CtrlStr = new SetoresController();
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
            str.DescricaoSetor = txtNomeSetor.Text;
            str.ResponsavelTecnico = txtRspTecnico.Text;
            str.CRMResponsavel = txtCRMResp.Text;
            str.SupervisorTecnico = txtSupervisor.Text;
            str.CRVSupervisor = txtCRMSup.Text;
            str.ResponsavelLegal = txtNomeResponsavel.Text;
            str.CRMResponsavelLegal = txtCrmResponsavel.Text;


            if (!Validacoes.EstaPreenchido(txtRspTecnico.Text))
            {
                if (string.IsNullOrEmpty(txtRspTecnico.Text))
                {
                    Mensagens.Alerta("Necessário informar Nome do responsável válido para cadastro.");
                    return;
                }
            }

            if (!Validacoes.Numerico(txtCrmResponsavel.Text))
            {
                if (string.IsNullOrEmpty(txtCrmResponsavel.Text))
                {
                    Mensagens.Alerta("Necessário informar CRM válido para cadastro.");
                    return;
                }
            }

            str.CodCliente = Convert.ToInt32(hdnCodCliente.Value);

            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) == "Salvar")
            {
                str.CodSetor = Convert.ToInt32(hdnCodSetor.Value);


                if (CtrlStr.Alterar(str))
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
                if (CtrlStr.Inserir(str))
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
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Cadastro de setores ::";
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
                hdnCodCliente.Value = Session["CodClienteEditar"].RecuperarValor<string>();
                CarregarTela(Session["CodSetorClienteEditar"].RecuperarValor<Int32>());

            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["ClienteEditar"] = hdnCodCliente.Value;
            Server.Transfer("cadClientes.aspx");
        }

        #endregion

        #region Metodos

        private void CarregarTela(Int32 valorRecebido = 0)
        {
            if (valorRecebido > 0)
            {
                str.CodCliente = Convert.ToInt32(hdnCodCliente.Value);
                str.CodSetor = valorRecebido;
                str = CtrlStr.PesquisarPorCodigoSetor(str.CodSetor);
                hdnCodSetor.Value = str.CodSetor.ToString();
                hdnCodCliente.Value = str.CodCliente.ToString();
                txtNomeSetor.Text = str.DescricaoSetor;
                txtRspTecnico.Text = str.ResponsavelTecnico;
                txtCRMResp.Text = str.CRMResponsavel;
                txtSupervisor.Text = str.SupervisorTecnico;
                txtCRMSup.Text = str.CRVSupervisor;
                txtNomeResponsavel.Text = str.ResponsavelLegal;
                txtCrmResponsavel.Text = str.CRMResponsavelLegal;

                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
            }
            else
            {

            }
        }

        #endregion
    }
}