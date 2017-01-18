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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {

        #region variables
        bool isOwnProfile;
        DatabaseHelper dbHelper;
        string userEmail;
        string userPhone;
        #endregion


        #region listeners

        public UserWindow()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);
        }

        public UserWindow(String email)
        {
            initializeScreen(email);

            //This is your profile
            if (userEmail.Equals(Globals.email))
            {
                selfProfileInit();
            }
            else
            {
                isOwnProfile = false;
                userPropertyView.Visibility = Visibility.Hidden;
                listLabel.Visibility = Visibility.Hidden;
                updatePass.Visibility = Visibility.Hidden;
            }

        }

        //back button functionality must work from all pages
        private void back_Click(object sender, RoutedEventArgs e)
        {
            Globals.goBack();
            this.Close();
        }

        private void propertySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!Globals.isManager || userPropertyView.SelectedIndex < 0)
            {
                deletePropertyButton.Visibility = Visibility.Hidden;
            }
            else
            {
                deletePropertyButton.Visibility = Visibility.Visible;
            }
        }

        private void property_Click(object sender, MouseButtonEventArgs e)
        {
            if (Globals.isManager)
            {
                if (userPropertyView.SelectedIndex < 0)
                    return;

                PropertyItem n = (PropertyItem)userPropertyView.SelectedItem;

                //Add this page onto the window stack
                Globals.windowStack.Push("UserWindow|" + userEmail);

                UpdateProperty screen = new UpdateProperty(n.PropID);
                App.Current.MainWindow = screen;
                screen.Show();
                this.Close();
            }
            else
            {
                if (userPropertyView.SelectedIndex < 0)
                    return;

                PropertyItem n = (PropertyItem)userPropertyView.SelectedItem;

                //Add this page onto the window stack
                Globals.windowStack.Push("UserWindow|" + userEmail);

                PropertyDescription pd = new PropertyDescription(n.PropID);
                App.Current.MainWindow = pd;
                pd.Show();
                this.Close();
            }
        }


        private void newPropertyButton_Click(object sender, RoutedEventArgs e)
        {
            //Add this page onto the window stack
            Globals.windowStack.Push("UserWindow|" + userEmail);

            UpdateProperty screen = new UpdateProperty();
            App.Current.MainWindow = screen;
            screen.Show();
            this.Close();
        }

        private void deletePropertyButton_Click(object sender, RoutedEventArgs e)
        {
            if (userPropertyView.SelectedIndex < 0)
                return;

            PropertyItem n = (PropertyItem)userPropertyView.SelectedItem;

            dbHelper.databaseDelete("Property", "`PropertyID` = " + n.PropID);
            selfProfileInit();
        }

        private void updatePass_Click(object sender, RoutedEventArgs e)
        {

            ChangePassword newScreen = new ChangePassword();
            App.Current.MainWindow = newScreen;
            newScreen.Show();
            this.Close();
        }

        private void phone_mouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isOwnProfile)
            {
                string[] arr = dbHelper.databaseSelectFirst("User", "`email`='" + Globals.email + "'");
                userPhone = arr[3];
                phoneTextBox.Text = userPhone;

                thisPhoneLabel.Visibility = Visibility.Hidden;
                phoneTextBox.Visibility = Visibility.Visible;
                changePhoneButton.Visibility = Visibility.Visible;
            }
        }

        private void signup_mouseDown(object sender, MouseButtonEventArgs e)
        {
            //Add this page onto the window stack
            Globals.windowStack.Push("UserWindow|" + userEmail);

            SignUp signUpPage = new SignUp();
            App.Current.MainWindow = signUpPage;
            this.Close();
            signUpPage.Show();
        }

        private void signup_mouseEnter(object sender, MouseEventArgs e)
        {
            signupTextblock.TextDecorations = TextDecorations.Underline;
        }

        private void signup_mouseLeave(object sender, MouseEventArgs e)
        {
            signupTextblock.TextDecorations = null;
        }

        private void email_mouseEnter(object sender, MouseEventArgs e)
        {
            userEmailTextBlock.TextDecorations = TextDecorations.Underline;
        }

        private void email_mouseLeave(object sender, MouseEventArgs e)
        {
            userEmailTextBlock.TextDecorations = null;
        }

        private void email_mouseDown(object sender, MouseEventArgs e)
        {
            //Add this page onto the window stack
            Globals.windowStack.Push("UserWindow|" + userEmail);

            if (!Globals.email.Equals(userEmail))
            {
                UserWindow u = new UserWindow(Globals.email);
                App.Current.MainWindow = u;
                this.Close();
                u.Show();
            }
        }

        private void login_mouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Globals.loggedOn)
            {
                Globals.loggedOn = false;
                Globals.email = "";
                Globals.isManager = false;

                //Add this page onto the window stack
                Globals.windowStack.Push("UserWindow|" + userEmail);

                MainWindow m = new MainWindow();
                App.Current.MainWindow = m;
                this.Close();
                m.Show();
            }
            else
            {
                //Add this page onto the window stack
                Globals.windowStack.Push("UserWindow|" + userEmail);

                Login log = new Login();
                App.Current.MainWindow = log;
                this.Close();
                log.Show();
            }
        }

        private void login_mouseEnter(object sender, MouseEventArgs e)
        {
            loginTextblock.TextDecorations = TextDecorations.Underline;
        }

        private void login_mouseLeave(object sender, MouseEventArgs e)
        {
            loginTextblock.TextDecorations = null;
        }

        private void changePhoneButton_Click(object sender, RoutedEventArgs e)
        {
            //db update phone to new phone
            dbHelper.databaseUpdate("User", "`phone`='" + phoneTextBox.Text + "'", "`email`='" + Globals.email + "'"); //tablename, column, where

            string phoneNum = phoneTextBox.Text;

            //change box back to label
            phoneLabel.Content = "Phone: ";
            phoneTextBox.Text = "";
            thisPhoneLabel.Content = phoneNum;
            thisPhoneLabel.Visibility = Visibility.Visible;

            changePhoneButton.Visibility = Visibility.Hidden;
            phoneTextBox.Visibility = Visibility.Hidden;
        }

        #endregion


        #region logic

        public void selfProfileInit()
        {
            isOwnProfile = true;
            changePhoneNotification.Visibility = Visibility.Visible;

            //if you are a manager
            if (Globals.isManager)
            {
                newPropertyButton.Visibility = Visibility.Visible;
                listLabel.Content = "My Properties:";
                userPropertyView.Items.Clear();

                ArrayList rows = dbHelper.databaseSelect("Property", "`ManagerEmail` = '" + Globals.email + "'");

                foreach (String[] row in rows)
                {
                    userPropertyView.Items.Add(new PropertyItem { PropID = Convert.ToInt32(row[0]), Addr = row[2], Rent = Convert.ToInt32(row[5]), NumberOfRooms = Convert.ToInt32(row[4]) });
                }
            }
            else
            {
                listLabel.Content = "Watched Properties:";
                newPropertyButton.Visibility = Visibility.Hidden;

                userPropertyView.Items.Clear();

                String watchedPropertiesCSV = getUserWatchedPropertyCSV();

                ArrayList rows = dbHelper.databaseSelect("Property", "`PropertyID` IN (" + watchedPropertiesCSV + ")");

                foreach (String[] row in rows)
                {
                    String dateStr = (row[6].Split(' '))[0];
                    userPropertyView.Items.Add(new PropertyItem { PropID = Convert.ToInt32(row[0]), Addr = row[2], Rent = Convert.ToInt32(row[5]), NumberOfRooms = Convert.ToInt32(row[4]), DateAvailable = dateStr });
                }
            }
        }
        public void initializeScreen(string email)
        {
            InitializeComponent();
            changePhoneNotification.Visibility = Visibility.Hidden;
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);

            userEmail = email;
            try
            {
                string[] phoneNumArr = dbHelper.databaseSelectFirst("User", "`email` = '" + email + "'");
                userPhone = phoneNumArr[3]; //db hit 
            }
            catch (NullReferenceException ex)
            {
                userPhone = "           ";
            }
            userEmailTextBlock.Text = Globals.email;

            emailLabel.Content = "Email: ";

            thisEmailLabel.Content = userEmail;

            thisPhoneLabel.Content = userPhone;
            
            phoneLabel.Content = "Phone: ";

            phoneTextBox.Visibility = Visibility.Hidden;
            changePhoneButton.Visibility = Visibility.Hidden;
            newPropertyButton.Visibility = Visibility.Hidden;

            if (Globals.loggedOn)
            {
                userEmailTextBlock.Visibility = Visibility.Visible;

                loginTextblock.Text = "Log Out";
                orTextblock.Visibility = Visibility.Hidden;
                signupTextblock.Visibility = Visibility.Hidden;
            }
            else
            {
                userEmailTextBlock.Visibility = Visibility.Hidden;
                newPropertyButton.Visibility = Visibility.Hidden;

                loginTextblock.Text = "Login";
                orTextblock.Visibility = Visibility.Visible;
                signupTextblock.Visibility = Visibility.Visible;
            }
        }

        public String getUserWatchedPropertyCSV()
        {
            String[] user = dbHelper.databaseSelectFirst("User", "`email` = '" + Globals.email + "'");

            return (String)user[4];
        }

        private class PropertyItem
        {
            public int PropID { get; set; }
            public string Addr { get; set; }
            public int Rent { get; set; }
            public int NumberOfRooms { get; set; }
            public string DateAvailable { get; set; }
        }

        #endregion        

    }
}
