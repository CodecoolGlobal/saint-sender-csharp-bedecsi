using System.Windows;
using SaintSender.Core.Models;
using SaintSender.DesktopUI.ViewModels;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for NewEmail.xaml
    /// </summary>
    /// 
    
    public partial class NewEmail : Window
    {
        public Credentials Credentials { get; set; }
        public Email EmailToReply { get; set; }

        NewEmailViewModel _vm;
        public NewEmail(Credentials loggedInUser)
        {
            Credentials = loggedInUser;
            _vm = new NewEmailViewModel();
            DataContext = _vm;
            InitializeComponent();
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            _vm.SendEmail(to.Text, Credentials, subject.Text, body.Text);
            Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
