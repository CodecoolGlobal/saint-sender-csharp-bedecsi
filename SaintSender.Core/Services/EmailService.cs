using System.Collections.Generic;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MimeKit;
using SaintSender.Core.Models;

namespace SaintSender.Core.Services
{
    public class EmailService
    {
        public bool Authenticate(Credentials credentials)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate(credentials.EmailAddress, credentials.Password);
                    client.Disconnect(true);
                    return true;
                }
                catch (System.Exception)
                {
                    return false;
                }
            }
        }

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

        public List<Email> RetrieveEmails(Credentials email)
        {
            List<Email> emails = new List<Email>();
            using (var client = new Pop3Client())
            {
                client.Connect("pop.gmail.com", 995, false);

                client.Authenticate(email.EmailAddress, email.Password);

                for (int i = 0; i < client.Count; i++)
                {
                    var message = client.GetMessage(i);
                    emails.Add(new Email(message.From.ToString(), message.Subject, message.Date.UtcDateTime, message.Body.ToString()));
                }

                client.Disconnect(true);
                return emails;
            }
        }
    }
}
