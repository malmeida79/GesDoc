using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class GruposAcessoController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Cadastro de GruposAcesso");

        /// <summary>
        /// Listar GruposAcessos
        /// </summary>
        /// <param name="GruposAcesso">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<GruposAcesso> GetAll()
        {
            GruposAcesso acc;
            List<GruposAcesso> retorno = null;
            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaGruposAcesso", par);

            if (dr.HasRows)
            {

                retorno = new List<GruposAcesso>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    acc = new GruposAcesso();

                    acc.CodGrupo = dr["codGrupo"].DefaultDbNull<Int32>(0);
                    acc.NomeGrupo = dr["nomeGrupo"].ToString();
                    acc.InfoGrupo = dr["infoGrupo"].ToString();
                    acc.GrupoPadrao = dr["grupoPadrao"].DefaultDbNull<bool>(0);
                    acc.GrupoPadraoCliente = dr["grupoPadraoCliente"].DefaultDbNull<bool>(0);

                    retorno.Add(acc);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Recupera uma listagem de grupos pelo nome do grupo
        /// </summary>
        /// <param name="nomeGrupo">Nome a ser pesquisado</param>
        /// <returns>Lista de grupos</returns>
        public List<GruposAcesso> GetListaGruposNome(string nomeGrupo = "")
        {
            List<GruposAcesso> retorno = null;
            GruposAcesso grupoAcesso;
            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_BuscaGruposAcessoNome", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    grupoAcesso = new GruposAcesso();
                    grupoAcesso.CodGrupo = dr["codGrupo"].DefaultDbNull<Int32>(0);
                    grupoAcesso.NomeGrupo = dr["nomeGrupo"].ToString();
                    grupoAcesso.InfoGrupo = dr["infoGrupo"].ToString();
                    grupoAcesso.GrupoPadrao = dr["grupoPadrao"].DefaultDbNull<bool>(0);
                    grupoAcesso.GrupoPadraoCliente = dr["grupoPadraoCliente"].DefaultDbNull<bool>(0);
                    retorno.Add(grupoAcesso);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Retorna entidade pesquisada
        /// </summary>
        /// <param name="GruposAcesso">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public GruposAcesso GetGrupoCodigo(GruposAcesso GruposAcesso)
        {
            GruposAcesso retorno = null;

            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (GruposAcesso.CodGrupo > 0)
            {
                par.Add(new SqlParameter("@codGrupo", GruposAcesso.CodGrupo));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaGruposAcessoCodigo", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new GruposAcesso();
                    retorno.CodGrupo = dr["codGrupo"].DefaultDbNull<Int32>(0);
                    retorno.NomeGrupo = dr["nomeGrupo"].ToString();
                    retorno.InfoGrupo = dr["infoGrupo"].ToString();
                    retorno.GrupoPadrao = dr["grupoPadrao"].DefaultDbNull<bool>(0);
                    retorno.GrupoPadraoCliente = dr["grupoPadraoCliente"].DefaultDbNull<bool>(0);
                }

            }

            Dbase.Desconectar();

            return retorno;

        }

        /// <summary>
        /// Cadastrar GruposAcesso
        /// </summary>
        /// <param name="GruposAcesso">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Inserir(GruposAcesso GruposAcesso)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@nomeGrupo", GruposAcesso.NomeGrupo));
            par.Add(new SqlParameter("@grupoPadrao", GruposAcesso.GrupoPadrao));
            par.Add(new SqlParameter("@InfoGrupo", GruposAcesso.InfoGrupo));
            par.Add(new SqlParameter("@grupoPadraoCliente", GruposAcesso.GrupoPadraoCliente));
            retorno = Dbase.ExecutaProcedure("spc_cadastraGruposAcesso", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar GruposAcesso
        /// </summary>
        /// <param name="GruposAcesso">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Alterar(GruposAcesso GruposAcesso)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@nomeGrupo", GruposAcesso.NomeGrupo));
            par.Add(new SqlParameter("@grupoPadrao", GruposAcesso.GrupoPadrao));
            par.Add(new SqlParameter("@InfoGrupo", GruposAcesso.InfoGrupo));
            par.Add(new SqlParameter("@grupoPadraoCliente", GruposAcesso.GrupoPadraoCliente));
            par.Add(new SqlParameter("@codGrupo", GruposAcesso.CodGrupo));
            retorno = Dbase.ExecutaProcedure("spc_atualizaGruposAcesso", par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Excluir grupos de acesso
        /// </summary>
        /// <param name="codGrupoAcesso"></param>
        /// <returns></returns>
        public bool Excluir(int codGrupoAcesso)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            // Passagem de parametros
            par.Add(new SqlParameter("@codGrupo", codGrupoAcesso));
            retorno = Dbase.ExecutaProcedure("spc_ExcluiGruposAcesso", par);
            Dbase.Desconectar();

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

            par.Add(new SqlParameter("@codGrupo", codigoTipoContato));

            dr = Dbase.GeraReaderProcedure("spc_contaUsoGrupoAcesso", par);

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