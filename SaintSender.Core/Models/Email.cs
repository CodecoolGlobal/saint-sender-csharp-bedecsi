using System;

namespace SaintSender.Core.Models
{
    public class Email
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }

        public bool IsRead { get; set; }

        public Email()
        {

        }

        public Email(string sender, string subject, DateTime date, string body)
        {
            Sender = sender;
            Subject = subject;
            Date = date;
            Body = body;
        }

    }
}
