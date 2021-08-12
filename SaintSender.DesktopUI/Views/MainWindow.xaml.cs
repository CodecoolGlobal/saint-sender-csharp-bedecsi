using SaintSender.Core.Models;
using SaintSender.DesktopUI.ViewModels;
using System.Windows;

namespace SaintSender.DesktopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Serializer _serializer { get; set; }
        public Credentials Credentials { get; set; }
        private MainWindowViewModel _vm;

        public MainWindow()
        {
            // set DataContext to the ViewModel object
            _vm = new MainWindowViewModel();
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
    }

}
