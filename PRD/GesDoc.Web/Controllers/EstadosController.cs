using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class EstadosController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Cadastro de Estados");

        /// <summary>
        /// Listar Estadoss
        /// </summary>
        /// <param name="Estados">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<Estado> GetAll()
        {
           Estado estado;
            List<Estado> retorno = null;
            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaestados",  par);

            if (dr.HasRows)
            {

                retorno = new List<Estado>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    estado = new Estado();

                    estado.CodEstado = dr["codEstado"].DefaultDbNull<Int32>(0);
                    estado.DescricaoEstado = dr["descricaoEstado"].ToString();
                    estado.UFEstado = dr["ufEstado"].ToString();

                    retorno.Add(estado);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Listagem de Estados por Paiss
        /// </summary>
        /// <param name="paisSelecionado">Pais para o qual se ira listar as Estados</param>
        /// <returns></returns>
        public List<Estado> ListarEstadosPorPais(int codigoPais)
        {
            List<Estado> retorno = null;
           Estado Estado;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            if (codigoPais > 0)
            {
                par.Add(new SqlParameter("@codPais", codigoPais));
            }

            dr = Dbase.GeraReaderProcedure("spc_listaEstadoPais",  par);

            if (dr.HasRows)
            {

                retorno = new List<Estado>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    Estado = new Estado();

                    Estado.CodEstado = dr["codEstado"].DefaultDbNull<Int32>(0);
                    Estado.DescricaoEstado = dr["descricaoEstado"].ToString();
                    Estado.CodPais = dr["codPais"].DefaultDbNull<Int32>(0);
                    Estado.DescricaoPais = dr["descricaoPais"].ToString();
                    Estado.UFEstado = dr["ufEstado"].ToString();

                    retorno.Add(Estado);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Retorna entidade pesquisada
        /// </summary>
        /// <param name="Estados">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public Estado Pesquisar(Estado Estados)
        {
           Estado retorno = null;


            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (Estados.CodEstado > 0)
            {
                par.Add(new SqlParameter("@codEstado", Estados.CodEstado));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaEstadoCodigo",  par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new Estado();
                    retorno.CodEstado = dr["codEstado"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoEstado = dr["descricaoEstado"].ToString();
                    retorno.CodPais = dr["codPais"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoPais = dr["descricaoPais"].ToString();
                    retorno.UFEstado = dr["ufEstado"].ToString();
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastrar Estados
        /// </summary>
        /// <param name="Estados">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Inserir(Estado Estados)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@descricaoEstado", Estados.DescricaoEstado));
            par.Add(new SqlParameter("@codPais", Estados.CodPais));
            par.Add(new SqlParameter("@ufEstado", Estados.UFEstado));
            retorno = Dbase.ExecutaProcedure("spc_cadastraEstado",  par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar Estados
        /// </summary>
        /// <param name="Estados">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Alterar(Estado Estados)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@descricaoEstado", Estados.DescricaoEstado));
            par.Add(new SqlParameter("@codPais", Estados.CodPais));
            par.Add(new SqlParameter("@ufEstado", Estados.UFEstado));
            par.Add(new SqlParameter("@codEstado", Estados.CodEstado));
            retorno = Dbase.ExecutaProcedure("spc_atualizaEstado",  par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Excluir Estados
        /// </summary>
        /// <param name="Estados">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Excluir(Estado Estados)
        {
            bool retorno = false;
            return retorno;
        }

    }
}