using GesDoc.Models;
using GesDoc.Web.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace GesDoc.Web.Controllers
{
    public class EquipamentosController
    {
        private SQLBase Dbase = new SQLBase("Cadastro de Equipamentos");

        /// <summary>
        /// Lista de Equipamentos
        /// </summary>
        /// <param name="Equipamentos">Entidade Equipamentos</param>
        /// <returns>Lista de Equipamentos</returns>
        public List<Equipamento> GetAll()
        {
            List<Equipamento> retEquipamentos = new List<Equipamento>();


            return retEquipamentos;
        }

        /// <summary>
        /// Lista de Equipamentos pesquisados
        /// </summary>
        /// <param name="Equipamentos">Entidade Equipamentos</param>
        /// <returns>Lista de Equipamentos pesquisados</returns>
        public List<Equipamento> Pesquisar(Equipamento Equipamentos)
        {

            List<Equipamento> retorno = null;

            return retorno;
        }

        /// <summary>
        /// Pesquisa Equipamentos do cliente pelo codigo do Equipamento
        /// </summary>
        /// <param name="Equipamentos"></param>
        /// <returns></returns>
        public Equipamento PesquisarPorCodigoEquipamento(int codEquipamento)
        {
           Equipamento retorno = new Equipamento();
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codEquipamento", codEquipamento));

            dr = Dbase.GeraReaderProcedure("spc_BuscaEquipamentosCodigo", par);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno.CodEquipamento = dr["codEquipamento"].DefaultDbNull<Int32>(0);
                    retorno.CodCliente = dr["codCliente"].DefaultDbNull<Int32>(0);
                    retorno.CodSetor = dr["codSetor"].DefaultDbNull<Int32>(0);
                    retorno.CodSala = dr["CodSala"].DefaultDbNull<Int32>(0);
                    retorno.NomeSala = dr["NomeSala"].ToString();
                    retorno.CodTipoEquipamento = dr["codTipoEquipamento"].DefaultDbNull<Int32>(0);
                    retorno.DescricaoTipoEquipamento = dr["dscTipoEquipamento"].ToString();
                    retorno.Marca = dr["Marca"].ToString();
                    retorno.Modelo = dr["Modelo"].ToString();
                    retorno.NumeroSerie = dr["NumeroSerie"].ToString();
                    retorno.NumeroPatrimonio = dr["NumeroPatrimonio"].ToString();
                    retorno.RegistroAnvisa = dr["RegistroAnvisa"].ToString();
                    retorno.AnoFabricacao = dr["AnoFabricacao"].ToString();
                    retorno.StatusEquip = dr["StatusEquip"].ToString();
                    retorno.DescricaoEquipamento = retorno.Marca + '-' + retorno.Modelo + '-' + retorno.NumeroSerie;
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Pesquisa Equipamentos para o cliente
        /// </summary>
        /// <param name="codCliente">Codigo do cliente para o qual se deseja os Equipamentos.</param>
        /// <returns></returns>
        public List<Equipamento> PesquisarPorCodigoCliente(int codCliente)
        {
            List<Equipamento> retorno = null;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codCliente", codCliente));

            dr = Dbase.GeraReaderProcedure("spc_BuscaEquipamentosCliente", par);

            if (dr.HasRows)
            {
                retorno = new List<Equipamento>();
                while (dr.Read())
                {
                   Equipamento Equipamentos = new Equipamento();
                    Equipamentos.CodEquipamento = dr["codEquipamento"].DefaultDbNull<Int32>(0);
                    Equipamentos.CodCliente = dr["codCliente"].DefaultDbNull<Int32>(0);
                    Equipamentos.CodSala = dr["CodSala"].DefaultDbNull<Int32>(0);
                    Equipamentos.CodSetor = dr["codSetor"].DefaultDbNull<Int32>(0);
                    Equipamentos.NomeSala = dr["NomeSala"].ToString();
                    Equipamentos.CodTipoEquipamento = dr["codTipoEquipamento"].DefaultDbNull<Int32>(0);
                    Equipamentos.DescricaoTipoEquipamento = dr["dscTipoEquipamento"].ToString();
                    Equipamentos.Marca = dr["Marca"].ToString();
                    Equipamentos.Modelo = dr["Modelo"].ToString();
                    Equipamentos.NumeroSerie = dr["NumeroSerie"].ToString();
                    Equipamentos.NumeroPatrimonio = dr["NumeroPatrimonio"].ToString();
                    Equipamentos.RegistroAnvisa = dr["RegistroAnvisa"].ToString();
                    Equipamentos.AnoFabricacao = dr["AnoFabricacao"].ToString();
                    Equipamentos.StatusEquip = dr["StatusEquip"].ToString();
                    Equipamentos.DescricaoEquipamento = Equipamentos.Marca + '-' + Equipamentos.Modelo + '-' + Equipamentos.NumeroSerie;
                    retorno.Add(Equipamentos);
                    Equipamentos = null;
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Pesquisa Equipamentos para o cliente
        /// </summary>
        /// <param name="codCliente">Codigo do cliente para o qual se deseja os Equipamentos.</param>
        /// <returns></returns>
        public List<Equipamento> PesquisarPorCodigoClienteComTipo(int codCliente)
        {
            List<Equipamento> retorno = null;
            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@codCliente", codCliente));

            dr = Dbase.GeraReaderProcedure("spc_BuscaEquipamentosCliente", par);

            if (dr.HasRows)
            {
                retorno = new List<Equipamento>();
                while (dr.Read())
                {
                   Equipamento Equipamentos = new Equipamento();
                    Equipamentos.CodEquipamento = dr["codEquipamento"].DefaultDbNull<Int32>(0);
                    Equipamentos.CodCliente = dr["codCliente"].DefaultDbNull<Int32>(0);
                    Equipamentos.CodSala = dr["CodSala"].DefaultDbNull<Int32>(0);
                    Equipamentos.CodSetor = dr["codSetor"].DefaultDbNull<Int32>(0);
                    Equipamentos.NomeSala = dr["NomeSala"].ToString();
                    Equipamentos.CodTipoEquipamento = dr["codTipoEquipamento"].DefaultDbNull<Int32>(0);
                    Equipamentos.DescricaoTipoEquipamento = dr["dscTipoEquipamento"].ToString();
                    Equipamentos.Marca = dr["Marca"].ToString();
                    Equipamentos.Modelo = dr["Modelo"].ToString();
                    Equipamentos.NumeroSerie = dr["NumeroSerie"].ToString();
                    Equipamentos.NumeroPatrimonio = dr["NumeroPatrimonio"].ToString();
                    Equipamentos.RegistroAnvisa = dr["RegistroAnvisa"].ToString();
                    Equipamentos.AnoFabricacao = dr["AnoFabricacao"].ToString();
                    Equipamentos.StatusEquip = dr["StatusEquip"].ToString();

                    StringBuilder nomeEquip = new StringBuilder();
                    nomeEquip.Append(Equipamentos.DescricaoTipoEquipamento)
                        .Append(" - ")
                        .Append(Equipamentos.Marca)
                        .Append(" - ")
                        .Append(Equipamentos.Modelo)
                        .Append(" - ")
                        .Append(Equipamentos.NumeroSerie);

                    Equipamentos.DescricaoEquipamento = nomeEquip.ToString();
                    retorno.Add(Equipamentos);
                    Equipamentos = null;
                }

            }

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Grava dados do Equipamentos
        /// </summary>
        /// <param name="Equipamentos">Entidade Equipamentos</param>
        /// <returns>Gravacao dos dados do Equipamentos</returns>
        public bool Alterar(Equipamento Equipamentos)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codEquipamento", Equipamentos.CodEquipamento));
            par.Add(new SqlParameter("@codCliente", Equipamentos.CodCliente));
            par.Add(new SqlParameter("@CodSala", Equipamentos.CodSala));
            par.Add(new SqlParameter("@codTipoEquipamento", Equipamentos.CodTipoEquipamento));
            par.Add(new SqlParameter("@Marca", Equipamentos.Marca));
            par.Add(new SqlParameter("@Modelo", Equipamentos.Modelo));
            par.Add(new SqlParameter("@NumeroSerie", Equipamentos.NumeroSerie));
            par.Add(new SqlParameter("@NumeroPatrimonio", Equipamentos.NumeroPatrimonio));
            par.Add(new SqlParameter("@RegistroAnvisa", Equipamentos.RegistroAnvisa));
            par.Add(new SqlParameter("@AnoFabricacao", Equipamentos.AnoFabricacao));
            par.Add(new SqlParameter("@StatusEquip", Equipamentos.StatusEquip));

            retorno = Dbase.ExecutaProcedure("spc_atualizaEquipamentoCliente", par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Cadastra dados do Equipamentos
        /// </summary>
        /// <param name="Equipamentos">Entidade Equipamentos</param>
        /// <returns>Retorna sucesso ou falha</returns>
        public bool Inserir(Equipamento Equipamentos)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codCliente", Equipamentos.CodCliente));
            par.Add(new SqlParameter("@CodSala", Equipamentos.CodSala));
            par.Add(new SqlParameter("@codTipoEquipamento", Equipamentos.CodTipoEquipamento));
            par.Add(new SqlParameter("@Marca", Equipamentos.Marca));
            par.Add(new SqlParameter("@Modelo", Equipamentos.Modelo));
            par.Add(new SqlParameter("@NumeroSerie", Equipamentos.NumeroSerie));
            par.Add(new SqlParameter("@NumeroPatrimonio", Equipamentos.NumeroPatrimonio));
            par.Add(new SqlParameter("@RegistroAnvisa", Equipamentos.RegistroAnvisa));
            par.Add(new SqlParameter("@AnoFabricacao", Equipamentos.AnoFabricacao));
            par.Add(new SqlParameter("@StatusEquip", Equipamentos.StatusEquip));

            retorno = Dbase.ExecutaProcedure("spc_cadastraEquipamentoCliente", par);

            Dbase.Desconectar();

            return retorno;
        }

        /// <summary>
        /// Exclui dados do Equipamentos
        /// </summary>
        /// <param name="Equipamentos">Entidade Equipamentos</param>
        /// <returns>Retorna sucesso ou falha</returns>
        public bool Excluir(Equipamento Equipamentos)
        {
            bool retorno = false;
            List<SqlParameter> par = new List<SqlParameter>();

            // Passagem de parametros
            par.Add(new SqlParameter("@codEquipamento", Equipamentos.CodEquipamento));

            Dbase.Conectar();

            retorno = Dbase.ExecutaProcedure("spc_excluiEquipamentoCliente", par);

            Dbase.Desconectar();

            return retorno;
        }


        /// <summary>
        /// Valida numero de serie para impedir duplicidade
        /// </summary>
        /// <param name="codCliente"></param>
        /// <param name="codigoEquipamento"></param>
        /// <returns></returns>
        public int ValidaEquipamentoExistente(int codCliente, string numeroSerie)
        {

            int retorno = 0;

            SqlDataReader dr;

            List<SqlParameter> par = new List<SqlParameter>();

            Dbase.Conectar();

            par.Add(new SqlParameter("@CODCLIENTE", codCliente));
            par.Add(new SqlParameter("@NUMEROSERIE", numeroSerie));

            dr = Dbase.GeraReaderProcedure("spc_validaEquipamentoExistente", par);

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

    }
}
