using System;
using System.Collections.Generic;
using GesDoc.Models;
using GesDoc.Web.Services;
using System.Data.SqlClient;
using System.Linq;

namespace GesDoc.Web.Controllers
{
    public class UsuarioController
    {

        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Cadastro de usuários", true);

        /// <summary>
        /// Listar usuarios
        /// </summary>
        /// <param name="usuario">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<Usuario> GetAll()
        {
            List<Usuario> retorno = null;
            Usuario usr;
            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaUsuarios", par);

            if (dr.HasRows)
            {
                retorno = new List<Usuario>();

                while (dr.Read())
                {
                    usr = new Usuario();

                    usr.codUsuario = dr["codUsuario"].DefaultDbNull<Int32>(0);
                    usr.nomeUsuario = dr["nomeUsuario"].ToString();
                    usr.sobreNome = dr["sobreNome"].ToString();
                    usr.login = dr["login"].ToString();
                    usr.senha = "******";
                    usr.dataCadastro = dr["dataCadastro"].DefaultDbNull<DateTime?>(null);
                    usr.dataUltimoAcesso = dr["dataUltimoAcesso"].DefaultDbNull<DateTime?>(null);
                    usr.dataAlteracao = dr["dataAlteracao"].DefaultDbNull<DateTime?>(null);
                    usr.Bloqueado = dr["Bloqueado"].DefaultDbNull<bool>(false);
                    usr.Ativo = dr["Ativo"].DefaultDbNull<bool>(true);
                    usr.email = dr["email"].ToString();
                    usr.TipoCliente = dr["TipoCliente"].DefaultDbNull<bool>(true);
                    usr.AssinaDocumento = dr["AssinaDocumento"].DefaultDbNull<bool>(false);
                    usr.LiberaDocumento = dr["LiberaDocumento"].DefaultDbNull<bool>(false);
                    usr.deletado = dr["D_E_L_E_T_E_D"].DefaultDbNull<bool>(false);

                    retorno.Add(usr);

                }

            }

            Dbase.Desconectar();

            return retorno;

        }

        /// <summary>
        /// Listar usuarios
        /// </summary>
        /// <param name="usuario">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<Usuario> ListarCompleta()
        {
            List<Usuario> retorno = null;
            Usuario usr;
            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaUsuariosCompleta", par);

            if (dr.HasRows)
            {
                retorno = new List<Usuario>();

                while (dr.Read())
                {
                    usr = new Usuario();

                    usr.codUsuario = dr["codUsuario"].DefaultDbNull<Int32>(0);
                    usr.nomeUsuario = dr["nomeUsuario"].ToString();
                    usr.sobreNome = dr["sobreNome"].ToString();
                    usr.login = dr["login"].ToString();
                    usr.senha = "******";
                    usr.dataCadastro = dr["dataCadastro"].DefaultDbNull<DateTime?>(null);
                    usr.dataUltimoAcesso = dr["dataUltimoAcesso"].DefaultDbNull<DateTime?>(null);
                    usr.dataAlteracao = dr["dataAlteracao"].DefaultDbNull<DateTime?>(null);
                    usr.Bloqueado = dr["Bloqueado"].DefaultDbNull<bool>(false);
                    usr.Ativo = dr["Ativo"].DefaultDbNull<bool>(true);
                    usr.email = dr["email"].ToString();
                    usr.TipoCliente = dr["TipoCliente"].DefaultDbNull<bool>(true);
                    usr.AssinaDocumento = dr["AssinaDocumento"].DefaultDbNull<bool>(false);
                    usr.LiberaDocumento = dr["LiberaDocumento"].DefaultDbNull<bool>(false);
                    usr.deletado = dr["D_E_L_E_T_E_D"].DefaultDbNull<bool>(false);

                    retorno.Add(usr);

                }

            }

            Dbase.Desconectar();

            return retorno;

        }

        /// <summary>
        /// Retorna entidade pesquisada
        /// </summary>
        /// <param name="usuario">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public Usuario Pesquisar(Usuario usuario = null, string parametro = "", bool PesquisaPorEmail = false)
        {
            Usuario retorno = null;
            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (usuario != null)
            {
                if (usuario.codUsuario > 0)
                {
                    par.Add(new SqlParameter("@codUsuario", usuario.codUsuario));
                }

                dr = Dbase.GeraReaderProcedure("spc_BuscaUsuarioCodigo", par);
            }
            else
            {
                if (PesquisaPorEmail)
                {
                    par.Add(new SqlParameter("@email", parametro));
                    dr = Dbase.GeraReaderProcedure("spc_BuscaUsuarioEmail", par);
                }
                else
                {
                    par.Add(new SqlParameter("@nome", parametro));
                    dr = Dbase.GeraReaderProcedure("spc_BuscaUsuarioNome", par);
                }
            }

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new Usuario();

                    retorno.codUsuario = dr["codUsuario"].DefaultDbNull<Int32>(0);
                    retorno.nomeUsuario = dr["nomeUsuario"].ToString();
                    retorno.sobreNome = dr["sobreNome"].ToString();
                    retorno.login = dr["login"].ToString();
                    retorno.senha = dr["senha"].ToString();
                    retorno.dataCadastro = dr["dataCadastro"].DefaultDbNull<DateTime?>(null);
                    retorno.dataUltimoAcesso = dr["dataUltimoAcesso"].DefaultDbNull<DateTime?>(null);
                    retorno.Bloqueado = dr["Bloqueado"].DefaultDbNull<bool>(false);
                    retorno.Ativo = dr["Ativo"].DefaultDbNull<bool>(true);
                    retorno.email = dr["email"].ToString();
                    retorno.TipoCliente = dr["TipoCliente"].DefaultDbNull<bool>(true);
                    retorno.AssinaDocumento = dr["AssinaDocumento"].DefaultDbNull<bool>(false);
                    retorno.LiberaDocumento = dr["AssinaDocumento"].DefaultDbNull<bool>(false);
                    retorno.deletado = dr["D_E_L_E_T_E_D"].DefaultDbNull<bool>(false);
                }
            }

            Dbase.Desconectar();

            return retorno;
        }

        public bool Reativar(int codUsuario)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codUsuario", codUsuario));

            retorno = Dbase.ExecutaProcedure("spc_reativaUsuario", par, "reativação de usuário");

            return retorno;
        }

        /// <summary>
        /// Retorna lista da entidade pesquisada
        /// </summary>
        /// <param name="usuario">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public List<Usuario> PesquisarLista(Usuario usuario = null, string parametro = "", bool PesquisaPorEmail = false)
        {
            List<Usuario> retorno = null;
            Usuario usr;
            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();

            if (usuario != null)
            {
                if (usuario.codUsuario > 0)
                {
                    par.Add(new SqlParameter("@codUsuario", usuario.codUsuario));
                }

                dr = Dbase.GeraReaderProcedure("spc_BuscaUsuarioCodigo", par);
            }
            else
            {

                if (PesquisaPorEmail)
                {
                    par.Add(new SqlParameter("@email", parametro));
                    dr = Dbase.GeraReaderProcedure("spc_BuscaUsuarioEmail", par);
                }
                else
                {
                    par.Add(new SqlParameter("@nome", parametro));
                    dr = Dbase.GeraReaderProcedure("spc_BuscaUsuarioNome", par);
                }
            }

            if (dr.HasRows)
            {
                retorno = new List<Usuario>();
                while (dr.Read())
                {
                    usr = new Usuario();
                    usr.codUsuario = dr["codUsuario"].DefaultDbNull<Int32>(0);
                    usr.nomeUsuario = dr["nomeUsuario"].ToString();
                    usr.sobreNome = dr["sobreNome"].ToString();
                    usr.login = dr["login"].ToString();
                    usr.senha = dr["senha"].ToString();
                    usr.dataCadastro = dr["dataCadastro"].DefaultDbNull<DateTime?>(null);
                    usr.dataUltimoAcesso = dr["dataUltimoAcesso"].DefaultDbNull<DateTime?>(null);
                    usr.Bloqueado = dr["Bloqueado"].DefaultDbNull<bool>(false);
                    usr.Ativo = dr["Ativo"].DefaultDbNull<bool>(true);
                    usr.email = dr["email"].ToString();
                    usr.TipoCliente = dr["TipoCliente"].DefaultDbNull<bool>(true);
                    usr.AssinaDocumento = dr["AssinaDocumento"].DefaultDbNull<bool>(false);
                    usr.LiberaDocumento = dr["AssinaDocumento"].DefaultDbNull<bool>(false);
                    usr.deletado = dr["D_E_L_E_T_E_D"].DefaultDbNull<bool>(false);
                    retorno.Add(usr);
                }
            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastrar usuario
        /// </summary>
        /// <param name="usuario">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Inserir(Usuario usuario)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@nomeUsuario", usuario.nomeUsuario));
            par.Add(new SqlParameter("@sobreNome", usuario.sobreNome));
            par.Add(new SqlParameter("@login", usuario.login));
            par.Add(new SqlParameter("@senha", usuario.senha));
            par.Add(new SqlParameter("@email", usuario.email));
            par.Add(new SqlParameter("@bloqueado", usuario.Bloqueado));
            par.Add(new SqlParameter("@ativo", usuario.Ativo));
            par.Add(new SqlParameter("@TipoCliente", usuario.TipoCliente));
            par.Add(new SqlParameter("@AssinaDocumento", usuario.AssinaDocumento));
            par.Add(new SqlParameter("@LiberaDocumento", usuario.LiberaDocumento));

            retorno = Dbase.ExecutaProcedure("spc_cadastraUsuario", par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar usuario
        /// </summary>
        /// <param name="usuario">Entidade a ser alterada</param>
        /// <returns>true para sucesso</returns>
        public bool Alterar(Usuario usuario)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@nomeUsuario", usuario.nomeUsuario));
            par.Add(new SqlParameter("@sobreNome", usuario.sobreNome));
            par.Add(new SqlParameter("@login", usuario.login));
            par.Add(new SqlParameter("@senha", usuario.senha));
            par.Add(new SqlParameter("@email", usuario.email));
            par.Add(new SqlParameter("@bloqueado", usuario.Bloqueado));
            par.Add(new SqlParameter("@ativo", usuario.codUsuario));
            par.Add(new SqlParameter("@codUsuario", usuario.codUsuario));
            par.Add(new SqlParameter("@TipoCliente", usuario.TipoCliente));
            par.Add(new SqlParameter("@AssinaDocumento", usuario.AssinaDocumento));
            par.Add(new SqlParameter("@LiberaDocumento", usuario.LiberaDocumento));

            retorno = Dbase.ExecutaProcedure("spc_atualizaUsuario", par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar senha de um usuário logado
        /// </summary>
        /// <param name="codUsuario"></param>
        /// <param name="novaSenha"></param>
        /// <returns></returns>
        public bool AlterarSenha(int codUsuario, string novaSenha)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codUsuario", codUsuario));
            par.Add(new SqlParameter("@senha", novaSenha));

            retorno = Dbase.ExecutaProcedure("spc_atualizaSenhaUsuario", par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// REcuperar a senha de um usuario atraves do email
        /// </summary>
        /// <param name="email">Email para pesquisar a senha</param>
        /// <returns></returns>
        public string RecuperaSenha(string email)
        {

            string retorno = "";
            List<SqlParameter> par = new List<SqlParameter>();
            SqlDataReader dr;

            Dbase.Conectar();
            par.Add(new SqlParameter("@email", email));
            dr = Dbase.GeraReaderProcedure("spc_BuscaSenhaEmail", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = dr["senha"].ToString();
                }
            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Excluir usuario
        /// </summary>
        /// <param name="usuario">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Excluir(Usuario usuario)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codUsuario", usuario.codUsuario));

            retorno = Dbase.ExecutaProcedure("spc_ExcluiUsuario", par, "Exclusao de usuário");

            return retorno;
        }

        /// <summary>
        /// bloquear usuario
        /// </summary>
        /// <param name="usuario">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool BloquearUsuario(Usuario usuario)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codUsuario", usuario.codUsuario));

            retorno = Dbase.ExecutaProcedure("spc_bloqueiaUsuario", par);

            return retorno;
        }

        /// <summary>
        /// desbloquear usuario
        /// </summary>
        /// <param name="usuario">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool DesBloquearUsuario(Usuario usuario)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codUsuario", usuario.codUsuario));

            retorno = Dbase.ExecutaProcedure("spc_desbloqueiaUsuario", par);

            return retorno;
        }

        /// <summary>
        /// Clona acessos do usuario de origem para o usuario destino
        /// </summary>
        /// <param name="codigoOrigem">Codigo do usuario que serao copiados os acessos</param>
        /// <param name="codigoDestino">Codio do usuario para onde serao atribuidos os acessos</param>
        /// <returns></returns>
        public bool ClonarAcessosUsuario(int codigoOrigem, int codigoDestino)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codOrigem", codigoOrigem));
            par.Add(new SqlParameter("@codDestino", codigoDestino));

            retorno = Dbase.ExecutaProcedure("spc_clonaAcessosUsuario", par);

            return retorno;
        }

        /// <summary>
        /// desbloquear lista usuarios
        /// </summary>
        /// <param name="usuario">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool DesBloquearListaUsuarios(List<Usuario> listaUsuarios)
        {
            bool retorno = false;

            foreach (Usuario usr in listaUsuarios)
            {
                retorno = DesBloquearUsuario(usr);
            }

            return retorno;
        }

        /// <summary>
        /// bloquear lista usuarios
        /// </summary>
        /// <param name="usuario">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool BloquearListaUsuarios(List<Usuario> listaUsuarios)
        {
            bool retorno = false;

            foreach (Usuario usr in listaUsuarios)
            {
                retorno = BloquearUsuario(usr);
            }

            return retorno;
        }

        /// <summary>
        /// Valida usuario e login existentes para impedir duplicidades
        /// </summary>
        /// <param name="nomeUsuario"></param>
        /// <param name="loginUsuario"></param>
        /// <returns></returns>
        public int ValidaExistenciaUsuario(string nomeUsuario, string loginUsuario, int codUsuario = 0)
        {

            int retorno = 0;

            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@nomeUsuario", nomeUsuario));
            par.Add(new SqlParameter("@login", loginUsuario));

            if (codUsuario > 0)
            {
                par.Add(new SqlParameter("@codusuario", codUsuario));
            }

            dr = Dbase.GeraReaderProcedure("spc_validaExistenciaUsuario", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = dr["CONTAGEM"].DefaultDbNull<Int32>(0);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }


        /// <summary>
        /// Busca codigo do usuario atraves do login e senha
        /// </summary>
        /// <param name="login">login do usuario</param>
        /// <param name="senha">senha usuario</param>
        /// <returns>Codigo do usuario</returns>
        public int BuscaCodigoUsuario(string login, string senha)
        {
            SqlDataReader dr;
            int retorno = 0;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@login", login));
            par.Add(new SqlParameter("@senha", senha));

            dr = Dbase.GeraReaderProcedure("spc_buscaCodigoUsuariocadastrado", par);

            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    //configura o objeto usuario logado
                    retorno = dr["codUsuario"].DefaultDbNull<Int32>(0);
                }
            }

            Dbase.Desconectar();

            return retorno;
        }


        public UsuarioLogado Logar(Usuario usuario)
        {
            SqlDataReader dr;
            UsuarioLogado usuarioLogado = new UsuarioLogado();
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@login", usuario.login));
            par.Add(new SqlParameter("@senha", usuario.senha));

            dr = Dbase.GeraReaderProcedure("spc_loginUsuario", par);

            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    //configura o objeto usuario logado
                    usuarioLogado.codUsuario = dr["codUsuario"].DefaultDbNull<Int32>(0);
                    usuarioLogado.Ativo = true;
                    usuarioLogado.Bloqueado = false;
                    usuarioLogado.nomeUsuario = dr["nomeUsuario"].ToString();
                    usuarioLogado.sobreNome = dr["sobreNome"].ToString();
                    usuarioLogado.login = dr["login"].ToString();
                    usuarioLogado.email = dr["nomeUsuario"].ToString();
                    usuarioLogado.senha = dr["senha"].ToString();
                    usuarioLogado.Tentativas = 0;
                    usuarioLogado.TipoCliente = dr["TipoCliente"].DefaultDbNull<bool>(true);
                    usuarioLogado.AssinaDocumento = dr["AssinaDocumento"].DefaultDbNull<bool>(false);
                    usuarioLogado.LiberaDocumento = dr["LiberaDocumento"].DefaultDbNull<bool>(false);
                    usuarioLogado.deletado = dr["D_E_L_E_T_E_D"].DefaultDbNull<bool>(false);

                    // validando se usuario ativo e com acesso
                    if (usuarioLogado.Ativo == true)
                    {
                        usuario.codUsuario = dr["codUsuario"].DefaultDbNull<Int32>(0);

                        // recuperando acessos do usuario e montando menu
                        GruposUsuarioAcessoController dalAcc = new GruposUsuarioAcessoController();
                        UsuarioGrupoClienteController dalUSGpCli = new UsuarioGrupoClienteController();

                        // Montando o menu do usuário
                        usuarioLogado.GETMenuUsuario = dalAcc.MontaMenuUsuario(usuario.codUsuario);

                        // recuperando acessos do usuário
                        usuarioLogado.GETFuncionalidesAcessos = dalAcc.GetFuncionalidadesAcessoUsuario(usuario.codUsuario);

                        // recuperando grupos que o usuário tem acesso
                        usuarioLogado.GETGruposAcessos = dalAcc.GetGruposAcessoUsuario(usuario.codUsuario);

                        // Somente para funcionarios
                        if (!usuarioLogado.TipoCliente)
                        {
                            // tipo de recados internos
                            usuarioLogado.codtipoRecado = 1;
                        }
                        else
                        {
                            // Tipo recado cliente
                            usuarioLogado.codtipoRecado = 2;
                        }

                        // tratamento especial para usuario master e definicao de cores
                        if (usuarioLogado.login.ToLower() == "master")
                        {
                            usuarioLogado.TextoLabel = $"*** {usuarioLogado.nomeUsuario.ToUpper()} - MASTER ***";
                            usuarioLogado.CorLabel = System.Drawing.Color.Yellow;
                            usuarioLogado.GETDadosAcessosGrupoClientes = dalUSGpCli.Pesquisar();
                        }
                        else
                        {
                            usuarioLogado.TextoLabel = $"Bem vindo(a), {usuarioLogado.nomeUsuario}";
                            usuarioLogado.CorLabel = System.Drawing.Color.White;

                            // buscando todos os dados de acesso a grupos e clientes desse usuario
                            usuarioLogado.GETDadosAcessosGrupoClientes = dalUSGpCli.Pesquisar(usuarioLogado.codUsuario);

                            if (usuarioLogado.GETDadosAcessosGrupoClientes != null)
                            {

                                // buscando todos os grupos que ele tem acesso
                                usuarioLogado.GETCodGruposAcesso = usuarioLogado.GETDadosAcessosGrupoClientes.Select(o => o.codGrupo).Distinct().ToList();

                                // buscando todos os clientes que ele tem acesso
                                usuarioLogado.GETClientesAcesso = usuarioLogado.GETDadosAcessosGrupoClientes.Select(o => o.codCliente).Distinct().ToList();
                            }
                        }

                        dalAcc = null;
                        dalUSGpCli = null;
                    }
                }

            }
            else
            {
                usuarioLogado = null;
            }

            Dbase.Desconectar();

            return usuarioLogado;
        }
    }
}