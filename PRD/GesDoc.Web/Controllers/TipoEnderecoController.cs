using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class TipoEnderecoController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Cadastro de Tipo de endereço");

        /// <summary>
        /// Listar TipoEnderecos
        /// </summary>
        /// <param name="TipoEndereco">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<TipoEndereco> GetAll()
        {

           TipoEndereco tps;
            List<TipoEndereco> retorno = null;
            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaTipoEndereco",  par);

            if (dr.HasRows)
            {

                retorno = new List<TipoEndereco>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    tps = new TipoEndereco();

                    tps.CodTipoEndereco = dr["codTipoEndereco"].DefaultDbNull<Int32>(0);
                    tps.DescricaoTipoEndereco = dr["descricaoTipoEndereco"].ToString();

                    retorno.Add(tps);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Retorna entidade pesquisada
        /// </summary>
        /// <param name="TipoEndereco">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public TipoEndereco Pesquisar(TipoEndereco TipoEndereco)
        {
           TipoEndereco retorno = null;

            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (TipoEndereco.CodTipoEndereco > 0)
            {
                par.Add(new SqlParameter("@codTipoEndereco", TipoEndereco.CodTipoEndereco));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaTipoEnderecoCodigo", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new TipoEndereco();
                    retorno.CodTipoEndereco = dr["codTipoEndereco"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoTipoEndereco = dr["descricaoTipoEndereco"].ToString();
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastrar TipoEndereco
        /// </summary>
        /// <param name="TipoEndereco">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Inserir(TipoEndereco TipoEndereco)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@descricaoTipoEndereco", TipoEndereco.DescricaoTipoEndereco));

            retorno = Dbase.ExecutaProcedure("spc_cadastraTipoEndereco", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar TipoEndereco
        /// </summary>
        /// <param name="TipoEndereco">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Alterar(TipoEndereco TipoEndereco)
        {
            bool retorno = false;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@codTipoEndereco", TipoEndereco.CodTipoEndereco));
            par.Add(new SqlParameter("@descricaoTipoEndereco", TipoEndereco.DescricaoTipoEndereco));

            retorno = Dbase.ExecutaProcedure("spc_atualizaTipoEndereco", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Excluir TipoEndereco
        /// </summary>
        /// <param name="TipoEndereco">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Excluir(int codTipoEndereco)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@codTipoEndereco", codTipoEndereco));

            retorno = Dbase.ExecutaProcedure("spc_excluiTipoEndereco", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Conta e devolve a quantidade vezes que esse item esta
        /// em outros relacionamentos (Qts vezes e usado)
        /// </summary>
        /// <param name="codigoTipoEndereco">TipoEndereco a ser pesquisado</param>
        /// <returns></returns>
        public int ContaUso(int codigoTipoEndereco)
        {

            int retorno = 0;

            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codTipoEndereco", codigoTipoEndereco));

            dr = Dbase.GeraReaderProcedure("spc_contaUsoTipoEndereco", par);

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