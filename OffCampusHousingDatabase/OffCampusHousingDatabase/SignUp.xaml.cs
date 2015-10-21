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
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        DatabaseHelper dbHelper;
        public SignUp()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);
            emailTextbox.Focus();
        }

        private void signup_Click(object sender, RoutedEventArgs e)
        {
            //First validate to make sure all of the fields are filled with some information
            if (emailTextbox.Text.Equals(""))
            {
                StatusLabel.Text = "Email cannot be empty, please enter valid email";
                return;
            }
            else if (!emailTextbox.Text.Contains("@"))
            {
                StatusLabel.Text = "Invalid Email Address";
                return;
            }
            else if (passwordBox.Password.Length<6)
            {
                StatusLabel.Text = "Password must be at least 6 characters";
                return;
            }
            else if (!passwordBox.Password.Equals(passwordConfirmBox.Password))
            {
                StatusLabel.Text = "Password Confirmation does not match Password";
                return;
            }
            else if(!((bool)StudentRadioButton.IsChecked || (bool)ManagerRadioButton.IsChecked))
            {
                StatusLabel.Text = "Please select your account status (Student / Manager)";
                return;
            }

            //if you reach here you have passed all of the input checks, you can now validate 
            //the user email to make sure that it has not been used
            try
            {
                //Check the uniqueness of the user email
                if (dbHelper.DatabaseSelect("User", "`email` = '" + emailTextbox.Text + "'").Count > 0)
                {
                    StatusLabel.Text = "This email is already in use, please log in, or use a different email";
                    return;
                }

                //Hash user password to encrypt it
                String pw = ((Int32)passwordBox.Password.GetHashCode()).ToString();
                int isManager = 0;
                if((bool)ManagerRadioButton.IsChecked)
                    isManager = 1;

                //Insert the email and user info
                if (dbHelper.DatabaseInsert("User", "`email`, `password`, `isManager`", "'" + emailTextbox.Text + "','" + pw + "','" + isManager + "'"))
                {

                    //successfully inserted, switch views and update the global user email variable
                    MainWindow m = new MainWindow(emailTextbox.Text);
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
        
    }
}
