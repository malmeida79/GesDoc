using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class UsuarioOnLineController
    {

        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Gestão de UsuarioOnLinees");

        /// <summary>
        /// Listar UsuarioOnLines
        /// </summary>
        /// <param name="UsuarioOnLine">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<UsuarioOnLine> GetAll()
        {
            UsuarioOnLine acc;
            List<UsuarioOnLine> retorno = null;
            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaUsuarioOnLine", par);

            if (dr.HasRows)
            {

                retorno = new List<UsuarioOnLine>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    acc = new UsuarioOnLine();
                    acc.CodUsuario = dr["CodUsuario"].DefaultDbNull<Int32>(0);
                    acc.USLogin = dr["usLogin"].ToString();
                    acc.IdSessao = dr["IdSessao"].ToString();
                    acc.HorarioLogin = dr["HorariousLogin"].DefaultDbNull<DateTime?>(null);
                    retorno.Add(acc);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Retorna entidade pesquisada
        /// </summary>
        /// <param name="UsuarioOnLine">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public UsuarioOnLine GetFromLogin(string usLogin)
        {
            UsuarioOnLine retorno = null;

            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            par.Add(new SqlParameter("@usLogin", usLogin));


            dr = Dbase.GeraReaderProcedure("spc_consultaOnline", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new UsuarioOnLine();
                    retorno.CodUsuario = dr["CodUsuario"].DefaultDbNull<Int32>(0);
                    retorno.USLogin = dr["usLogin"].ToString();
                    retorno.IdSessao = dr["IdSessao"].ToString();
                    retorno.HorarioLogin = dr["HorariousLogin"].DefaultDbNull<DateTime?>(null);
                }

            }

            Dbase.Desconectar();

            return retorno;

        }

        /// <summary>
        /// Cadastrar UsuarioOnLine
        /// </summary>
        /// <param name="UsuarioOnLine">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Registrer(UsuarioOnLine UsuarioOnLine)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@CodUsuario", UsuarioOnLine.CodUsuario));
            par.Add(new SqlParameter("@usLogin", UsuarioOnLine.USLogin));
            par.Add(new SqlParameter("@IdSessao", UsuarioOnLine.IdSessao));

            retorno = Dbase.ExecutaProcedure("spc_registraOnline", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar UsuarioOnLine
        /// </summary>
        /// <param name="UsuarioOnLine">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Remove(int codUsuario)
        {
            bool retorno = false;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@CodUsuario", codUsuario));
            retorno = Dbase.ExecutaProcedure("spc_removeOnline", par);

            Dbase.Desconectar();

            return retorno;
        }

    }
}