using System.Net;
using System.Net.Mail;

namespace MinhaSaudeFeminina.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string message)
        {
            using var client = new SmtpClient(_config["Smtp:Host"], int.Parse(_config["Smtp:Port"]));
            client.Credentials = new NetworkCredential(_config["Smtp:User"], _config["Smtp:Pass"]);
            client.EnableSsl = true;

            var mail = new MailMessage(_config["Smtp:User"], to, subject, message)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(mail);
        }
    }
}
