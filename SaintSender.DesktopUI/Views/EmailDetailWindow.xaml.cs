using SaintSender.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for EmailDetailWindow.xaml
    /// </summary>
    public partial class EmailDetailWindow : Window
    {
        private EmailDetailWindowViewModel _emailDetailWindowViewModel;
        private Credentials _loggedInUser;
        public EmailDetailWindow(Credentials loggedInUser, Email selected)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            _emailDetailWindowViewModel = new EmailDetailWindowViewModel(selected);
            DataContext = _emailDetailWindowViewModel.SelectedEmail;
        }

        private void ReplyButton_Click(object sender, RoutedEventArgs e)
        {
            NewEmail newEmailWindow = new NewEmail(_loggedInUser);
            newEmailWindow.Show();
        }
    }
}
