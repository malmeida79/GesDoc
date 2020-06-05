using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class GruposUsuarioAcessoController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("gestão de Acessos de usuários a grupos", true);

        /// <summary>
        /// Listar AcessosGrupoUsuario
        /// </summary>
        /// <param name="AcessosGrupo">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<AcessosGrupoUsuario> MontaMenuUsuario(int codUsuario)
        {

            AcessosGrupoUsuario acc;
            List<AcessosGrupoUsuario> retorno = null;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codUsuario", codUsuario));

            dr = Dbase.GeraReaderProcedure("spc_montaMenuGrupoUsuario", par);

            if (dr.HasRows)
            {
                retorno = new List<AcessosGrupoUsuario>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    acc = new AcessosGrupoUsuario();
                    acc.CodUsuario = dr["codUsuario"].DefaultDbNull<Int32>(0);
                    acc.DescricaoDepartamento = acc.DescricaoAcesso = dr["descricaoDepartamento"].ToString();
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
        /// Listagem de grupos que o usuario tem acesso
        /// </summary>
        /// <param name="codUsuario"> Codigo do usuario Consultado</param>
        /// <returns></returns>
        public List<GruposUsuarioAcesso> GetGruposAcessoUsuario(int codUsuario)
        {
            GruposUsuarioAcesso acc;
            List<GruposUsuarioAcesso> retorno = null;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codUsuario", codUsuario));

            dr = Dbase.GeraReaderProcedure("spc_listaGruposAcessoUsuario", par);

            if (dr.HasRows)
            {

                retorno = new List<GruposUsuarioAcesso>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    acc = new GruposUsuarioAcesso();

                    acc.CodGrupo = dr["codGrupo"].DefaultDbNull<Int32>(0);
                    acc.NomeGrupo = dr["nomeGrupo"].ToString();
                    acc.InfoGrupo = dr["infoGrupo"].ToString();
                    acc.GrupoPadrao = dr["grupoPadrao"].DefaultDbNull<bool>(false);
                    acc.GrupoPadraoCliente = dr["codUsuario"].DefaultDbNull<bool>(false);
                    acc.CodUsuario = dr["codUsuario"].DefaultDbNull<Int32>(0);
                    retorno.Add(acc);
                }
            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Libera acesso ao usuario ao grupo selecionado
        /// </summary>
        /// <param name="codigoGrupo">grupo selecionado</param>
        /// <param name="codUsuario">usuario selecionado</param>
        /// <returns> True para sucesso e False para erro</returns>
        public bool Inserir(int codigoGrupo, int codUsuario)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@codgrupo", codigoGrupo));
            par.Add(new SqlParameter("@codUsuario", codUsuario));

            retorno = Dbase.ExecutaProcedure("spc_cadastraAcessoClienteGrupo", par, $"liberado acesso ao grupo:{codigoGrupo.ToString()} ao cliente :{codUsuario.ToString()}.");
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Altera acesso ao usuario ao grupo selecionado
        /// </summary>
        /// <param name="codigoGrupo">grupo selecionado</param>
        /// <param name="codUsuario">usuario selecionado</param>
        /// <returns> True para sucesso e False para erro</returns>
        public bool Alterar(int codigoGrupo, int codUsuario)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@codgrupo", codigoGrupo));
            par.Add(new SqlParameter("@codUsuario", codUsuario));
            retorno = Dbase.ExecutaProcedure("spc_atualizaAcessoClienteGrupo", par, $"Atualizado acesso ao grupo:{codigoGrupo.ToString()} ao cliente :{codUsuario.ToString()}.");
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Exclui acesso ao usuario ao grupo selecionado
        /// </summary>
        /// <param name="codigoGrupo">grupo selecionado</param>
        /// <param name="codUsuario">usuario selecionado</param>
        /// <returns> True para sucesso e False para erro</returns>
        public bool Excluir(int codigoGrupo, int codUsuario)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();
            par.Add(new SqlParameter("@codgrupo", codigoGrupo));
            par.Add(new SqlParameter("@codUsuario", codUsuario));
            retorno = Dbase.ExecutaProcedure("spc_excluiClienteGrupo", par, $"liberado acesso ao grupo:{codigoGrupo.ToString()} ao cliente :{codUsuario.ToString()}.");
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Funcionalides que o usuário tem acesso
        /// </summary>
        /// <param name="codUsuario"> codigo do usuario</param>
        /// <returns> lista de todos as funcionalidades que o usuário tem acesso</returns>
        public List<AcessosGrupoUsuario> GetFuncionalidadesAcessoUsuario(int codUsuario)
        {
            AcessosGrupoUsuario acc;
            List<AcessosGrupoUsuario> retorno = null;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codUsuario", codUsuario));

            dr = Dbase.GeraReaderProcedure("spc_listaFuncionalidadesAcessoUsuario", par);

            if (dr.HasRows)
            {
                retorno = new List<AcessosGrupoUsuario>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    acc = new AcessosGrupoUsuario();
                    acc.CodUsuario = dr["codusuario"].DefaultDbNull<Int32>(0);
                    acc.DescricaoDepartamento = acc.DescricaoAcesso = dr["descricaoDepartamento"].ToString();
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
    }
}