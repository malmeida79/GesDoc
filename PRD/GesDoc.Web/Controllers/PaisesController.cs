using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class PaisesController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Cadastro de paises");

        /// <summary>
        /// Listar Paiss
        /// </summary>
        /// <param name="Pais">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<Pais> GetAll()
        {
           Pais acc;
            List<Pais> retorno = null;
            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaPais",  par);

            if (dr.HasRows)
            {

                retorno = new List<Pais>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    acc = new Pais();

                    acc.CodPais = dr["codPais"].DefaultDbNull<Int32>(0);
                    acc.DescricaoPais = dr["descricaoPais"].ToString();

                    retorno.Add(acc);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Retorna entidade pesquisada
        /// </summary>
        /// <param name="Pais">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public Pais Pesquisar(Pais Pais)
        {
           Pais retorno = null;

            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (Pais.CodPais > 0)
            {
                par.Add(new SqlParameter("@codPais", Pais.CodPais));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaPaisCodigo",  par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new Pais();
                    retorno.CodPais = dr["codPais"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoPais = dr["descricaoPais"].ToString();
                }

            }

            Dbase.Desconectar();

            return retorno;

        }

        /// <summary>
        /// Cadastrar Pais
        /// </summary>
        /// <param name="Pais">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Inserir(Pais Pais)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@descricaoPais", Pais.DescricaoPais));
            retorno = Dbase.ExecutaProcedure("spc_cadastraPais",  par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar Pais
        /// </summary>
        /// <param name="Pais">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Alterar(Pais Pais)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@descricaoPais", Pais.DescricaoPais));
            par.Add(new SqlParameter("@codPais", Pais.CodPais));

            retorno = Dbase.ExecutaProcedure("spc_atualizaPais",  par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Excluir Pais
        /// </summary>
        /// <param name="Pais">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Excluir(Pais Pais)
        {
            bool retorno = false;
            return retorno;
        }

    }
}