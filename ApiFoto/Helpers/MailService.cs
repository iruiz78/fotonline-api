using ApiFoto.Domain;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace ApiFoto.Helpers
{
    public class MailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmail(int emailType, string toEmail, string subject = "", string body = "", string ccoEmail = "", string SenderEmail = "", string senderName = "")
        {
            try
            {
                SmtpClient smtpSettings = BuildSmtpSetting();
                MailMessage mailMessage = BuildEmailMessage(emailType, toEmail, subject, body, ccoEmail, _mailSettings.SenderEmail, _mailSettings.SenderName);

                smtpSettings.SendCompleted += (s, e) =>
                {
                    smtpSettings.Dispose();
                    mailMessage.Dispose();
                };

                await smtpSettings.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private SmtpClient BuildSmtpSetting()
        {
            SmtpClient smtpClient = new()
            {
                UseDefaultCredentials = false,
                Host = _mailSettings.Smtp,
                Port = _mailSettings.PortNumber,
                EnableSsl = _mailSettings.EnabledSSL,
                Credentials = new NetworkCredential(_mailSettings.UserName, _mailSettings.Password)
            };
            return smtpClient;
        }

        private MailMessage BuildEmailMessage(int emailType, string toEmail, string subject, string body, string ccoEmail, string senderEmail, string senderName)
        {
            switch (emailType)
            {
                case (int)Enums.EmailType.ResetPassword:
                    subject = "Recuperación de Contraseña | Foto Online.";
                    body = MailTemplates.ResetPassword(body);
                break;
                default:

                break;
            }

            MailMessage mailMessage = new()
            {
                From = new MailAddress(senderEmail, senderName),
                Subject = subject,
                IsBodyHtml = true,
                Body = body
            };

            if (toEmail.Contains(";"))
            {
                var arMails = toEmail.Split(';');
                for (int i = 0; i < arMails.Length; i++)
                {
                    mailMessage.To.Add(new MailAddress(arMails[i]));
                }
            }
            else
                mailMessage.To.Add(new MailAddress(toEmail));

            if (!string.IsNullOrEmpty(ccoEmail))
            {
                if (ccoEmail.Contains(";"))
                {
                    var arCCOMails = ccoEmail.Split(';');
                    for (int i = 0; i < arCCOMails.Length; i++)
                    {
                        mailMessage.Bcc.Add(new MailAddress(arCCOMails[i]));
                    }
                }
                else
                    mailMessage.Bcc.Add(new MailAddress(ccoEmail));
            }
            return mailMessage;
        }
    }
}
