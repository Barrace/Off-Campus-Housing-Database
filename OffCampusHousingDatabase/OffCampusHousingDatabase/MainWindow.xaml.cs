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
      #region Variables

      bool loggedOn;
      String email;
      DatabaseHelper dbHelper;

      #endregion


      #region Listeners
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
         loginTextblock.Text = "";
         OrTextblock.Text = "";
         signupTextblock.Text = "";

         //Display user email on page
         userEmailTextBlock.Text = email;

         loadProperties();

      }

      private void loginMouseDown(object sender, MouseButtonEventArgs e)
      {
         Login l = new Login();
         App.Current.MainWindow = l;
         this.Close();
         l.Show();
      }

      private void loginMouseEnter(object sender, MouseEventArgs e)
      {
         loginTextblock.TextDecorations = TextDecorations.Underline;
      }

      private void loginMouseLeave(object sender, MouseEventArgs e)
      {
         loginTextblock.TextDecorations = null;
      }

      private void signupMouseDown(object sender, MouseButtonEventArgs e)
      {
         SignUp sign = new SignUp();
         App.Current.MainWindow = sign;
         this.Close();
         sign.Show();
      }

      private void signupMouseEnter(object sender, MouseEventArgs e)
      {
         signupTextblock.TextDecorations = TextDecorations.Underline;
      }

      private void signupMouseLeave(object sender, MouseEventArgs e)
      {
         signupTextblock.TextDecorations = null;
      }

      private void emailMouseEnter(object sender, MouseEventArgs e)
      {
         userEmailTextBlock.TextDecorations = TextDecorations.Underline;
      }

      private void emailMouseLeave(object sender, MouseEventArgs e)
      {
         userEmailTextBlock.TextDecorations = null;
      }

      private void emailMouseDown(object sender, MouseEventArgs e)
      {
         //Add code that will transition to the user's profile page
          UserWindow user = new UserWindow();
          App.Current.MainWindow = user;
          this.Close();
          user.Show();
      }

      private void propertyClick(object sender, MouseButtonEventArgs e)
      {
         if (propertyListView.SelectedIndex < 0)
            return;

         PropertyItem n = (PropertyItem)propertyListView.SelectedItem;

         PropertyDescription pd = new PropertyDescription(n.PropID);
         pd.setEmail(this.email);
         App.Current.MainWindow = pd;
         pd.Show();
         this.Close();
      }

      private void filterButtonClick(object sender, RoutedEventArgs e)
      {
         loadProperties();
      }

      public void uploadImageButtonClick(object sender, RoutedEventArgs e)
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

      public void viewImageButtonClick(object sender, RoutedEventArgs e)
      {
         ArrayList rows = dbHelper.databaseSelectImage("Image");
         foreach (Object[] row in rows)
         {
            byte[] arr = (byte[])row[2];
            MessageBox.Show(arr.ToString());
         }
      }

      #endregion


      #region Logic
      private void loadProperties()
      {
         //go through filters and see if any need to be added
         StringBuilder whereClause = new StringBuilder();
         bool andNeeded = false;


         //iterate through all of the different filter controls
         if (filterRentTextbox.Text != "")
         {
            whereClause.Append("`MonthlyRent` < " + filterRentTextbox.Text);
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

         //doesn't work when both are filtered together
         if (filterRoomTextbox.Text != "")
         {
               whereClause.Append("`NumberOfRooms` > " + filterRoomTextbox.Text);
               andNeeded = true;
         }

         if (false)
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



         propertyListView.Items.Clear();

         ArrayList rows = dbHelper.databaseSelect("Property", whereClause.ToString());

         foreach (String[] row in rows)
         {
            propertyListView.Items.Add(new PropertyItem { PropID = Convert.ToInt32(row[0]), Addr = row[2], Rent = Convert.ToInt32(row[5]), NumberOfRooms = Convert.ToInt32(row[4]) });
         }

      }

      private class PropertyItem
      {
         public int PropID { get; set; }
         public string Addr { get; set; }
         public int Rent { get; set; }
         public int NumberOfRooms { get; set; }
      }

      #endregion


   }
}
