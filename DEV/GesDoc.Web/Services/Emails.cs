using GesDoc.Web.Infraestructure;
using System;
using System.Net.Mail;
using System.Text;

namespace GesDoc.Web.Services
{
    public class Emails
    {


        public static void EnviarEmail(string EmailPara, string EmailDe, string EmailTitulo, string EmailMensagem, string copiaEmail = "")
        {
            if (!Ambiente.ISProducao() && EmailPara != "ncad")
            {
                // Instancia o Objeto Email como MailMessage 
                MailMessage Email = new MailMessage();

                // Atribui ao método From o valor do Remetente 
                Email.From = new MailAddress(EmailDe);

                // Atribui ao método To o valor do Destinatário 
                Email.To.Add(EmailPara.Trim());

                if (copiaEmail != "")
                {
                    Email.ReplyToList.Add(copiaEmail.Trim());
                }

                // Atribui ao método Subject o assunto da mensagem 
                Email.Subject = EmailTitulo;

                Email.Priority = MailPriority.Normal;

                // Define o formato da mensagem que pode ser Texto ou Html 
                Email.IsBodyHtml = true;

                // Atribui ao método Body a texto da mensagem 
                Email.Body = EmailMensagem;
                Email.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
                Email.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

                // Cria objeto com os dados do SMTP
                SmtpClient objSmtp = new SmtpClient();

                // Alocamos o endereço do host para enviar os e-mails, localhost(recomendado) 
                objSmtp.Host = "mail.radimenstein.com.br";

                // Enviamos o e-mail através do método .Send()
                try
                {
                    objSmtp.Send(Email);
                }
                catch (Exception ex)
                {
                    Mensagens.MsgErro = $"Erro ao enviar email:{ex.Message.ToString()}";
                }

                // Excluímos o objeto de e-mail da memória
                Email.Dispose();
            }
        }
    }
}
