using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class ClientesController
    {
        /// <summary>
        /// Chamada principal da classe de manipulação de dados SQL
        /// </summary>
        private SQLBase Dbase = new SQLBase("Cadastro de clientes");

        /// <summary>
        /// Listar clientes
        /// </summary>
        /// <param name="cliente">Entidade a ser Listada</param>
        /// <returns>lista do tipo da entidade carregada</returns>
        public List<Cliente> GetAll()
        {
           Cliente acc;
            List<Cliente> retorno = null;

            SqlDataReader dr;


            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            dr = Dbase.GeraReaderProcedure("spc_listaClientes", par);

            if (dr.HasRows)
            {

                retorno = new List<Cliente>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    acc = new Cliente();

                    acc.CodCliente = dr["codcliente"].DefaultDbNull<Int32>(0);
                    acc.NomeCliente = dr["nomeCliente"].ToString();

                    retorno.Add(acc);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Retorna entidade pesquisada
        /// </summary>
        /// <param name="cliente">Entidade a ser pesquisada</param>
        /// <returns></returns>
        public Cliente Pesquisar(Cliente cliente)
        {
           Cliente retorno = null;
            return retorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codCliente"></param>
        /// <returns></returns>
        public Cliente PesquisarPorCodigo(int codCliente)
        {
           Cliente retCliente = new Cliente();

           Cliente retorno = null;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codCliente", codCliente));


            dr = Dbase.GeraReaderProcedure("spc_BuscaClienteCodigo", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new Cliente();

                    retorno.CodCliente = dr["codcliente"].DefaultDbNull<Int32>(0);
                    retorno.CodGrupo = dr["codGrupo"].DefaultDbNull<Int32>(0);
                    retorno.NomeCliente = dr["nomeCliente"].ToString();
                    retorno.CpfCnpjCliente = dr["cpfCnpjCliente"].ToString();
                    retorno.RazaoSocialCliente = dr["razaoSocialCliente"].ToString();
                    retorno.Status = dr["status"].DefaultDbNull<bool>(false);
                    retorno.NomeGrupo = dr["nomeGrupo"].ToString();
                    retorno.Deletado = dr["D_E_L_E_T_E_D"].DefaultDbNull<bool>(false);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Listagem de clientes pesquisada
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public List<Cliente> PesquisarLista(Cliente cliente)
        {
            List<Cliente> retCliente = new List<Cliente>();

            List<Cliente> retorno = null;
           Cliente entCli;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            if (cliente.NomeCliente != null)
            {
                par.Add(new SqlParameter("@cliente", cliente.NomeCliente));
            }

            if (cliente.CpfCnpjCliente != null)
            {
                par.Add(new SqlParameter("@cnpj", cliente.CpfCnpjCliente));
            }


            if (cliente.NomeGrupo != null)
            {
                par.Add(new SqlParameter("@grupo", cliente.NomeGrupo));
            }

            if (cliente.CodGrupo > 0)
            {
                par.Add(new SqlParameter("@codGrupo", cliente.CodGrupo));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaCliente", par);

            if (dr.HasRows)
            {
                retorno = new List<Cliente>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    entCli = new Cliente();

                    entCli.CodCliente = dr["codcliente"].DefaultDbNull<Int32>(0);
                    entCli.NomeCliente = dr["nomeCliente"].ToString();
                    entCli.CpfCnpjCliente = dr["cpfCnpjCliente"].ToString();
                    entCli.RazaoSocialCliente = dr["razaoSocialCliente"].ToString();
                    entCli.Status = dr["status"].DefaultDbNull<bool>(false);
                    entCli.NomeGrupo = dr["nomeGrupo"].ToString();
                    entCli.Deletado = dr["D_E_L_E_T_E_D"].DefaultDbNull<bool>(false);

                    retorno.Add(entCli);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastrar cliente
        /// </summary>
        /// <param name="cliente">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Inserir(Cliente cliente)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@nomeCliente", cliente.NomeCliente));
            par.Add(new SqlParameter("@razaoSocialCliente", cliente.RazaoSocialCliente));
            par.Add(new SqlParameter("@cpfCnpjCliente", cliente.CpfCnpjCliente));
            par.Add(new SqlParameter("@codGrupo", cliente.CodGrupo));
            par.Add(new SqlParameter("@status", cliente.Status));

            retorno = Dbase.ExecutaProcedure("spc_cadastraCliente", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Alterar cliente
        /// </summary>
        /// <param name="cliente">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Alterar(Cliente cliente)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codcliente", cliente.CodCliente));
            par.Add(new SqlParameter("@nomeCliente", cliente.NomeCliente));
            par.Add(new SqlParameter("@razaoSocialCliente", cliente.RazaoSocialCliente));
            par.Add(new SqlParameter("@cpfCnpjCliente", cliente.CpfCnpjCliente));
            par.Add(new SqlParameter("@codGrupo", cliente.CodGrupo));
            par.Add(new SqlParameter("@status", cliente.Status));

            retorno = Dbase.ExecutaProcedure("spc_atualizaCliente", par);
            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Excluir cliente
        /// </summary>
        /// <param name="cliente">Entidade a ser cadastrada</param>
        /// <returns>true para sucesso</returns>
        public bool Excluir(Cliente cliente)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codcliente", cliente.CodCliente));

            retorno = Dbase.ExecutaProcedure("spc_excluiCliente", par);
            Dbase.Desconectar();

            return retorno;
        }

    }
}