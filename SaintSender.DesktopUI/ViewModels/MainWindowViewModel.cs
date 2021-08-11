using SaintSender.Core.Interfaces;
using SaintSender.Core.Models;
using SaintSender.Core.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SaintSender.DesktopUI.ViewModels
{
    /// <summary>
    /// ViewModel for Main window. Contains all shown information
    /// and necessary service classes to make view functional.
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private List<Email> emails;

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
           
        }

        public void CollectEmails(Credentials loggedInUser)
        {
            Emails = new EmailService().RetrieveEmails(loggedInUser);
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
