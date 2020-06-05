using GesDoc.Web.Controllers;
using GesDoc.Models;
using GesDoc.Web.Infraestructure;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using PdfSharp.Pdf.Security;

namespace GesDoc.Web.Services
{
    /// <summary>
    /// Classe para tratamento de PDFS (Assinatura e Pagina de rosto)
    /// </summary>
    public class PDFs
    {
        #region Metodos

        /// <summary>
        /// Insere pagina de rosto no documento
        /// </summary>
        /// <param name="NomeDocumento">Arquivo que irá receber a´pagina de documento</param>
        /// <param name="cliente">Cliente destino do documento</param>
        /// <param name="equipamento">Equipamento que foi laudado</param>
        /// <returns>Em caso de sucesso, TRUE para falha False</returns>
        public bool InsereRosto(string NomeDocumento, int codCliente, int codEquipamento)
        {
            bool retorno = false;

            // insere pagina de rosto do arquivo                        
            if (Ambiente.InserePaginaRosto())
            {
                try
                {
                    ClientesController ctrlCliente = new ClientesController();
                    Cliente cliente = new Cliente();
                    cliente = ctrlCliente.PesquisarPorCodigo(codCliente);

                    EquipamentosController ctrlEquip = new EquipamentosController();
                    Equipamento equipamento = new Equipamento();
                    equipamento = ctrlEquip.PesquisarPorCodigoEquipamento(codEquipamento);

                    XFont font;

                    var pdfDoc = PdfReader.Open(NomeDocumento, PdfDocumentOpenMode.Modify);

                    // dados de criação e propriedade do documento
                    pdfDoc.Info.Title = "RAD DIMENSTEIN & ASSOCIADOS";
                    pdfDoc.Info.Author = "RAD DIMENSTEIN & ASSOCIADOS";
                    pdfDoc.Info.CreationDate = DateTime.Now;
                    pdfDoc.Info.Creator = "RAD DIMENSTEIN & ASSOCIADOS";

                    var page = pdfDoc.Pages.Insert(0);

                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    // Insere o cliente
                    font = new XFont("Verdana", 25, XFontStyle.BoldItalic);
                    gfx.DrawString(cliente.NomeCliente, font, XBrushes.Black, new XRect(0, -200, page.Width, page.Height), XStringFormats.Center);

                    // Pula uma linha
                    font = new XFont("Verdana", 12, XFontStyle.BoldItalic);
                    gfx.DrawString("", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);

                    // Insere o cliente
                    font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
                    gfx.DrawString(equipamento.DescricaoEquipamento, font, XBrushes.Black, new XRect(0, 50, page.Width, page.Height), XStringFormats.Center);

                    // Insere o logotipo
                    XImage image = XImage.FromFile(Ambiente.CaminhoLogo());
                    gfx.DrawImage(image, ((page.Width / 2) - 100), 50, 200, 60);

                    font = null;
                    gfx = null;
                    page.Close();

                    // salva documento
                    pdfDoc.Save(NomeDocumento);

                    ctrlEquip = null;
                    ctrlCliente = null;
                    cliente = null;
                    equipamento = null;
                    pdfDoc = null;
                }
                catch (Exception ex)
                {
                    Mensagens.MsgErro = $"Ocorreu um erro:{ex.Message.ToString()}";
                    retorno = false;
                }
            }

            return retorno;
        }

        /// <summary>
        /// Assina todas as páginas de documentos PDF com assinatura default no canto direito
        /// </summary>
        /// <param name="NomeDocumento">Arquivo a ser assinado</param>
        /// <param name="hashCode">Código hash do arquivo a ser assinado</param>
        /// <param name="nomeAssinante">Nome do Assinante</param>
        /// <returns>Em caso de sucesso, TRUE para falha False</returns>
        public bool AssinaDoc(string NomeDocumento, string hashCode, string nomeAssinante)
        {
            // Insere a assinatura abaixo em todas as paginas do documento.
            // Este documento foi assinado digitalmente por NOME DO ASSINANTE na data de DATA DA ASSINATURA.A autenticidade deste documento pode ser verificado junto à Rad Dimenstein & Associados pelo código ####.
            bool retorno = false;

            try
            {
                PdfDocument pdfDoc = PdfReader.Open($@"{NomeDocumento}", PdfDocumentOpenMode.Modify);

                string assinaturaLn1 = Ambiente.GetLinha1Assinatura(nomeAssinante);
                string assinaturaLn2 = Ambiente.GetLinha2Assinatura(hashCode);

                for (var i = 0; i < pdfDoc.PageCount; i++)
                {
                    PdfPage page = pdfDoc.Pages[i];
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    XFont font = new XFont("Verdana", 6);

                    gfx.RotateAtTransform(-90, new XPoint(400, 320));
                    gfx.DrawString(assinaturaLn1, font, XBrushes.Black, new XRect(10, 75, page.Width, page.Height), XStringFormats.Center);
                    gfx.DrawString(assinaturaLn2, font, XBrushes.Black, new XRect(10, 85, page.Width, page.Height), XStringFormats.Center);

                    page.Close();
                    font = null;
                    gfx = null;
                }

                if (Ambiente.InsereSenhaDocPdf())
                {
                    PdfSecuritySettings securitySettings = pdfDoc.SecuritySettings;

                    // Senhas para proteção
                    securitySettings.UserPassword = Ambiente.GetSenhaSecurityPdf();
                    securitySettings.OwnerPassword = Ambiente.GetSenhaSecurityMasterPdf();

                    // Permissões
                    securitySettings.PermitAccessibilityExtractContent = false;
                    securitySettings.PermitAnnotations = false;
                    securitySettings.PermitAssembleDocument = false;
                    securitySettings.PermitExtractContent = false;
                    securitySettings.PermitFormsFill = true;
                    securitySettings.PermitFullQualityPrint = true;
                    securitySettings.PermitModifyDocument = false;
                    securitySettings.PermitPrint = true;
                }

                pdfDoc.Save(NomeDocumento);

                pdfDoc = null;

                retorno = true;

            }
            catch (Exception ex)
            {
                Mensagens.MsgErro = $"Ocorreu um erro:{ex.Message.ToString()}";
                retorno = false;
            }

            return retorno;
        }

        #endregion
    }
}