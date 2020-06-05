using System;
using System.Text.RegularExpressions;

namespace GesDoc.Web.Services
{

    public static class Validacoes
    {

        /// <summary>
        /// Compara dois valores
        /// </summary>
        /// <param name="valor1">informacao a ser comparada</param>
        /// <param name="valor2">informacao a ser comparada</param>
        /// <returns>True caso iguais e false caso nao</returns>
        public static bool ComparaValores(string valor1, string valor2)
        {

            bool retorno = false;

            if (valor1 == valor2)
            {
                retorno = true;
            }

            return retorno;
        }

        /// <summary>
        /// Valida se o campo esta preenchido
        /// </summary>
        /// <param name="valor">Valor a ser validado</param>
        /// <param name="quantidadeMinima">Se informado, determina a qua
        /// ntidade minima de caracteres no campo.</param>
        /// <returns>True caso validado ou false caso nao</returns>
        public static bool EstaPreenchido(string valor, int quantidadeMinima = 0)
        {
            bool retorno = false;

            if (quantidadeMinima == 0)
            {
                if (valor.Length > 0)
                {
                    retorno = true;
                }
            }
            else
            {
                if (valor.Length >= quantidadeMinima)
                {
                    retorno = true;
                }
            }

            return retorno;
        }

        /// <summary>
        /// Verifica se valor é numerico
        /// </summary>
        /// <param name="valor">Valor a ser verificado</param>
        /// <returns>True caso numero e false caso nao</returns>
        public static bool Numerico(string valor)
        {

            int result;
            bool retorno = false;

            if (!int.TryParse(valor, out result))
            {
                retorno = true;
            }

            return retorno;

        }

        /// <summary>
        /// Valida se existe item selecionado
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool SelecionadoItem(int index)
        {

            bool retorno = false;

            if (index >= 0)
            {
                retorno = true;
            }

            return retorno;

        }

        /// <summary>
        /// Validação para horario inicial menor que final
        /// </summary>
        /// <param name="horaInicio">Horario inciial</param>
        /// <param name="horaFim">Horario Final</param>
        /// <returns>True caso verdadeiro</returns>
        public static bool ValidaHoraInicialMenorQueFinal(string horaInicio, string horaFim)
        {

            bool retorno = false;
            DateTime hrIni = Convert.ToDateTime(horaInicio);
            DateTime hrFim = Convert.ToDateTime(horaFim);

            if (hrIni < hrFim)
            {
                retorno = true;
            }

            return retorno;

        }

        /// <summary>
        /// Validação de e-mails
        /// </summary>
        /// <param name="inputEmail">E-mail a ser validado</param>
        /// <returns></returns>
        public static bool isValidEmail(string inputEmail)
        {
            // outra possibilidade de range
            // Regexregex = new Regex(@”^([w.-]+)@([w-]+)((.(w){2,3})+)$”);
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        /// <summary>
        /// Validação de CPF
        /// </summary>
        /// <param name="cpf">CPF a ser validado</param>
        /// <returns></returns>
        public static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        /// <summary>
        /// Validacao de PIS
        /// </summary>
        /// <param name="pis">PIS a ser validado</param>
        /// <returns></returns>
        public static bool IsPis(string pis)
        {
            int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            if (pis.Trim().Length != 11)
                return false;
            pis = pis.Trim();
            pis = pis.Replace("-", "").Replace(".", "").PadLeft(11, '0');

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(pis[i].ToString()) * multiplicador[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            return pis.EndsWith(resto.ToString());
        }

        /// <summary>
        /// Validação de CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public static bool IsCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

    }
}
