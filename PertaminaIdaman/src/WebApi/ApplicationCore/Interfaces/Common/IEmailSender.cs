using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Common
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
