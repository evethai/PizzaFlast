


using Application.Interface;
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
        private readonly IUnitOfWork _unitOfWork;
        public EmailService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _config = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> SendEmail(int userId)
        {
            string email = _config.GetSection("email").Value;
            string appPassword = _config.GetSection("password").Value;
            string subject = "Confirmation Email";

            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            string link = "https://pizzafapi.azurewebsites.net/api/users/verify?token=" + user.VerificationToken;
            string mess = $"Hello {user.Name},\n\n" +
                "Please click the link below to verify your email address.\n\n" +
                "Verification Link: " + link + "\n\n" +
                "Thank you!";
            try
            {
                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.Credentials = new NetworkCredential(email, appPassword);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage(email, user.Email, subject, mess);
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
