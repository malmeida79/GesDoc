using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class TipoDocumentoController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Cadastro de Tipo de Contato");

        /// <summary>
        /// Listar TipoDocumentos
        /// </summary>
        /// <param name="TipoDocumento">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<TipoDocumento> GetAll()
        {

            TipoDocumento tps;
            List<TipoDocumento> retorno = null;
            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaTipoDocumento", par);

            if (dr.HasRows)
            {

                retorno = new List<TipoDocumento>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    tps = new TipoDocumento();

                    tps.CodTipoDocumento = dr["codTipoDocumento"].DefaultDbNull<Int32>(0);
                    tps.DescricaoTipoDocumento = dr["TipoDocumento"].ToString();
                    tps.TipoGeral = dr["TipoGeral"].DefaultDbNull<bool>(0);
                    tps.TipoCliente = dr["TipoCliente"].DefaultDbNull<bool>(0);
                    tps.ExigeLiberacao = dr["ExigeLiberacao"].DefaultDbNull<bool>(0);
                    tps.ClassificadoTipoServico = dr["ClassificadoTipoServico"].DefaultDbNull<bool>(0);

                    retorno.Add(tps);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Retorna entidade pesquisada
        /// </summary>
        /// <param name="TipoDocumento">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public TipoDocumento GET(TipoDocumento TipoDocumento)
        {
            TipoDocumento retorno = null;

            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (TipoDocumento.CodTipoDocumento > 0)
            {
                par.Add(new SqlParameter("@codTipoDocumento", TipoDocumento.CodTipoDocumento));
            }

            if (!string.IsNullOrEmpty(TipoDocumento.DescricaoTipoDocumento))
            {
                par.Add(new SqlParameter("@descTipoDocumento", TipoDocumento.DescricaoTipoDocumento));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaTipoDocumento", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new TipoDocumento();
                    retorno.CodTipoDocumento = dr["codTipoDocumento"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoTipoDocumento = dr["TipoDocumento"].ToString();
                    retorno.TipoGeral = dr["TipoGeral"].DefaultDbNull<bool>(0);
                    retorno.TipoCliente = dr["TipoCliente"].DefaultDbNull<bool>(0);
                    retorno.ExigeLiberacao = dr["ExigeLiberacao"].DefaultDbNull<bool>(0);
                    retorno.ClassificadoTipoServico = dr["ClassificadoTipoServico"].DefaultDbNull<bool>(0);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastrar TipoDocumento
        /// </summary>
        /// <param name="TipoDocumento">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Inserir(TipoDocumento TipoDocumento)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@descricaoTipoDocumento", TipoDocumento.DescricaoTipoDocumento));

            retorno = Dbase.ExecutaProcedure("spc_cadastraTipoDocumento", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar TipoDocumento
        /// </summary>
        /// <param name="TipoDocumento">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Alterar(TipoDocumento TipoDocumento)
        {
            bool retorno = false;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@codTipoDocumento", TipoDocumento.CodTipoDocumento));
            par.Add(new SqlParameter("@descricaoTipoDocumento", TipoDocumento.DescricaoTipoDocumento));

            retorno = Dbase.ExecutaProcedure("spc_atualizaTipoDocumento", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Excluir TipoDocumento
        /// </summary>
        /// <param name="TipoDocumento">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Excluir(int codTipoDocumento)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@codTipoDocumento", codTipoDocumento));

            retorno = Dbase.ExecutaProcedure("spc_excluiTipoDocumento", par);
            Dbase.Desconectar();

            return retorno;
        }


        /// <summary>
        /// Conta e devolve a quantidade vezes que esse item esta
        /// em outros relacionamentos (Qts vezes e usado)
        /// </summary>
        /// <param name="codigoTipoDocumento">TipoDocumento a ser pesquisado</param>
        /// <returns></returns>
        public int ContaUso(int codigoTipoDocumento)
        {

            int retorno = 0;

            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codTipoDocumento", codigoTipoDocumento));

            dr = Dbase.GeraReaderProcedure("spc_contaUsoTipoDocumento", par);

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

    }
}