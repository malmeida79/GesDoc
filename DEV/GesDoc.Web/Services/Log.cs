using System.Collections.Generic;
using GesDoc.Models;
using System.Data.SqlClient;

namespace GesDoc.Web.Services
{
    public class Log
    {
        bool histAtivo = Properties.Settings.Default.HistAtivo;
        bool erroAtivo = Properties.Settings.Default.ErroAtivo;
        bool logAtivo = Properties.Settings.Default.LogAtivo;
        SQLBase bd = new SQLBase("LGS", false);

        public UsuarioLogado UsuarioLogado { get;set; }

        /// <summary>
        /// Registra Historico de alteracoes
        /// </summary>
        /// <param name="codUsuario"></param>
        /// <param name="acao"></param>
        /// <param name="tabela"></param>
        /// <param name="chaveAlterada"></param>
        public void RegistraHistorico(string acao, string tabela, int chaveAlterada)
        {
            List<SqlParameter> par = new List<SqlParameter>();

            // caso usuário logado não exista então entendemos que o
            // a falha foi capturada internamente, leva codigo do master
            // pq precisa ter um código relacional para tabela de usuarios
            if (UsuarioLogado == null) {
                UsuarioLogado = new UsuarioLogado();
                UsuarioLogado.codUsuario = 1;
                UsuarioLogado.nomeUsuario = "Application";
                acao = $"log interno:{acao}";
            }

            if (histAtivo)
            {
                bd.Conectar();               

                par.Add(new SqlParameter("@codUsuario", UsuarioLogado.codUsuario));
                par.Add(new SqlParameter("@acao", acao));
                par.Add(new SqlParameter("@tabela", tabela));
                par.Add(new SqlParameter("@idChaveAlterada", chaveAlterada));

                bd.ExecutaProcedure("spc_registraHistorico", par);

                bd.Desconectar();
            }

            this.UsuarioLogado = null;
        }

        /// <summary>
        /// Registra erro ocorrido na execução da aplicação
        /// </summary>
        /// <param name="usuario">Usuario em questão</param>
        /// <param name="area">area que ocorreu o erro</param>
        /// <param name="erro">msg de erro</param>
        public void RegistraErro(string area, string erro)
        {
            if (erroAtivo)
            {
                List<SqlParameter> par = new List<SqlParameter>();

                // caso usuário logado não exista então entendemos que o
                // a falha foi capturada internamente, leva codigo do master
                // pq precisa ter um código relacional para tabela de usuarios
                if (UsuarioLogado == null)
                {
                    UsuarioLogado = new UsuarioLogado();
                    UsuarioLogado.codUsuario = 1;
                    UsuarioLogado.nomeUsuario = "Application";
                    erro = $"log interno:{erro}";
                }

                bd.Conectar();

                par.Add(new SqlParameter("@codUsuario", UsuarioLogado.codUsuario));
                par.Add(new SqlParameter("@Usuario", UsuarioLogado.nomeUsuario));
                par.Add(new SqlParameter("@area", area));
                par.Add(new SqlParameter("@erro", erro));

                bd.ExecutaProcedure("spc_registraErro", par);

                bd.Desconectar();
            }

            this.UsuarioLogado = null;
        }

        /// <summary>
        /// Resgistro de logs do sistema
        /// </summary>
        /// <param name="msgLog">Mensagem a ser registrada</param>
        /// <param name="contaLinhasAfetadas">Qtd de linhas afetadas</param>
        /// <param name="AcaoBancoDeDados">Quando acao de log de banco de dados necessario existirem linhas alteradas para registrar o log.</param>
        public void RegistraLog(string msgLog, string modulo,int contaLinhasAfetadas = 0, bool AcaoBancoDeDados = false)
        {
            if (logAtivo)
            {
                List<SqlParameter> par = new List<SqlParameter>();

                // quando solicitado registrar log da acao realizada pelo banco necessario linhas afetadas, msg 
                // preenchida e informar acao de banco de dados
                if ((msgLog == null || contaLinhasAfetadas == 0) && AcaoBancoDeDados == true)
                {
                    // Não realizar registro se condições nao satisfatorias
                    return;
                }

                // caso usuário logado não exista então entendemos que o
                // a falha foi capturada internamente, leva codigo do master
                // pq precisa ter um código relacional para tabela de usuarios
                if (UsuarioLogado == null)
                {
                    UsuarioLogado = new UsuarioLogado();
                    UsuarioLogado.codUsuario = 1;
                    UsuarioLogado.nomeUsuario = "Application";
                    msgLog = $"log interno:{msgLog}";
                }

                bd.Conectar();

                par.Add(new SqlParameter("@codUsuario", UsuarioLogado.codUsuario));
                par.Add(new SqlParameter("@Usuario", UsuarioLogado.login));
                par.Add(new SqlParameter("@modulo", modulo));
                par.Add(new SqlParameter("@msg", msgLog));

                bd.ExecutaProcedure("spc_registraLog", par);

                bd.Desconectar();

                this.UsuarioLogado = null;
            }
        }
    }
}