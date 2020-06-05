using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GesDoc.Web.Controllers
{
    public class EnderecoController
    {
        private SQLBase Dbase = new SQLBase("Cadastro de Endereço");

        /// <summary>
        /// Lista de Enderecos
        /// </summary>
        /// <param name="Endereco">Entidade Endereco</param>
        /// <returns>Lista de Enderecos</returns>
        public List<Endereco> GetAll()
        {
            List<Endereco> retEndereco = new List<Endereco>();


            return retEndereco;
        }

        /// <summary>
        /// Lista de Enderecos pesquisados
        /// </summary>
        /// <param name="Endereco">Entidade Endereco</param>
        /// <returns>Lista de Enderecos pesquisados</returns>
        public List<Endereco> Pesquisar(Endereco Endereco)
        {
            List<Endereco> retEndereco = new List<Endereco>();

            List<Endereco> retorno = null;
           Endereco entEnd;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            if (Endereco.CepEndereco != null)
            {
                par.Add(new SqlParameter("@cep", Endereco.CepEndereco));
            }

            if (Endereco.DescricaoEndereco != null)
            {
                par.Add(new SqlParameter("@parteendereco", Endereco.DescricaoEndereco));
            }

            dr = Dbase.GeraReaderProcedure("spc_BuscaEnderecoCepParteNome",  par);

            if (dr.HasRows)
            {
                retorno = new List<Endereco>();

                //configura o objeto usuario logado
                while (dr.Read())
                {
                    entEnd = new Endereco();

                    entEnd.CodEndereco = dr["codEndereco"].DefaultDbNull<Int32>(0);
                    entEnd.DescricaoLogradouro = dr["descricaoLogradouro"].ToString();
                    entEnd.DescricaoEndereco = dr["descricaoEndereco"].ToString();
                    entEnd.CepEndereco = dr["cepEndereco"].ToString();
                    entEnd.DescricaoBairro = dr["descricaoBairro"].ToString();
                    entEnd.DescricaoCidade = dr["descricaoCidade"].ToString();
                    entEnd.DescricaoEstado = dr["descricaoEstado"].ToString();
                    entEnd.DescricaoPais = dr["descricaoPais"].ToString();

                    retorno.Add(entEnd);
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Endereco"></param>
        /// <returns></returns>
        public Endereco PesquisarPorCodigo(int codEndereco)
        {
           Endereco retEndereco = new Endereco();

           Endereco retorno = null;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codEndereco", codEndereco));


            dr = Dbase.GeraReaderProcedure("spc_BuscaEnderecoCodigo",  par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = new Endereco();
                    retorno.CodPais = dr["codPais"].DefaultDbNull<Int32>(0);
                    retorno.CodEstado = dr["codEstado"].DefaultDbNull<Int32>(0);
                    retorno.CodCidade = dr["codCidade"].DefaultDbNull<Int32>(0);
                    retorno.CodBairro = dr["codBairro"].DefaultDbNull<Int32>(0);
                    retorno.CodLogradouro = dr["codLogradouro"].DefaultDbNull<Int32>(0);
                    retorno.CodEndereco = dr["codEndereco"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoLogradouro = dr["descricaoLogradouro"].ToString();
                    retorno.DescricaoEndereco = dr["descricaoEndereco"].ToString();
                    retorno.CepEndereco = dr["cepEndereco"].ToString();
                    retorno.DescricaoBairro = dr["descricaoBairro"].ToString();
                    retorno.DescricaoCidade = dr["descricaoCidade"].ToString();
                    retorno.DescricaoEstado = dr["descricaoEstado"].ToString();
                    retorno.DescricaoPais = dr["descricaoPais"].ToString();
                    retorno.UFEstado = dr["ufEstado"].ToString();
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Grava dados do Endereco
        /// </summary>
        /// <param name="Endereco">Entidade Endereco</param>
        /// <returns>Gravacao dos dados do Endereco</returns>
        public bool Alterar(Endereco Endereco)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codEndereco", Endereco.CodEndereco));
            par.Add(new SqlParameter("@DescricaoEndereco", Endereco.DescricaoEndereco));
            par.Add(new SqlParameter("@codLogradouro", Endereco.CodLogradouro));
            par.Add(new SqlParameter("@cepEndereco", Endereco.CepEndereco));
            par.Add(new SqlParameter("@codBairro", Endereco.CodBairro));

            retorno = Dbase.ExecutaProcedure("spc_atualizaEndereco",  par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastra dados do Endereco
        /// </summary>
        /// <param name="Endereco">Entidade Endereco</param>
        /// <returns>Retorna sucesso ou falha</returns>
        public bool Inserir(Endereco Endereco)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@DescricaoEndereco", Endereco.DescricaoEndereco));
            par.Add(new SqlParameter("@codLogradouro", Endereco.CodLogradouro));
            par.Add(new SqlParameter("@cepEndereco", Endereco.CepEndereco));
            par.Add(new SqlParameter("@codBairro", Endereco.CodBairro));

            retorno = Dbase.ExecutaProcedure("spc_cadastraEndereco",  par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Exclui dados do Endereco
        /// </summary>
        /// <param name="Endereco">Entidade Endereco</param>
        /// <returns>Retorna sucesso ou falha</returns>
        public bool Excluir(Endereco Endereco, UsuarioLogado UsuarioLogado)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codEndereco", Endereco.CodEndereco));

            // Registrando Historico
            Log lg = new Log();
            lg.RegistraHistorico("delete", "tbEndereco", Endereco.CodEndereco);
            lg = null;

            return retorno;
        }
    }
}
