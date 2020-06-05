using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using iTextSharp.text.xml.xmp;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace RAD.UTILS
{
    /// <summary>
    /// Class hold the certificate and extract private key needed for e-signature
    /// </summary>
    public class Certificacao
    {

        #region "Variaveis Locais"

        protected char[] PASSWORD = "R@d2017".ToCharArray();
        protected string caminhoPfx = MapeamentoPaths.GetPathDominio() + @"Certificados\RAD.pfx";
        protected string caminhoRaizPdfs = MapeamentoPaths.GetPathDominio() + @"Laudos\";
        protected string caminhoSaidaSRC = @"C:\Users\prog_\Desktop\testeAssinado.pdf";
        public string caminhoEntradaSRC { get; set; }

        #endregion

        #region "Atributos"

        private string password = "";
        private AsymmetricKeyParameter akp;
        private X509Certificate[] chain;

        #endregion

        #region "Metodos de acesso"

        public X509Certificate[] Chain
        {
            get { return chain; }
        }

        public AsymmetricKeyParameter Akp
        {
            get { return akp; }
        }

        public string RaizPdfs
        {
            get { return caminhoRaizPdfs; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        #endregion

        #region "Ações"

        /// <summary>
        /// Acessa e verifica o Certificado
        /// </summary>
        private bool ValidaCertificado()
        {
            bool retorno = false;
            string alias = null;

            Pkcs12Store pk12;

            try
            {
                // Lendo o arquivo do certificado
                pk12 = new Pkcs12Store(new FileStream(caminhoPfx, FileMode.Open, FileAccess.Read), this.password.ToCharArray());

                // Buscando a chave dentro do certificado
                IEnumerator i = pk12.Aliases.GetEnumerator();

                while (i.MoveNext())
                {
                    alias = ((string)i.Current);
                    if (pk12.IsKeyEntry(alias))
                        break;
                }

                this.akp = pk12.GetKey(alias).Key;

                X509CertificateEntry[] ce = pk12.GetCertificateChain(alias);

                this.chain = new X509Certificate[ce.Length];

                for (int k = 0; k < ce.Length; ++k)
                {
                    chain[k] = ce[k].Certificate;
                }

                retorno = true;
            }
            catch (System.Exception)
            {
                retorno = false;
                Tratamentos.MsgErro = "Existe um problema com o certificado do Sistema. Certificado não carregado.";
            }

            return retorno;

        }

        #endregion

        public bool Assinar(string reason, string location)
        {
            bool retorno = false;
            try
            {
                // criando as pastas raizes para os PDFs
                if (!Directory.Exists(caminhoRaizPdfs))
                {
                    Directory.CreateDirectory(caminhoRaizPdfs);
                }

                // definindo o caminho de saida a partir do arquivo de entrada.
                caminhoSaidaSRC = caminhoEntradaSRC.Replace(".pdf", "_assinado.pdf");
                Pkcs12Store store = new Pkcs12Store(new FileStream(caminhoPfx, FileMode.Open), PASSWORD);
                String alias = "";
                ICollection<X509Certificate> chain = new List<X509Certificate>();

                // Buscando a palavra chave privada
                foreach (string al in store.Aliases)
                {
                    if (store.IsKeyEntry(al) && store.GetKey(al).Key.IsPrivate)
                    {
                        alias = al;
                        break;
                    }
                }

                AsymmetricKeyEntry pk = store.GetKey(alias);

                foreach (X509CertificateEntry c in store.GetCertificateChain(alias))
                {
                    chain.Add(c.Certificate);
                }

                RsaPrivateCrtKeyParameters parameters = pk.Key as RsaPrivateCrtKeyParameters;

                // Leitura do arquivo e criação do carimbo para assinatura
                using (PdfReader reader = new PdfReader(caminhoEntradaSRC))
                {
                    PdfReader.unethicalreading = true;
                    AcroFields fields = reader.AcroFields;
                    using (FileStream os = new FileStream(caminhoSaidaSRC, FileMode.Create))
                    {
                        using (PdfStamper stamper = PdfStamper.CreateSignature(reader, os, '\0'))
                        {
                            // Configurando a Assinatura
                            PdfSignatureAppearance appearance = stamper.SignatureAppearance;

                            // Reação da assinatura
                            appearance.Reason = reason;

                            // Localização da assinatura
                            appearance.Location = location;

                            Image signatureFieldImage = Image.GetInstance(MapeamentoPaths.GetPathDominio() + @"/Imagens/assinatura.png");
                            appearance.SignatureGraphic = signatureFieldImage;
                            appearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.GRAPHIC_AND_DESCRIPTION;

                            // SignatureTextBox.Text;
                            appearance.Layer2Text = "RAD Dimenstein";
                            appearance.Layer2Font = new Font(Font.FontFamily.COURIER, 7.0f, Font.NORMAL, BaseColor.LIGHT_GRAY);
                            appearance.Acro6Layers = true;
                            appearance.SetVisibleSignature(new iTextSharp.text.Rectangle(100, 100, 250, 150), 1, null);

                            // Criando a assinatura
                            IExternalSignature pks = new PrivateKeySignature(parameters, DigestAlgorithms.SHA256.ToString());

                            MakeSignature.SignDetached(appearance, pks, chain, null, null, null, 0, CryptoStandard.CMS);

                            retorno = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retorno = false;
            }

            return retorno;
        }
    }
}