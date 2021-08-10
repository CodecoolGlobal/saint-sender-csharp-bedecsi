using System;
using System.Net;
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
using SaintSender.Core;
using SaintSender.Core.Models;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        private Credentials UserOne = new Credentials("bedecsi2ndtw1@gmail.com", "IHateWPF");
        private Credentials UserTwo = new Credentials("bedecsi2ndtw2@gmail.com", "IHateWPF");
        private Credentials UserThree = new Credentials("bedecsi2ndtw3@gmail.com", "IHateWPF");
        private MainWindow MainWindow;
        public LoginScreen()
        {
            InitializeComponent();
            MainWindow = new MainWindow();
        }




        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(Verification())
            {
                MainWindow.Show();
                Close();
            }
        }

        private bool Verification()
        {

            if (!UserNameVerification()) 
            {
                MessageBox.Show("Wrong Username Or Password!");
                return false;
            }
            if (!PasswordVerification())
            {
                MessageBox.Show("Wrong Username Or Password!");
                return false;
            }
            if (MatchVerification())
            {
                return true;        
            } else
            {
                MessageBox.Show("Wrong Username Or Password!");
                return false;
            }


        }


        private bool UserNameVerification()
        {
            if (UserOne.EmailAddress.Equals(txtUsername.Text) || UserTwo.EmailAddress.Equals(txtUsername.Text) || UserThree.EmailAddress.Equals(txtUsername.Text))
            {
                return true;
            }
            return false;
        }

        private bool PasswordVerification()
        {
            if(UserOne.Password.Equals(txtPassword.Password) || UserTwo.Password.Equals(txtPassword.Password) || UserThree.Password.Equals(txtPassword.Password))
            {
                return true;
            }
            return false;
        }

        private bool MatchVerification()
        {
            if((UserOne.EmailAddress.Equals(txtUsername.Text) && UserOne.Password.Equals(txtPassword.Password)) || (UserTwo.EmailAddress.Equals(txtUsername.Text) && UserTwo.Password.Equals(txtPassword.Password)) || (UserThree.EmailAddress.Equals(txtUsername.Text) && UserThree.Password.Equals(txtPassword.Password)))
            {
                return true;
            }
            return false;
        }

        private void NetCheck_Click(object sender, RoutedEventArgs e)
        {
            if(CheckNetwork())
            {
                MessageBox.Show("You have internet connection.");
            } else
            {
                MessageBox.Show("Don't haven internet connection.");
            }
        }

        private bool CheckNetwork()
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
