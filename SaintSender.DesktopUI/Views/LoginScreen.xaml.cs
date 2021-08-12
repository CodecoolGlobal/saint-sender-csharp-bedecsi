using SaintSender.Core.Models;
using SaintSender.Core.Services;
using System;
using System.IO;
using System.Net;
using System.Windows;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {

        private MainWindow MainWindow;
        private readonly EmailService EmailService;
        private readonly Credentials Credentials;
        private readonly Serializer Serializer;



        public LoginScreen()
        {
            InitializeComponent();
            EmailService = new EmailService();
            Credentials = new Credentials();
            Serializer = new Serializer(Credentials);
        }


        public void SetCredential()
        {
            Credentials.EmailAddress = txtUsername.Text;
            Credentials.Password = txtPassword.Password;
            Credentials.Name = txtUsername.Text.Split('@')[0];
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (IsConnectedToNetwork())
            {
                if (EmailService.Authenticate(txtUsername.Text, txtPassword.Password))
                {


                    FileInfo[] fileInfos = Serializer.GetXMLfiles();
                    foreach (FileInfo file in fileInfos)
                    {

                        if (file.Name.Equals(txtUsername.Text + ".xml"))
                        {
                            SetCredential();
                            // set MainWindow Credential
                            MainWindow = new MainWindow(Credentials);
                            MainWindow.Serializer.credentials = Credentials;
                            MainWindow.Show();
                            Close();
                            return;
                        }
                    }

                    SetCredential();

                    var hashedPass = BCrypt.Net.BCrypt.HashPassword(txtPassword.Password);
                    Serializer.credentials.Password = hashedPass;
                    Serializer.XMLsave();
                    Credentials.Password = txtPassword.Password;

                    // set MainWindow Credential
                    MainWindow = new MainWindow(Credentials);
                    
                    MainWindow.Show();
                    Close();
                    return;
                }
                else
                {
                    MessageBox.Show("Wrong Username Or Password!");
                }

            }
            else
            {
                if (OfflineVerification())
                {
                    SetCredential();
                    // set MainWindow Credential
                    MainWindow = new MainWindow(Credentials);
                    MainWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Wrong Username Or Password!");
                }
            }
        }

        private bool OfflineVerification()
        {
            FileInfo[] fileInfos = Serializer.GetXMLfiles();
            foreach (FileInfo file in fileInfos)
            {
                if (file.Name.Equals(txtUsername.Text + ".xml"))
                {
                    Credentials LoadCredentials = Serializer.ReadXML(file);
                    return BCrypt.Net.BCrypt.Verify(txtPassword.Password, LoadCredentials.Password);
                }
            }
            return false;
        }

        private void NetCheck_Click(object sender, RoutedEventArgs e)
        {
            if (IsConnectedToNetwork())
            {
                MessageBox.Show("You have internet connection.");
            }
            else
            {
                MessageBox.Show("Don't haven internet connection.");
            }
        }

        private bool IsConnectedToNetwork()
        {
            WebRequest webRequest = WebRequest.Create("http://www.google.com");
            WebResponse webResponse;
            try
            {
                webResponse = webRequest.GetResponse();
                webResponse.Close();
                webRequest = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
