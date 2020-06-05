using System;
using System.Collections.Generic;
using System.Web.UI;
using GesDoc.Models;
using GesDoc.Web.Controllers;
using GesDoc.Web.Services;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class cadEnderecos : System.Web.UI.Page
    {
        #region "Declarações , inicialização e encerramento"

        Endereco Est = new Endereco();
        PaisesController CtrlPais = new PaisesController();
        EstadosController CtrlEst = new EstadosController();
        CidadesController CtrlCid = new CidadesController();
        BairroController CtrlBair = new BairroController();
        LogradourosController CtrlLog = new LogradourosController();
        EnderecoController CtrlEnd = new EnderecoController();
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        int codPais = 0;
        int codEstado = 0;
        int codCidade = 0;
        int codBairro = 0;
        int codLogradouro = 0;
        int codEndereco = 0;

        ~cadEnderecos()
        {
            CtrlPais = null;
            CtrlEst = null;
            CtrlCid = null;
            CtrlBair = null;
            CtrlLog = null;
            CtrlEnd = null;
        }

        #endregion

        #region "Eventos"

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            txtCep.Text = txtCep.Text.Replace("-", "");

            if (!Validacoes.EstaPreenchido(txtNomeEndereco.Text, 3))
            {
                Mensagens.Alerta("Necessário informar um endereço para cadastro.");
                return;
            }

            if (!Validacoes.EstaPreenchido(txtCep.Text, 9))
            {
                Mensagens.Alerta("Necessário informar um cep válido para cadastro.");
                return;
            }

            if (!Validacoes.Numerico(txtCep.Text))
            {
                Mensagens.Alerta("Necessário informar cep válido para cadastro.");
                return;
            }

            if (CboBairro.SelectedIndex <= 0)
            {
                Mensagens.Alerta("Necessário informar um bairro para cadastro.");
                return;
            }

            // de acordo com a ação da tela o Endereco podera
            // ser alterado ou cadastrado. Todo o controle e 
            // realizado pela sessao que apresenta o codigo
            // do Endereco.
            Est.DescricaoEndereco = txtNomeEndereco.Text;
            Est.CodCidade = Convert.ToInt32(cboCidade.SelectedValue);

            if (ButtonBar.GetButtonText(Ambiente.BotoesBarra.Acao) == "Salvar")
            {
                Est.CodEndereco = Convert.ToInt32(hdnCodEndereco.Value);
                Est.CodLogradouro = Convert.ToInt32(cboLogradouro.SelectedValue.ToString());
                Est.DescricaoEndereco = txtNomeEndereco.Text;
                Est.CepEndereco = txtCep.Text;
                Est.CodBairro = Convert.ToInt32(CboBairro.SelectedValue.ToString());

                if (CtrlEnd.Alterar(Est))
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
                if (CtrlEnd.Inserir(Est))
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
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Cadastro de endereços ::";
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
                CarregarTela(Session["EnderecoEditar"].RecuperarValor<Int32>());
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["EnderecoEditar"] = string.Empty;
            Server.Transfer("listaEnderecos.aspx");
        }

        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPais.SelectedIndex > 0)
            {
                carregaEstado();
                carregaLogradouro();
                cboCidade.Descarregar();
                CboBairro.Descarregar();
            }
            else
            {
                cboEstado.Descarregar();
                cboCidade.Descarregar();
                CboBairro.Descarregar();
            }
        }

        protected void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstado.SelectedIndex > 0)
            {
                CarregaCidades();
                CboBairro.Descarregar();
            }
            else
            {
                cboCidade.Descarregar();
                CboBairro.Descarregar();
            }
        }

        protected void cboCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstado.SelectedIndex > 0)
            {
                carregaBairro();
            }
            else
            {
                CboBairro.Descarregar();
            }
        }

        #endregion

        #region "Metodos"       

        public void carregaPais(string selecionadoPais = null)
        {
            cboPais.Preencher<Pais>(CtrlPais.GetAll(), "descricaoPais", "codPais", true, selecionadoPais);
        }

        public void carregaEstado(string selecionadoEstado = null)
        {
            cboEstado.Preencher<Estado>(CtrlEst.ListarEstadosPorPais(Convert.ToInt32(cboPais.SelectedValue.ToString())), "descricaoEstado", "codEstado", true, selecionadoEstado);
        }

        public void CarregaCidades(string selecionadoCidade = null)
        {
            cboCidade.Preencher<Cidade>(CtrlCid.ListarCidadesPorEstado(Convert.ToInt32(cboEstado.SelectedValue.ToString())), "descricaoCidade", "codCidade", true, selecionadoCidade);
        }

        public void carregaBairro(string selecionadoBairro = null)
        {
            CboBairro.Preencher<Bairro>(CtrlBair.ListarBairroPorCidade(Convert.ToInt32(cboCidade.SelectedValue.ToString())), "descricaoBairro", "codBairro", true, selecionadoBairro);
        }

        public void carregaLogradouro(string selecionadoLogradouro = null)
        {
            List<Logradouro> listaPais = new List<Logradouro>();
            listaPais = CtrlLog.GetAll();
            cboLogradouro.Preencher<Logradouro>(listaPais, "descricaoLogradouro", "codLogradouro", true, selecionadoLogradouro);
        }

        private void CarregarTela(Int32 valorRecebido = 0)
        {
            if (valorRecebido > 0)
            {
                // recebendo codigo do endereço e carregando a tela;
                codEndereco = valorRecebido;

                // Buscando demais dados do endereço
                Est = CtrlEnd.PesquisarPorCodigo(codEndereco);

                codPais = Est.CodPais;
                codEstado = Est.CodEstado;
                codCidade = Est.CodCidade;
                codBairro = Est.CodBairro;
                codLogradouro = Est.CodLogradouro;

                // Carrega demais dados da tela com base no endereco selecionado
                carregaPais(codPais.ToString());
                carregaEstado(codEstado.ToString());
                CarregaCidades(codCidade.ToString());
                carregaBairro(codBairro.ToString());
                carregaLogradouro(codLogradouro.ToString());

                hdnCodEndereco.Value = codEndereco.ToString();
                txtNomeEndereco.Text = Est.DescricaoEndereco;
                txtCep.Text = Est.CepEndereco.ToString();

                cboCidade.SetSelectedValue(Est.CodCidade.ToString());

                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class="" glyphicon glyphicon-floppy-saved""></span> Salvar");
            }
            else
            {

                // Carrega combo de paises somente pois conforme
                // usuario for selecionando iremos carregando
                // as demais de acordo com as escolhas

                carregaPais();
            }
        }

        #endregion
    }
}