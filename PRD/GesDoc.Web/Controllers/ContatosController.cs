using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class ContatosController
    {
        private SQLBase Dbase = new SQLBase("Cadastro de contatos");

        /// <summary>
        /// Lista de Contatos
        /// </summary>
        /// <param name="Contatos">Entidade Contatos</param>
        /// <returns>Lista de Contatos</returns>
        public List<Contatos> GetAll()
        {
            List<Contatos> retContatos = new List<Contatos>();


            return retContatos;
        }

        /// <summary>
        /// Lista de Contatos pesquisados
        /// </summary>
        /// <param name="Contatos">Entidade Contatos</param>
        /// <returns>Lista de Contatos pesquisados</returns>
        public List<Contatos> Pesquisar(Contatos Contatos)
        {

            List<Contatos> retorno = null;

            return retorno;
        }

        /// <summary>
        /// Pesquisa contatos do cliente pelo codigo do contato
        /// </summary>
        /// <param name="Contatos"></param>
        /// <returns></returns>
        public Contatos PesquisarPorCodigoContato(int codContato)
        {
           Contatos retorno = new Contatos();
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codigoContato", codContato));

            dr = Dbase.GeraReaderProcedure("spc_BuscaContatosCodigoContato", par);

            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    retorno.CodContato = dr["codContato"].DefaultDbNull<Int32>(0);
                    retorno.CodCliente = dr["codCliente"].DefaultDbNull<Int32>(0);
                    retorno.Nome = dr["nome"].ToString();
                    retorno.CodDDD = dr["codddd"].ToString();
                    retorno.Telefone = dr["telefone"].ToString();
                    retorno.Ramal = dr["ramal"].ToString();
                    retorno.Email = dr["email"].ToString();
                    retorno.CodTipoContato = dr["codTipoContato"].DefaultDbNull<Int32>(0);
                    retorno.TipoContato = dr["TipoContato"].ToString();
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Pesquisa contatos para o cliente
        /// </summary>
        /// <param name="codCliente">Codigo do cliente para o qual se deseja os contatos.</param>
        /// <returns></returns>
        public List<Contatos> PesquisarPorCodigoCliente(int codCliente)
        {
            List<Contatos> retorno = null;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codCliente", codCliente));

            dr = Dbase.GeraReaderProcedure("spc_BuscaContatosCodigoCliente", par);

            if (dr.HasRows)
            {
                retorno = new List<Contatos>();
                while (dr.Read())
                {
                   Contatos Contatos = new Contatos();
                    Contatos.CodContato = dr["codContato"].DefaultDbNull<Int32>(0);
                    Contatos.CodCliente = dr["codCliente"].DefaultDbNull<Int32>(0);
                    Contatos.Nome = dr["nome"].ToString();
                    Contatos.CodDDD = dr["codddd"].ToString();
                    Contatos.Telefone = dr["telefone"].ToString();
                    Contatos.Ramal = dr["ramal"].ToString();
                    Contatos.Email = dr["email"].ToString();
                    Contatos.CodTipoContato = dr["codTipoContato"].DefaultDbNull<Int32>(0);
                    Contatos.TipoContato = dr["TipoContato"].ToString();
                    retorno.Add(Contatos);
                    Contatos = null;
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Pesquisa email notificação documento
        /// </summary>
        /// <param name="codCliente">Codigo do cliente para o qual se deseja o email
        /// <returns></returns>
        public string BuscaEmailDocumento(int codCliente)
        {
            string retorno = "ncad";
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codCliente", codCliente));

            dr = Dbase.GeraReaderProcedure("spc_buscaEmailNotificacao", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = dr["email"].ToString();
                }

            }

            Dbase.Desconectar();

            return retorno;
        }


        /// <summary>
        /// Grava dados do Contatos
        /// </summary>
        /// <param name="Contatos">Entidade Contatos</param>
        /// <returns>Gravacao dos dados do Contatos</returns>
        public bool Alterar(Contatos Contatos)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codContato", Contatos.CodContato));
            par.Add(new SqlParameter("@codCliente", Contatos.CodCliente));
            par.Add(new SqlParameter("@nome", Contatos.Nome));
            par.Add(new SqlParameter("@codddd", Contatos.CodDDD));
            par.Add(new SqlParameter("@telefone", Contatos.Telefone));
            par.Add(new SqlParameter("@ramal", Contatos.Ramal));
            par.Add(new SqlParameter("@email", Contatos.Email));
            par.Add(new SqlParameter("@codTipoContato", Contatos.CodTipoContato));

            retorno = Dbase.ExecutaProcedure("spc_atualizaContatos", par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastra dados do Contatos
        /// </summary>
        /// <param name="Contatos">Entidade Contatos</param>
        /// <returns>Retorna sucesso ou falha</returns>
        public bool Inserir(Contatos Contatos)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codCliente", Contatos.CodCliente));
            par.Add(new SqlParameter("@nome", Contatos.Nome));
            par.Add(new SqlParameter("@codddd", Contatos.CodDDD));
            par.Add(new SqlParameter("@telefone", Contatos.Telefone));
            par.Add(new SqlParameter("@ramal", Contatos.Ramal));
            par.Add(new SqlParameter("@email", Contatos.Email));
            par.Add(new SqlParameter("@codTipoContato", Contatos.CodTipoContato));

            retorno = Dbase.ExecutaProcedure("spc_cadastraContatos", par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Exclui dados do Contatos
        /// </summary>
        /// <param name="Contatos">Entidade Contatos</param>
        /// <returns>Retorna sucesso ou falha</returns>
        public bool Excluir(Contatos Contatos)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codContato", Contatos.CodContato));

            Dbase.Conectar();

            retorno = Dbase.ExecutaProcedure("spc_excluiContatos", par);

            Dbase.Desconectar();

            return retorno;
        }
    }
}
