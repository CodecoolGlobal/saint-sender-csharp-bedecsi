using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using SaintSender.Core.Models;
using System.Net;

namespace SaintSender.Core.Services
{
    public class EmailService
    {
        public void Send(string to, Credentials from, string subject, string content)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(from.Name, from.EmailAddress));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = content
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(from.EmailAddress, from.Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
