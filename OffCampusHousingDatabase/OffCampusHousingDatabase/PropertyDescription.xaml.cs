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
        String email = "";

        public PropertyDescription(int propertyID)
        {
            InitializeComponent();

            ID = propertyID;
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);

            loadProperty();
            loadComments();
        }

        public void loadProperty()
        {
            String[] row = dbHelper.DatabaseSelectFirst("Property", "`PropertyID` = '" + ID + "'");
            AddrTextBlock.Text = row[1];
            DesTextBlock.Text = row[2];
            RentTextBlock.Text = row[4];

        }

        public void loadComments()
        {
            ArrayList rows = dbHelper.DatabaseSelect("Comment");

            foreach (String[] row in rows)
            {
                CommentListView.Items.Add(new Comment { commentID = Convert.ToInt32(row[0]), userEmail = row[2], propertyID = Convert.ToInt32(row[5]), comment = row[8] });
            }
        }

        private class Comment
        {
            public int commentID { get; set; }
            public string userEmail { get; set; }
            public int propertyID { get; set; }
            public string comment { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m;
            if (this.email.Equals(""))
            {
                m = new MainWindow();
            }
            else
            {
                m = new MainWindow(email);
            }
            App.Current.MainWindow = m;
            this.Close();
            m.Show();
        }

        public void setEmail(String email)
        {
            this.email = email;
        }

     

    }
}
