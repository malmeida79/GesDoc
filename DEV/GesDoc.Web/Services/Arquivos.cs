using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using GesDoc.Web.Infraestructure;
using System.Text;
using GesDoc.Models;
using GesDoc.Web.Controllers;

namespace GesDoc.Web.Services
{
    public class Arquivos
    {
        /// <summary>
        /// Pasta raiz dos arquivos
        /// </summary>
        protected string _pastaArquivos = $@"~{MapeamentoPaths.CaminhoRaizVirtualApp()}Arquivos";

        /// <summary>
        /// Codigo do cliente
        /// </summary>
        /// 

        public int CodCliente { get; set; }

        /// <summary>
        /// Codigo do Equipamento
        /// </summary>
        public int CodEquipamento { get; set; }

        /// <summary>
        /// Codigo do Equipamento
        /// </summary>
        public int CodTipoServico { get; set; }

        /// <summary>
        /// Codigo do Equipamento
        /// </summary>
        public string SubPastaGeral { get; set; }

        /// <summary>
        /// Recupera caminho da pasta tipo solicitada
        /// </summary>
        /// <param name="tipoPasta">Pasta tipo solicitada (ENUM) de tipo de pastas </param>
        /// <returns>Arquivos da pasta solicitada</returns>
        public string GETCaminhoPasta(Ambiente.TipoPasta tipoPasta)
        {
            switch (tipoPasta)
            {
                case Ambiente.TipoPasta.Geral:
                    if (!string.IsNullOrEmpty(SubPastaGeral))
                    {
                        return $@"{_pastaArquivos}/Geral/{SubPastaGeral}/";
                    }
                    else
                    {
                        return $@"{_pastaArquivos}/Geral/";
                    }
                case Ambiente.TipoPasta.DocsCliente:
                    if (CodCliente <= 0)
                    {
                        throw new Exception("Erro recuperando pasta documentos do cliente!");
                    }
                    return $@"{_pastaArquivos}/CLI{CodCliente.ToString("000000")}/Docs/";
                case Ambiente.TipoPasta.Cliente:
                    if (CodCliente <= 0)
                    {
                        throw new Exception("Erro recuperando pasta do cliente!");
                    }
                    return $@"{_pastaArquivos}/CLI{CodCliente.ToString("000000")}/";
                case Ambiente.TipoPasta.DocumentosClassificadosCliente:
                    if (CodCliente <= 0 || CodEquipamento <= 0)
                    {
                        throw new Exception("Erro recuperando pasta documentos do cliente!");
                    }
                    return $@"{_pastaArquivos}/CLI{CodCliente.ToString("000000")}/Documentos/";
                case Ambiente.TipoPasta.DocumentosEquipamentoCliente:
                    if (CodCliente <= 0 || CodEquipamento <= 0)
                    {
                        throw new Exception("Erro recuperando pasta equipamentos do cliente!");
                    }
                    return $@"{_pastaArquivos}/CLI{CodCliente.ToString("000000")}/Documentos/EQUP{CodEquipamento.ToString("000000")}/";
                case Ambiente.TipoPasta.DocumentosServicoEquipamentoCliente:
                    if (CodCliente <= 0 || CodEquipamento <= 0 || CodTipoServico <= 0)
                    {
                        throw new Exception("Erro recuperando pasta de serviços para o equipamentos do cliente!");
                    }
                    return $@"{_pastaArquivos}/CLI{CodCliente.ToString("000000")}/Documentos/EQUP{CodEquipamento.ToString("000000")}/SVC{ CodTipoServico.ToString("000")}/";
                default:
                    throw new Exception("Erro recuperando pasta!");
            }
        }

        /// <summary>
        /// Pega a pasta solicitada no tipo pasta, caso nao exista cria a mesma.
        /// </summary>
        /// <param name="tipoPasta">Pasta tipo solicitada (ENUM) de tipo de pastas </param>
        /// <returns>Arquivos da pasta solicitada</returns>
        public string GETPasta(Ambiente.TipoPasta tipoPasta)
        {
            string pasta = GETCaminhoPasta(tipoPasta);
            CriarPasta(pasta);
            return pasta;
        }

        /// <summary>
        /// Lista todos arquivos da pasta tipo solicitada
        /// </summary>
        /// <param name="tipoPasta">Pasta tipo solicitada (ENUM) de tipo de pastas </param>
        /// <returns>Arquivos da pasta solicitada</returns>
        public List<string> GetArquivosPasta(Ambiente.TipoPasta tipoPasta)
        {
            string caminho = GETCaminhoPasta(tipoPasta);
            CriarPasta(caminho);

            caminho = caminho.ConverteVirtualParaFisico();

            List<string> retorno = new List<string>();

            retorno = Directory.GetFiles(caminho).ToList<string>();

            for (int i = 0; i < retorno.Count(); i++)
            {
                retorno[i] = retorno[i].Replace(caminho, "");
            }

            return retorno;
        }

        /// <summary>
        /// Cria estrutura de pastas informada no path
        /// </summary>
        /// <param name="caminhoPasta"></param>
        protected void CriarPasta(string caminhoPasta)
        {
            // Limpando pastas vazias sem uso.
            ApagaDiretoriosVazios(_pastaArquivos.ConverteVirtualParaFisico());

            // buscando o caminho da pasta
            caminhoPasta = caminhoPasta.ConverteVirtualParaFisico();

            // caso a pasta nao exista criar
            if (!Directory.Exists(caminhoPasta))
            {
                Directory.CreateDirectory(caminhoPasta);
            }
        }

        /// <summary>
        /// Excluir arquivo selecionado
        /// </summary>
        /// <param name="path">Caminho do arquivo a ser excluido</param>
        /// <returns>True para sucesso / False para falha</returns>
        public bool ExcluirArquivo(string path)
        {
            bool retorno = false;

            try
            {
                File.Delete(path.ConverteVirtualParaFisico());
                retorno = true;
            }
            catch (Exception)
            {
                retorno = false;
            }

            return retorno;
        }

        /// <summary>
        /// Compara Binariamente hash de dois arquivos
        /// </summary>
        /// <param name="caminhoArq1">Arquivo 1</param>
        /// <param name="caminhoArq2">Arquivo 2</param>
        /// <returns>True ou false</returns>
        public bool CompararHashArquivos(string caminhoArq1, string caminhoArq2)
        {
            string strConteudo1 = null;
            string strConteudo2 = null;
            bool retorno = false;

            //verifica se o arquivo1 existe
            if (!File.Exists(caminhoArq1))
            {
                Mensagens.MsgErro = caminhoArq1 + " NÃO existe...";
                return retorno;
            }

            //verifica se o arquivo2 existe
            if (!File.Exists(caminhoArq2))
            {
                Mensagens.MsgErro = caminhoArq2 + " NÃO existe...";
                return retorno;
            }

            try
            {
                HashAlgorithm ha = HashAlgorithm.Create();
                FileStream f1 = new FileStream(caminhoArq1, FileMode.Open);
                FileStream f2 = new FileStream(caminhoArq2, FileMode.Open);

                /* Calculate Hash */
                byte[] hash1 = ha.ComputeHash(f1);
                byte[] hash2 = ha.ComputeHash(f2);

                f1.Close();
                f2.Close();

                strConteudo1 = BitConverter.ToString(hash1);
                strConteudo2 = BitConverter.ToString(hash2);

                /* Compare the hash and Show Message box */
                if (strConteudo1 == strConteudo2)
                {
                    retorno = true;
                }
                else
                {
                    retorno = false;
                }
            }
            catch (Exception ex)
            {
                Mensagens.MsgErro = $"ocorreu erro:{ex.Message.ToString()}";
                return retorno;
            }

            return retorno;
        }

        /// <summary>
        /// VAlidação de hash code de arquivo
        /// </summary>
        /// <param name="caminhoArq1">Arquivo que se deseja validar</param>
        /// <param name="hash">Hash code cadastrado no banco de dados para o arquivo</param>
        /// <returns></returns>
        public bool ValidaHash(string caminhoArq1, string hash)
        {
            bool retorno = false;
            string strConteudo1 = null;

            // Alterar por metodo que busque no banco de dados
            string hashBase = "5D-FA-C3-9F-71-AD-4D-35-A1-53-BA-4F-C1-2D-94-3A-0E-17-8E-6A";

            //verifica se o arquivo1 existe
            if (!File.Exists(caminhoArq1))
            {
                Mensagens.MsgErro = caminhoArq1 + " NÃO existe...";
                return retorno;
            }

            try
            {
                HashAlgorithm ha = HashAlgorithm.Create();
                FileStream f1 = new FileStream(caminhoArq1, FileMode.Open);

                /* Calculate Hash */
                byte[] hash1 = ha.ComputeHash(f1);
                f1.Close();

                strConteudo1 = BitConverter.ToString(hash1);

                /* Compare the hash and Show Message box */
                if (strConteudo1 == hashBase)
                {
                    retorno = true;
                }
                else
                {
                    retorno = false;
                }
            }
            catch (Exception ex)
            {
                Mensagens.MsgErro = $"ocorreu erro:{ex.Message.ToString()}";
                return retorno;
            }

            return retorno;
        }

        /// <summary>
        /// Recupera o HashCode do arquivo
        /// </summary>
        /// <param name="caminhoArq1">Arquivo para o qual se deseja o hash</param>
        /// <returns>String HashCode</returns>
        public string RecuperaHash(string caminhoArq1)
        {
            string retorno = string.Empty;
            string strConteudo1 = null;

            //verifica se o arquivo1 existe
            if (!File.Exists(caminhoArq1))
            {
                Mensagens.MsgErro = caminhoArq1 + " NÃO existe...";
                return retorno;
            }

            try
            {
                HashAlgorithm ha = HashAlgorithm.Create();
                FileStream f1 = new FileStream(caminhoArq1, FileMode.Open);

                /* Calculate Hash */
                byte[] hash1 = ha.ComputeHash(f1);
                f1.Close();

                strConteudo1 = BitConverter.ToString(hash1);

                /* Compare the hash and Show Message box */
                retorno = strConteudo1;

            }
            catch (Exception ex)
            {
                Mensagens.MsgErro = $"ocorreu erro:{ex.Message.ToString()}";
                return retorno;
            }

            return retorno;
        }

        /// <summary>
        /// A partir do nome da pasta para documentos e serviços busca o nome 
        /// do equipamento (ex: EQUP005148) -> 5148 é o código do 
        /// equipamento
        /// </summary>
        /// <param name="nomePasta">Pasta a tentar identificar</param>
        /// <returns>O nome do equipamento para pasta </returns>
        protected string ConvertNomePastaNomeEquipamento(string nomePasta)
        {
            string retorno;
            EquipamentosController ctrlEquipamento;
            Equipamento equipamento;

            ctrlEquipamento = new EquipamentosController();
            Int32 numero = 0;
            Int32.TryParse(nomePasta.Replace("EQUP", ""), out numero);
            equipamento = ctrlEquipamento.PesquisarPorCodigoEquipamento(numero);

            retorno = equipamento.DescricaoEquipamento;

            ctrlEquipamento = null;
            equipamento = null;

            return retorno;
        }

        /// <summary>
        /// A partir do nome da pasta para documentos e serviços busca o nome 
        /// do serviço (ex: SVC005148) -> 5148 é o código do serviço
        /// </summary>
        /// <param name="nomePasta">Pasta a tentar identificar</param>
        /// <returns>O nome do serviçp para pasta </returns>
        protected string ConvertNomePasteNomeServico(string nomePasta)
        {
            string retorno;
            TipoServicoController ctrlTipoServico;
            TipoServico tipoServico;

            ctrlTipoServico = new TipoServicoController();

            Int32 numero = 0;
            Int32.TryParse(nomePasta.Replace("SVC", ""), out numero);
            tipoServico = ctrlTipoServico.PesquisarPorCodigoServico(numero);


            retorno = tipoServico.DescricaoTipoServico;

            ctrlTipoServico = null;
            tipoServico = null;

            return retorno;
        }

        /// <summary>
        /// Controi uma lista não ordenada para exibição dos arquivos
        /// </summary>
        /// <param name="caminho">Caminho para listagem</param>
        /// <param name="codigoLogado">Codigo do usuario logado para checar permissoes</param>
        /// <returns>lista não ordenada <ul> ... </ul></returns>
        public string ListaParaExibicao(string caminho)
        {
            StringBuilder retorno = new StringBuilder();
            string raiz = caminho.ConverteVirtualParaFisico().Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).Last();

            if (raiz.ToUpper().Contains("SVC"))
            {
                retorno.Append($@"<li class=""menuPasta""><a href=""#"">{ConvertNomePasteNomeServico(raiz)}</a>");
            }

            else if (raiz.ToUpper().Contains("EQUP"))
            {
                retorno.Append($@"<li class=""menuPasta""><a href=""#"">{ConvertNomePastaNomeEquipamento(raiz)}</a>");
            }
            else
            {
                retorno.Append($@"<li class=""menuPasta""><a href=""#"">{raiz}</a>");
            }


            DirectoryInfo info = new DirectoryInfo(caminho.ConverteVirtualParaFisico());

            if (info.GetDirectories().Count() > 0)
            {
                retorno.Append(MontaListaParaExibicao(caminho));
            }
            else
            {
                // recupera a lista de arquivos desse diretorio
                retorno.Append(MontaListaArquivosExibicao(info));
            }

            retorno.Append($@"</li>");
            return retorno.ToString();
        }

        /// <summary>
        /// Metodo que constroi a lista para chamada
        /// </summary>
        /// <param name="caminho">Caminho para listagem</param>
        /// <param name="codigoLogado">Codigo do usuario logado para checar permissoes</param>
        /// <returns>lista não ordenada <ul> ... </ul></returns>
        protected string MontaListaParaExibicao(string caminho)
        {
            StringBuilder retorno = new StringBuilder();
            DirectoryInfo info = new DirectoryInfo(caminho.ConverteVirtualParaFisico());

            foreach (var _directory in info.GetDirectories())
            {
                retorno.Append($@"<Ul>");

                // trocando o nome da pasta EQUP00010 para o nome do equipamento
                if (_directory.Name.ToUpper().Contains("EQUP"))
                {
                    retorno.Append($@"<li class=""menuPasta""><a href=""#"">{ConvertNomePastaNomeEquipamento(_directory.Name)}</a>");
                }

                // trocando o nome da pasta SVC00010 para o nome do serviço
                else if (_directory.Name.ToUpper().Contains("SVC"))
                {
                    retorno.Append($@"<li class=""menuPasta""><a href=""#"">{ConvertNomePasteNomeServico(_directory.Name)}</a>");
                }
                else
                {
                    retorno.Append($@"<li class=""menuPasta""><a href=""#"">{_directory.Name}</a>");
                }

                // recupera a lista de arquivos desse diretorio
                retorno.Append(MontaListaArquivosExibicao(_directory));

                // Caso existam mais diretorios na listagem vai aprofundando e capturando a listagem
                retorno.Append(MontaListaParaExibicao(_directory.FullName));

                // encerra grupo pai
                retorno.Append($@"</li></Ul>");
            }


            info = null;
            return retorno.ToString();
        }

        /// <summary>
        /// Captura arquivos do diretorio solicitado e devolve como item da lista <li>
        /// </summary>
        /// <param name="_directory">Diretorio a ser pesquisado</param>
        /// <param name="codigoLogado">Codigo do usuario logado</param>
        /// <returns></returns>
        protected string MontaListaArquivosExibicao(DirectoryInfo _directory)
        {
            //pega a lista de arquivos
            FileInfo[] NextFile = _directory.GetFiles("*.*");

            StringBuilder retorno = new StringBuilder();
            DocumentosController ctrlDocumentos = new DocumentosController();

            int codDocumento = 0;
            string tratamentoHexa;

            // abre lista diretorio grupo pai
            retorno.Append($@"<Ul>");

            // looping para filhos
            foreach (var _file in NextFile)
            {
                Documentos documento = new Documentos();

                // Somente documentos classificados por serviço tem essas caracteristicas
                // buscamos entao pelo serviço  e cliente o codigo do arquivo.
                if (CodCliente > 0 && CodEquipamento > 0)
                {
                    documento.CodCliente = CodCliente;
                    documento.CodEquipamento = CodEquipamento;

                    // recuperando o codigo do tipo de servico
                    int codTpServico = 0;
                    int.TryParse(_directory.Name.Replace("SVC", ""), out codTpServico);

                    if (codTpServico > 0)
                    {
                        documento.CodTipoServico = codTpServico;
                    }

                    documento.NomeDocumento = _file.Name;
                    var lista = ctrlDocumentos.GET(documento);

                    if (lista != null && lista.Count > 0)
                    {
                        codDocumento = lista[0].CodDocumento.RecuperarValor<Int32>();
                    }
                    else
                    {
                        codDocumento = 0;
                    }
                }
                // somente documentos especificos do cliente tem essas caracteristicas
                // buscamos pelo codido do cliente qual sua pasta especifica
                else if (CodCliente > 0 && CodEquipamento <= 0)
                {
                    documento.CodCliente = CodCliente;
                    documento.NomeDocumento = _file.Name;

                    var lista = ctrlDocumentos.GET(documento);

                    if (lista.Count > 0)
                    {
                        codDocumento = lista[0].CodDocumento.RecuperarValor<Int32>();
                    }
                    else
                    {
                        codDocumento = 0;
                    }
                }
                // Somente arquivos gerais tem essa caracteristica (nao cliente e nem serviço)
                // buscamos entao pelo nome do diretorio (que é a descricao do tipo de arquivo)
                else
                {
                    var quebraPath = _file.FullName.ToString().Split('\\');
                    if (quebraPath.Count() > 2)
                    {
                        SubPastaGeral = quebraPath[quebraPath.Length - 2];
                    }

                    TipoDocumentoController ctrlTpDoc = new TipoDocumentoController();
                    TipoDocumento tpdoc = new TipoDocumento();

                    tpdoc.DescricaoTipoDocumento = SubPastaGeral;
                    tpdoc = ctrlTpDoc.GET(tpdoc);

                    documento.CodTipoDocumento = tpdoc.CodTipoDocumento;
                    documento.NomeDocumento = _file.Name;


                    var lista = ctrlDocumentos.GET(documento);

                    if (lista == null)
                    {
                        // se lista nula então temos um arquivo na pasta nao cadastrado
                        // devemos entao pular esse arquivo e ir ao proximo.
                        continue;
                    }

                    if (lista.Count > 0)
                    {
                        codDocumento = lista[0].CodDocumento.RecuperarValor<Int32>();
                    }
                    else
                    {
                        codDocumento = 0;
                    }

                    tpdoc = null;
                    ctrlTpDoc = null;
                }

                documento = null;

                if (codDocumento > 0)
                {
                    // valor passado sera codigo documento em string hexa por precaucao
                    tratamentoHexa = $"{codDocumento.ToString("00000000")}".ToHexString();

                    // retornando o item da lista para ser acessado pelo usuário.
                    retorno.Append($@"<li class=""menuItem""><a href=""#{tratamentoHexa}"">{_file.Name}</a></li>");
                }
            }
            // encerra grupo filho
            retorno.Append($@"</Ul>");

            ctrlDocumentos = null;

            return retorno.ToString();
        }

        /// <summary>
        /// Apaga pastas vazias no caminho especificado
        /// </summary>
        /// <param name="caminhoPasta">Raiz a ser limpa</param>
        public void ApagaDiretoriosVazios(String caminhoPasta)
        {
            DirectoryInfo dir = new DirectoryInfo(caminhoPasta);

            DirectoryInfo[] subDir = dir.GetDirectories();

            if (subDir.Length > 0)
            {
                foreach (DirectoryInfo auxSubDir in subDir)
                    ApagaDiretoriosVazios(auxSubDir.FullName);
            }

            FileInfo[] rbFiles = dir.GetFiles();
            DirectoryInfo[] rbDirs = dir.GetDirectories();
            if (rbFiles.Length == 0 && rbDirs.Length == 0)
            {
                dir.Delete();
            }
        }
    }
}