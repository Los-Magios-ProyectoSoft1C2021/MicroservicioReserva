using System.Threading.Tasks;

namespace Template.Application.EmailServices
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
