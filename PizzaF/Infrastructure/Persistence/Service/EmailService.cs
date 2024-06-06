


using Application.Interface.Service;
using Domain.Model.Email;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Persistence.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<bool> SendEmail(EmailModel request)
        {
            string email = _config.GetSection("email").Value;
            string appPassword = _config.GetSection("password").Value;
            string subject = "Confirmation Email";


            try
            {
                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.Credentials = new NetworkCredential(email, appPassword);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage(email, request.To, subject, request.VerificationLink);
                    await client.SendMailAsync(mailMessage);

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
