using SaintSender.Core.Models;
using SaintSender.DesktopUI.ViewModels;
using SaintSender.DesktopUI.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SaintSender.DesktopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Serializer _serializer { get; set; }
        private MainWindowViewModel _vm;
        private EmailDetailWindow _emailDetailWindow;
        public Credentials LoggedInUser { get; }

        public MainWindow(Credentials loggedInUser)
        {
            LoggedInUser = loggedInUser;
            // set DataContext to the ViewModel object
            _vm = new MainWindowViewModel(loggedInUser);
            DataContext = _vm;
            _serializer = new Serializer();
            InitializeComponent();
            EmailsListView.ItemsSource = _vm.Emails;
        }


        private void LogoutlButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Delete_Credentials(object sender, RoutedEventArgs e)
        {
            _serializer.DeleteXMLfiles();
            MessageBox.Show("Your Credentials have been removed!");
        }

        void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Control control = (Control)sender;
            Email clickedItem =(Email)control.DataContext;
            _emailDetailWindow = new EmailDetailWindow(LoggedInUser, clickedItem);
            _emailDetailWindow.Show();
        }
    }

}
