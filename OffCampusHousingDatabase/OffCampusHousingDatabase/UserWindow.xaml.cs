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

        String email;
        DatabaseHelper dbHelper;
        bool isManager;
        bool isOwnProfile;

        #endregion


        #region listeners

        public UserWindow()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);
        }

        public UserWindow(String e, bool isOwn)
        {
            InitializeComponent();
            email = e;
            isOwnProfile = isOwn;
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);

            isManager = true;
            if (isManager)
            {
                listLabel.Content = "My Properties:";
                userPropertyView.Items.Clear();

                ArrayList rows = dbHelper.databaseSelect("Property", "`ManagerEmail` = '" + email + "'");

                foreach (String[] row in rows)
                {
                    userPropertyView.Items.Add(new PropertyItem { PropID = Convert.ToInt32(row[0]), Addr = row[2], Rent = Convert.ToInt32(row[5]), NumberOfRooms = Convert.ToInt32(row[4]) });
                }
            }
            emailLabel.Content = "Email: " + email;

            if (isOwnProfile)
            {

            }
            else
            {
                updatePass.IsEnabled = false;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main;
            main = new MainWindow(email);
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        private void propertyClick(object sender, MouseButtonEventArgs e)
        {
            if (isManager)
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


    }
}
