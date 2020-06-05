using System;
using GesDoc.Models;
using GesDoc.Web.Controllers;
using GesDoc.Web.Services;
using System.Web.UI;

namespace GesDoc.Web
{
    public partial class rastreioDocumentos : Page
    {
        #region "Declarações , inicialização e encerramento"

        DocumentosController CtrlDocumentos = new DocumentosController();

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                pnlResultado.Visible = false;
            }
        }


        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            pnlResultado.Visible = false;

            try
            {
                Documentos documento = new Documentos();
                documento.HashCode = txtParPesquisahash.Text;

                var lista = CtrlDocumentos.GET(documento);

                if (lista.Count > 0)
                {
                    documento = lista[0];
                }
                else
                {
                    documento = null;
                }


                if (documento != null)
                {
                    CodDocumento.Text = documento.CodDocumento.ToString();
                    NomeDocumento.Text = documento.NomeDocumento;
                    HashCode.Text = documento.HashCode;
                    HashCodeAposAssinado.Text = documento.HashCodeAposAssinado;
                    usuarioGeracao.Text = documento.UsuarioGeracao;
                    dataGeracao.Text = TrataData(documento.DataGeracao.ToString());
                    assinado.Text = TrataBool(documento.Assinado);
                    usuarioAssinatura.Text = documento.UsuarioAssinatura;
                    dataAssinatura.Text = TrataData(documento.DataAssinatura.ToString());
                    liberado.Text = TrataBool(documento.Liberado);
                    usuarioLiberacao.Text = documento.UsuarioLiberacao;
                    dataLiberacao.Text = TrataData(documento.DataLiberacao.ToString());
                    clienteNotificado.Text = TrataBool(documento.ClienteNotificado);
                    emailNotificacao.Text = documento.EmailNotificacao.ToString();
                    dataNotificacao.Text = TrataData(documento.DataNotificacao.ToString());
                    pnlResultado.Visible = true;
                }
                else
                {
                    Mensagens.Alerta("Documento não localizado");
                    pnlResultado.Visible = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                Mensagens.Alerta($"Erro na consulta de documento:{ex.Message.ToString()}");
                return;
            }

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            // limpado dados de consulta e tela
            txtParPesquisahash.Text = string.Empty;
            pnlResultado.Visible = false;
            return;
        }

        #endregion

        #region "Metodos"

        public string TrataBool(bool parametro)
        {

            if (parametro)
            {
                return "Sim";
            }
            else
            {
                return "Não";
            }

        }

        public string TrataData(string dataRecebida)
        {

            if (dataRecebida == "01/01/0001 00:00:00")
            {
                return "";
            }
            else
            {
                return dataRecebida;
            }

        }

        #endregion
    }
}