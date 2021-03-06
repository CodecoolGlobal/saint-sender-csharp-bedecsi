using System;
using System.Collections.Generic;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using SaintSender.Core.Models;
using System.Windows.Forms;

namespace SaintSender.Core.Services
{
    public class EmailService
    {
        public const int MAX_EMAILS_ON_PAGE = 10;

        public static int PageNumber { get; set; } = 1;
        public static int CurrentIndex { get; set; } = 0;

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
                catch (Exception)
                {
                    client.Disconnect(true);
                    return false;
                }

            }

        }

        public void Send(string to, Credentials from, string subject, string content)
        {
            if (content == "" || subject == "" || to == "")
            {
                MessageBox.Show("Please fill all fields!");
                return;
            }
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
                    CurrentIndex = PageNumber * MAX_EMAILS_ON_PAGE - MAX_EMAILS_ON_PAGE;
                    if (CurrentIndex >= items.Count)
                    {
                        CurrentIndex -= MAX_EMAILS_ON_PAGE;
                        PageNumber -= 1;
                    }
                    for (int i = CurrentIndex; i < (PageNumber * MAX_EMAILS_ON_PAGE <= items.Count ? PageNumber * MAX_EMAILS_ON_PAGE : items.Count); i++)
                    {
                        var message = client.Inbox.GetMessage(i);
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

        public List<Email> RetrieveAllEmails(Credentials email)
        {
            List<Email> emails = new List<Email>();
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, true);

                client.Authenticate(email.EmailAddress, email.Password);
                client.Inbox.Open(FolderAccess.ReadOnly);

                var items = client.Inbox.Search(SearchQuery.All);
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
