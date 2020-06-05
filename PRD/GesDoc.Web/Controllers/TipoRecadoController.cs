using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class TipoRecadoController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("cadastro de Tipo de Recado");

        /// <summary>
        /// Listar TipoRecados
        /// </summary>
        /// <param name="TipoRecado">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<TipoRecado> GetAll()
        {

           TipoRecado tps;
            List<TipoRecado> retorno = null;
            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaTipoRecado",  par);

            if (dr.HasRows)
            {

                retorno = new List<TipoRecado>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    tps = new TipoRecado();

                    tps.CodTipoRecado = dr["codTipoRecado"].DefaultDbNull<Int32>(0);
                    tps.DescricaoTipoRecado = dr["TipoRecado"].ToString();

                    retorno.Add(tps);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Retorna entidade pesquisada
        /// </summary>
        /// <param name="TipoRecado">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public TipoRecado Pesquisar(TipoRecado TipoRecado)
        {
           TipoRecado retorno = null;

            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (TipoRecado.CodTipoRecado > 0)
            {
                par.Add(new SqlParameter("@codTipoRecado", TipoRecado.CodTipoRecado));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaTipoRecadoCodigo", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new TipoRecado();
                    retorno.CodTipoRecado = dr["codTipoRecado"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoTipoRecado = dr["TipoRecado"].ToString();
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastrar TipoRecado
        /// </summary>
        /// <param name="TipoRecado">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Inserir(TipoRecado TipoRecado)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@descricaoTipoRecado", TipoRecado.DescricaoTipoRecado));

            retorno = Dbase.ExecutaProcedure("spc_cadastraTipoRecado", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar TipoRecado
        /// </summary>
        /// <param name="TipoRecado">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Alterar(TipoRecado TipoRecado)
        {
            bool retorno = false;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@codTipoRecado", TipoRecado.CodTipoRecado));
            par.Add(new SqlParameter("@descricaoTipoRecado", TipoRecado.DescricaoTipoRecado));

            retorno = Dbase.ExecutaProcedure("spc_atualizaTipoRecado", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Excluir TipoRecado
        /// </summary>
        /// <param name="TipoRecado">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Excluir(int codTipoRecado)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@codTipoRecado", codTipoRecado));

            retorno = Dbase.ExecutaProcedure("spc_excluiTipoRecado", par);
            Dbase.Desconectar();
            return retorno;
        }

        /// <summary>
        /// Conta e devolve a quantidade vezes que esse item esta
        /// em outros relacionamentos (Qts vezes e usado)
        /// </summary>
        /// <param name="codigoTipoRecado">TipoRecado a ser pesquisado</param>
        /// <returns></returns>
        public int ContaUso(int codigoTipoRecado)
        {

            int retorno = 0;

            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codTipoRecado", codigoTipoRecado));

            dr = Dbase.GeraReaderProcedure("spc_contaUsoTipoRecado", par);

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