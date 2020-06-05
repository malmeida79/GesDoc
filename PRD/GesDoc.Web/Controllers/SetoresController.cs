using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class SetoresController
    {
        private SQLBase Dbase = new SQLBase("Cadastro de setores");

        /// <summary>
        /// Lista de Setoress
        /// </summary>
        /// <param name="Setores">Entidade Setores</param>
        /// <returns>Lista de Setoress</returns>
        public List<Setores> GetAll()
        {
            List<Setores> retSetores = new List<Setores>();


            return retSetores;
        }

        /// <summary>
        /// Lista de Setoress pesquisados
        /// </summary>
        /// <param name="Setores">Entidade Setores</param>
        /// <returns>Lista de Setoress pesquisados</returns>
        public List<Setores> Pesquisar(Setores Setores)
        {

            List<Setores> retorno = null;

            return retorno;
        }

        /// <summary>
        /// Pesquisa endereços do cliente pelo codigo do cliente
        /// </summary>
        /// <param name="codCliente"></param>
        /// <returns></returns>
        public List<Setores> PesquisarPorCodigoCliente(int codCliente)
        {
            List<Setores> retorno = null;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codCliente", codCliente));

            dr = Dbase.GeraReaderProcedure("spc_BuscaSetoresCodigoCliente", par);

            if (dr.HasRows)
            {
                retorno = new List<Setores>();
                while (dr.Read())
                {
                   Setores Setores = new Setores();
                    Setores.CodSetor = dr["codSetor"].DefaultDbNull<Int32>(0);
                    Setores.CodCliente = dr["codCliente"].DefaultDbNull<Int32>(0);
                    Setores.DescricaoSetor = dr["descricaoSetor"].ToString();
                    Setores.ResponsavelTecnico = dr["responsavelTecnico"].ToString();
                    Setores.CRMResponsavel = dr["crmResponsavel"].ToString();
                    Setores.SupervisorTecnico = dr["supervisorTecnico"].ToString();
                    Setores.ResponsavelLegal = dr["responsavelLegal"].ToString();
                    Setores.CRMResponsavelLegal = dr["crmResponsavelLegal"].ToString();
                    Setores.CRVSupervisor = dr["crvSupervisor"].ToString();
                    retorno.Add(Setores);
                    Setores = null;
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Busca setor selecionado pelo cliente para editar.
        /// </summary>
        /// <param name="codSetor"></param>
        /// <returns></returns>
        public Setores PesquisarPorCodigoSetor(int codSetor)
        {
           Setores retorno = null;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codigoSetor", codSetor));

            dr = Dbase.GeraReaderProcedure("spc_BuscaSetoresCodigoSetor", par);

            if (dr.HasRows)
            {
                retorno = new Setores();
                while (dr.Read())
                {
                    retorno.CodSetor = dr["codSetor"].DefaultDbNull<Int32>(0);
                    retorno.CodCliente = dr["codCliente"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoSetor = dr["descricaoSetor"].ToString();
                    retorno.ResponsavelTecnico = dr["responsavelTecnico"].ToString();
                    retorno.CRMResponsavel = dr["crmResponsavel"].ToString();
                    retorno.SupervisorTecnico = dr["supervisorTecnico"].ToString();
                    retorno.ResponsavelLegal = dr["responsavelLegal"].ToString();
                    retorno.CRMResponsavelLegal = dr["crmResponsavelLegal"].ToString();
                    retorno.CRVSupervisor = dr["crvSupervisor"].ToString();

                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Grava dados do Setores
        /// </summary>
        /// <param name="Setores">Entidade Setores</param>
        /// <returns>Gravacao dos dados do Setores</returns>
        public bool Alterar(Setores Setores)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codSetor", Setores.CodSetor));
            par.Add(new SqlParameter("@codCliente", Setores.CodCliente));
            par.Add(new SqlParameter("@descricaoSetor", Setores.DescricaoSetor));
            par.Add(new SqlParameter("@responsavelTecnico", Setores.ResponsavelTecnico));
            par.Add(new SqlParameter("@crmResponsavel", Setores.CRMResponsavel));
            par.Add(new SqlParameter("@supervisorTecnico", Setores.SupervisorTecnico));
            par.Add(new SqlParameter("@crvSupervisor", Setores.CRVSupervisor));
            par.Add(new SqlParameter("@responsavelLegal", Setores.ResponsavelLegal));
            par.Add(new SqlParameter("@crmResponsavelLegal", Setores.CRMResponsavelLegal));

            retorno = Dbase.ExecutaProcedure("spc_atualizaSetores", par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastra dados do Setores
        /// </summary>
        /// <param name="Setores">Entidade Setores</param>
        /// <returns>Retorna sucesso ou falha</returns>
        public bool Inserir(Setores Setores)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codCliente", Setores.CodCliente));
            par.Add(new SqlParameter("@descricaoSetor", Setores.DescricaoSetor));
            par.Add(new SqlParameter("@responsavelTecnico", Setores.ResponsavelTecnico));
            par.Add(new SqlParameter("@crmResponsavel", Setores.CRMResponsavel));
            par.Add(new SqlParameter("@supervisorTecnico", Setores.SupervisorTecnico));
            par.Add(new SqlParameter("@crvSupervisor", Setores.CRVSupervisor));
            par.Add(new SqlParameter("@responsavelLegal", Setores.ResponsavelLegal));
            par.Add(new SqlParameter("@crmResponsavelLegal", Setores.CRMResponsavelLegal));
            retorno = Dbase.ExecutaProcedure("spc_cadastraSetores", par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Exclui dados do Setores
        /// </summary>
        /// <param name="Setores">Entidade Setores</param>
        /// <returns>Retorna sucesso ou falha</returns>
        public bool Excluir(Setores Setores)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@codSetor", Setores.CodSetor));

            retorno = Dbase.ExecutaProcedure("spc_excluiSetores", par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Conta e devolve a quantidade de salas para esse setor
        /// </summary>
        /// <param name="codigoSetor">Setor a ser pesquisado</param>
        /// <returns></returns>
        public int ContaSalasetor(int codigoSetor)
        {

            int retorno = 0;

            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codSetor", codigoSetor));

            dr = Dbase.GeraReaderProcedure("spc_contaSalasetor", par);

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
