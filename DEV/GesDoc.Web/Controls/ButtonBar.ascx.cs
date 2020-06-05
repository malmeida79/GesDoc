using GesDoc.Models;
using System;
using System.Text;
using System.Web.UI.WebControls;
using static GesDoc.Web.Infraestructure.Ambiente;

namespace GesDoc.Web.Controls
{
    public partial class ButtonBar : System.Web.UI.UserControl
    {
        #region Handlers

        public event EventHandler PesquisaClick;
        public event EventHandler NovoClick;
        public event EventHandler AcaoClick;
        public event EventHandler LimparClick;
        public event EventHandler RecuperarClick;
        public event EventHandler ExcluirClick;
        public event EventHandler CancelarClick;
        public event EventHandler ExportCsvClick;
        public event EventHandler ExportExcelClick;
        public event EventHandler ExportTxtClick;
        public event EventHandler AcaoJQueryClick;

        #endregion

        #region Metodos

        protected LinkButton IDBotaoTratado(BotoesBarra botao)
        {
            LinkButton defaultButton = new LinkButton();

            switch (botao)
            {
                case BotoesBarra.Pesquisa:
                    return btnPesquisa;
                case BotoesBarra.Novo:
                    return btnNovo;
                case BotoesBarra.Acao:
                    return btnAcao;
                case BotoesBarra.Limpar:
                    return btnLimpar;
                case BotoesBarra.Recuperar:
                    return btnRecuperar;
                case BotoesBarra.Excluir:
                    return btnExcluir;
                case BotoesBarra.Cancelar:
                    return btnCancelar;
                case BotoesBarra.ExportaCsv:
                    return btnExportCsv;
                case BotoesBarra.ExportaExcel:
                    return btnExportExcel;
                case BotoesBarra.ExportaTxt:
                    return btnExportTxt;
                default:
                    throw new Exception("Tipo de botão desconhecido !");
            }
        }

        /// <summary>
        /// Configrador de estado de botoes, para tratamento individual quando necessario.
        /// </summary>
        /// <param name="botao">Botao</param>
        /// <param name="texto">Texto caso se deseje alterar o texto</param>
        /// <param name="visivel">Exibir ou não botao</param>
        /// <param name="habilitado">Habilita ou não botao</param>
        public void ConfigButtons(BotoesBarra botao, string texto = "", bool? visivel = null, bool? habilitado = null)
        {
            LinkButton botaoTratado = IDBotaoTratado(botao);
            if (!string.IsNullOrEmpty(texto))
            {
                botaoTratado.Text = texto;
            }

            if (visivel != null)
            {
                botaoTratado.Visible = (bool)visivel;
            }

            if (habilitado != null)
            {
                botaoTratado.Enabled = (bool)habilitado;
            }

        }

        /// <summary>
        /// Recupera o Text para o botao selecionado
        /// </summary>
        /// <param name="botao"></param>
        /// <returns></returns>
        public string GetButtonText(BotoesBarra botao)
        {
            string retorno = "";

            LinkButton botaoTratado = IDBotaoTratado(botao);

            // esse tratamento abaixo se fez necessario devido aos icones
            if (botaoTratado.Text.ToLower().Contains("salvar"))
            {
                retorno = "Salvar";
            }
            else if (botaoTratado.Text.ToLower().Contains("cadastrar"))
            {
                retorno = "Cadastrar";
            }
            else if (botaoTratado.Text.ToLower().Contains("excluir"))
            {
                retorno = "Excluir";
            }
            else if (botaoTratado.Text.ToLower().Contains("limpar"))
            {
                retorno = "Limpar";
            }

            return retorno;
        }

        /// <summary>
        /// Devolve a cor das bordas do botao.
        /// </summary>
        /// <param name="botao"></param>
        /// <returns></returns>
        public System.Drawing.Color GetButtonBorderColor(BotoesBarra botao)
        {
            LinkButton botaoTratado = IDBotaoTratado(botao);
            return botaoTratado.BorderColor;
        }

        /// <summary>
        /// Alteração da cor do botao
        /// </summary>
        /// <param name="botao">botao a ser alteado</param>
        /// <param name="cor">nova cor</param>
        public void ChangeColorButton(BotoesBarra botao, System.Drawing.Color cor)
        {
            LinkButton botaoTratado = IDBotaoTratado(botao);

            botaoTratado.BackColor = cor;
        }

        /// <summary>
        /// Alteração da cor  do texto do botão
        /// </summary>
        /// <param name="botao">botao a ser alteado</param>
        /// <param name="cor">nova cor</param>
        public void ChangeColorButtonText(BotoesBarra botao, System.Drawing.Color cor)
        {
            LinkButton botaoTratado = IDBotaoTratado(botao);

            botaoTratado.ForeColor = cor;
        }

        /// <summary>
        /// Alteração de cores das bordas do botão 
        /// </summary>
        /// <param name="botao">botao a ser alteado</param>
        /// <param name="cor">nova cor</param>
        public void ChanceColorButtonBorder(BotoesBarra botao, System.Drawing.Color cor)
        {
            LinkButton botaoTratado = IDBotaoTratado(botao);
            botaoTratado.BorderColor = cor;
        }

        /// <summary>
        /// Inicia a barra de ferramentas apenas com as ações de cadastro
        /// </summary>
        /// <param name="permissoesUser">Permissões do usuário</param>
        public void DefaultCadBar(Permissoes permissoesUser)
        {
            textoPermissao(permissoesUser);

            // ativos padrao para cadastro
            ConfigButtons(BotoesBarra.Acao, visivel: true, habilitado: permissoesUser.Gravacao);
            ConfigButtons(BotoesBarra.Excluir, visivel: true, habilitado: permissoesUser.Excluir);
            ConfigButtons(BotoesBarra.Cancelar, visivel: true, habilitado: true);
            ConfigButtons(BotoesBarra.Limpar, visivel: true, habilitado: true);
            ConfigButtons(BotoesBarra.Recuperar, visivel: false, habilitado: permissoesUser.Gravacao);

            // inativos padrao para cadastro
            ConfigButtons(BotoesBarra.Novo, visivel: false, habilitado: permissoesUser.Gravacao);
            ConfigButtons(BotoesBarra.Pesquisa, visivel: false, habilitado: permissoesUser.Leitura);
            ConfigButtons(BotoesBarra.Recuperar, visivel: false, habilitado: permissoesUser.Gravacao);

            DisableExports(permissoesUser);
        }

        /// <summary>
        /// Inicia a barra de ferramentas apenas com as ações de consulta
        /// </summary>
        /// <param name="permissoesUser">Permissões do usuário</param>
        public void DefaultListBar(Permissoes permissoesUser)
        {
            textoPermissao(permissoesUser);

            // ativos padrao para cadastro
            ConfigButtons(BotoesBarra.Acao, visivel: false, habilitado: permissoesUser.Gravacao);
            ConfigButtons(BotoesBarra.Excluir, visivel: false, habilitado: permissoesUser.Excluir);
            ConfigButtons(BotoesBarra.Cancelar, visivel: false, habilitado: true);
            ConfigButtons(BotoesBarra.Limpar, visivel: false, habilitado: true);
          
            // inativos padrao para cadastro
            ConfigButtons(BotoesBarra.Novo, visivel: true, habilitado: permissoesUser.Gravacao);
            ConfigButtons(BotoesBarra.Pesquisa, visivel: true, habilitado: permissoesUser.Leitura);
            ConfigButtons(BotoesBarra.Recuperar, visivel: false, habilitado: permissoesUser.Gravacao);

            DisableExports(permissoesUser);
        }

        /// <summary>
        /// Habilita botoes de exportacao do usuario
        /// </summary>
        /// <param name="permissoesUser">permissoes do usuario</param>
        public void EnableExports(Permissoes permissoesUser)
        {
            ConfigButtons(BotoesBarra.ExportaCsv, visivel: true, habilitado: permissoesUser.Leitura);
            ConfigButtons(BotoesBarra.ExportaExcel, visivel: true, habilitado: permissoesUser.Leitura);
            ConfigButtons(BotoesBarra.ExportaTxt, visivel: true, habilitado: permissoesUser.Leitura);
        }

        /// <summary>
        /// Desabilita botoes de exportacao do usuario
        /// </summary>
        /// <param name="permissoesUser">permissoes do usuario</param>
        public void DisableExports(Permissoes permissoesUser)
        {
            ConfigButtons(BotoesBarra.ExportaCsv, visivel: false, habilitado: permissoesUser.Leitura);
            ConfigButtons(BotoesBarra.ExportaExcel, visivel: false, habilitado: permissoesUser.Leitura);
            ConfigButtons(BotoesBarra.ExportaTxt, visivel: false, habilitado: permissoesUser.Leitura);
        }

        /// <summary>
        /// texto a ser exibido quando usuário tiver alguma permissao restrita
        /// </summary>
        /// <param name="permissoesUser"></param>
        protected void textoPermissao(Permissoes permissoesUser)
        {
            StringBuilder msg = new StringBuilder("Permissões restritas:");
            int contaRestrito = 0;

            if (!permissoesUser.Gravacao)
            {
                msg.Append(" [Não pode alterar]");
                contaRestrito++;
            }

            if (!permissoesUser.Excluir)
            {
                msg.Append(" [Não pode excluir]");
                contaRestrito++;
            }

            if (!permissoesUser.Leitura)
            {
                msg.Append(" [Não pode acessar]");
                contaRestrito++;
            }

            if (contaRestrito <= 0)
            {
                msg.Clear();
            }

            lblPermissaoAviso.Text = msg.ToString();

        }

        #endregion

        #region Acoes

        public void OnPesquisaClick(object sender, EventArgs e)
        {
            if (PesquisaClick != null)
            {
                PesquisaClick(sender, e);
            }
        }

        public void OnNovoClick(object sender, EventArgs e)
        {
            if (NovoClick != null)
            {
                NovoClick(sender, e);
            }
        }

        public void OnAcaoClick(object sender, EventArgs e)
        {
            if (AcaoClick != null)
            {
                AcaoClick(sender, e);
            }
        }

        public void OnLimparClick(object sender, EventArgs e)
        {
            if (LimparClick != null)
            {
                LimparClick(sender, e);
            }
        }

        public void OnExcluirClick(object sender, EventArgs e)
        {
            if (ExcluirClick != null)
            {
                ExcluirClick(sender, e);
            }
        }

        public void OnRecuperarClick(object sender, EventArgs e)
        {
            if (RecuperarClick != null)
            {
                RecuperarClick(sender, e);
            }
        }

        public void OnCancelarClick(object sender, EventArgs e)
        {
            if (CancelarClick != null)
            {
                CancelarClick(sender, e);
            }
        }

        public void OnExportCsvClick(object sender, EventArgs e)
        {
            if (ExportCsvClick != null)
            {
                ExportCsvClick(sender, e);
            }
        }

        public void OnExportExcelClick(object sender, EventArgs e)
        {
            if (ExportExcelClick != null)
            {
                ExportExcelClick(sender, e);
            }
        }

        public void OnExportTxtClick(object sender, EventArgs e)
        {
            if (ExportTxtClick != null)
            {
                ExportTxtClick(sender, e);
            }
        }

        public void OnAcaoJQueryClick(object sender, EventArgs e)
        {
            if (AcaoJQueryClick != null)
            {
                AcaoJQueryClick(sender, e);
            }
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            OnPesquisaClick(sender, e);
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            OnNovoClick(sender, e);
        }

        protected void btnAcao_Click(object sender, EventArgs e)
        {
            OnAcaoClick(sender, e);
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            OnLimparClick(sender, e);
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            OnExcluirClick(sender, e);
        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            OnRecuperarClick(sender, e);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            OnCancelarClick(sender, e);
        }

        protected void btnExportCsv_Click(object sender, EventArgs e)
        {
            OnExportCsvClick(sender, e);
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcelClick(sender, e);
        }

        protected void btnExportTxt_Click(object sender, EventArgs e)
        {
            OnExportTxtClick(sender, e);
        }

        protected void btnAcaoJQuery_click(object sender, EventArgs e)
        {
            OnAcaoJQueryClick(sender, e);
        }

        #endregion
    }
}