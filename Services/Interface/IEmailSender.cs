using CodePulse.API.Models.DTO;
using MimeKit;

namespace CodePulse.API.Services.Interface
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message mailMessage);
    }
}
