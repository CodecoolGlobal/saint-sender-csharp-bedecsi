using System.ComponentModel;
using SaintSender.Core.Models;

namespace SaintSender.DesktopUI.Views
{
    public class EmailDetailWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Email SelectedEmail { get; set; }

        public EmailDetailWindowViewModel(Email selected)
        {
            SelectedEmail = selected;
        }
    }
}
