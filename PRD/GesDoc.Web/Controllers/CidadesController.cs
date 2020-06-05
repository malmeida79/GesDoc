using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class CidadesController
    {        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Cadastro de cidades");

        /// <summary>
        /// Listar Cidadess
        /// </summary>
        /// <param name="Cidade">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<Cidade> GetAll()
        {
           Cidade cidade;
            List<Cidade> retorno = null;
            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listacidades",  par);

            if (dr.HasRows)
            {

                retorno = new List<Cidade>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    cidade = new Cidade();

                    cidade.CodCidade = dr["codCidade"].DefaultDbNull<Int32>(0);
                    cidade.DescricaoCidade = dr["descricaoCidade"].ToString();

                    retorno.Add(cidade);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Listagem de Cidades por Estados
        /// </summary>
        /// <param name="EstadoSelecionado">Estado para o qual se ira listar as Cidades</param>
        /// <returns></returns>
        public List<Cidade> ListarCidadesPorEstado(int codigoEstado)
        {
            List<Cidade> retorno = null;
           Cidade Cidade;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            if (codigoEstado > 0)
            {
                par.Add(new SqlParameter("@codEstado", codigoEstado));
            }

            dr = Dbase.GeraReaderProcedure("spc_listaCidadeEstado",  par);

            if (dr.HasRows)
            {

                retorno = new List<Cidade>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    Cidade = new Cidade();

                    Cidade.CodCidade = dr["codCidade"].DefaultDbNull<Int32>(0);
                    Cidade.DescricaoCidade = dr["descricaoCidade"].ToString();
                    Cidade.CodEstado = dr["codEstado"].DefaultDbNull<Int32>(0);
                    Cidade.DescricaoEstado = dr["descricaoEstado"].ToString();

                    retorno.Add(Cidade);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Retorna entidade pesquisada
        /// </summary>
        /// <param name="Cidade">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public Cidade Pesquisar(Cidade Cidades)
        {
           Cidade retorno = null;


            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (Cidades.CodCidade > 0)
            {
                par.Add(new SqlParameter("@codCidade", Cidades.CodCidade));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaCidadeCodigo",  par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new Cidade();
                    retorno.CodCidade = dr["codCidade"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoCidade = dr["descricaoCidade"].ToString();
                    retorno.CodEstado = dr["codEstado"].DefaultDbNull<Int32>(0);
                    retorno.CodPais = dr["codPais"].DefaultDbNull<Int32>(0);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastrar Cidades
        /// </summary>
        /// <param name="Cidade">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Inserir(Cidade Cidades)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@descricaoCidade", Cidades.DescricaoCidade));
            par.Add(new SqlParameter("@codEstado", Cidades.CodEstado));
            retorno = Dbase.ExecutaProcedure("spc_cadastraCidade",  par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar Cidades
        /// </summary>
        /// <param name="Cidade">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Alterar(Cidade Cidades)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@descricaoCidade", Cidades.DescricaoCidade));
            par.Add(new SqlParameter("@codEstado", Cidades.CodEstado));
            par.Add(new SqlParameter("@codCidade", Cidades.CodCidade));
            retorno = Dbase.ExecutaProcedure("spc_atualizaCidade",  par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Excluir Cidades
        /// </summary>
        /// <param name="Cidade">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Excluir(Cidade Cidades)
        {
            bool retorno = false;
            return retorno;
        }
    }
}