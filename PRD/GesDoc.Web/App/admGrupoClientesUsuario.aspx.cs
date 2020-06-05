using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class admGrupoClientesUsuario : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        UsuarioGrupoClienteController Ctrlgt = new UsuarioGrupoClienteController();
        GruposClientesController CtrlGrupos = new GruposClientesController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        UsuarioGrupoCliente usuario;
        Permissoes permissoes;

        ~admGrupoClientesUsuario()
        {
            Ctrlgt = null;
            CtrlGrupos = null;
        }

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Administração de grupos ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            ButtonBar.AcaoClick += new EventHandler(btnAcao_Click);
            ButtonBar.CancelarClick += new EventHandler(btnVoltar_Click);

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultCadBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Limpar, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, habilitado: false);
                if (!string.IsNullOrEmpty((string)Session["usuarioEditar"]))
                {
                    hdnCodUsuario.Value = Session["usuarioEditar"].ToString();
                }
                CarregaGrupos();
                lblGrupos.Text = Ctrlgt.BuscaGruposUsuarioAcesso(Convert.ToInt32(hdnCodUsuario.Value));
            }
        }

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            try
            {
                int marcados = 0;
                
                usuario = new UsuarioGrupoCliente();
                usuario.codUsuario = Convert.ToInt32(hdnCodUsuario.Value);
                usuario.codGrupo = Convert.ToInt32(cboGrupo.SelectedValue);
                Ctrlgt.Remover(usuario);
                usuario = null;

                // Se opção de pesquisa por grupos entao grava a informação e nao adiciona a lista
                if (!chkPesquisaGrupo.Checked)
                {
                    foreach (ListItem item in chkGrpClientes.Items)
                    {
                        if (item.Selected)
                        {
                            // insere os clientes 1 a 1, cada cliente soma
                            // um em marcados, caso nao some entao deletaremos
                            // a lista toda pois nao tem cliente marcado
                            usuario = new UsuarioGrupoCliente();
                            usuario.codCliente = Convert.ToInt32(item.Value);
                            usuario.codUsuario = Convert.ToInt32(hdnCodUsuario.Value);
                            usuario.codGrupo = Convert.ToInt32(cboGrupo.SelectedValue);
                            usuario.consultaGrupo = chkPesquisaGrupo.Checked;
                            usuario.usuarioCadastro = UsuarioLogado.codUsuario;
                            Ctrlgt.Inserir(usuario);
                            usuario = null;
                            marcados++;
                        }
                    }

                    if (marcados > 0)
                    {
                        Mensagens.Alerta("Clientes adicionados com sucesso !!!");
                    }

                }
                else
                {
                    usuario = new UsuarioGrupoCliente();
                    usuario.codUsuario = Convert.ToInt32(hdnCodUsuario.Value);
                    usuario.codGrupo = Convert.ToInt32(cboGrupo.SelectedValue);
                    usuario.consultaGrupo = chkPesquisaGrupo.Checked;
                    usuario.usuarioCadastro = UsuarioLogado.codUsuario;
                    Ctrlgt.Inserir(usuario);
                    marcados++;
                    usuario = null;

                    if (marcados > 0)
                    {
                        Mensagens.Alerta("Grupo de Clientes adicionado com sucesso !!!");
                    }
                }

                // se nada marcado, deletamos o grupo do usuário e os clientes
                // selecionados também para esse grupo.
                if (marcados == 0)
                {
                    Mensagens.Alerta("Grupo e seus clientes removidos com sucesso !!!");
                }

                lblGrupos.Text = Ctrlgt.BuscaGruposUsuarioAcesso(Convert.ToInt32(hdnCodUsuario.Value));


            }
            catch (Exception ex)
            {
                Mensagens.Alerta($"Ocorreu um erro:{ex.Message.ToString()}");
                return;
            }
            // atualizando visao do usuario sobre os grupos selecioandos
            lblGrupos.Text = Ctrlgt.BuscaGruposUsuarioAcesso(Convert.ToInt32(hdnCodUsuario.Value));
        }

        protected void cboGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGrupo.SelectedIndex > 0)
            {
                CarregaListaChk();
                chkPesquisaGrupo.Enabled = true;
                VerificaSeTodosMarcados();
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, habilitado: permissoes.Gravacao);
            }
            else
            {
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, habilitado: false);
                chkPesquisaGrupo.Enabled = false;
                chkGrpClientes.ClearSelection();
                chkGrpClientes.Items.Clear();
                chkGrpClientes.Descarregar();
            }
        }

        protected void chkPesquisaGrupo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPesquisaGrupo.Checked)
            {
                // caso selecionado opção por grupo deverá ser desmarcado
                // todos os checks e descarregado.
                chkGrpClientes.ClearSelection();
                chkGrpClientes.Items.Clear();
                chkGrpClientes.DataSource = null;
                chkPesquisaGrupo.DataBind();
            }
            else
            {
                // carrega lista de clientes
                CarregaListaChk();
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Server.Transfer("cadUsuarios.aspx");
        }

        protected void CarregaListaChk(List<UsuarioGrupoCliente> lista = null)
        {
            if (cboGrupo.SelectedIndex > 0)
            {
                if (!chkPesquisaGrupo.Checked)
                {
                    if (lista == null)
                    {
                        lista = Ctrlgt.ListarClientesGrupo(Convert.ToInt32(cboGrupo.SelectedValue));
                    }

                    chkGrpClientes.Preencher<UsuarioGrupoCliente>(lista, "nomecliente", "codCliente");

                    preencherCHKList();
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, habilitado: permissoes.Gravacao);
                }
                else
                {
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, habilitado: false);
                    chkPesquisaGrupo.Enabled = false;
                    chkGrpClientes.ClearSelection();
                    chkGrpClientes.Items.Clear();
                    chkGrpClientes.Descarregar();
                }
            }
        }

        protected void chkGrpClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerificaSeTodosMarcados();
        }

        #endregion

        #region "Metodos"

        protected void CarregaGrupos()
        {
            cboGrupo.Preencher<GruposClientes>(CtrlGrupos.GetAll(), "nomeGrupo", "codGrupo", true);
        }

        protected void preencherCHKList()
        {

            List<UsuarioGrupoCliente> listagem = Ctrlgt.Pesquisar(Convert.ToInt32(hdnCodUsuario.Value), Convert.ToInt32(cboGrupo.SelectedValue));

            if (listagem != null)
            {

                if (chkPesquisaGrupo.Checked)
                {
                    chkGrpClientes.ClearSelection();
                    chkGrpClientes.Items.Clear();
                    return;
                }

                foreach (UsuarioGrupoCliente item in listagem)
                {
                    foreach (ListItem itemLista in chkGrpClientes.Items)
                    {
                        if (Convert.ToInt32(itemLista.Value) == item.codCliente)
                        {
                            itemLista.Selected = true;
                            break;
                        }
                    }
                }
            }

        }

        protected void VerificaSeTodosMarcados()
        {
            var total = chkGrpClientes.Items.Count;
            var marcado = 0;

            foreach (ListItem item in chkGrpClientes.Items)
            {
                if (item.Selected)
                {
                    marcado++;
                }
            }

            if (total > 0)
            {
                // significa que todos fora selecionados
                if (total == marcado)
                {
                    chkPesquisaGrupo.Checked = true;
                    chkGrpClientes.ClearSelection();
                    chkGrpClientes.Items.Clear();
                    chkGrpClientes.DataSource = null;
                    chkPesquisaGrupo.DataBind();
                }
                else
                {
                    chkPesquisaGrupo.Checked = false;
                }
            }
        }

        #endregion
    }
}