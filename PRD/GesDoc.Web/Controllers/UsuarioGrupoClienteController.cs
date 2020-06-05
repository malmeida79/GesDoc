using System;
using System.Collections.Generic;
using GesDoc.Models;
using GesDoc.Web.Services;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class UsuarioGrupoClienteController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Controle de acessos grupo usuário");

        /// <summary>
        /// Listar usuarios
        /// </summary>
        public List<UsuarioGrupoCliente> GetAll()
        {
            List<UsuarioGrupoCliente> retorno = null;
            UsuarioGrupoCliente usr;
            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_BuscaGrupoClienteAcessousuario", par);

            if (dr.HasRows)
            {
                retorno = new List<UsuarioGrupoCliente>();

                while (dr.Read())
                {
                    usr = new UsuarioGrupoCliente();

                    usr.codUsuario = dr["codUsuario"].DefaultDbNull<Int32>(0);
                    usr.codCliente = dr["codCliente"].DefaultDbNull<Int32>(0);
                    usr.nomeCliente = dr["nomeCliente"].ToString();
                    usr.usuarioCadastro = dr["usuarioCadastro"].DefaultDbNull<Int32>(0);
                    usr.dataCadastro = dr["dataCadastro"].DefaultDbNull<DateTime?>(null);

                    retorno.Add(usr);
                }

            }

            Dbase.Desconectar();

            return retorno;

        }

        /// <summary>
        /// Pesquisa lista de grupos que o usuario pode acessar
        /// </summary>
        /// <param name="codUsuario"></param>
        /// <returns></returns>
        public List<UsuarioGrupoCliente> Pesquisar(int codUsuario = 0, int codigoGrupo = 0)
        {
            List<UsuarioGrupoCliente> retorno = null;
            UsuarioGrupoCliente usr;
            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (codUsuario > 0)
            {
                par.Add(new SqlParameter("@codUsuario", codUsuario));
            }

            if (codigoGrupo > 0)
            {
                par.Add(new SqlParameter("@codGrupo", codigoGrupo));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaGrupoClienteAcessoUsuario", par);

            if (dr.HasRows)
            {
                retorno = new List<UsuarioGrupoCliente>();

                while (dr.Read())
                {
                    usr = new UsuarioGrupoCliente();

                    usr.codUsuario = dr["codUsuario"].DefaultDbNull<Int32>(0);
                    usr.codCliente = dr["codCliente"].DefaultDbNull<Int32>(0);
                    usr.codGrupo = dr["codGrupo"].DefaultDbNull<Int32>(0);
                    usr.nomeCliente = dr["nomeCliente"].ToString();
                    usr.usuarioCadastro = dr["usuarioCadastro"].DefaultDbNull<Int32>(0);
                    usr.dataCadastro = dr["dataCadastro"].DefaultDbNull<DateTime?>(null);
                    usr.consultaGrupo = dr["consultaGrupo"].DefaultDbNull<bool>(false);

                    retorno.Add(usr);

                }

            }

            Dbase.Desconectar();

            return retorno;

        }

        /// <summary>
        /// Devolve uma string com todos os nomes dos grupos do usuario
        /// </summary>
        /// <param name="codUsuario"></param>
        /// <returns></returns>
        public string BuscaGruposUsuarioAcesso(int codUsuario)
        {

            string retorno = string.Empty;
            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            // so adiciona se tiver grupo selecionado
            if (codUsuario > 0)
            {
                par.Add(new SqlParameter("@codUsuario", codUsuario));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaGruposClienteAcesso", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = dr["grupos"].ToString();
                }
                if (string.IsNullOrEmpty(retorno))  
                {
                    retorno = "Nada selecionado!";
                }
            }
            else
            {
                retorno = "Nada selecionado!";
            }

            Dbase.Desconectar();

            return retorno;
        }

        ///// <summary>
        ///// Busca a situaçãop de consulta de um cliente em relacao ao grupo
        ///// </summary>
        ///// <param name="codUsuario"></param>
        ///// <param name="codigoGrupo"></param>
        ///// <returns></returns>
        //public bool BuscaConsultaGrupoCliente(int codigoGrupo = 0, int codUsuario = 0)
        //{

        //    bool retorno = false;
        //    List<SqlParameter> par = new List<SqlParameter>();
        //    SqlDataReader dr;

        //    Dbase.Conectar();

        //    // so adiciona se tiver grupo selecionado
        //    if (codUsuario > 0)
        //    {
        //        par.Add(new SqlParameter("@codUsuario", codUsuario));
        //    }

        //    if (codigoGrupo > 0)
        //    {
        //        par.Add(new SqlParameter("@codGrupo", codigoGrupo));
        //    }

        //    dr = Dbase.GeraReaderProcedure("spc_BuscaConsultaGrupoCliente ", par);

        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            retorno = dr["consultagrupo"].DefaultDbNull<bool>(false);
        //        }
        //    }
        //    else
        //    {
        //        retorno = false;
        //    }

        //    Dbase.Desconectar();

        //    return retorno;
        //}

        /// <summary>
        /// Listagem de clientes para o grupo selecionado
        /// </summary>
        /// <param name="codigoGrupo"></param>
        /// <returns></returns>
        public List<UsuarioGrupoCliente> ListarClientesGrupo(int codigoGrupo = 0)
        {
            List<UsuarioGrupoCliente> retorno = null;
            UsuarioGrupoCliente usr;
            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            // so adiciona se tiver grupo selecionado
            if (codigoGrupo > 0)
            {
                par.Add(new SqlParameter("@codGrupo", codigoGrupo));
            }

            dr = Dbase.GeraReaderProcedure("spc_listaClientesGrupo", par);

            if (dr.HasRows)
            {
                retorno = new List<UsuarioGrupoCliente>();

                while (dr.Read())
                {
                    usr = new UsuarioGrupoCliente();

                    usr.codGrupo = dr["codGrupo"].DefaultDbNull<Int32>(0);
                    usr.codCliente = dr["codCliente"].DefaultDbNull<Int32>(0);
                    usr.nomeCliente = dr["nomeCliente"].ToString();
                    retorno.Add(usr);

                }

            }

            Dbase.Desconectar();

            return retorno;

        }

        /// <summary>
        /// Cadastro de grupos e clientes para o usuário
        /// </summary>
        /// <param name="UsuarioCliente"></param>
        /// <returns></returns>
        public bool Remover(UsuarioGrupoCliente UsuarioCliente)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codgrupo", UsuarioCliente.codGrupo));
            par.Add(new SqlParameter("@codusuario", UsuarioCliente.codUsuario));  
            retorno = Dbase.ExecutaProcedure("spc_removeGrupoClienteUsuario", par);

            return retorno;
        }

        /// <summary>
        /// Cadastro de grupos e clientes para o usuário
        /// </summary>
        /// <param name="UsuarioCliente"></param>
        /// <returns></returns>
        public bool Inserir(UsuarioGrupoCliente UsuarioCliente)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codcliente", UsuarioCliente.codCliente));
            par.Add(new SqlParameter("@codgrupo", UsuarioCliente.codGrupo));
            par.Add(new SqlParameter("@codusuario", UsuarioCliente.codUsuario));
            par.Add(new SqlParameter("@codUsuarioCad", UsuarioCliente.usuarioCadastro));
            par.Add(new SqlParameter("@PesquisaGrupo", UsuarioCliente.consultaGrupo));

            retorno = Dbase.ExecutaProcedure("spc_acessoGrupoClienteUsuario", par);

            return retorno;
        }
    }
}