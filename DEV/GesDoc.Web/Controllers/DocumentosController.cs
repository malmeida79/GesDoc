using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GesDoc.Models;
using GesDoc.Web.Services;

namespace GesDoc.Web.Controllers
{
    public class DocumentosController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Cadastro de documentos");

        /// <summary>
        /// Listar documentoss
        /// </summary>
        /// <param name="documentos">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<Documentos> GETAll()
        {
            Documentos documento;
            List<Documentos> retorno = null;
            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listadocumentos", par);

            if (dr.HasRows)
            {

                retorno = new List<Documentos>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    documento = new Documentos();

                    documento.CodDocumento = dr["CodDocumento"].DefaultDbNull<Int32>(0);
                    documento.CodCliente = dr["codCliente"].DefaultDbNull<Int32>(0);
                    documento.NomeDocumento = dr["NomeDocumento"].ToString();
                    documento.CodUsuarioGeracao = dr["codUsuarioGeracao"].DefaultDbNull<Int32>(0);
                    documento.CodTipoServico = dr["codTipoServico"].DefaultDbNull<Int32>(0);
                    documento.CodTipoDocumento = dr["codTipoDocumento"].DefaultDbNull<Int32>(0);
                    documento.DataGeracao = dr["dataGeracao"].DefaultDbNull<DateTime>(0);
                    documento.Assinado = dr["assinado"].DefaultDbNull<bool>(false);
                    documento.CodUsuarioAssinatura = dr["assinado"].DefaultDbNull<Int32>(0);
                    documento.DataAssinatura = dr["assinado"].DefaultDbNull<DateTime?>(null);
                    documento.Liberado = dr["assinado"].DefaultDbNull<bool>(false);
                    documento.CodUsuarioLiberacao = dr["assinado"].DefaultDbNull<Int32>(0);
                    documento.DataLiberacao = dr["assinado"].DefaultDbNull<DateTime?>(null);
                    documento.ClienteNotificado = dr["assinado"].DefaultDbNull<bool>(false);
                    documento.EmailNotificacao = dr["assinado"].ToString();
                    documento.DataNotificacao = dr["assinado"].DefaultDbNull<DateTime?>(null);

                    retorno.Add(documento);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Busca um documento para casos onde precisamos de apenas um 
        /// docuemnto e temos apenas o codigo como parametro.
        /// </summary>
        /// <param name="codDocumento"></param>
        /// <returns></returns>
        public Documentos GET(int codDocumento)
        {
            Documentos documento = new Documentos();
            documento.CodDocumento = codDocumento;
            var lista = GET(documento);
            if (lista.Count > 0)
            {
                documento = lista[0];
            }
            else
            {
                documento = null;
            }
            return documento;
        }

        /// <summary>
        /// Pesquisa documentos dos mais diversos tipos
        /// </summary>
        /// <param name="documento">Informações do documento a filtrar</param>
        /// <param name="documento">Informações do documento a filtrar</param>
        /// <returns>Lista de documentos ou documento pesquisado</returns>
        public List<Documentos> GET(Documentos documento, bool consideraStatus = false)
        {
            List<Documentos> retorno = null;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            #region "trataParametros"

            if (documento.CodDocumento > 0)
            {
                par.Add(new SqlParameter("@CODDOCUMENTO", documento.CodDocumento));
            }

            if (documento.CodCliente > 0)
            {
                par.Add(new SqlParameter("@CODCLIENTE", documento.CodCliente));
            }

            if (documento.CodEquipamento > 0)
            {
                par.Add(new SqlParameter("@CODEQUIPAMENTO", documento.CodEquipamento));
            }

            if (documento.CodTipoServico > 0)
            {
                par.Add(new SqlParameter("@CODTIPOSERVICO", documento.CodTipoServico));
            }

            if (documento.CodTipoDocumento > 0)
            {
                par.Add(new SqlParameter("@CodTipoDocumento", documento.CodTipoDocumento));
            }

            if (!string.IsNullOrEmpty(documento.HashCode))
            {
                par.Add(new SqlParameter("@HASHCODE", documento.HashCode));
            }

            if (!string.IsNullOrEmpty(documento.NomeDocumento))
            {
                par.Add(new SqlParameter("@NOMEDOCUMENTO", documento.NomeDocumento));
            }

            // usado somente quando queremos levar em consideração filtrar por assinados e ou liberados
            // ex: quero listar todos assinados, todos não assinados, etc ...
            if (consideraStatus)
            {
                par.Add(new SqlParameter("@ASSINADO", documento.Assinado));

                par.Add(new SqlParameter("@LIBERADO", documento.Liberado));
            }


            #endregion

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("SPC_BUSCADOCUMENTO", par);

            if (dr.HasRows)
            {

                retorno = new List<Documentos>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    documento = new Documentos();

                    documento.CodDocumento = dr["CodDocumento"].DefaultDbNull<Int32>(0);
                    documento.CodCliente = dr["codCliente"].DefaultDbNull<Int32>(0);
                    documento.CodEquipamento = dr["codEquipamento"].DefaultDbNull<Int32>(0);
                    documento.NomeDocumento = dr["NomeDocumento"].ToString();
                    documento.NomeCliente = dr["NomeCliente"].ToString();
                    documento.HashCode = dr["HashCode"].ToString();
                    documento.HashCodeAposAssinado = dr["HashCodeAposAssinado"].ToString();
                    documento.CodUsuarioGeracao = dr["codUsuarioGeracao"].DefaultDbNull<Int32>(0);
                    documento.UsuarioGeracao = dr["UsuarioGeracao"].ToString();
                    documento.CodTipoServico = dr["codTipoServico"].DefaultDbNull<Int32>(0);
                    documento.CodTipoDocumento = dr["codTipoDocumento"].DefaultDbNull<Int32>(0);
                    documento.DataGeracao = dr["dataGeracao"].DefaultDbNull<DateTime>(null);
                    documento.Assinado = dr["assinado"].DefaultDbNull<bool>(false);
                    documento.CodUsuarioAssinatura = dr["CodUsuarioAssinatura"].DefaultDbNull<Int32>(0);
                    documento.UsuarioAssinatura = dr["UsuarioAssinatura"].ToString();
                    documento.DataAssinatura = dr["DataAssinatura"].DefaultDbNull<DateTime?>(null);
                    documento.Liberado = dr["Liberado"].DefaultDbNull<bool>(false);
                    documento.CodUsuarioLiberacao = dr["CodUsuarioLiberacao"].DefaultDbNull<Int32>(0);
                    documento.UsuarioLiberacao = dr["UsuarioLiberacao"].ToString();
                    documento.DataLiberacao = dr["DataLiberacao"].DefaultDbNull<DateTime?>(null);
                    documento.ClienteNotificado = dr["ClienteNotificado"].DefaultDbNull<bool>(false);
                    documento.EmailNotificacao = dr["emailNotificacao"].ToString();
                    documento.DataNotificacao = dr["DataNotificacao"].DefaultDbNull<DateTime?>(null);

                    retorno.Add(documento);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Quantidade de documentos para o equipamento
        /// </summary>
        /// <param name="codCliente">codigo cliente</param>
        /// <param name="codEquipamento">codigo equipamento</param>
        /// <returns></returns>
        public int ContaDocumentosEquipamentoCliente(int codCliente, int codEquipamento)
        {
            int retorno = 0;

            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (codCliente > 0)
            {
                par.Add(new SqlParameter("@codCliente", codCliente));
            }

            if (codEquipamento > 0)
            {
                par.Add(new SqlParameter("@codEquipamento", codEquipamento));
            }

            dr = Dbase.GeraReaderProcedure("spc_ContadocumentoEquipamentoCliente", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = dr["contagem"].DefaultDbNull<Int32>(0);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastrar documentos
        /// </summary>
        /// <param name="documentos">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Put(Documentos documentos)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@codCliente", documentos.CodCliente));
            par.Add(new SqlParameter("@codEquipamento", documentos.CodEquipamento));
            par.Add(new SqlParameter("@NomeDocumento", documentos.NomeDocumento));
            par.Add(new SqlParameter("@codusuarioGeracao", documentos.CodUsuarioGeracao));
            par.Add(new SqlParameter("@codTipoDocumento", documentos.CodTipoDocumento));
            par.Add(new SqlParameter("@codTipoServico", documentos.CodTipoServico));
            retorno = Dbase.ExecutaProcedure("spc_cadastraGeracaodocumento", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Assinar documentos
        /// </summary>
        /// <param name="documentos">Entidade a ser Assinada</param>
        /// <returns>true para sucesso</returns>
        public bool Sign(Documentos documentos)
        {
            if (documentos.Assinado)
            {
                throw new Exception("Não é possivel assinar um documento já assinado!");
            }

            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@CodDocumento", documentos.CodDocumento));
            par.Add(new SqlParameter("@codCliente", documentos.CodCliente));
            par.Add(new SqlParameter("@NomeDocumento", documentos.NomeDocumento));
            par.Add(new SqlParameter("@codusuario", documentos.CodUsuarioAssinatura));
            par.Add(new SqlParameter("@hashcode", documentos.HashCode));
            par.Add(new SqlParameter("@HashCodeAposAssinado", documentos.HashCodeAposAssinado));
            retorno = Dbase.ExecutaProcedure("spc_assinadocumento", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Liberar documentos
        /// </summary>
        /// <param name="documentos">Entidade a ser Liberada</param>
        /// <returns>true para sucesso</returns>
        public bool Release(Documentos documentos)
        {
            if (!documentos.Assinado)
            {
                throw new Exception("Não é possivel liberar um documento não assinado!");
            }

            if (documentos.Liberado)
            {
                throw new Exception("Não é possivel liberar um documento já liberado!");
            }

            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@CodDocumento", documentos.CodDocumento));
            par.Add(new SqlParameter("@codCliente", documentos.CodCliente));
            par.Add(new SqlParameter("@NomeDocumento", documentos.NomeDocumento));
            par.Add(new SqlParameter("@codusuario", documentos.CodUsuarioLiberacao));
            retorno = Dbase.ExecutaProcedure("spc_liberadocumento", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Registrar envio de notificação para cliente
        /// </summary>
        /// <param name="documentos">Entidade a ser Notificada de liberação de documento</param>
        /// <returns>true para sucesso</returns>
        public bool RegistryNotification(Documentos documentos)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@CodDocumento", documentos.CodDocumento));
            par.Add(new SqlParameter("@codCliente", documentos.CodCliente));
            par.Add(new SqlParameter("@codEquipamento", documentos.CodEquipamento));
            par.Add(new SqlParameter("@codusuario", documentos.CodUsuarioLiberacao));
            par.Add(new SqlParameter("@emailCliente", documentos.EmailNotificacao));
            retorno = Dbase.ExecutaProcedure("spc_registraNotificacaodocumento", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Excluir documentos
        /// </summary>
        /// <param name="documentos">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Delete(Documentos documentos)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@CodDocumento", documentos.CodDocumento));

            retorno = Dbase.ExecutaProcedure("spc_excluidocumento", par);
            Dbase.Desconectar();

            return retorno;
        }
    }
}