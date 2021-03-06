using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SaintSender.Core.Models;
using SaintSender.Core.Services;

namespace SaintSender.DesktopUI.ViewModels
{
    /// <summary>
    /// ViewModel for Main window. Contains all shown information
    /// and necessary service classes to make view functional.
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private List<Email> emails;
        public Credentials LoggedInUser { get; set; }
        public EmailService EmailService { get; set; }

        public List<Email> Emails
        {
            get { return emails; }
            set { emails = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Whenever a property value changed the subscribed event handler is called.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        public MainWindowViewModel(Credentials loggedInUser)
        {
            EmailService = new EmailService();
            LoggedInUser = loggedInUser;
        }

        public void CollectEmails(Credentials loggedInUser)
        {
            Emails = EmailService.RetrieveEmails(loggedInUser);
        }

        public void CollectAllEmails(Credentials loggedInUser)
        {
            Emails = EmailService.RetrieveAllEmails(loggedInUser);
        }

        public void NextPage()
        {
            EmailService.PageNumber += 1;
        }

        public void PrevPage()
        {
            if (EmailService.PageNumber > 1)
            {
                EmailService.PageNumber -= 1;
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<Email> FilterEmailsBySearchTerms(string searchTerms)
        {
            List<Email> searchEmails = new List<Email>();
            foreach (Email email in emails)
            {
                if(email.Body.Contains(searchTerms) || email.Subject.Contains(searchTerms) || email.Sender.Contains(searchTerms))
                {
                    searchEmails.Add(email);
                }
            }
            return searchEmails;

        }

    }
}
