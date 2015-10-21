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
    /// Interaction logic for PropertyDescription.xaml
    /// </summary>
    public partial class PropertyDescription : Window
    {
        DatabaseHelper dbHelper;

        int ID;

        public PropertyDescription(int propertyID)
        {
            InitializeComponent();

            ID = propertyID;
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);

            loadProperty();
        }

        public void loadProperty()
        {
            String[] row = dbHelper.DatabaseSelectFirst("Property", "`PropertyID` = '" + ID + "'");
            AddrTextBlock.Text = row[1];
            DesTextBlock.Text = row[2];
            RentTextBlock.Text = row[4];
            RealTextBlock.Text = row[5];

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            App.Current.MainWindow = m;
            this.Close();
            m.Show();
        }
    }
}
