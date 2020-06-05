using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class FuncionalidadesController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Cadastro de Funcionalidades");

        /// <summary>
        /// Listar Funcionalidadess
        /// </summary>
        /// <param name="Funcionalidades">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<Funcionalidades> GetAll()
        {
           Funcionalidades fnc;
            List<Funcionalidades> retorno = null;
            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaFuncionalidades", par);

            if (dr.HasRows)
            {

                retorno = new List<Funcionalidades>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    fnc = new Funcionalidades();

                    fnc.CodFuncionalidade = dr["codFuncionalidade"].DefaultDbNull<Int32>(0);
                    fnc.DescricaoFuncionalidade = dr["descricaoFuncionalidade"].ToString();
                    fnc.FuncionalidadePadrao = dr["menuItemPadrao"].DefaultDbNull<bool>(false);
                    retorno.Add(fnc);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Listagem de funcionalidades por departamentos
        /// </summary>
        /// <param name="dptoSelecionado">Departamento para o qual se ira listar as funcionalidades</param>
        /// <returns></returns>
        public List<Funcionalidades> ListarPorDepartamento(int dptoSelecionado)
        {
            List<Funcionalidades> retorno = null;
           Funcionalidades funcionalidade;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            if (dptoSelecionado > 0)
            {
                par.Add(new SqlParameter("@codDepartamento", dptoSelecionado));
            }

            dr = Dbase.GeraReaderProcedure("spc_listaFuncionalidadesDpto", par);

            if (dr.HasRows)
            {

                retorno = new List<Funcionalidades>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    funcionalidade = new Funcionalidades();

                    funcionalidade.CodFuncionalidade = dr["codFuncionalidade"].DefaultDbNull<Int32>(0);
                    funcionalidade.DescricaoFuncionalidade = dr["descricaoFuncionalidade"].ToString();
                    funcionalidade.CodDepartamento = dr["codDepartamento"].DefaultDbNull<Int32>(0);
                    funcionalidade.DescricaoDepartamento = dr["descricaoDepartamento"].ToString();
                    funcionalidade.UrlFuncionalidade = dr["urlFuncionalidade"].ToString();
                    funcionalidade.ExibeMenu = dr["exibeMenu"].DefaultDbNull<bool>(false);
                    funcionalidade.FuncionalidadePadrao = dr["menuItemPadrao"].DefaultDbNull<bool>(false);
                    funcionalidade.DepartamentoPadrao = dr["menuPadrao"].DefaultDbNull<bool>(false);

                    retorno.Add(funcionalidade);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Retorna entidade pesquisada
        /// </summary>
        /// <param name="Funcionalidades">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public Funcionalidades Pesquisar(Funcionalidades Funcionalidades)
        {
           Funcionalidades retorno = null;


            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (Funcionalidades.CodFuncionalidade > 0)
            {
                par.Add(new SqlParameter("@codFuncionalidade", Funcionalidades.CodFuncionalidade));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaFuncionalidadeCodigo", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new Funcionalidades();
                    retorno.CodFuncionalidade = dr["codFuncionalidade"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoFuncionalidade = dr["descricaoFuncionalidade"].ToString();
                    retorno.CodDepartamento = dr["codDepartamento"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoDepartamento = dr["descricaoDepartamento"].ToString();
                    retorno.UrlFuncionalidade = dr["urlFuncionalidade"].ToString();
                    retorno.ExibeMenu = dr["exibeMenu"].DefaultDbNull<bool>(false);
                    retorno.FuncionalidadePadrao = dr["menuItemPadrao"].DefaultDbNull<bool>(false);
                    retorno.DepartamentoPadrao = dr["menuPadrao"].DefaultDbNull<bool>(false);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastrar Funcionalidades
        /// </summary>
        /// <param name="Funcionalidades">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Inserir(Funcionalidades Funcionalidades)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@descricaoFuncionalidade", Funcionalidades.DescricaoFuncionalidade));
            par.Add(new SqlParameter("@codDepartamento", Funcionalidades.CodDepartamento));
            par.Add(new SqlParameter("@urlFuncionalidade", Funcionalidades.UrlFuncionalidade));
            par.Add(new SqlParameter("@exibeMenu", Funcionalidades.ExibeMenu));
            par.Add(new SqlParameter("@menuItemPadrao", Funcionalidades.FuncionalidadePadrao));
            retorno = Dbase.ExecutaProcedure("spc_cadastraFuncionalidade", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar Funcionalidades
        /// </summary>
        /// <param name="Funcionalidades">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Alterar(Funcionalidades Funcionalidades)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@descricaoFuncionalidade", Funcionalidades.DescricaoFuncionalidade));
            par.Add(new SqlParameter("@codDepartamento", Funcionalidades.CodDepartamento));
            par.Add(new SqlParameter("@urlFuncionalidade", Funcionalidades.UrlFuncionalidade));
            par.Add(new SqlParameter("@codFuncionalidade", Funcionalidades.CodFuncionalidade));
            par.Add(new SqlParameter("@exibeMenu", Funcionalidades.ExibeMenu));
            par.Add(new SqlParameter("@menuItemPadrao", Funcionalidades.FuncionalidadePadrao));
            retorno = Dbase.ExecutaProcedure("spc_atualizaFuncionalidade", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Excluir Funcionalidades
        /// </summary>
        /// <param name="Funcionalidades">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Excluir(int codFuncionalidade)
        {
            bool retorno = false;

            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codFuncionalidade", codFuncionalidade));

            retorno = Dbase.ExecutaProcedure("spc_excluiFuncionalidade", par);


            return retorno;
        }

        /// <summary>
        /// Conta e devolve a quantidade vezes que esse item esta
        /// em outros relacionamentos (Qts vezes e usado)
        /// </summary>
        /// <param name="codigoTipoContato">TipoContato a ser pesquisado</param>
        /// <returns></returns>
        public int ContaUso(int codigoTipoContato)
        {

            int retorno = 0;

            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codFuncionalidade", codigoTipoContato));

            dr = Dbase.GeraReaderProcedure("spc_contaUsoFuncionalidade", par);

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