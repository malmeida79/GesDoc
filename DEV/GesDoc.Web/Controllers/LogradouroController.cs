using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class LogradourosController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Cadastro de Logradouros");

        /// <summary>
        /// Listar Logradouross
        /// </summary>
        /// <param name="Logradouros">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<Logradouro> GetAll()
        {
           Logradouro acc;
            List<Logradouro> retorno = null;
            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaLogradouros",  par);

            if (dr.HasRows)
            {

                retorno = new List<Logradouro>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    acc = new Logradouro();

                    acc.CodLogradouro = dr["codLogradouro"].DefaultDbNull<Int32>(0);
                    acc.DescricaoLogradouro = dr["descricaoLogradouro"].ToString();

                    retorno.Add(acc);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Retorna entidade pesquisada
        /// </summary>
        /// <param name="Logradouros">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public Logradouro Pesquisar(Logradouro Logradouros)
        {
           Logradouro retorno = null;

            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (Logradouros.CodLogradouro > 0)
            {
                par.Add(new SqlParameter("@codLogradouro", Logradouros.CodLogradouro));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaLogradouroCodigo",  par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new Logradouro();
                    retorno.CodLogradouro = dr["codLogradouro"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoLogradouro = dr["descricaoLogradouro"].ToString();
                }

            }

            Dbase.Desconectar();

            return retorno;

        }

        /// <summary>
        /// Cadastrar Logradouros
        /// </summary>
        /// <param name="Logradouros">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Inserir(Logradouro Logradouros)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@descricaoLogradouro", Logradouros.DescricaoLogradouro));
            retorno = Dbase.ExecutaProcedure("spc_cadastraLogradouro",  par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar Logradouros
        /// </summary>
        /// <param name="Logradouros">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Alterar(Logradouro Logradouros)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@descricaoLogradouro", Logradouros.DescricaoLogradouro));
            par.Add(new SqlParameter("@codLogradouro", Logradouros.CodLogradouro));

            retorno = Dbase.ExecutaProcedure("spc_atualizaLogradouro",  par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Excluir Logradouros
        /// </summary>
        /// <param name="Logradouros">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Excluir(Logradouro Logradouros)
        {
            bool retorno = false;
            return retorno;
        }

    }
}