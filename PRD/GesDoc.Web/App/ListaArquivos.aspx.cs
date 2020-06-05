using GesDoc.Web.Controllers;
using GesDoc.Web.Infraestructure;
using GesDoc.Web.Services;
using GesDoc.Models;
using System;
using System.Linq;
using System.Web.Services;

namespace GesDoc.Web.App
{
    public partial class ListaArquivos : System.Web.UI.Page
    {

        #region "Declaracoes e inicialização"

        static UsuarioLogado UsuarioLogado = new UsuarioLogado();
        static Permissoes permissoes;
        protected static int _codigoDocumento = 0;
        protected static int _codLogado = 0;
        protected static string _caminhoArquivo = "";

        #endregion

        #region "Metodos"

        protected static void Iniciadados()
        {
            UsuarioLogado = Ambiente.ValidaAcesso();
            _codLogado = UsuarioLogado.codUsuario;
            permissoes = Ambiente.GetPermissoes(MapeamentoPaths.GetPaginaAtual(), UsuarioLogado);
        }

        /// <summary>
        /// Recebe o codigo em hexa, tranforma para string e separa
        /// </summary>
        /// <param name="_parametro">Codigo em hexastring a ser tratado</param>
        protected static void TrataCodigos(string _parametro)
        {
            Iniciadados();
            string parametro = _parametro.Replace("#", "").FromHexString();
            Int32.TryParse(parametro, out _codigoDocumento);
            if (_codigoDocumento <= 0) {
                throw new Exception("O documento selecionado é inválido!");
            }
        }

        protected static void TrataParametro(string _parametro)
        {
            Iniciadados();
            string parametro = _parametro.Replace("#", "").FromHexString();
            _caminhoArquivo = parametro;
        }

        protected static string GETCaminhoDocumento(int codigoDocumento)
        {
            string retorno = string.Empty;

            DocumentosController CtrlDocumento = new DocumentosController();
            TipoDocumentoController ctrlTpDoc = new TipoDocumentoController();

            Documentos documento = new Documentos();
            TipoDocumento tpdoc = new TipoDocumento();
            Arquivos flAdm = new Arquivos();

            // buscando documento pelo codigo;
            documento = CtrlDocumento.GET(_codigoDocumento);

            // identificando o tipo do documento e buscando dados do tipo
            tpdoc = new TipoDocumento();
            tpdoc.CodTipoDocumento = documento.CodTipoDocumento;
            tpdoc = ctrlTpDoc.GET(tpdoc);

            if (tpdoc.TipoCliente && tpdoc.ClassificadoTipoServico)
            {
                flAdm.CodCliente = Convert.ToInt32(documento.CodCliente);
                flAdm.CodEquipamento = Convert.ToInt32(documento.CodEquipamento);
                flAdm.CodTipoServico = Convert.ToInt32(documento.CodTipoServico);
                retorno = flAdm.GETPasta(Ambiente.TipoPasta.DocumentosServicoEquipamentoCliente);
            }
            else if (tpdoc.TipoCliente && !tpdoc.ClassificadoTipoServico)
            {
                flAdm.CodCliente = Convert.ToInt32(documento.CodCliente);
                retorno = flAdm.GETPasta(Ambiente.TipoPasta.DocsCliente);
            }
            else
            {
                flAdm.SubPastaGeral = tpdoc.DescricaoTipoDocumento;
                retorno = flAdm.GETPasta(Ambiente.TipoPasta.Geral);
            }

            flAdm = null;
            documento = null;
            CtrlDocumento = null;

            return retorno;
        }

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Abre o documento informado no parametro
        /// </summary>
        /// <param name="_codigoDocumento">Codigo do documento</param>
        /// <returns></returns>
        [WebMethod]
        public static string GetCaminhoAbrir(string codigoBase)
        {
            string retorno = string.Empty;

            try
            {
                TrataCodigos(codigoBase);

                DocumentosController CtrlDocumento = new DocumentosController();
                Documentos documento = new Documentos();

                documento = CtrlDocumento.GET(_codigoDocumento);

                retorno = $"abre:{GETCaminhoDocumento(_codigoDocumento).Replace("~", MapeamentoPaths.CaminhoRaizHttp())}{documento.NomeDocumento}";

                documento = null;
                CtrlDocumento = null;
            }
            catch (Exception ex)
            {
                retorno = $"Ocorreu uma falha:{ex.Message.ToString()}";
            }

            return retorno;
        }

        /// <summary>
        /// Abre o documento informado no parametro
        /// </summary>
        /// <param name="_codigoDocumento">Codigo do documento</param>
        /// <returns></returns>
        [WebMethod]
        public static string GetCaminhoDownload(string codigoBase)
        {
            string retorno = string.Empty;

            try
            {
                TrataCodigos(codigoBase);
                DocumentosController CtrlDocumento = new DocumentosController();
                TipoDocumentoController ctrlTpDoc = new TipoDocumentoController();
                Documentos documento = new Documentos();
                TipoDocumento tpdoc = new TipoDocumento();

                // buscando documento pelo codigo;
                documento = CtrlDocumento.GET(_codigoDocumento);

                // identificando o tipo do documento e buscando dados do tipo
                tpdoc = new TipoDocumento();
                tpdoc.CodTipoDocumento = documento.CodTipoDocumento;
                tpdoc = ctrlTpDoc.GET(tpdoc);

                // se liberado ou sem classificacao
                if (documento.Liberado || !tpdoc.ClassificadoTipoServico)
                {
                    retorno = $"baixa:{GETCaminhoDocumento(_codigoDocumento).ConverteVirtualParaFisico()}{documento.NomeDocumento}";
                }
                else
                {
                    retorno = "Falha: Arquivo ainda não liberado para download!";
                }

                tpdoc = null;
                ctrlTpDoc = null;
                documento = null;
                CtrlDocumento = null;

            }
            catch (Exception ex)
            {
                retorno = $"Ocorreu uma falha:{ex.Message.ToString()}";
            }

            return retorno;
        }

        /// <summary>
        /// Assina o documento informado no parametro
        /// </summary>
        /// <param name="_codigoDocumento">Codigo do documento</param>
        /// <returns></returns>
        [WebMethod]
        public static string Assinar(string codigoBase)
        {
            string retorno = string.Empty;

            try
            {
                TrataCodigos(codigoBase);

                DocumentosController CtrlDocumento = new DocumentosController();
                ContatosController CtrlCtt = new ContatosController();
                UsuarioController ctrlusuario = new UsuarioController();
                Arquivos flAdm = new Arquivos();
                Documentos documento = new Documentos();
                Usuario usuario = new Usuario();

                usuario.codUsuario = _codLogado;
                usuario = ctrlusuario.Pesquisar(usuario);

                // buscando dados do documento
                documento = CtrlDocumento.GET(_codigoDocumento);
                documento.CodUsuarioAssinatura = usuario.codUsuario;

                // buscando caminho do arquivo
                string pdfPath = $"{GETCaminhoDocumento(_codigoDocumento).ConverteVirtualParaFisico()}{documento.NomeDocumento.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).Last()}";

                // complementando dados que faltam para o documento
                documento.HashCode = flAdm.RecuperaHash(pdfPath);
                documento.EmailNotificacao = CtrlCtt.BuscaEmailDocumento(documento.CodCliente);

                if (documento.Assinado) {
                    throw new Exception("Falha, não é possivel assinar um documento já assinado.");
                }

                // Assinando documento
                PDFs pdf = new PDFs();
                if (!pdf.AssinaDoc(pdfPath, documento.HashCode, $"{usuario.nomeUsuario} {usuario.sobreNome}")) {
                    throw new Exception(Mensagens.MsgErro);
                }
                pdf = null;

                // capturando hash do arquivo apos assinatura
                documento.HashCodeAposAssinado = flAdm.RecuperaHash(pdfPath);

                if (CtrlDocumento.Sign(documento))
                {
                    if (Ambiente.EnviaNotificacoes())
                    {
                        Emails.EnviarEmail(Ambiente.EmailNotificacoes(), "sistema@radimenstein.com.br", "Novo documento para liberar", "Um documento esta assinado para liberação.", "prog.marcos@gmail.com");
                    }

                    retorno = "Documento assinado com sucesso.";
                }
                else
                {
                    retorno = $"Falha na assinatura do documento:{Mensagens.MsgErro}";
                }

                flAdm = null;
                documento = null;
                usuario = null;
                CtrlCtt = null;
                ctrlusuario = null;
                CtrlDocumento = null;
            }
            catch (Exception ex)
            {
                retorno = $"Ocorreu uma falha:{ex.Message.ToString()}";
            }

            return retorno;
        }

        /// <summary>
        /// libera o documento informado no parametro
        /// </summary>
        /// <param name="_codigoDocumento">Codigo do documento</param>
        /// <returns></returns>
        [WebMethod]
        public static string Liberar(string codigoBase)
        {
            string retorno = string.Empty;

            try
            {
                TrataCodigos(codigoBase);

                DocumentosController CtrlDocumento = new DocumentosController();
                Documentos documento = new Documentos();

                documento = CtrlDocumento.GET(_codigoDocumento);
                documento.CodUsuarioLiberacao = _codLogado;

                if (documento.Liberado)
                {
                    throw new Exception("Falha, não é possivel liberar um documento já liberado.");
                }

                if (CtrlDocumento.Release(documento))
                {
                    Emails.EnviarEmail(documento.EmailNotificacao, "sistema@radimenstein.com.br", "Novo documento disponivel", $"Sr cliente, <br><br> O documento:{documento.NomeDocumento} foi liberado em:{DateTime.Now.ToShortDateString()}. <br><br> Atenciosamente, <br><br> EQUIPE RADDIMENSTEIN <BR> (11)3885-6329 - (11)3884-7538");
                    CtrlDocumento.RegistryNotification(documento);
                    retorno = "Documento liberado com sucesso.";
                }
                else
                {
                    retorno = $"Falha na liberação do documento ou notificação ao cliente:{Mensagens.MsgErro}";

                }

                documento = null;
                CtrlDocumento = null;
            }
            catch (Exception ex)
            {
                retorno = $"Ocorreu uma falha:{ex.Message.ToString()}";
            }

            return retorno;
        }

        /// <summary>
        /// Exclui o documento informado no parametro
        /// </summary>
        /// <param name="_codigoDocumento">Codigo do documento</param>
        /// <returns></returns>
        [WebMethod]
        public static string Excluir(string codigoBase)
        {
            string retorno = string.Empty;

            try
            {
                TrataCodigos(codigoBase);

                DocumentosController CtrlDocumento = new DocumentosController();
                Documentos documento = new Documentos();

                documento = CtrlDocumento.GET(_codigoDocumento);

                Arquivos flAdm = new Arquivos();

                // buscando caminho do arquivo
                string pdfPath = $"{GETCaminhoDocumento(_codigoDocumento).ConverteVirtualParaFisico()}{documento.NomeDocumento}";

                // realizando a exclusao do arquivo
                if (flAdm.ExcluirArquivo(pdfPath))
                {
                    // apagando do banco de dados o documento
                    CtrlDocumento.Delete(documento);

                    // msg de sucesso
                    retorno = "excluido:Documento excluido com sucesso.";
                }
                else
                {
                    // msg de erro
                    retorno = "Falha ao excluir documento.";
                }

                flAdm = null;
                documento = null;
                CtrlDocumento = null;
            }
            catch (Exception ex)
            {
                retorno = $"Ocorreu uma falha:{ex.Message.ToString()}";
            }

            return retorno;
        }

        #endregion
    }
}