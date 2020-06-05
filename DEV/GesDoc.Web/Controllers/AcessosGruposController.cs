using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class AcessosGruposController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("gestão de Acessos de grupos", true);

        /// <summary>
        /// Lista os acessos do grupo selecionado
        /// </summary>
        /// <param name="codigoGrupo">codigo do grupo selecionado</param>
        /// <returns>Lista de acesoss do Selecionados</returns>
        public List<AcessosGrupos> GetAcessosGrupo(int codigoGrupo)
        {
            AcessosGrupos acc;
            List<AcessosGrupos> retorno = null;
            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codGrupo", codigoGrupo));

            dr = Dbase.GeraReaderProcedure("spc_listaAcessosGrupos", par);

            if (dr.HasRows)
            {

                retorno = new List<AcessosGrupos>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    acc = new AcessosGrupos();

                    acc.CodGrupo = dr["codGrupoAcesso"].DefaultDbNull<Int32>(0);
                    acc.CodFuncionalidade = dr["codFuncionalidade"].DefaultDbNull<Int32>(0);
                    acc.DescricaoAcesso = dr["descricaoFuncionalidade"].ToString();
                    acc.Gravacao = dr["gravacao"].DefaultDbNull<bool>(false);
                    acc.Leitura = dr["leitura"].DefaultDbNull<bool>(false);
                    acc.Excluir = dr["excluir"].DefaultDbNull<bool>(false);
                    acc.UrlAcesso = dr["urlFuncionalidade"].ToString();
                    acc.ExibeMenu = dr["exibeMenu"].DefaultDbNull<bool>(false);

                    retorno.Add(acc);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Clona acessos do grupo de origem para o grupo de destino
        /// </summary>
        /// <param name="codigoOrigem">Codigo do usuario que serao copiados os acessos</param>
        /// <param name="codigoDestino">Codio do usuario para onde serao atribuidos os acessos</param>
        /// <returns></returns>
        public bool ClonarAcessosGrupo(int codigoOrigem, int codigoDestino)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codOrigem", codigoOrigem));
            par.Add(new SqlParameter("@codDestino", codigoDestino));

            retorno = Dbase.ExecutaProcedure("spc_clonaAcessosGrupo", par);

            return retorno;
        }

        /// <summary>
        /// Retorna entidade pesquisada
        /// </summary>
        /// <param name="AcessosGrupo">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public AcessosGrupos Pesquisar(AcessosGrupos AcessosGrupo)
        {
            AcessosGrupos retorno = null;
            return retorno;
        }

        /// <summary>
        /// Cadastrar AcessosGrupo
        /// </summary>
        /// <param name="AcessosGrupo">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Inserir(AcessosGrupos AcessosGrupo)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@codgrupo", AcessosGrupo.CodGrupo));
            par.Add(new SqlParameter("@codFuncionalidade", AcessosGrupo.CodFuncionalidade));
            par.Add(new SqlParameter("@gravacao", AcessosGrupo.Gravacao));
            par.Add(new SqlParameter("@leitura", AcessosGrupo.Leitura));
            par.Add(new SqlParameter("@excluir", AcessosGrupo.Excluir));

            retorno = Dbase.ExecutaProcedure("spc_cadastraAcessoGrupo", par, $"liberado acesso leitura:{AcessosGrupo.Leitura.ToString()},gravacao:{AcessosGrupo.Gravacao.ToString()},excluir:{AcessosGrupo.Excluir.ToString()}, funcionalidade:{AcessosGrupo.CodFuncionalidade.ToString()} ao grupo:{AcessosGrupo.CodGrupo.ToString()}");
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar AcessosGrupo
        /// </summary>
        /// <param name="AcessosGrupo">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Alterar(AcessosGrupos AcessosGrupo)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@codGrupo", AcessosGrupo.CodGrupo));
            par.Add(new SqlParameter("@codFuncionalidade", AcessosGrupo.CodFuncionalidade));
            par.Add(new SqlParameter("@gravacao", AcessosGrupo.Gravacao));
            par.Add(new SqlParameter("@leitura", AcessosGrupo.Leitura));
            par.Add(new SqlParameter("@excluir", AcessosGrupo.Excluir));
            retorno = Dbase.ExecutaProcedure("spc_atualizaAcessoGrupo", par, $"Alterado acesso leitura:{AcessosGrupo.Leitura.ToString()},gravacao:{AcessosGrupo.Gravacao.ToString()}, funcionalidade:{AcessosGrupo.CodFuncionalidade.ToString()} ao Grupo:{AcessosGrupo.CodGrupo.ToString()}");
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Excluir AcessosGrupo
        /// </summary>
        /// <param name="AcessosGrupo">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Excluir(AcessosGrupos AcessosGrupo)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@codGrupo", AcessosGrupo.CodGrupo));
            par.Add(new SqlParameter("@codFuncionalidade", AcessosGrupo.CodFuncionalidade));
            retorno = Dbase.ExecutaProcedure("spc_excluiAcessoGrupo", par, $"Removido acesso funcionalidade:{AcessosGrupo.CodFuncionalidade.ToString()} ao grupo:{AcessosGrupo.CodGrupo.ToString()}");
            Dbase.Desconectar();

            return retorno;
        }
    }
}