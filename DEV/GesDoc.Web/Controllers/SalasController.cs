using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class SalasController
    {
        private SQLBase Dbase = new SQLBase("Cadastro de Salas");

        /// <summary>
        /// Lista de Salas
        /// </summary>
        /// <param name="Salas">Entidade Salas</param>
        /// <returns>Lista de Salas</returns>
        public List<Salas> GetAll()
        {
            List<Salas> retSalas = new List<Salas>();


            return retSalas;
        }

        /// <summary>
        /// Lista de Salas pesquisados
        /// </summary>
        /// <param name="Salas">Entidade Salas</param>
        /// <returns>Lista de Salas pesquisados</returns>
        public List<Salas> Pesquisar(Salas Salas)
        {           
            List<Salas> retorno = null;
           
            return retorno;
        }

        /// <summary>
        /// Pesquisa endereços do cliente pelo codigo do cliente
        /// </summary>
        /// <param name="Salas"></param>
        /// <returns></returns>
        public List<Salas> PesquisarPorCodigoCliente(int codCliente)
        {
            List<Salas> retorno = null;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codCliente", codCliente));


            dr = Dbase.GeraReaderProcedure("spc_BuscaSalasCodigoCliente",  par);

            if (dr.HasRows)
            {
                retorno = new List<Salas>();
                while (dr.Read())
                {
                   Salas Salas = new Salas();
                    Salas.CodSala = dr["CodSala"].DefaultDbNull<Int32>(0);
                    Salas.CodCliente = dr["codcliente"].DefaultDbNull<Int32>(0);
                    Salas.NomeSala = dr["NomeSala"].ToString();
                    Salas.ResponsavelSala = dr["ResponsavelSala"].ToString();
                    Salas.DescricaoSetor = dr["descricaoSetor"].ToString();

                    retorno.Add(Salas);
                    Salas = null;
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Pesquisa endereços do cliente pelo codigo do cliente
        /// </summary>
        /// <param name="Salas"></param>
        /// <returns></returns>
        public List<Salas> PesquisarPorCodigoSetor(int codSetor)
        {
            List<Salas> retorno = null;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codigoSetor", codSetor));


            dr = Dbase.GeraReaderProcedure("spc_BuscaSalasCodigoSetor", par);

            if (dr.HasRows)
            {
                retorno = new List<Salas>();
                while (dr.Read())
                {
                   Salas Salas = new Salas();
                    Salas.CodSala = dr["CodSala"].DefaultDbNull<Int32>(0);
                    Salas.CodCliente = dr["codcliente"].DefaultDbNull<Int32>(0);
                    Salas.NomeSala = dr["NomeSala"].ToString();
                    Salas.ResponsavelSala = dr["ResponsavelSala"].ToString();
                    Salas.DescricaoSetor = dr["descricaoSetor"].ToString();

                    retorno.Add(Salas);
                    Salas = null;
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Pesquisa salas do cliente pelo codigo da sala
        /// </summary>
        /// <param name="CodSala">Codigo da sala a ser pesquisa</param>
        /// <returns>Dados da sala encontrada</returns>
        public Salas PesquisarPorCodigoSala(int CodSala)
        {
           Salas retorno = null;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codigoSala", CodSala));

            dr = Dbase.GeraReaderProcedure("spc_BuscaSalasCodigoSala",  par);

            if (dr.HasRows)
            {
                retorno = new Salas();
                while (dr.Read())
                {
                    retorno.CodSala = dr["CodSala"].DefaultDbNull<Int32>(0);
                    retorno.CodCliente = dr["codcliente"].DefaultDbNull<Int32>(0);
                    retorno.NomeSala = dr["NomeSala"].ToString();
                    retorno.ResponsavelSala = dr["ResponsavelSala"].ToString();
                    retorno.DescricaoSetor = dr["descricaoSetor"].ToString();
                    retorno.CodSetor = dr["codSetor"].DefaultDbNull<Int32>(0);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Grava dados do Salas
        /// </summary>
        /// <param name="Salas">Entidade Salas</param>
        /// <returns>Gravacao dos dados do Salas</returns>
        public bool Alterar(Salas Salas)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@CodSala", Salas.CodSala));
            par.Add(new SqlParameter("@codcliente", Salas.CodCliente));
            par.Add(new SqlParameter("@NomeSala", Salas.NomeSala));
            par.Add(new SqlParameter("@ResponsavelSala", Salas.ResponsavelSala));
            par.Add(new SqlParameter("@codSetor", Salas.CodSetor));

            retorno = Dbase.ExecutaProcedure("spc_atualizaSalas",  par);

            return retorno;
        }

        /// <summary>
        /// Cadastra dados do Salas
        /// </summary>
        /// <param name="Salas">Entidade Salas</param>
        /// <returns>Retorna sucesso ou falha</returns>
        public bool Inserir(Salas Salas)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codcliente", Salas.CodCliente));
            par.Add(new SqlParameter("@NomeSala", Salas.NomeSala));
            par.Add(new SqlParameter("@ResponsavelSala", Salas.ResponsavelSala));
            par.Add(new SqlParameter("@codSetor", Salas.CodSetor));

            retorno = Dbase.ExecutaProcedure("spc_cadastraSalas",  par);

            return retorno;
        }

        /// <summary>
        /// Exclui dados do Salas
        /// </summary>
        /// <param name="Salas">Entidade Salas</param>
        /// <returns>Retorna sucesso ou falha</returns>
        public bool Excluir(Salas Salas)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@CodSala", Salas.CodSala));

            retorno = Dbase.ExecutaProcedure("spc_excluiSalas", par);

            return retorno;
        }
    }
}
