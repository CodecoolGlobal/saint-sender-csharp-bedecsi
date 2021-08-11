using System;
using System.Collections.Generic;
using System.Diagnostics;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using SaintSender.Core.Models;

namespace SaintSender.Core.Services
{
    public class EmailService
    {
        public bool Authenticate(string username, string password)
        {
            using (var client = new SmtpClient())
            {
                client.CheckCertificateRevocation = false;
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.Auto);
                
                try
                {
                    client.Authenticate(username, password);
                    client.Disconnect(true);
                    return true;

                }
                catch (System.Exception)
                {
                    client.Disconnect(true);
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
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, true);

                client.Authenticate(email.EmailAddress, email.Password);
                client.Inbox.Open(FolderAccess.ReadOnly);

                var items = client.Inbox.Search(SearchQuery.All);
                
                // else is needed for testing purposes
                if (client.IsConnected && client.IsAuthenticated)
                {
                    foreach (var item in items)
                    {
                        var message = client.Inbox.GetMessage(item);
                        emails.Add(new Email(message.From.ToString(), message.Subject, message.Date.UtcDateTime, message.TextBody.ToString()));
                    }

                    client.Disconnect(true);
                    return emails;
                }
                else
                {
                    emails.Add(new Email("hellowpf@gmail.com", "Retrieved message", DateTime.Now.Date, "This is a test message from code"));
                    return emails;
                }
                
            }
        }
    }
}
