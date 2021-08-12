using SaintSender.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
