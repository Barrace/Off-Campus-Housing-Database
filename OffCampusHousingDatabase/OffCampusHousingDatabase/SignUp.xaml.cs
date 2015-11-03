using System;
using System.Collections;
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
    public partial class SignUp : Window
    {
        #region Variables

        DatabaseHelper dbHelper;

        #endregion

        #region Listeners

        public SignUp()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);
            emailTextbox.Focus();
        }

        private void signup_Click(object sender, RoutedEventArgs e)
        {
            signupUser(emailTextbox.Text, passwordBox.Password, passwordConfirmBox.Password, ManagerRadioButton.IsChecked());
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            //switch back to main view taking no other action
            MainWindow m = new MainWindow();
            App.Current.MainWindow = m;
            this.Close();
            m.Show();
        }

        private void login_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Login l = new Login();
            App.Current.MainWindow = l;
            this.Close();
            l.Show();
        }

        private void login_MouseEnter(object sender, MouseEventArgs e)
        {
            loginTextblock.TextDecorations = TextDecorations.Underline;
        }

        private void login_MouseLeave(object sender, MouseEventArgs e)
        {
            loginTextblock.TextDecorations = null;
        }

        #endregion

        #region Logic

        private void signupUser(String email, String password1, String password2, bool isManager)
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
            else if (password1.Length < 6)
            {
                StatusLabel.Text = "Password must be at least 6 characters";
                return;
            }
            else if (!password1.Equals(password2))
            {
                StatusLabel.Text = "Password Confirmation does not match Password";
                return;
            }
            else if (!((bool)StudentRadioButton.IsChecked || (bool)ManagerRadioButton.IsChecked))
            {
                StatusLabel.Text = "Please select your account status (Student / Manager)";
                return;
            }

            //if you reach here you have passed all of the input checks, you can now validate 
            //the user email to make sure that it has not been used
            try
            {
                //Check the uniqueness of the user email
                if (dbHelper.DatabaseSelect("User", "`email` = '" + email + "'").Count > 0)
                {
                    StatusLabel.Text = "This email is already in use, please log in, or use a different email";
                    return;
                }

                //Hash user password to encrypt it
                String pw = ((Int32)password1.GetHashCode()).ToString();
                int isManagerBit = 0;
                if (isManager)
                    isManagerBit = 1;

                //Insert the email and user info
                if (dbHelper.DatabaseInsert("User", "`email`, `password`, `isManager`", "'" + email + "','" + pw + "','" + isManagerBit + "'"))
                {

                    //successfully inserted, switch views and update the global user email variable
                    MainWindow m = new MainWindow(email);
                    App.Current.MainWindow = m;
                    this.Close();
                    m.Show();


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error");
            }
        }

        #endregion

    }
}
