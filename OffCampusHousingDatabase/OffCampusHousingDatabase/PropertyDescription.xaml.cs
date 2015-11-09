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
            String[] row = dbHelper.databaseSelectFirst("Property", "`PropertyID` = '" + ID + "'");
            AddrTextBlock.Text = row[2];
            DesTextBlock.Text = row[3];
            RentTextBlock.Text = row[5];

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

        public void loadComments()
        {
            commentListView.Items.Clear();

            ArrayList rows = dbHelper.databaseSelect("Comment");

            foreach (String[] row in rows)
            {
                commentListView.Items.Add(new Comment { CommentID = Convert.ToInt32(row[0]), UserEmail = row[2], PropID = Convert.ToInt32(row[5]), comment = row[4] });
            }
        }

        private class Comment
        {
            public int CommentID { get; set; }
            public string UserEmail { get; set; }
            public int PropID { get; set; }
            public string comment { get; set; }
        }
    }
}
