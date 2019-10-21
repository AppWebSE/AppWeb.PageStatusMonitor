using AppWeb.PageStatusMonitor.Configurations;
using AppWeb.PageStatusMonitor.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AppWeb.PageStatusMonitor.Services
{
    public class EmailService : IEmailService
    {
        SmtpClient _smtpClient;
        EmailConfiguration _emailConfiguration;

        public EmailService(EmailConfiguration emailConfiguration)
        {
			_emailConfiguration = emailConfiguration ?? throw new ArgumentNullException(nameof(emailConfiguration));

            _smtpClient = new SmtpClient(_emailConfiguration.SmtpHost);
            _smtpClient.Credentials = new NetworkCredential(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
            _smtpClient.EnableSsl = _emailConfiguration.UseSSL;
        }
        
        public async Task<bool> SendEmailAsync(string subject, string body, bool isBodyHtml)
        {
            if (subject == null)
			{
				throw new ArgumentNullException(nameof(subject));
			}

			if (body == null)
			{
				throw new ArgumentNullException(nameof(body));
			}

			try
            {
                MailAddress fromAddress = new MailAddress(_emailConfiguration.FromEmail, _emailConfiguration.FromName);
                MailAddress toAddress = new MailAddress(_emailConfiguration.ToEmail, _emailConfiguration.ToName);
                MailMessage mailMessage = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    SubjectEncoding = Encoding.UTF8,
                    Body = body,
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = isBodyHtml
                };

                await _smtpClient.SendMailAsync(mailMessage).ConfigureAwait(false);

                return true;
            }
            catch (Exception e)
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
