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
            StatusLabel.Text = loginUser(EmailTextbox.Text, PasswordBox.Password);

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Globals.goBack();
            this.Close();
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

        private String loginUser(String email, String password)
        {
            //First validate to make sure all of the fields are filled with some information
            if (email.Equals(""))
            {
                return "Email cannot be empty, please enter valid email";
            }
            else if (!email.Contains("@"))
            {
                return "Invalid Email Address";
            }
            else if (email.Contains(" "))
            {
                return "Emails cannot contain any spaces";
            }
            else if (password.Length < 6)
            {
                return "Password must be at least 6 characters";
            }

            String pw = ((Int32)password.GetHashCode()).ToString();

            String[] res = dbHelper.databaseSelectFirst("User", "`email` = '" + email + "' AND `password` = '" + pw + "'");

            if (res.Length == 0)
            {
                return "Invalid Credentials";
            }

            //Authenticated Successfully at this point
            Globals.email = email;
            Globals.loggedOn = true;
            Globals.isManager = res[2].Equals("1");

            Globals.goBack();
            this.Close();

            //No errors, return an empty error message
            return "";
        }


        #endregion

    }
}
