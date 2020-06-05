using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class DepartamentosController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Cadastro de departamentos");

        /// <summary>
        /// Listar Departamentoss
        /// </summary>
        /// <param name="Departamentos">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<Departamentos> GetAll()
        {
           Departamentos acc;
            List<Departamentos> retorno = null;
            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaDepartamentos", par);

            if (dr.HasRows)
            {

                retorno = new List<Departamentos>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    acc = new Departamentos();

                    acc.CodDepartamento = dr["codDepartamento"].DefaultDbNull<Int32>(0);
                    acc.DescricaoDepartamento = dr["descricaoDepartamento"].ToString();
                    acc.DepartamentoPadrao = dr["menuPadrao"].DefaultDbNull<bool>(false);
                    acc.OrdemTela = dr["ordemTela"].DefaultDbNull<Int32>(0);
                    retorno.Add(acc);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Retorna entidade pesquisada
        /// </summary>
        /// <param name="Departamentos">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public Departamentos Pesquisar(Departamentos Departamentos)
        {
           Departamentos retorno = null;

            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (Departamentos.CodDepartamento > 0)
            {
                par.Add(new SqlParameter("@codDepartamento", Departamentos.CodDepartamento));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaDepartamentoCodigo", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new Departamentos();
                    retorno.CodDepartamento = dr["codDepartamento"].DefaultDbNull<Int32>(0);
                    retorno.DepartamentoPadrao = dr["menuPadrao"].DefaultDbNull<bool>(false);
                    retorno.OrdemTela = dr["ordemTela"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoDepartamento = dr["descricaoDepartamento"].ToString();
                }

            }

            Dbase.Desconectar();

            return retorno;

        }

        /// <summary>
        /// Cadastrar Departamentos
        /// </summary>
        /// <param name="Departamentos">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Inserir(Departamentos Departamentos)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@descricaoDepartamento", Departamentos.DescricaoDepartamento));
            par.Add(new SqlParameter("@menuPadrao", Departamentos.DepartamentoPadrao));
            retorno = Dbase.ExecutaProcedure("spc_cadastraDepartamento", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar Departamentos
        /// </summary>
        /// <param name="Departamentos">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Alterar(Departamentos Departamentos)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@descricaoDepartamento", Departamentos.DescricaoDepartamento));
            par.Add(new SqlParameter("@codDepartamento", Departamentos.CodDepartamento));
            par.Add(new SqlParameter("@menuPadrao", Departamentos.DepartamentoPadrao));
            retorno = Dbase.ExecutaProcedure("spc_atualizaDepartamento", par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Excluir Departamentos
        /// </summary>
        /// <param name="Departamentos">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Excluir(Departamentos Departamentos)
        {
            bool retorno = false;
            return retorno;
        }

    }
}