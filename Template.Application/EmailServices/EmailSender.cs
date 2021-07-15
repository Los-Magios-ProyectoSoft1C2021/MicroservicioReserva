using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Template.Domain.Email;

namespace Template.Application.EmailServices
{
    public class EmailSender : IEmailSender
    {
        private SmtpClient Client { get; set; }
        private EmailSenderOptions Options { get; set; }

        public EmailSender(IOptions<EmailSenderOptions> options)
        {
            Options = options.Value;
            Client = new SmtpClient
            {
                Host = Options.Host,
                Port = Options.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Options.Email, Options.Password),
                EnableSsl = Options.EnableSsl
            };
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mailMessage = new MailMessage(from: Options.Email, to: email, subject: subject, body: message)
            {
                IsBodyHtml = true
            };

            return Client.SendMailAsync(mailMessage);
        }
    }
}
