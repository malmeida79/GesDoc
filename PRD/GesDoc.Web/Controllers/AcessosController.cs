using System;
using System.Collections.Generic;
using GesDoc.Models;
using GesDoc.Web.Services;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class AcessosController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Gestão de acessos");

        /// <summary>
        /// Listar usuarios
        /// </summary>
        /// <param name="usuario">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<Acessos> GetAll()
        {
            List<Acessos> retorno = null;
           Acessos usr;
            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaacessos", par);

            if (dr.HasRows)
            {
                retorno = new List<Acessos>();

                while (dr.Read())
                {
                    usr = new Acessos();

                    usr.CodUsuario = dr["codUsuario"].DefaultDbNull<Int32>(0);
                    usr.Contagem = dr["contagem"].DefaultDbNull<Int32>(0);
                    usr.NomeUsuario = dr["nomeUsuario"].ToString();
                    usr.Login = dr["login"].ToString();
                    usr.UltimaData = dr["ultimaData"].DefaultDbNull<DateTime?>(null);

                    retorno.Add(usr);

                }

            }

            Dbase.Desconectar();

            return retorno;

        }

    }
}