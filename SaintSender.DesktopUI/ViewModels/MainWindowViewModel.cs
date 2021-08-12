using SaintSender.Core.Interfaces;
using SaintSender.Core.Models;
using SaintSender.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SaintSender.DesktopUI.ViewModels
{
    /// <summary>
    /// ViewModel for Main window. Contains all shown information
    /// and necessary service classes to make view functional.
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private List<Email> emails;
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


        public MainWindowViewModel()
        {
            CollectEmails(new Credentials("bedecsi2ndtw1@gmail.com", "IHateWPF", ""));
            EmailService = new EmailService();
        }

        public void CollectEmails(Credentials loggedInUser)
        {
            Emails = new EmailService().RetrieveEmails(loggedInUser);
        }

        public void CollectAllEmails(Credentials loggedInUser)
        {
            Emails = new EmailService().RetrieveAllEmails(loggedInUser);
        }

        public void NextPage()
        {
            EmailService.PageNumber += 1;
        }


        public void PrevPage()
        {
            EmailService.PageNumber -= 1;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
