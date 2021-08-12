using SaintSender.Core.Models;
using SaintSender.Core.Services;

namespace SaintSender.DesktopUI.ViewModels
{
    public class NewEmailViewModel
    {
        private readonly EmailService _emailService;

        public NewEmailViewModel()
        {
            _emailService = new EmailService();
        }

        public void SendEmail(string to, Credentials from, string subject, string content)
        {
            _emailService.Send(to, from, subject, content);
        }
    }
}
