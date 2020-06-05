using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class RecadosController
    {
        private SQLBase Dbase = new SQLBase("Cadastro de Recados");

        /// <summary>
        /// Lista de Recados
        /// </summary>
        /// <param name="Recados">Entidade Recados</param>
        /// <returns>Lista de Recados</returns>
        public List<Recados> GetAll()
        {
            List<Recados> retorno = new List<Recados>();
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaRecado", par);

            if (dr.HasRows)
            {
                retorno = new List<Recados>();
                while (dr.Read())
                {
                   Recados Recados = new Recados();
                    Recados.CodRecado = dr["codRecado"].DefaultDbNull<Int32>(0);
                    Recados.CodUsuarioRecado = dr["codusuarioRecado"].DefaultDbNull<Int32>(0);
                    Recados.Recado = dr["recado"].ToString();
                    Recados.CodTipoRecado = dr["codTipoRecado"].DefaultDbNull<Int32>(0);
                    Recados.Ativo = dr["ativo"].DefaultDbNull<bool>(true);
                    retorno.Add(Recados);
                    Recados = null;
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Lista de Recados pesquisados
        /// </summary>
        /// <param name="Recados">Entidade Recados</param>
        /// <returns>Lista de Recados pesquisados</returns>
        public Recados Pesquisar(int codRecado)
        {

           Recados retorno = null;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codigoRecado", codRecado));

            dr = Dbase.GeraReaderProcedure("spc_listaRecadoCodigo", par);

            if (dr.HasRows)
            {
                retorno = new Recados();
                while (dr.Read())
                {
                    retorno.CodRecado = dr["codRecado"].DefaultDbNull<Int32>(0);
                    retorno.CodUsuarioRecado = dr["codusuarioRecado"].DefaultDbNull<Int32>(0);
                    retorno.Recado = dr["recado"].ToString();
                    retorno.CodTipoRecado = dr["codTipoRecado"].DefaultDbNull<Int32>(0);
                    retorno.Ativo = dr["ativo"].DefaultDbNull<bool>(true);
                }

            }

            Dbase.Desconectar();


            return retorno;
        }

        /// <summary>
        /// Pesquisa Recados do cliente pelo codigo do Recado
        /// </summary>
        /// <param name="Recados"></param>
        /// <returns></returns>
        public List<Recados> PesquisarPorCodigoTipoRecado(int codTipoRecado)
        {
            List<Recados> retorno = new List<Recados>();
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codigoTipoRecado", codTipoRecado));

            dr = Dbase.GeraReaderProcedure("spc_listaRecadoTipo", par);

            if (dr.HasRows)
            {
                retorno = new List<Recados>();
                while (dr.Read())
                {
                   Recados Recados = new Recados();
                    Recados.CodRecado = dr["codRecado"].DefaultDbNull<Int32>(0);
                    Recados.CodUsuarioRecado = dr["codusuarioRecado"].DefaultDbNull<Int32>(0);
                    Recados.Recado = dr["recado"].ToString();
                    Recados.CodTipoRecado = dr["codTipoRecado"].DefaultDbNull<Int32>(0);
                    Recados.Ativo = dr["ativo"].DefaultDbNull<bool>(true);
                    retorno.Add(Recados);
                    Recados = null;
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Pesquisa Recados do cliente pelo codigo do Recado
        /// </summary>
        /// <param name="Recados"></param>
        /// <returns></returns>
        public List<Recados> PesquisarPorCodigoTipoRecadoAtivo(int codTipoRecado)
        {
            List<Recados> retorno = new List<Recados>();
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codigoTipoRecado", codTipoRecado));

            dr = Dbase.GeraReaderProcedure("spc_listaRecadoAtivoTipo", par);

            if (dr.HasRows)
            {
                retorno = new List<Recados>();
                while (dr.Read())
                {
                   Recados Recados = new Recados();
                    Recados.CodRecado = dr["codRecado"].DefaultDbNull<Int32>(0);
                    Recados.CodUsuarioRecado = dr["codusuarioRecado"].DefaultDbNull<Int32>(0);
                    Recados.Recado = dr["recado"].ToString();
                    Recados.CodTipoRecado = dr["codTipoRecado"].DefaultDbNull<Int32>(0);
                    Recados.Ativo = dr["ativo"].DefaultDbNull<bool>(true);
                    retorno.Add(Recados);
                    Recados = null;
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Grava dados do Recados
        /// </summary>
        /// <param name="Recados">Entidade Recados</param>
        /// <returns>Gravacao dos dados do Recados</returns>
        public bool Alterar(Recados Recados)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codRecado", Recados.CodRecado));
            par.Add(new SqlParameter("@codUsuario", Recados.CodUsuarioRecado));
            par.Add(new SqlParameter("@recado", Recados.Recado));
            par.Add(new SqlParameter("@codTipoRecado", Recados.CodTipoRecado));
            par.Add(new SqlParameter("@ativo", Recados.Ativo));

            retorno = Dbase.ExecutaProcedure("spc_atualizaRecado", par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastra dados do Recados
        /// </summary>
        /// <param name="Recados">Entidade Recados</param>
        /// <returns>Retorna sucesso ou falha</returns>
        public bool Inserir(Recados Recados)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codUsuario", Recados.CodUsuarioRecado));
            par.Add(new SqlParameter("@recado", Recados.Recado));
            par.Add(new SqlParameter("@codTipoRecado", Recados.CodTipoRecado));
            par.Add(new SqlParameter("@ativo", Recados.Ativo));

            retorno = Dbase.ExecutaProcedure("spc_cadastraRecado", par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Exclui dados do Recados
        /// </summary>
        /// <param name="Recados">Entidade Recados</param>
        /// <returns>Retorna sucesso ou falha</returns>
        public bool Excluir(Recados Recados)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codRecado", Recados.CodRecado));

            Dbase.Conectar();

            retorno = Dbase.ExecutaProcedure("spc_excluiRecados", par);

            Dbase.Desconectar();

            return retorno;
        }
    }
}
