using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class EnderecoClienteController
    {
        private SQLBase Dbase = new SQLBase("Cadastro de endereços do cliente");

        /// <summary>
        /// Lista de EnderecoClientes
        /// </summary>
        /// <param name="EnderecoCliente">Entidade EnderecoCliente</param>
        /// <returns>Lista de EnderecoClientes</returns>
        public List<EnderecoCliente> GetAll()
        {
            List<EnderecoCliente> retEnderecoCliente = new List<EnderecoCliente>();


            return retEnderecoCliente;
        }

        /// <summary>
        /// Lista de EnderecoClientes pesquisados
        /// </summary>
        /// <param name="EnderecoCliente">Entidade EnderecoCliente</param>
        /// <returns>Lista de EnderecoClientes pesquisados</returns>
        public List<EnderecoCliente> Pesquisar(EnderecoCliente EnderecoCliente)
        {
            List<EnderecoCliente> retEnderecoCliente = new List<EnderecoCliente>();

            List<EnderecoCliente> retorno = null;
           EnderecoCliente entEnd;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            if (EnderecoCliente.CepEndereco != null)
            {
                par.Add(new SqlParameter("@cep", EnderecoCliente.CepEndereco));
            }

            if (EnderecoCliente.DescricaoEndereco != null)
            {
                par.Add(new SqlParameter("@parteEnderecoCliente", EnderecoCliente.CodEnderecoCliente));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaEnderecoClienteCepParteNome",  par);

            if (dr.HasRows)
            {
                retorno = new List<EnderecoCliente>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    entEnd = new EnderecoCliente();

                    entEnd.CodEnderecoCliente =  dr["codEnderecoCliente"].DefaultDbNull<Int32>(0);
                    entEnd.CodCliente = dr["codcliente"].DefaultDbNull<Int32>(0);
                    entEnd.CodEndereco = dr["codEndereco"].DefaultDbNull<Int32>(0);
                    entEnd.DescricaoLogradouro = dr["descricaoLogradouro"].ToString();
                    entEnd.DescricaoEndereco = dr["descricaoEndereco"].ToString();
                    entEnd.CepEndereco = dr["cepEndereco"].ToString();
                    entEnd.Numero = dr["numero"].ToString();
                    entEnd.Complemento = dr["complemento"].ToString();
                    entEnd.Referencia = dr["referencia"].ToString();
                    entEnd.DescricaoBairro = dr["descricaoBairro"].ToString();
                    entEnd.DescricaoCidade = dr["descricaoCidade"].ToString();
                    entEnd.DescricaoEstado= dr["descricaoEstado"].ToString();
                    entEnd.Descricaopais = dr["descricaopais"].ToString();
                    entEnd.UFEstado = dr["ufestado"].ToString();
                    entEnd.DescricaoTipoEndereco = dr["descricaoTipoEndereco"].ToString();
                    entEnd.CodTipoEndereco = dr["codTipoEndereco"].DefaultDbNull<Int32>(0);
                    retorno.Add(entEnd);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Contagem de quantos endereços estão definidos como default do cliente
        /// </summary>
        /// <param name="codCliente">Codigo do cliente para verificação</param>
        /// <returns>Quantidade de endereços definidos como default</returns>
        public int ContaEnderecosDefaultCliente(int codCliente) {

            int retorno = 0;

            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codCliente", codCliente));

            dr = Dbase.GeraReaderProcedure("spc_contaEnderecosDefaultCliente",  par);

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

        /// <summary>
        /// Pesquisa endereços do cliente pelo codigo do cliente
        /// </summary>
        /// <param name="codCliente"> Codigo do cliente para  qual se deseja o endereço.</param>
        /// <returns>Lista de endereços para o cliente</returns>
        public List<EnderecoCliente> PesquisarPorCodigoCliente(int codCliente)
        {            
            List<EnderecoCliente> retorno = null;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codCliente", codCliente));


            dr = Dbase.GeraReaderProcedure("spc_BuscaEnderecoClienteCodigoCliente",  par);

            if (dr.HasRows)
            {
                retorno = new List<EnderecoCliente>();
                while (dr.Read())
                {
                   EnderecoCliente EnderecoCliente = new EnderecoCliente();
                    EnderecoCliente.CodEnderecoCliente = dr["codEnderecoCliente"].DefaultDbNull<Int32>(0);
                    EnderecoCliente.CodCliente = dr["codcliente"].DefaultDbNull<Int32>(0);
                    EnderecoCliente.CodEndereco = dr["codEndereco"].DefaultDbNull<Int32>(0);
                    EnderecoCliente.DescricaoLogradouro = dr["descricaoLogradouro"].ToString();
                    EnderecoCliente.DescricaoEndereco = dr["descricaoEndereco"].ToString();
                    EnderecoCliente.CepEndereco = dr["cepEndereco"].ToString();
                    EnderecoCliente.Numero = dr["numero"].ToString();
                    EnderecoCliente.Complemento = dr["complemento"].ToString();
                    EnderecoCliente.Referencia = dr["referencia"].ToString();
                    EnderecoCliente.DescricaoBairro = dr["descricaoBairro"].ToString();
                    EnderecoCliente.DescricaoCidade = dr["descricaoCidade"].ToString();
                    EnderecoCliente.DescricaoEstado = dr["descricaoEstado"].ToString();
                    EnderecoCliente.Descricaopais = dr["descricaopais"].ToString();
                    EnderecoCliente.UFEstado = dr["ufestado"].ToString();
                    EnderecoCliente.DescricaoTipoEndereco = dr["descricaoTipoEndereco"].ToString();
                    EnderecoCliente.CodTipoEndereco = dr["codTipoEndereco"].DefaultDbNull<Int32>(0);
                    retorno.Add(EnderecoCliente);
                    EnderecoCliente = null;
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Pesquisa endereços do cliente pelo codigo do endereço para o cliente
        /// </summary>
        /// <param name="EnderecoCliente"></param>
        /// <returns>endereço pesquisado</returns>
        public EnderecoCliente PesquisarPorCodigoEndereco(int codEndereco)
        {
           EnderecoCliente retorno = null;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codigoEndereco", codEndereco));


            dr = Dbase.GeraReaderProcedure("spc_BuscaEnderecoClienteCodigoEndereco",  par);

            if (dr.HasRows)
            {
                retorno = new EnderecoCliente();
                while (dr.Read())
                {
                    retorno.CodEnderecoCliente = dr["codEnderecoCliente"].DefaultDbNull<Int32>(0);
                    retorno.CodCliente = dr["codcliente"].DefaultDbNull<Int32>(0);
                    retorno.CodEndereco = dr["codEndereco"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoLogradouro = dr["descricaoLogradouro"].ToString();
                    retorno.DescricaoEndereco = dr["descricaoEndereco"].ToString();
                    retorno.CepEndereco = dr["cepEndereco"].ToString();
                    retorno.Numero = dr["numero"].ToString();
                    retorno.Complemento = dr["complemento"].ToString();
                    retorno.Referencia = dr["referencia"].ToString();
                    retorno.DescricaoBairro = dr["descricaoBairro"].ToString();
                    retorno.DescricaoCidade = dr["descricaoCidade"].ToString();
                    retorno.DescricaoEstado = dr["descricaoEstado"].ToString();
                    retorno.Descricaopais = dr["descricaopais"].ToString();
                    retorno.UFEstado = dr["ufestado"].ToString();
                    retorno.DescricaoTipoEndereco = dr["descricaoTipoEndereco"].ToString();
                    retorno.CodTipoEndereco = dr["codTipoEndereco"].DefaultDbNull<Int32>(0);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Grava dados do EnderecoCliente
        /// </summary>
        /// <param name="EnderecoCliente">Entidade EnderecoCliente</param>
        /// <returns>Gravacao dos dados do EnderecoCliente</returns>
        public bool Alterar(EnderecoCliente EnderecoCliente)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codEnderecoCliente", EnderecoCliente.CodEnderecoCliente));
            par.Add(new SqlParameter("@codcliente", EnderecoCliente.CodCliente));
            par.Add(new SqlParameter("@codEndereco", EnderecoCliente.CodEndereco));
            par.Add(new SqlParameter("@numero", EnderecoCliente.Numero));
            par.Add(new SqlParameter("@complemento", EnderecoCliente.Complemento));
            par.Add(new SqlParameter("@referencia", EnderecoCliente.Referencia));
            par.Add(new SqlParameter("@codTipoEndereco", EnderecoCliente.CodTipoEndereco));

            retorno = Dbase.ExecutaProcedure("spc_atualizaEnderecoCliente",  par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastra dados do EnderecoCliente
        /// </summary>
        /// <param name="EnderecoCliente">Entidade EnderecoCliente</param>
        /// <returns>Retorna sucesso ou falha</returns>
        public bool Inserir(EnderecoCliente EnderecoCliente)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codcliente", EnderecoCliente.CodCliente));
            par.Add(new SqlParameter("@codEndereco", EnderecoCliente.CodEndereco));
            par.Add(new SqlParameter("@numero", EnderecoCliente.Numero));
            par.Add(new SqlParameter("@complemento", EnderecoCliente.Complemento));
            par.Add(new SqlParameter("@referencia", EnderecoCliente.Referencia));
            par.Add(new SqlParameter("@codTipoEndereco", EnderecoCliente.CodTipoEndereco));

            retorno = Dbase.ExecutaProcedure("spc_cadastraEnderecoCliente",  par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Exclui dados do EnderecoCliente
        /// </summary>
        /// <param name="EnderecoCliente">Entidade EnderecoCliente</param>
        /// <returns>Retorna sucesso ou falha</returns>
        public bool Excluir(EnderecoCliente EnderecoCliente)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codCliente", EnderecoCliente.CodCliente));
            par.Add(new SqlParameter("@codEnderecoCliente", EnderecoCliente.CodEnderecoCliente));

            retorno = Dbase.ExecutaProcedure("spc_excluiEnderecoCliente",  par);

            return retorno;
        }
    }
}
