using System;
using GesDoc.Web.Services;
using GesDoc.Models;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class docsGeral : System.Web.UI.Page
    {
        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);

            if (!permissoes.EhCliente)
            {
                liAbrir.Visible = permissoes.Leitura;
                liExcluir.Visible = permissoes.Excluir;
            }
            else {
                liAbrir.Visible = false;
                liExcluir.Visible = false;
            }

            liDownload.Visible = true;

            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            Arquivos flAdm = new Arquivos();
            // Buscando dados do arquivo
            flAdm = new Arquivos();

            // Carregando a listagem de arquivos na pagina.
            string ArquivosParalista = flAdm.ListaParaExibicao(flAdm.GETPasta(Ambiente.TipoPasta.Geral));
            listaArquivos.InnerHtml = ArquivosParalista;

            flAdm = null;

            ButtonBar.AcaoClick += new EventHandler(btnAcao_Click);

            if (!Page.IsPostBack)
            {
                ButtonBar.DefaultCadBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Limpar, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, visivel: false);

                if (permissoes.EhCliente)
                {
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, visivel: false);
                }
            }
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Gestão de documentos ::";
        }

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            Server.Transfer("adicionaDocumento.aspx");
        }
    }
}