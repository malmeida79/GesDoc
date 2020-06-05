using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class BairroController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Cadastro de Bairros");

        /// <summary>
        /// Listar Bairros
        /// </summary>
        /// <param name="Bairro">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<Bairro> GetAll()
        {
           Bairro bairro;
            List<Bairro> retorno = null;
            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listabairros", par);

            if (dr.HasRows)
            {

                retorno = new List<Bairro>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    bairro = new Bairro();

                    bairro.CodBairro = dr["codBairro"].DefaultDbNull<Int32>(0);
                    bairro.DescricaoBairro = dr["descricaoBairro"].ToString();

                    retorno.Add(bairro);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Listagem de Bairro por Cidades
        /// </summary>
        /// <param name="CidadeSelecionado">Cidade para o qual se ira listar as Bairro</param>
        /// <returns></returns>
        public List<Bairro> ListarBairroPorCidade(int codigoCidade)
        {
            List<Bairro> retorno = null;
           Bairro Bairro;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            if (codigoCidade > 0)
            {
                par.Add(new SqlParameter("@codCidade", codigoCidade));
            }

            dr = Dbase.GeraReaderProcedure("spc_listaBairroCidade", par);

            if (dr.HasRows)
            {

                retorno = new List<Bairro>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    Bairro = new Bairro();

                    Bairro.CodBairro = dr["codBairro"].DefaultDbNull<Int32>(0);
                    Bairro.DescricaoBairro = dr["descricaoBairro"].ToString();
                    Bairro.CodCidade = dr["codCidade"].DefaultDbNull<Int32>(0);
                    Bairro.DescricaoCidade = dr["descricaoCidade"].ToString();

                    retorno.Add(Bairro);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Retorna entidade pesquisada
        /// </summary>
        /// <param name="Bairro">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public Bairro Pesquisar(Bairro Bairro)
        {
           Bairro retorno = null;
            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (Bairro.CodBairro > 0)
            {
                par.Add(new SqlParameter("@codBairro", Bairro.CodBairro));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaBairroCodigo", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new Bairro();
                    retorno.CodBairro = dr["codBairro"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoBairro = dr["descricaoBairro"].ToString();
                    retorno.CodEstado = dr["codEstado"].DefaultDbNull<Int32>(0);
                    retorno.CodPais = dr["codPais"].DefaultDbNull<Int32>(0);
                    retorno.CodCidade = dr["codCidade"].DefaultDbNull<Int32>(0);
                }
            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastrar Bairro
        /// </summary>
        /// <param name="Bairro">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Inserir(Bairro Bairro)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@descricaoBairro", Bairro.DescricaoBairro));
            par.Add(new SqlParameter("@codCidade", Bairro.CodCidade));
            retorno = Dbase.ExecutaProcedure("spc_cadastraBairro", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar Bairro
        /// </summary>
        /// <param name="Bairro">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Alterar(Bairro Bairro)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@descricaoBairro", Bairro.DescricaoBairro));
            par.Add(new SqlParameter("@codCidade", Bairro.CodCidade));
            par.Add(new SqlParameter("@codBairro", Bairro.CodBairro));
            retorno = Dbase.ExecutaProcedure("spc_atualizaBairro", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Excluir Bairro
        /// </summary>
        /// <param name="Bairro">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Excluir(Bairro Bairro)
        {
            bool retorno = false;
            return retorno;
        }

    }
}