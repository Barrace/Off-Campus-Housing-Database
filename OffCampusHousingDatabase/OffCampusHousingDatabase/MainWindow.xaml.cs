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
using System.Text.RegularExpressions;

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
          dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);
          loadAllProperties();

          if (Globals.loggedOn)
          {
              //Show on the UI that the user is logged on, and hide the Login, Or, and Signup textblocks
              loginTextblock.Text = "";
              OrTextblock.Text = "";
              signupTextblock.Text = "";

              //Display user email on page
              userEmailTextBlock.Text = Globals.email;
          }
      }

      
      private void loginMouseDown(object sender, MouseButtonEventArgs e)
      {
         Login loginPage = new Login();
         App.Current.MainWindow = loginPage;
         this.Close();
         loginPage.Show();
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
         SignUp signUpPage = new SignUp();
         App.Current.MainWindow = signUpPage;
         this.Close();
         signUpPage.Show();
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
          UserWindow user = new UserWindow(Globals.email);
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

      #endregion


      #region Logic
      private void loadProperties()
      {
         //go through filters and see if any need to be added
         StringBuilder whereClause = new StringBuilder();
         bool andNeeded = false;
         bool isRentFiltered = checkFilter(filterRentTextbox.Text);
         bool isRoomFiltered = checkFilter(filterRoomTextbox.Text);

         //iterate through all of the different filter controls
         if (filterRentTextbox.Text != "" && isRentFiltered)
         {
            whereClause.Append("`MonthlyRent` < " + filterRentTextbox.Text);
            andNeeded = true;
         }

         if (filterRoomTextbox.Text!= "" && isRoomFiltered)
         {
             String whereString = "`NumberOfRooms` > " + filterRoomTextbox.Text;
             if(andNeeded)
             {
                 whereClause.Append(" and " + whereString);
             }
             else
             {
                 whereClause.Append(whereString);
                 andNeeded = true;
             }
         }

         if(isRentFiltered == false || isRoomFiltered == false)
         {
            filterLabel.Visibility = System.Windows.Visibility.Visible;
         }
         else
         {
            filterLabel.Visibility = System.Windows.Visibility.Hidden;
         }
        
         propertyListView.Items.Clear();

         ArrayList rows = dbHelper.databaseSelect("Property", whereClause.ToString());

         foreach (String[] row in rows)
         {
            propertyListView.Items.Add(new PropertyItem { PropID = Convert.ToInt32(row[0]), Addr = row[2], Rent = Convert.ToInt32(row[5]), NumberOfRooms = Convert.ToInt32(row[4]) });
         }

      }

      private void loadAllProperties()
      {
         StringBuilder whereClause = new StringBuilder();
         bool andNeeded = false;

         propertyListView.Items.Clear();

         ArrayList rows = dbHelper.databaseSelect("Property", whereClause.ToString());

         foreach (String[] row in rows)
         {
            propertyListView.Items.Add(new PropertyItem { PropID = Convert.ToInt32(row[0]), Addr = row[2], Rent = Convert.ToInt32(row[5]), NumberOfRooms = Convert.ToInt32(row[4]) });
         }
      }

      private bool checkFilter(String filter)
      {
         if (!filter.Any(Char.IsLetter) || filter.Equals(""))
         { 
            return true;
         }
         else
         {
            return false;
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
