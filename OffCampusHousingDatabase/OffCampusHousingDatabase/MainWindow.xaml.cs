using System;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

namespace OffCampusHousingDatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool loggedOn;
        String email;
        DatabaseHelper dbHelper;

        public MainWindow()
        {
            InitializeComponent();
            loggedOn = false;
            email = "";

            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);

            loadProperties();
        }

        public MainWindow(String email)
        {
            InitializeComponent();
            loggedOn = true;
            this.email = email;

            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);

            //Show on the UI that the user is logged on, and hide the Login, Or, and Signup textblocks
            LoginTextblock.Text = "";
            OrTextblock.Text = "";
            SignupTextblock.Text = "";

            //Display user email on page
            UserEmailTextBlock.Text = email;

            loadProperties();

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
            LoginTextblock.TextDecorations = TextDecorations.Underline;
        }

        private void login_MouseLeave(object sender, MouseEventArgs e)
        {
            LoginTextblock.TextDecorations = null;
        }

        private void signup_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SignUp sign = new SignUp();
            App.Current.MainWindow = sign;
            this.Close();
            sign.Show();
        }

        private void signup_MouseEnter(object sender, MouseEventArgs e)
        {
            SignupTextblock.TextDecorations = TextDecorations.Underline;
        }

        private void signup_MouseLeave(object sender, MouseEventArgs e)
        {
            SignupTextblock.TextDecorations = null;
        }

        private void Email_MouseEnter(object sender, MouseEventArgs e)
        {
            UserEmailTextBlock.TextDecorations = TextDecorations.Underline;
        }

        private void Email_MouseLeave(object sender, MouseEventArgs e)
        {
            UserEmailTextBlock.TextDecorations = null;
        }

        private void Email_MouseDown(object sender, MouseEventArgs e)
        {
            //Add code that will transition to the user's profile page


        }

        private void Property_Click(object sender, MouseButtonEventArgs e)
        {
            if (PropertyListView.SelectedIndex < 0)
                return;

            PropertyItem n = (PropertyItem)PropertyListView.SelectedItem;

            PropertyDescription pd = new PropertyDescription(n.PropID);
            pd.setEmail(this.email);
            App.Current.MainWindow = pd;
            pd.Show();
            this.Close();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            loadProperties();
        }

        public void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".png";
            dlg.Filter = "Images (.png)|*.png";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                FileStream fs;
                BinaryReader br;

                string FileName = dlg.FileName;
                byte[] ImageData;
                fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fs);
                ImageData = br.ReadBytes((int)fs.Length);

                //StringBuilder sb = new StringBuilder();
                //foreach (byte b in ImageData)
                //{
                //    sb.Append(b.ToString() + ",");
                //}


                bool ex = dbHelper.databaseInsertImage("Image", "202", ImageData);
                MessageBox.Show("");
            }
        }

        public void ViewImageButton_Click(object sender, RoutedEventArgs e)
        {
            ArrayList rows = dbHelper.databaseSelectImage("Image");
            foreach (Object[] row in rows)
            {

                byte[] arr = (byte[])row[2];
                MessageBox.Show(arr.ToString());
            }
        }

        private void loadProperties()
        {
            //go through filters and see if any need to be added
            StringBuilder whereClause = new StringBuilder();
            bool andNeeded = false;


            //iterate through all of the different filter controls
            if (FilterRentTextbox.Text != "")
            {
                whereClause.Append("`MonthlyRent` < " + FilterRentTextbox.Text);
                andNeeded = true;
            }

            if(false)
            {
                if (andNeeded)
                {
                    whereClause.Append(" and " + "");
                }
                else
                {
                    whereClause.Append("");
                    andNeeded = true;
                }

            }







            PropertyListView.Items.Clear();

            ArrayList rows = dbHelper.databaseSelect("Property", whereClause.ToString());

            foreach (String[] row in rows)
            {
                PropertyListView.Items.Add(new PropertyItem { PropID = Convert.ToInt32(row[0]), Addr = row[2], Rent = Convert.ToInt32(row[5]), NumberOfRooms = Convert.ToInt32(row[4]) });
            }

        }


        private class PropertyItem
        {
            public int PropID { get; set; }
            public string Addr { get; set; }
            public int Rent { get; set; }
            public int NumberOfRooms { get; set; }
        }

        

        
    }
}
