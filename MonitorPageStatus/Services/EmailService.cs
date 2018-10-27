using MonitorPageStatus.Configurations;
using MonitorPageStatus.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MonitorPageStatus.Services
{
    public class EmailService : IEmailService
    {
        SmtpClient _smtpClient;
        EmailConfiguration _emailConfiguration;

        public EmailService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;

            _smtpClient = new SmtpClient(_emailConfiguration.Host);
            _smtpClient.Credentials = new NetworkCredential(_emailConfiguration.Username, _emailConfiguration.Password);
            _smtpClient.EnableSsl = _emailConfiguration.UseSSL;
        }

        public bool SendEmail(string toEmail, string toName, string subject, string body, bool isBodyHtml)
        {
            if (toEmail == null)
                throw new ArgumentNullException(nameof(toEmail));

            if (toName == null)
                throw new ArgumentNullException(nameof(toName));

            if (subject == null)
                throw new ArgumentNullException(nameof(subject));

            if (body == null)
                throw new ArgumentNullException(nameof(body));

            try { 
                MailAddress fromAddress = new MailAddress(_emailConfiguration.FromEmail, _emailConfiguration.FromName);
                MailAddress toAddress = new MailAddress(toEmail, toName);
                MailMessage mailMessage = new MailMessage(fromAddress, toAddress);
                
                mailMessage.Subject = subject;
                mailMessage.SubjectEncoding = Encoding.UTF8;
                
                mailMessage.Body = body;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.IsBodyHtml = isBodyHtml;

                _smtpClient.Send(mailMessage);

                return true;
            }
            catch(Exception e)
            {
                // todo: check exception and handle
                return false;
            }
        }
        
        public void Dispose()
        {
            _smtpClient.Dispose();
        }
    }
}
