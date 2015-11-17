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

        DatabaseHelper dbHelper;
        #endregion


        #region listeners

        public UserWindow()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);
        }

        public UserWindow(String userEmail)
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);
            emailLabel.Content = userEmail;

            //This is your profile
            if (userEmail.Equals(Globals.email))
            {
                //if you are a manager
                if (Globals.isManager)
                {
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
                    userPropertyView.Items.Clear();

                    String watchedPropertiesCSV = "1,5,6";

                    ArrayList rows = dbHelper.databaseSelect("Property", "`PropertyID` IN (" + watchedPropertiesCSV +")");

                    foreach (String[] row in rows)
                    {
                        userPropertyView.Items.Add(new PropertyItem { PropID = Convert.ToInt32(row[0]), Addr = row[2], Rent = Convert.ToInt32(row[5]), NumberOfRooms = Convert.ToInt32(row[4]) });
                    }
                }
            }
            else
            {
                userPropertyView.Visibility = Visibility.Hidden;
                listLabel.Visibility = Visibility.Hidden;
                updatePass.Visibility = Visibility.Hidden;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main;
            main = new MainWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        private void propertyClick(object sender, MouseButtonEventArgs e)
        {
            if (Globals.isManager)
            {
                if (userPropertyView.SelectedIndex < 0)
                    return;

                PropertyItem n = (PropertyItem)userPropertyView.SelectedItem;

                UpdateProperty screen = new UpdateProperty(n.PropID);
                App.Current.MainWindow = screen;
                screen.Show();
                this.Close();
            }
        }


        private void NewPropertyButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateProperty screen = new UpdateProperty();
            App.Current.MainWindow = screen;
            screen.Show();
            this.Close();
        }

        #endregion


        #region logic
        
        #endregion

        private class PropertyItem
        {
            public int PropID { get; set; }
            public string Addr { get; set; }
            public int Rent { get; set; }
            public int NumberOfRooms { get; set; }
        }

        private void updatePass_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword newScreen = new ChangePassword();
            App.Current.MainWindow = newScreen;
            newScreen.Show();
            this.Close();
        }


    }
}
