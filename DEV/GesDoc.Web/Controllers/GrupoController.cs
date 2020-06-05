using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class GruposClientesController
    {
        private SQLBase Dbase = new SQLBase("Cadastro de Grupos");

       /// <summary>
        /// Lista de Grupos
        /// </summary>
        /// <param name="Grupo">Entidade Grupo</param>
        /// <returns>Lista de Grupos</returns>
        public List<GruposClientes> GetAll()
        {
           GruposClientes grupo;
            List<GruposClientes> retorno = null;
            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaGrupos",  par);

            if (dr.HasRows)
            {

                retorno = new List<GruposClientes>();

                // configura o objeto usuario logado
                while (dr.Read())
                {
                    grupo = new GruposClientes();
                    grupo.CodGrupo = dr["codGrupo"].DefaultDbNull<Int32>(0);
                    grupo.NomeGrupo = dr["nomeGrupo"].DefaultDbNull<string>(null);
                    retorno.Add(grupo);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Lista de Grupos pesquisados
        /// </summary>
        /// <param name="Grupo">Entidade Grupo</param>
        /// <returns>Lista de Grupos pesquisados</returns>
        public GruposClientes Pesquisar(GruposClientes grupo)
        {

           GruposClientes retorno = null;

            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (grupo.CodGrupo > 0)
            {
                par.Add(new SqlParameter("@codGrupo", grupo.CodGrupo));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaGrupoCodigo", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new GruposClientes();
                    retorno.CodGrupo = dr["codGrupo"].DefaultDbNull<Int32>(0);
                    retorno.NomeGrupo = dr["nomeGrupo"].ToString();
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Grava dados do Grupo
        /// </summary>
        /// <param name="Grupo">Entidade Grupo</param>
        /// <returns>Gravacao dos dados do Grupo</returns>
        public bool Alterar(GruposClientes Grupo)
        {
            bool retorno = false;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@codGrupo", Grupo.CodGrupo));
            par.Add(new SqlParameter("@nomeGrupo", Grupo.NomeGrupo));

            retorno = Dbase.ExecutaProcedure("spc_atualizaGrupo", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastra dados do Grupo
        /// </summary>
        /// <param name="Grupo">Entidade Grupo</param>
        /// <returns>Retorna sucesso ou falha</returns>
        public bool Inserir(GruposClientes Grupo)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@nomeGrupo", Grupo.NomeGrupo));

            retorno = Dbase.ExecutaProcedure("spc_cadastraGrupo", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Exclui dados do Grupo
        /// </summary>
        /// <param name="Grupo">Entidade Grupo</param>
        /// <returns>Retorna sucesso ou falha</returns>
        public bool Excluir(int codGrupo)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@codGrupo", codGrupo));

            retorno = Dbase.ExecutaProcedure("spc_excluiGrupo", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Conta e devolve a quantidade vezes que esse item esta
        /// em outros relacionamentos (Qts vezes e usado)
        /// </summary>
        /// <param name="codigoGrupo">Grupo a ser pesquisado</param>
        /// <returns></returns>
        public int ContaUso(int codigoGrupo)
        {

            int retorno = 0;

            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codGrupo", codigoGrupo));

            dr = Dbase.GeraReaderProcedure("spc_contaUsoGrupo", par);

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
