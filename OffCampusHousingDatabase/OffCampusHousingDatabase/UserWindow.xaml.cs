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

namespace OffCampusHousingDatabase
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {

        #region variables
        String email;
        #endregion

        #region listeners

        public UserWindow(String e)
        {
            InitializeComponent();
            email = e;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main;
            main = new MainWindow(email);
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }
        #endregion

        #region logic
        /*
            if email was set to the email that the user is logged into:
                Change password and update contact info is active
            else
                controls disabled and just a view
         */
        #endregion

    }
}
