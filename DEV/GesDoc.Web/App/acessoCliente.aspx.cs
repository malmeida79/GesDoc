using System;
using GesDoc.Web.Services;
using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Infraestructure;
using System.Web.UI.WebControls;

namespace GesDoc.Web.App
{
    public partial class acessoCliente : System.Web.UI.Page
    {
        #region declaracoes

        UsuarioLogado UsuarioLogado = new UsuarioLogado();
        Permissoes permissoes;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

            ((Label)Master.FindControl("lblPrincipal")).Text = ":: Meus documentos ::";
            UsuarioLogado = Ambiente.ValidaAcesso();
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
            HelperPages.SetHelp(
                ((HiddenField)Master.FindControl("hfDuvidas")),
                MapeamentoPaths.GetPaginaAtual(),
                UsuarioLogado.TipoCliente
            );

            if (!Page.IsPostBack)
            {
                listaArquivos.Visible = false;
                ButtonBar.DefaultCadBar(permissoes);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Excluir, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Limpar, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Acao, visivel: false);
                ButtonBar.ConfigButtons(Ambiente.BotoesBarra.Cancelar, visivel: false);

                hdnCodCliente.Value = Session["codClienteAcesso"].RecuperarValor<string>();

                if (permissoes.EhCliente)
                {
                    CarregaEquipamentos();
                }
                else {
                    Mensagens.Alerta("Essa página e apenas para usuários do tipo cliente!","interna.aspx");
                }
            }
        }

        #endregion

        #region metodos

        protected void CarregaEquipamentos()
        {
            EquipamentosController CtrlEquip = new EquipamentosController();
            cboEquipamentos.Preencher<Equipamento>(CtrlEquip.PesquisarPorCodigoClienteComTipo(Convert.ToInt32(hdnCodCliente.Value)), "descricaoEquipamento", "codEquipamento", incluiSelecione: true, textoSelecione: "-- Selecione --");
            CtrlEquip = null;
        }

        #endregion

        protected void cboEquipamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEquipamentos.SelectedIndex > 0)
            {
                Arquivos flAdm = new Arquivos();
                // Buscando dados do arquivo
                flAdm = new Arquivos();
                flAdm.CodCliente = Convert.ToInt32(hdnCodCliente.Value);
                flAdm.CodEquipamento = Convert.ToInt32(cboEquipamentos.SelectedValue);

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
    }
}