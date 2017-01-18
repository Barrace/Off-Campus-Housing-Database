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
using System.Globalization;

namespace OffCampusHousingDatabase
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      #region Variables

      //bool loggedOn;
      //String email;
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
              loginTextblock.Text = "Log Out";
              OrTextblock.Text = "";
              signupTextblock.Text = "";

              //Display user email on page
              userEmailTextBlock.Text = Globals.email;
          }
      }

      
      private void loginMouse_Down(object sender, MouseButtonEventArgs e)
      {
         if (Globals.loggedOn == false)
         {
            Login loginPage = new Login();
            App.Current.MainWindow = loginPage;
            this.Close();
            loginPage.Show();
         }
         else
         {
            Globals.email = "";
            Globals.loggedOn = false;
            Globals.isManager = false;

            loginTextblock.Text = "Login";
            OrTextblock.Text = "or";
            signupTextblock.Text = "Sign Up";

            MainWindow main = new MainWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
         }
      }

      private void loginMouse_Enter(object sender, MouseEventArgs e)
      {
         loginTextblock.TextDecorations = TextDecorations.Underline;
      }

      private void loginMouse_Leave(object sender, MouseEventArgs e)
      {
         loginTextblock.TextDecorations = null;
      }

      private void signupMouse_Down(object sender, MouseButtonEventArgs e)
      {
         SignUp signUpPage = new SignUp();
         App.Current.MainWindow = signUpPage;
         this.Close();
         signUpPage.Show();
      }

      private void signupMouse_Enter(object sender, MouseEventArgs e)
      {
         signupTextblock.TextDecorations = TextDecorations.Underline;
      }

      private void signupMouse_Leave(object sender, MouseEventArgs e)
      {
         signupTextblock.TextDecorations = null;
      }

      private void emailMouse_Enter(object sender, MouseEventArgs e)
      {
         userEmailTextBlock.TextDecorations = TextDecorations.Underline;
      }

      private void emailMouse_Leave(object sender, MouseEventArgs e)
      {
         userEmailTextBlock.TextDecorations = null;
      }

      private void emailMouse_Down(object sender, MouseEventArgs e)
      {
         //Add code that will transition to the user's profile page
          UserWindow user = new UserWindow(Globals.email);
          App.Current.MainWindow = user;
          this.Close();
          user.Show();
      }

      private void property_Click(object sender, MouseButtonEventArgs e)
      {
         if (propertyListView.SelectedIndex < 0)
            return;

         PropertyItem n = (PropertyItem)propertyListView.SelectedItem;

         PropertyDescription pd = new PropertyDescription(n.PropID);
         App.Current.MainWindow = pd;
         pd.Show();
         this.Close();
      }

      private void filterDateAvailable_GotFocus(object sender, RoutedEventArgs e)
      {
          filterDateAvailableTextbox.Text = "";
      }

      private void filterButton_Click(object sender, RoutedEventArgs e)
      {
         loadProperties();
      }

      private void resetButton_Click(object sender, RoutedEventArgs e)
      {
         filterRentTextbox.Text = "";
         filterRoomTextbox.Text = "";
         filterDateAvailableTextbox.Text = "mm/dd/yyyy";
         filterLabel.Visibility = System.Windows.Visibility.Hidden;
         sortByNoneRadioButton.IsChecked = true;
         loadAllProperties();
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
         bool isDateFiltered = checkFilter(filterDateAvailableTextbox.Text);
         string pattern = "MM/dd/yyyy";
         DateTime parsedDate;

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

         if (filterDateAvailableTextbox.Text != "" && isDateFiltered && DateTime.TryParseExact(filterDateAvailableTextbox.Text, pattern, null, DateTimeStyles.None, out parsedDate))
         {
            int userYear, userMonth, userDay;

            userDay = parsedDate.Day;
            userMonth = parsedDate.Month;
            userYear = parsedDate.Year;

            string userDateAvailable = userYear + "-" + userMonth + "-" + userDay;

            String whereString = "`dateAvailable` <= '" + userDateAvailable + "'";

            if (andNeeded)
            {
               whereClause.Append(" and " + whereString);
            }
            else
            {
               whereClause.Append(whereString);
               andNeeded = true;
            }
         }

         if(!isRentFiltered || !isRoomFiltered)
         {
            filterLabel.Visibility = System.Windows.Visibility.Visible;
         }
         else
         {
            filterLabel.Visibility = System.Windows.Visibility.Hidden;
         }

          //All filters applied

         String orderByCol = "";

          //If there is some sort of sorting to be done
         if (!(bool)sortByNoneRadioButton.IsChecked)
         {
             if ((bool)sortByDateRadioButton.IsChecked)
                 orderByCol = "DateAvailable";
             else if ((bool)sortByRentRadioButton.IsChecked)
                 orderByCol = "MonthlyRent";
             else if ((bool)sortByRoomRadioButton.IsChecked)
                 orderByCol = "NumberOfRooms";
         }

        
         propertyListView.Items.Clear();

         ArrayList rows = dbHelper.databaseSelect("Property", whereClause.ToString(), orderByCol);

         foreach (String[] row in rows)
         {
            //pulls just the date out of the date time
            String dateStr = (row[6].Split(' '))[0];

            propertyListView.Items.Add(new PropertyItem { PropID = Convert.ToInt32(row[0]), Addr = row[2], Rent = Convert.ToInt32(row[5]), NumberOfRooms = Convert.ToInt32(row[4]), DateAvailable = dateStr });
         }

      }

      private void loadAllProperties()
      {
         StringBuilder whereClause = new StringBuilder();

         propertyListView.Items.Clear();

         ArrayList rows = dbHelper.databaseSelect("Property", whereClause.ToString());

         foreach (String[] row in rows)
         {
            //pulls just the date out of the date time
            String dateStr = (row[6].Split(' '))[0];

            propertyListView.Items.Add(new PropertyItem { PropID = Convert.ToInt32(row[0]), Addr = row[2], Rent = Convert.ToInt32(row[5]), NumberOfRooms = Convert.ToInt32(row[4]), DateAvailable = dateStr });
         }
      }


      private bool checkFilter(String filter)
      {
          return (!filter.Any(Char.IsLetter) || filter.Equals(""));
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
