using System;
using GesDoc.Web.Services;
using GesDoc.Web.Controllers;
using GesDoc.Models;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class docsRaizCliente : System.Web.UI.Page
    {
        #region declaracoes

        string caminhoRaiz = string.Empty;
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);

            if (!permissoes.EhCliente)
            {
                liAbrir.Visible = permissoes.Leitura;
                liExcluir.Visible = permissoes.Excluir;
            }
            else
            {
                liAbrir.Visible = false;
                liExcluir.Visible = false;
            }

            liDownload.Visible = true;

            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            // cliente nao pode deletar arquivos
            if (UsuarioLogado.TipoCliente)
            {
                userCliente.Visible = false;
            }
            else
            {
                userCliente.Visible = true;
            }

            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Documentos do ** Cliente ** ::";

            ButtonBar.AcaoClick += new EventHandler(btnAcao_Click);

            if (!Page.IsPostBack)
            {
                listaArquivos.Visible = false;
                ButtonBar.DefaultCadBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Limpar, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, visivel: false);

                if (permissoes.EhCliente)
                {
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, visivel: false);
                }

                if (Session["CodRaizCliente"].RecuperarValor<Int32>() > 0)
                {
                    if (UsuarioLogado.TipoCliente)
                    {
                        preencheFileTree();
                    }
                }
                else
                {
                    if (!UsuarioLogado.TipoCliente)
                    {
                        CarregaCliente();
                    }
                }
            }
        }

        protected void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex > 0)
            {
                preencheFileTree();
                listaArquivos.Visible = true;
                Session["ClienteDocInserir"] = Convert.ToInt32(cboCliente.SelectedValue);
            }
            else
            {
                listaArquivos.Visible = false;
            }
        }

        #endregion

        #region metodos

        protected void CarregaCliente(string valorSelecionado = null)
        {
            ClientesController CtrlCli = new ClientesController();
            cboCliente.Preencher(CtrlCli.GetAll(), "NomeCliente", "codCliente", true, valorSelecionado);
            CtrlCli = null;
        }

        protected void preencheFileTree()
        {
            Arquivos flAdm = new Arquivos();
            // Buscando dados do arquivo
            flAdm = new Arquivos();

            if (!UsuarioLogado.TipoCliente)
            {
                flAdm.CodCliente = Convert.ToInt32(cboCliente.SelectedItem.Value);
            }
            else
            {
                flAdm.CodCliente = Session["CodRaizCliente"].RecuperarValor<Int32>();
            }

            listaArquivos.Visible = true;

            // Carregando a listagem de arquivos na pagina.
            string ArquivosParalista = flAdm.ListaParaExibicao(flAdm.GETPasta(Ambiente.TipoPasta.DocsCliente));
            listaArquivos.InnerHtml = ArquivosParalista;

            flAdm = null;
        }

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            Server.Transfer("adicionaDocumento.aspx");
        }

        #endregion
    }
}