using System;
using System.Security.Cryptography;

namespace GesDoc.Web.Services
{
    public class Criptografia
    {
        public static string EncryptString(string mensagem, string senha)
        {

            byte[] results; System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            // Passo 1. Calculamos o hash da senha usando MD5
            // Usamos o gerador de hash MD5 como o resultado é um array de bytes de 128 bits
            // que é um comprimento válido para o codificador TripleDES usado abaixo
            MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
            byte[] tDESKey = hashProvider.ComputeHash(UTF8.GetBytes(senha));

            // Passo 2. Cria um objeto new TripleDESCryptoServiceProvider
            TripleDESCryptoServiceProvider tDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Passo 3. Configuração do codificador
            tDESAlgorithm.Key = tDESKey;
            tDESAlgorithm.Mode = CipherMode.ECB;
            tDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Passo 4. Converta a seqüência de entrada para um byte []
            byte[] dataToEncrypt = UTF8.GetBytes(mensagem);
            // Passo 5. Tentativa para criptografar a seqüência de caracteres
            try
            {
                ICryptoTransform encryptor = tDESAlgorithm.CreateEncryptor();
                results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            }
            finally
            {
                // Limpe as tripleDES e serviços hashProvider de qualquer informação sensível
                tDESAlgorithm.Clear();
                hashProvider.Clear();
            }
            // Passo 6. Volte a seqüência criptografada como uma string base64 codificada
            return Convert.ToBase64String(results);
        }

        public static string DecryptString(string mensagem, string senha)
        {
            byte[] results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Passo 1. Calculamos o hash da senha usando MD5
            // Usamos o gerador de hash MD5 como o resultado é um array de bytes de 128 bits
            // que é um comprimento válido para o codificador TripleDES usado abaixo
            MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tDESAlgorithm = new TripleDESCryptoServiceProvider();

            byte[] tDESKey = hashProvider.ComputeHash(UTF8.GetBytes(senha));

            // Passo 2. Cria um objeto new TripleDESCryptoServiceProvider 
            tDESAlgorithm = new TripleDESCryptoServiceProvider();
            // Passo 3. Configuração do codificador
            tDESAlgorithm.Key = tDESKey;
            tDESAlgorithm.Mode = CipherMode.ECB;
            tDESAlgorithm.Padding = PaddingMode.PKCS7;
            // Passo 4. Converta a seqüência de entrada para um byte []
            byte[] dataToDecrypt = Convert.FromBase64String(mensagem);
            // Passo 5. Tentativa para criptografar a seqüência de caracteres
            try
            {
                ICryptoTransform Decryptor = tDESAlgorithm.CreateDecryptor();
                results = Decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            }
            finally
            {
                // Limpe as tripleDES e serviços hashProvider de qualquer informação sensível
                tDESAlgorithm.Clear();
                hashProvider.Clear();
            }

            // Passo 6. Volte a seqüência criptografada como uma string base64 codificada 
            return UTF8.GetString(results);
        }
    }
}