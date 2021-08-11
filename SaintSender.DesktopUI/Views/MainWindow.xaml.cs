using SaintSender.Core.Models;
using SaintSender.Core.Services;
using SaintSender.DesktopUI.ViewModels;
using SaintSender.DesktopUI.Views;
using System.Windows;

namespace SaintSender.DesktopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _vm;
        private readonly NewEmail NewEmail;

        public MainWindow()
        {
            // set DataContext to the ViewModel object
            _vm = new MainWindowViewModel();
            DataContext = _vm;
            InitializeComponent();
            EmailsListView.ItemsSource = _vm.Emails;
            NewEmail = new NewEmail();
        }

        private void NewEmailButton_Click(object sender, RoutedEventArgs e)
        {
            NewEmail.Show();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.CollectEmails(new Credentials("bedecsi2ndtw1@gmail.com", "IHateWPF", ""));
            EmailsListView.ItemsSource = _vm.Emails;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.NextPage();
            _vm.CollectEmails(new Credentials("bedecsi2ndtw1@gmail.com", "IHateWPF", ""));
            EmailsListView.ItemsSource = _vm.Emails;
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.PrevPage();
            _vm.CollectEmails(new Credentials("bedecsi2ndtw1@gmail.com", "IHateWPF", ""));
            EmailsListView.ItemsSource = _vm.Emails;
        }
    }
}