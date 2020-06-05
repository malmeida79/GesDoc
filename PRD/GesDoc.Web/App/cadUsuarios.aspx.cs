using GesDoc.Web.Controllers;
using GesDoc.Web.Services;
using GesDoc.Models;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class cadUsuarios : System.Web.UI.Page
    {
        #region Declações

        Usuario usr = new Usuario();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        UsuarioController CtrlUsr;
        ClientesController CtrlCli = new ClientesController();
        Permissoes permissoes;

        #endregion

        #region Eventos

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            #region Validações

            if (!Validacoes.EstaPreenchido(txtSenha.Text, 3))
            {
                Mensagens.Alerta("Necessário informar uma senha para cadastramento.");
                return;
            }

            if (!Validacoes.EstaPreenchido(txtCSenha.Text, 3))
            {
                Mensagens.Alerta("Necessário informar uma confirmação de senha para cadastramento.");
                return;
            }

            if (!Validacoes.ComparaValores(txtSenha.Text, txtCSenha.Text))
            {
                Mensagens.Alerta("A senha precisa ser igual a confirmação de senha.");
                return;
            }

            if (!Validacoes.isValidEmail(txtEmail.Text))
            {
                Mensagens.Alerta("Informe um e-mail válido.");
                return;
            }

            if (!Validacoes.EstaPreenchido(txtNomeUsuario.Text, 3))
            {
                Mensagens.Alerta("Necessário informar um nome de usuário para cadastramento.");
                return;
            }

            if (!Validacoes.EstaPreenchido(txtSobreNome.Text, 3))
            {
                Mensagens.Alerta("Necessário informar um sobrenome para o usuário para cadastramento.");
                return;
            }

            if (!Validacoes.EstaPreenchido(txtLogin.Text, 3))
            {
                Mensagens.Alerta("Necessário informar login para cadastramento.");
                return;
            }

            #endregion

            // de acordo com a ação da tela o usuario podera
            // ser alterado ou cadastrado. Todo o controle e 
            // realizado pela sessao que apresenta o codigo
            // do usuario.
            usr.nomeUsuario = txtNomeUsuario.Text;
            usr.sobreNome = txtSobreNome.Text;
            usr.login = txtLogin.Text;
            usr.email = txtEmail.Text;
            usr.senha = txtSenha.Text;
            usr.Bloqueado = chkBloqueado.Checked;
            usr.Ativo = chkAtivo.Checked;
            usr.TipoCliente = chkTipoCliente.Checked;
            usr.AssinaDocumento = chkAssinaDocumento.Checked;
            usr.LiberaDocumento = chkLiberaDocumento.Checked;

            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) == "Salvar")
            {
                usr.codUsuario = Convert.ToInt32(hdnCodUsuario.Value);

                if (CtrlUsr.ValidaExistenciaUsuario(usr.nomeUsuario, usr.login, usr.codUsuario) > 0)
                {
                    Mensagens.Alerta("Usuário ou login já existentes dados não gravados.");
                    return;
                }

                if (CtrlUsr.Alterar(usr))
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
                if (CtrlUsr.ValidaExistenciaUsuario(usr.nomeUsuario, usr.login) > 0)
                {
                    Mensagens.Alerta("Usuário ou login já existentes dados não gravados.");
                    return;
                }

                if (CtrlUsr.Inserir(usr))
                {
                    usr.codUsuario = CtrlUsr.BuscaCodigoUsuario(usr.login, usr.senha);
                    Session["usuarioEditar"] = usr.codUsuario.ToString();
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
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Cadastro de usuários ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            CtrlUsr = new UsuarioController();

            ButtonBar.AcaoJQueryClick += new EventHandler(btnAcaoJQuery_click);
            ButtonBar.AcaoClick += new EventHandler(btnAcao_Click);
            ButtonBar.CancelarClick += new EventHandler(btnCancelar_Click);
            ButtonBar.ExcluirClick += new EventHandler(btnExcluir_Click);
            ButtonBar.AcaoJQueryClick += new EventHandler(btnAcaoJQuery_click);

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultCadBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Limpar, visivel: false);
                CarregarTela(Session["usuarioEditar"].RecuperarValor<Int32>());
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["usuarioEditar"] = string.Empty;
            Server.Transfer("listaUsuarios.aspx");
        }

        protected void btnAcessoGrupos_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((string)Session["usuarioEditar"]))
            {
                Mensagens.Alerta("Usuário ainda não cadastrado, não é possivel atribuir acesso a grupos.");
                return;
            }
            else
            {
                Server.Transfer("admGruposUsuario.aspx");
            }
        }

        protected void btnAcessoClientes_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((string)Session["usuarioEditar"]))
            {
                Mensagens.Alerta("Usuário ainda não cadastrado, não é possivel atribuir acesso a grupos.");
                return;
            }
            else
            {
                Server.Transfer("admGrupoClientesUsuario.aspx");
            }
        }

        protected void chkTipoCliente_CheckedChanged(object sender, EventArgs e)
        {
            ConfiguraTela(chkTipoCliente.Checked);
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            Mensagens.Confirm("Deseja realmente excluir o usuário?");
        }

        protected void btnAcaoJQuery_click(object sender, EventArgs e)
        {
            usr.codUsuario = Convert.ToInt32(hdnCodUsuario.Value);

            if (CtrlUsr.Excluir(usr))
            {
                Mensagens.Alerta("Usuario excluido com sucesso!");
                Session["usuarioEditar"] = null;
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Recuperar, visivel: false, habilitado: false);
                Server.Transfer(MapeamentoPaths.GetPaginaAtual());
            }
            else
            {
                Mensagens.Alerta($"Falha na exclusão dos dados:{Mensagens.MsgErro}");
                return;
            }
        }

        protected void btnReativa_Click(object sender, EventArgs e)
        {
            usr.codUsuario = Convert.ToInt32(hdnCodUsuario.Value);
            usr.nomeUsuario = txtNomeUsuario.Text;
            usr.login = txtLogin.Text;

            if (CtrlUsr.ValidaExistenciaUsuario(usr.nomeUsuario, usr.login, usr.codUsuario) > 0)
            {
                Mensagens.Alerta("Não é possivel reativar esse usuário, outro usuário usa o mesmo login.");
                return;
            }

            if (CtrlUsr.Reativar(usr.codUsuario))
            {
                Mensagens.Alerta("Usuario reativado com sucesso!");
                Server.Transfer(MapeamentoPaths.GetPaginaAtual());
            }
            else
            {
                Mensagens.Alerta($"Falha na reativação do usuário:{Mensagens.MsgErro}");
                return;
            }
        }

        #endregion

        #region Metodos

        private void CarregarTela(int codUsuario)
        {
            usr.codUsuario = codUsuario;

            if (codUsuario > 0)
            {
                usr = CtrlUsr.Pesquisar(usr);
                hdnCodUsuario.Value = usr.codUsuario.ToString();
                txtNomeUsuario.Text = usr.nomeUsuario;
                txtSobreNome.Text = usr.sobreNome;
                txtLogin.Text = usr.login;
                txtEmail.Text = usr.email;
                txtSenha.Attributes.Add("value", usr.senha);
                txtCSenha.Attributes.Add("value", usr.senha);

                chkBloqueado.Checked = usr.Bloqueado;
                chkAtivo.Checked = usr.Ativo;
                chkTipoCliente.Checked = usr.TipoCliente;
                chkAssinaDocumento.Checked = usr.AssinaDocumento;
                chkLiberaDocumento.Checked = usr.LiberaDocumento;

                if (usr.deletado)
                {
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, habilitado: false);
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, habilitado: false);
                    btnAcessoClientes.Enabled = false;
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Recuperar, visivel: true, habilitado: permissoes.Gravacao);
                }
                else
                {

                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, habilitado: permissoes.Gravacao);
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, habilitado: permissoes.Excluir);
                    btnAcessoClientes.Enabled = true;
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Recuperar, visivel: false, habilitado: false);
                }

                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, texto: @"<span class="" glyphicon glyphicon-ban-circle""></span> Cancelar");
            }

            ConfiguraTela(chkTipoCliente.Checked);

        }

        public void ConfiguraTela(bool ehCliente)
        {

            // tratando tela para exibir corretamente campos
            btnAcessoClientes.Enabled = ehCliente;
            chkAssinaDocumento.Enabled = !ehCliente;
            chkLiberaDocumento.Enabled = !ehCliente;

            // somente quando marcado cliente devemos remover os
            // chekcs, ao contrário nao devemos atribuir.
            if (ehCliente)
            {
                chkAssinaDocumento.Checked = !ehCliente;
                chkLiberaDocumento.Checked = !ehCliente;
                btnAcessoClientes.BorderColor = System.Drawing.Color.Yellow;
                btnAcessoClientes.ForeColor = System.Drawing.Color.Yellow;
            }
            else
            {
                btnAcessoClientes.BorderColor = ButtonBar.GetButtonBorderColor(Ambiente.BotoesBarra.Acao);
                btnAcessoClientes.ForeColor = System.Drawing.Color.White;
            }

            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) != "Salvar")
            {
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, habilitado: false);
            }
        }

        #endregion
    }
}