using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class TipoEquipamentoController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Cadastro de Tipo de Equipamento");

        /// <summary>
        /// Listar TipoEquipamentos
        /// </summary>
        /// <param name="TipoEquipamento">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<TipoEquipamento> GetAll()
        {

           TipoEquipamento tps;
            List<TipoEquipamento> retorno = null;
            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaTipoEquipamento",  par);

            if (dr.HasRows)
            {

                retorno = new List<TipoEquipamento>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    tps = new TipoEquipamento();

                    tps.CodTipoEquipamento = dr["codTipoEquipamento"].DefaultDbNull<Int32>(0);
                    tps.DescricaoTipoEquipamento = dr["dscTipoEquipamento"].ToString();

                    retorno.Add(tps);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Retorna entidade pesquisada
        /// </summary>
        /// <param name="TipoEquipamento">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public TipoEquipamento Pesquisar(TipoEquipamento TipoEquipamento)
        {
           TipoEquipamento retorno = null;

            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (TipoEquipamento.CodTipoEquipamento > 0)
            {
                par.Add(new SqlParameter("@codTipoEquipamento", TipoEquipamento.CodTipoEquipamento));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaTipoEquipamentoCodigo", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new TipoEquipamento();
                    retorno.CodTipoEquipamento = dr["codTipoEquipamento"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoTipoEquipamento = dr["dscTipoEquipamento"].ToString();
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastrar TipoEquipamento
        /// </summary>
        /// <param name="TipoEquipamento">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Inserir(TipoEquipamento TipoEquipamento)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@descricaoTipoEquipamento", TipoEquipamento.DescricaoTipoEquipamento));

            retorno = Dbase.ExecutaProcedure("spc_cadastraTipoEquipamento", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar TipoEquipamento
        /// </summary>
        /// <param name="TipoEquipamento">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Alterar(TipoEquipamento TipoEquipamento)
        {
            bool retorno = false;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@codTipoEquipamento", TipoEquipamento.CodTipoEquipamento));
            par.Add(new SqlParameter("@descricaoTipoEquipamento", TipoEquipamento.DescricaoTipoEquipamento));

            retorno = Dbase.ExecutaProcedure("spc_atualizaTipoEquipamento", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Excluir TipoEquipamento
        /// </summary>
        /// <param name="TipoEquipamento">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Excluir(int codTipoEquipamento)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@codTipoEquipamento", codTipoEquipamento));

            retorno = Dbase.ExecutaProcedure("spc_excluiTipoEquipamento", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Conta e devolve a quantidade vezes que esse item esta
        /// em outros relacionamentos (Qts vezes e usado)
        /// </summary>
        /// <param name="codigoTipoEquipamento">TipoEquipamento a ser pesquisado</param>
        /// <returns></returns>
        public int ContaUso(int codigoTipoEquipamento)
        {

            int retorno = 0;

            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codTipoEquipamento", codigoTipoEquipamento));

            dr = Dbase.GeraReaderProcedure("spc_contaUsoTipoEquipamento", par);

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