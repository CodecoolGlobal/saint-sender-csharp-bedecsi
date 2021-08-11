using SaintSender.Core.Interfaces;
using SaintSender.Core.Models;
using SaintSender.Core.Services;
using System.Collections.Generic;
using System.ComponentModel;

namespace SaintSender.DesktopUI.ViewModels
{
    /// <summary>
    /// ViewModel for Main window. Contains all shown information
    /// and necessary service classes to make view functional.
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public List<Email> Emails { get; set; }
        private Email _selectedEmail;



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

        public void ShowEmailDetails(Email selectedEmail)
        {

        }

    }
}
