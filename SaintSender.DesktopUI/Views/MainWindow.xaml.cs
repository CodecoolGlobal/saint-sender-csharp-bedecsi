using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SaintSender.Core.Models;
using SaintSender.DesktopUI.ViewModels;
using SaintSender.DesktopUI.Views;

namespace SaintSender.DesktopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _vm;
        private readonly NewEmail NewEmail;
        private EmailDetailWindow _emailDetailWindow;
        public Serializer Serializer { get; set; }
        public Credentials LoggedInUser { get; }

        public MainWindow(Credentials loggedInUser)
        {
            LoggedInUser = loggedInUser;
            // set DataContext to the ViewModel object
            _vm = new MainWindowViewModel(loggedInUser);
            DataContext = _vm;
            Serializer = new Serializer();
            InitializeComponent();
            if (File.Exists(Environment.CurrentDirectory + "\\Backup\\backup.xml"))
            {
                var boxButton = MessageBoxButton.YesNo;
                var boxText = "Backup found. Do you want to load emails from backup?";
                var boxCaption = "Load Backup";
                MessageBoxResult result = MessageBox.Show(boxText, boxCaption, boxButton);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        EmailsListView.ItemsSource = Serializer.ReadXMLbackup();
                        break;
                    case MessageBoxResult.No:
                        EmailsListView.ItemsSource = _vm.Emails;
                        break;
                }
            }
            else
            {
                EmailsListView.ItemsSource = _vm.Emails;
            }
            NewEmail = new NewEmail(LoggedInUser);
        }

        private void NewEmailButton_Click(object sender, RoutedEventArgs e)
        {
            NewEmail.Show();
        }


        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            SetEmails();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.NextPage();
            SetEmails();
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.PrevPage();
            SetEmails();
        }

        private void LogoutlButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Delete_Credentials(object sender, RoutedEventArgs e)
        {
            Serializer.DeleteXMLfiles();
            MessageBox.Show("Your Credentials have been removed!");
        }

        private void Search_Submit(object sender, RoutedEventArgs e)
        {
           EmailsListView.ItemsSource = _vm.FilterEmailsBySearchTerms(SerchTextBox.Text);
        }
       
        private void BackupButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.CollectAllEmails(LoggedInUser);
            Serializer.XMLbackup(_vm.Emails);
            MessageBox.Show("Backup created");
        }
    

        private void SetEmails()
        {
            _vm.CollectEmails(LoggedInUser);
            EmailsListView.ItemsSource = _vm.Emails;
        }

        void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Control control = (Control)sender;
            Email clickedItem = (Email)control.DataContext;
            _emailDetailWindow = new EmailDetailWindow(LoggedInUser, clickedItem);
            _emailDetailWindow.Show();
        }

    }
}