using GesDoc.Web.Services;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class HelpController
    {
        private SQLBase Dbase = new SQLBase("Gestão de Helps");

        public string GetHelp(string pagina, bool ehUsuario = false)
        {
            string retorno = string.Empty;

            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            par.Add(new SqlParameter("@pagina", pagina));
            par.Add(new SqlParameter("@Ehcliente", ehUsuario));

            dr = Dbase.GeraReaderProcedure("spc_buscaHelpPagina", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = dr["msgHelp"].ToString();
                }
            }

            Dbase.Desconectar();

            return retorno;
        }

    }
}