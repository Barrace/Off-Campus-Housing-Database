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
using System.Windows.Shapes;
using System.Configuration;

namespace OffCampusHousingDatabase
{
    public partial class Login : Window
    {
        #region Variables

        DatabaseHelper dbHelper;

        #endregion

        #region Listeners

        public Login()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);
            EmailTextbox.Focus();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            loginUser(EmailTextbox.Text, PasswordBox.Password);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            //switch back to main view taking no other action
            MainWindow m = new MainWindow();
            App.Current.MainWindow = m;
            this.Close();
            m.Show();
        }

        private void signup_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SignUp sign = new SignUp();
            App.Current.MainWindow = sign;
            this.Close();
            sign.Show();
        }

        private void signup_MouseEnter(object sender, MouseEventArgs e)
        {
            SignupTextblock.TextDecorations = TextDecorations.Underline;
        }

        private void signup_MouseLeave(object sender, MouseEventArgs e)
        {
            SignupTextblock.TextDecorations = null;
        }

        #endregion

        #region Logic

        private void loginUser(String email, String password)
        {
            //First validate to make sure all of the fields are filled with some information
            if (email.Equals(""))
            {
                StatusLabel.Text = "Email cannot be empty, please enter valid email";
                return;
            }
            else if (!email.Contains("@"))
            {
                StatusLabel.Text = "Invalid Email Address";
                return;
            }
            else if (email.Contains(" "))
            {
                StatusLabel.Text = "Emails cannot contain any spaces";
                return;
            }
            else if (password.Length < 6)
            {
                StatusLabel.Text = "Password must be at least 6 characters";
                return;
            }

            String pw = ((Int32)password.GetHashCode()).ToString();

            if (dbHelper.DatabaseSelect("User", "`email` = '" + email + "' AND `password` = '" + pw + "'").Count == 0)
            {
                StatusLabel.Text = "Invalid Credentials";
                return;
            }


            //Authenticated Successfully
            MainWindow m = new MainWindow(email);
            App.Current.MainWindow = m;
            this.Close();
            m.Show();
        }


        #endregion

    }
}
