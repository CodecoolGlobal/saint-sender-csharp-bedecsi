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
        private MainWindowViewModel _vm;

        public MainWindow()
        {
            // set DataContext to the ViewModel object
            _vm = new MainWindowViewModel();
            DataContext = _vm;
            InitializeComponent();
            EmailsListView.ItemsSource = _vm.Emails;
        }

        private void NewEmailButton_Click(object sender, RoutedEventArgs e)
        {
            NewEmail newEmail = new NewEmail();
            newEmail.Show();
        }
    }
}
