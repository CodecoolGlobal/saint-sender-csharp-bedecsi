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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SaintSender.Core.Models;
using SaintSender.DesktopUI.ViewModels;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for NewEmail.xaml
    /// </summary>
    public partial class NewEmail : Page
    {
        NewEmailViewModel _vm;
        public NewEmail()
        {
            _vm = new NewEmailViewModel();
            DataContext = _vm;
            InitializeComponent();
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            _vm.SendEmail(to.Text, new Credentials("", "", ""), subject.Text, body.Text); //TODO: Bence a new Credentials helyére tedd ide légyszi a loginelt credentialst
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            // not exactly sure what to do here
        }
    }
}
