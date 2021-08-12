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
        private EmailService emailService;
        private Credentials Credentials;
        private Serializer serializer;



        public LoginScreen()
        {
            InitializeComponent();
            MainWindow = new MainWindow();
            emailService = new EmailService();
            Credentials = new Credentials();
            serializer = new Serializer(Credentials);
        }


        public void setCredential()
        {
            Credentials.EmailAddress = txtUsername.Text;
            Credentials.Password = txtPassword.Password;
            string[] NamePart = txtUsername.Text.Split('@');
            Credentials.Name = NamePart[0];
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (isNetwork())
            {
                if (emailService.Authenticate(txtUsername.Text, txtPassword.Password))
                {

                    
                    FileInfo[] fileInfos = serializer.GetXMLfiles();
                    foreach (FileInfo file in fileInfos)
                    {
                        
                        if (file.Name.Equals(txtUsername.Text+".xml"))
                        {
                            setCredential();
                            MainWindow._serializer.credentials = Credentials;
                            MainWindow.Credentials = Credentials;
                        
                            MainWindow.Show();
                            Close();
                            return;
                        }
                    }
                    
                    setCredential();

                    var hashedPass = BCrypt.Net.BCrypt.HashPassword(txtPassword.Password);
                    serializer.credentials.Password = hashedPass;

                    serializer.XMLsave();
                    MainWindow._serializer.credentials = Credentials;
                    MainWindow.Credentials = Credentials;
                    
                    MainWindow.Show();
                    Close();
                    return;
                }
                else
                {
                    MessageBox.Show("Wrong Username Or Password!");
                }

            } else
            {
               if(offlineVerification())
                {
                    setCredential();
                    MainWindow._serializer.credentials = Credentials;
                    MainWindow.Credentials = Credentials;
                    MainWindow.Show();
                    Close();
                } else
                {
                    MessageBox.Show("Wrong Username Or Password!");
                }
            }
        }

       private bool offlineVerification()
        {
            FileInfo[] fileInfos = serializer.GetXMLfiles();
            foreach (FileInfo file in fileInfos)
            {
                if (file.Name.Equals(txtUsername.Text + ".xml"))
                {
                    Credentials LoadCredentials = serializer.ReadXML(file);
                    return BCrypt.Net.BCrypt.Verify(txtPassword.Password, LoadCredentials.Password);
                }
            }
            return false;
        }

        private void NetCheck_Click(object sender, RoutedEventArgs e)
        {
            if(isNetwork())
            {
                MessageBox.Show("You have internet connection.");
            } else
            {
                MessageBox.Show("Don't haven internet connection.");
            }
        }

        private bool isNetwork()
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
            catch(Exception)
            {
                webRequest = null;
                return false;
            }
        }
    }
}
