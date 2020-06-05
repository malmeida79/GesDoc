using System;
using System.Web;
using GesDoc.Models;
using System.Linq;
using GesDoc.Web.Services;

namespace GesDoc.Web.Infraestructure
{
    public static class Ambiente
    {
        /// <summary>
        /// Confirguracao de botoes da barra de ferramentas
        /// </summary>
        public enum BotoesBarra { Pesquisa, Novo, Acao, Limpar, Recuperar, Excluir, Cancelar, ExportaCsv, ExportaExcel, ExportaTxt, ConfirmJQuery }

        /// <summary>
        /// Configuracao de acesso a pastas gerenciadas da aplicacao
        /// </summary>
        public enum TipoPasta { Geral, DocsCliente, Cliente, DocumentosClassificadosCliente, DocumentosEquipamentoCliente, DocumentosServicoEquipamentoCliente }


        public static string CNStringDes = Properties.Settings.Default.DBCnstrDes;
        public static string CNStringPrd = Properties.Settings.Default.DBCnstrPrd;
        public static string ModoAmbiente = Properties.Settings.Default.Ambiente;
        public static bool HistoricoAtivo = Properties.Settings.Default.HistAtivo;
        public static bool ErroAtivo = Properties.Settings.Default.ErroAtivo;
        public static bool LogAtivo = Properties.Settings.Default.LogAtivo;

        /// <summary>
        /// Identifica se estamos executando em modo producao
        /// </summary>
        /// <returns>TRUE/FALSE</returns>
        public static bool ISProducao()
        {
            if (Properties.Settings.Default.Ambiente.ToLower() != "desenvolvimento")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //to do: rever esse cara para que ele leia de alguma fonte de configuração
        public static bool EnviaNotificacoes()
        {
            return true;
        }

        //to do: rever esse cara para que ele leia de alguma fonte de configuração
        public static string EmailNotificacoes()
        {
            return "renato.dimenstein@gmail.com";
        }

        //to do: rever esse cara para que ele leia de alguma fonte de configuração
        public static bool EnviaNotificacoesCliente()
        {
            return true;
        }

        //to do: rever esse cara para que ele leia de alguma fonte de configuração
        public static bool InserePaginaRosto()
        {
            return false;
        }

        //to do: rever esse cara para que ele leia de alguma fonte de configuração
        public static string GetLinha1Assinatura(string nomeAssinante)
        {
            return $"ESTE DOCUMENTO FOI ASSINADO DIGITALMENTE POR {nomeAssinante} NA DATA DE { DateTime.Now}.";
        }

        //to do: rever esse cara para que ele leia de alguma fonte de configuração
        public static string GetLinha2Assinatura(string hashCode)
        {
            return $"A AUTENTICIDADE DESTE DOCUMENTO PODE SER VERIFICADO JUNTO À RAD DIMENSTEIN & ASSOCIADOS PELO CÓDIGO: {hashCode}.";
        }

        //to do: rever esse cara para que ele leia de alguma fonte de configuração
        public static string GetSenhaSecurityMasterPdf()
        {
            return "54321";
        }

        //to do: rever esse cara para que ele leia de alguma fonte de configuração
        public static string GetSenhaSecurityPdf()
        {
            return "123456";
        }

        //to do: rever esse cara para que ele leia de alguma fonte de configuração
        public static bool InsereSenhaDocPdf()
        {
            // desativado a pedido do Renato e do Breno em 12-01-2018
            return false;
        }

        public static string CaminhoLogo()
        {
            return MapeamentoPaths.GetCaminhoFisicoRaiz() + @"/Imagens/logo.jpg";
        }

        /// <summary>
        /// valida acesso do usuario, caso nao ativo
        /// envia o mesmo para pagina de erro de
        /// acesso
        /// </summary>
        /// <returns></returns>
        public static UsuarioLogado ValidaAcesso()
        {
            // Se existe usuario logado
            if ((UsuarioLogado)HttpContext.Current.Session["usuarioLogado"] != null)
            {
                return (UsuarioLogado)HttpContext.Current.Session["usuarioLogado"];
            }
            else
            {
                HttpContext.Current.Server.Transfer("../erroAcesso.aspx");
                return null;
            }
        }

        /// <summary>
        /// Verifica para pagina em questão as permissões
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="codUsuario"></param>
        /// <returns></returns>
        public static Permissoes GetPermissoes(string pagina, UsuarioLogado usuario)
        {
            Permissoes retorno = new Permissoes();

            var acesso = usuario.GETFuncionalidesAcessos.Where(x => x.UrlAcesso.ToLower() == pagina.ToLower()).ToList();

            if (acesso != null && acesso.Count > 0)
            {
                retorno.Leitura = acesso[0].Leitura;
                retorno.Gravacao = acesso[0].Gravacao;
                retorno.Excluir = acesso[0].Excluir;
            }
            else
            {
                retorno.Leitura = false;
                retorno.Gravacao = false;
                retorno.Excluir = false;
            }

            retorno.AssinaDocumento = usuario.AssinaDocumento;
            retorno.LiberaDocumento = usuario.LiberaDocumento;
            retorno.EhCliente = usuario.TipoCliente;
            return retorno;
        }

        /// <summary>
        /// Informa a data somente se existe assiantura ou liberação
        /// </summary>
        /// <param name="campoReferencia">Validador para apresentar ou nao a data</param>
        /// <param name="data">data a ser informada</param>
        /// <returns></returns>
        public static string TrataData(bool campoReferencia, DateTime? data)
        {
            if (data == null)
            {
                return null;
            }

            string retorno = string.Empty;

            if (campoReferencia)
            {
                if (data.ToString().Length >= 10)
                {
                    retorno = data.ToString().Substring(0, 10);
                }
            }

            return retorno;
        }

        /// <summary>
        /// Para o grid troca true/false por sim/nao
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static string TrataBool(bool valor)
        {
            string retorno = "Não";
            if (valor)
            {
                retorno = "Sim";
            }
            return retorno;
        }
    }
}