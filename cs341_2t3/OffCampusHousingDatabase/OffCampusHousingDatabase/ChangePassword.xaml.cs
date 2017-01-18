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
    /// 
    /// </summary>
    public partial class ChangePassword : Window
    {
        #region Variables

        DatabaseHelper dbHelper;
        string oldPass;
        string oldPassHash;
        string newPass;
        string newPassConfirm;
        string newPassHash;

        #endregion

        #region Listeners

        public ChangePassword()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);
            oldPasswordBox.Focus();
            
        }

        private void changePass_Click(object sender, RoutedEventArgs e)
        {
            oldPass = oldPasswordBox.Password;
            oldPassHash = ((Int32)oldPass.GetHashCode()).ToString();
            newPass = newPasswordBox.Password;
            newPassConfirm = newPasswordConfirmBox.Password;
            newPassHash = ((Int32)newPass.GetHashCode()).ToString();
            string[] arr = dbHelper.databaseSelectFirst("User", "`email` = '" + Globals.email + "'");
            string oldPassActual = arr[1];

            if (validatePass())
            {
                dbHelper.databaseUpdate("User", "`password`='" + newPassHash + "'", "`email`='" + Globals.email + "'"); //tablename, column, where
                UserWindow window;
                window = new UserWindow(Globals.email);
                App.Current.MainWindow = window;
                this.Close();
                window.Show();
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            UserWindow window;
            window = new UserWindow(Globals.email);
            App.Current.MainWindow = window;
            this.Close();
            window.Show();
        }

        #endregion

        #region Logic

        private bool validatePass()
        {
            if (newPass.Length < 6 || newPassConfirm.Length < 6)
            {
                StatusLabel.Text = "Password must be at least 6 characters";
                return false;
            }
            else if (!newPass.Any(x => char.IsLetter(x)))
            {
                StatusLabel.Text = "Password must contain at least one letter";
                return false;
            }
            else if (!newPass.Any(x => char.IsNumber(x)))
            {
                StatusLabel.Text = "Password must contain at least one number";
                return false;
            }
            else if (!newPass.Equals(newPassConfirm))
            {
                StatusLabel.Text = "Password Confirmation does not match";
                return false;
            }
            return true;
        }

        #endregion
    }
}
