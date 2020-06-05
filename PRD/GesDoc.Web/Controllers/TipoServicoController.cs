using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class TipoServicoController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Cadastro de Tipo de Serviço");

        /// <summary>
        /// Listar TipoServicos
        /// </summary>
        /// <param name="TipoServico">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<TipoServico> GetAll()
        {

           TipoServico tps;
            List<TipoServico> retorno = null;
            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaTipoServico",  par);

            if (dr.HasRows)
            {

                retorno = new List<TipoServico>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    tps = new TipoServico();

                    tps.CodigoTipoServico = dr["codTipoServicos"].DefaultDbNull<Int32>(0);
                    tps.DescricaoTipoServico = dr["descricaoTipoServico"].ToString();

                    retorno.Add(tps);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Retorna entidade pesquisada
        /// </summary>
        /// <param name="TipoServico">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public TipoServico Pesquisar(TipoServico TipoServico)
        {
           TipoServico retorno = null;

            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (TipoServico.CodigoTipoServico > 0)
            {
                par.Add(new SqlParameter("@codTipoServico", TipoServico.CodigoTipoServico));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaTipoServicoCodigo", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new TipoServico();
                    retorno.CodigoTipoServico = dr["codTipoServicos"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoTipoServico = dr["descricaoTipoServico"].ToString();
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        internal TipoServico PesquisarPorCodigoServico(int CodigoServico)
        {
            TipoServico retorno = null;

            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (CodigoServico > 0)
            {
                par.Add(new SqlParameter("@codTipoServico", CodigoServico));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaTipoServicoCodigo", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new TipoServico();
                    retorno.CodigoTipoServico = dr["codTipoServicos"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoTipoServico = dr["descricaoTipoServico"].ToString();
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastrar TipoServico
        /// </summary>
        /// <param name="TipoServico">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Inserir(TipoServico TipoServico)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@descricaoTipoServico", TipoServico.DescricaoTipoServico));

            retorno = Dbase.ExecutaProcedure("spc_cadastraTipoServico", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar TipoServico
        /// </summary>
        /// <param name="TipoServico">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Alterar(TipoServico TipoServico)
        {
            bool retorno = false;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@codTipoServico", TipoServico.CodigoTipoServico));
            par.Add(new SqlParameter("@descricaoTipoServico", TipoServico.DescricaoTipoServico));

            retorno = Dbase.ExecutaProcedure("spc_atualizaTipoServico", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Excluir TipoServico
        /// </summary>
        /// <param name="TipoServico">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Excluir(int codTipoServico)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@codTipoServico", codTipoServico));

            retorno = Dbase.ExecutaProcedure("spc_excluiTipoServico", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Conta e devolve a quantidade vezes que esse item esta
        /// em outros relacionamentos (Qts vezes e usado)
        /// </summary>
        /// <param name="codigoTipoServico">TipoServico a ser pesquisado</param>
        /// <returns></returns>
        public int ContaUso(int codigoTipoServico)
        {

            int retorno = 0;

            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codTipoServico", codigoTipoServico));

            dr = Dbase.GeraReaderProcedure("spc_contaUsoTipoServico", par);

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