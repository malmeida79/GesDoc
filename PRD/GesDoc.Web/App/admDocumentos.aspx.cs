using System;
using GesDoc.Web.Services;
using GesDoc.Models;
using System.Web.UI.WebControls;
using GesDoc.Web.Infraestructure;

namespace GesDoc.Web.App
{
    public partial class admDocumentos : System.Web.UI.Page
    {
        #region declaracoes

        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Gestão de Documentos ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);

            if (!permissoes.EhCliente)
            {
                liAbrir.Visible = permissoes.Leitura;
                liAssinar.Visible = permissoes.AssinaDocumento;
                liLiberar.Visible = permissoes.LiberaDocumento;
                liExcluir.Visible = permissoes.Excluir;
            }

            liDownload.Visible = true;

            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            // recuperando dados do cliente
            hdnCodCliente.Value = Session["ClienteEditar"].RecuperarValor<string>();
            ButtonBar.AcaoClick += new EventHandler(btnAcao_Click);
            ButtonBar.CancelarClick += new EventHandler(btnCancelar_Click);
            DropEquipamentos.SelectedIndexChanged += new EventHandler(cboEquipamentos_SelectedIndexChanged);

            if (!Page.IsPostBack)
            {
                listaArquivos.Visible = false;
                ButtonBar.DefaultCadBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Novo, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Limpar, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, texto: @"<span class=""glyphicon glyphicon-plus""></span> Adicionar");

                DropEquipamentos.ClienteReferencia = hdnCodCliente.Value.RecuperarValor<Int32>();
                DropEquipamentos.CarregaEquipamentos();

                if (DropEquipamentos.GetItemCount() <= 0)
                {
                    Mensagens.Alerta("Cliente não possui equipamentos cadastrados!");
                    DropEquipamentos.SetEnable(true);
                    ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, habilitado: false);

                    return;
                }

                // O tratamento direto ocorre quando o usuario vem direto da pagina inicial, onde
                // ja temos o equipamento e serviço carregados no grid
                if (Session["tratamentoDireto"].RecuperarValor<bool>())
                {
                    DropEquipamentos.SetSelectedValue(Session["equipamentoDocumento"].RecuperarValor<string>());
                    cboEquipamentos_SelectedIndexChanged(sender, e);
                }
            }
        }

        protected void cboEquipamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropEquipamentos.GetSelectedIndex() > 0)
            {
                Arquivos flAdm = new Arquivos();
                // Buscando dados do arquivo
                flAdm = new Arquivos();
                flAdm.CodCliente = Convert.ToInt32(hdnCodCliente.Value);
                flAdm.CodEquipamento = DropEquipamentos.GetSelectedValue();
                Session["equipamentoDocumento"] = flAdm.CodCliente;

                listaArquivos.Visible = true;

                // Carregando a listagem de arquivos na pagina.
                string ArquivosParalista = flAdm.ListaParaExibicao(flAdm.GETPasta(Ambiente.TipoPasta.DocumentosEquipamentoCliente));
                listaArquivos.InnerHtml = ArquivosParalista;

                flAdm = null;
            }
            else
            {
                listaArquivos.Visible = false;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            // quando tratamento direto o usuario veio da index e para
            // ela deve voltar.
            if (Session["tratamentoDireto"].RecuperarValor<bool>())
            {
                Server.Transfer("interna.aspx");
            }
            else
            {
                Server.Transfer("listaClientesDocumento.aspx");
            }
        }

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            Server.Transfer("adicionaDocumento.aspx");
        }

        #endregion
    }
}