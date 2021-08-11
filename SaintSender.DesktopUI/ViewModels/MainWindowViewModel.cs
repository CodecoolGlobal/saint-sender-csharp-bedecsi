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


        /// <summary>
        /// Whenever a property value changed the subscribed event handler is called.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        public MainWindowViewModel(Credentials loggedInUser)
        {
            CollectEmails(loggedInUser);
           
        }

        public void CollectEmails(Credentials loggedInUser)
        {
            Emails = new EmailService().RetrieveEmails(loggedInUser);
        }

    }
}
