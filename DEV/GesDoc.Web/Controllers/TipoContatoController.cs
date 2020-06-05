using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class TipoContatoController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Cadastro de Tipo de Contato");

        /// <summary>
        /// Listar TipoContatos
        /// </summary>
        /// <param name="TipoContato">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<TipoContato> GetAll()
        {

           TipoContato tps;
            List<TipoContato> retorno = null;
            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaTipoContato",  par);

            if (dr.HasRows)
            {

                retorno = new List<TipoContato>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    tps = new TipoContato();

                    tps.CodTipoContato = dr["codTipoContato"].DefaultDbNull<Int32>(0);
                    tps.DescricaoTipoContato = dr["TipoContato"].ToString();

                    retorno.Add(tps);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Retorna entidade pesquisada
        /// </summary>
        /// <param name="TipoContato">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public TipoContato Pesquisar(TipoContato TipoContato)
        {
           TipoContato retorno = null;

            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (TipoContato.CodTipoContato > 0)
            {
                par.Add(new SqlParameter("@codTipoContato", TipoContato.CodTipoContato));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaTipoContatoCodigo", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new TipoContato();
                    retorno.CodTipoContato = dr["codTipoContato"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoTipoContato = dr["TipoContato"].ToString();
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastrar TipoContato
        /// </summary>
        /// <param name="TipoContato">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Inserir(TipoContato TipoContato)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@descricaoTipoContato", TipoContato.DescricaoTipoContato));

            retorno = Dbase.ExecutaProcedure("spc_cadastraTipoContato", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar TipoContato
        /// </summary>
        /// <param name="TipoContato">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Alterar(TipoContato TipoContato)
        {
            bool retorno = false;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@codTipoContato", TipoContato.CodTipoContato));
            par.Add(new SqlParameter("@descricaoTipoContato", TipoContato.DescricaoTipoContato));

            retorno = Dbase.ExecutaProcedure("spc_atualizaTipoContato", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Excluir TipoContato
        /// </summary>
        /// <param name="TipoContato">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Excluir(int codTipoContato)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@codTipoContato", codTipoContato));

            retorno = Dbase.ExecutaProcedure("spc_excluiTipoContato", par);
            Dbase.Desconectar();

            return retorno;
        }


        /// <summary>
        /// Conta e devolve a quantidade vezes que esse item esta
        /// em outros relacionamentos (Qts vezes e usado)
        /// </summary>
        /// <param name="codigoTipoContato">TipoContato a ser pesquisado</param>
        /// <returns></returns>
        public int ContaUso(int codigoTipoContato)
        {

            int retorno = 0;

            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codTipoContato", codigoTipoContato));

            dr = Dbase.GeraReaderProcedure("spc_contaUsoTipoContato", par);

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