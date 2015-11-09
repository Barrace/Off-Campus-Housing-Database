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
using System.Configuration;

namespace OffCampusHousingDatabase
{
    public partial class UpdateProperty : Window
    {
        #region Variables

        DatabaseHelper dbHelper;
        bool isNewProperty;
        int ID;

        #endregion

        #region Listeners
        
        //Allows user to create a new property
        public UpdateProperty()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);
            isNewProperty = true;
            addressTextbox.Focus();
            submit.Content = "Create";
        }

        //Allows user to update an existing property
        public UpdateProperty(int propertyID)
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);
            isNewProperty = false;
            ID = propertyID;
            fillInTextboxes();
            addressTextbox.Focus();
        }

        private void addPhoto_Click(object sender, RoutedEventArgs e)
        {
            //Will allow user to add a photo
        }

        private void removePhoto_Click(object sender, RoutedEventArgs e)
        {
            //Will allow user to remove a photo
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            if (isNewProperty)
            {
                createProperty(addressTextbox.Text, descriptionTextbox.Text, monthlyRentTextbox.Text, roomsAvailableTextbox.Text);
            }
            else
            {
                updateProperty(addressTextbox.Text, descriptionTextbox.Text, monthlyRentTextbox.Text, roomsAvailableTextbox.Text);
            }
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            if (isNewProperty)
            {
                backToMain();
            }
            else
            {
                backToPropertyDescription();
            }
        }

        #endregion

        #region Logic

        private void fillInTextboxes()
        {
            //Will fill in textboxes with information from existing property
        }

        private void createProperty(string address, string description, string monthlyRent, string numberOfRooms)
        {
            if (isValidInformation(address, description, monthlyRent, numberOfRooms))
            {
                //Creates new property in database
                string columns = "`Address`, `Description`, `MonthlyRent`, `NumberOfRooms`";
                string values = "'" + address + "','" + description + "','" + monthlyRent + "','" + numberOfRooms + "'";
                dbHelper.databaseInsert("Property", columns, values);
                statusLabel.Text = "Property successfully created.";

                //Will update window to recognize this property as an existing one
                //Update ID from database
                //submit.Content = "Update";
                //isNewProperty = false;
            }
        }

        private void updateProperty(string address, string description, string monthlyRent, string numberOfRooms)
        {
            if (isValidInformation(address, description, monthlyRent, numberOfRooms))
            {
                string update = "Address='" + address + "', Description='" + description + "', MonthlyRent='" + monthlyRent
                                + "', NumberOfRooms='" + numberOfRooms + "'";
                string where = "PropertyID=" + ID;
                dbHelper.databaseUpdate("Property", update, where);
                statusLabel.Text = "Property successfully updated.";
            }
        }

        private bool isValidInformation(string address, string description, string monthlyRent, string numberOfRooms)
        {
            //Checks whether all fields are filled in
            string errorMessage = "";
            if (address == "")
            {
                errorMessage += "Please enter an address.\n";
            }
            if (description == "")
            {
                errorMessage += "Please enter a description.\n";
            }
            if (monthlyRent == "")
            {
                errorMessage += "Please enter the monthly rent.\n";
            }
            if (numberOfRooms == "")
            {
                errorMessage += "Please enter the number of rooms available.\n";
            }

            //Checks whether value entered for monthlyRent and numberOfRooms are integers
            int rent, rooms;
            if (monthlyRent != "" && !int.TryParse(monthlyRent, out rent))
            {
                errorMessage += "Monthly rent must be an integer.\n";
            }
            if (numberOfRooms != "" && !int.TryParse(numberOfRooms, out rooms))
            {
                errorMessage += "Rooms available must be an integer.\n";
            }

            //Prints error message if any and returns whether the information was valid
            if (errorMessage == "")
            {
                return true;
            }
            else
            {
                statusLabel.Text = errorMessage;
                return false;
            }
        }

        private void backToPropertyDescription()
        {
            PropertyDescription p = new PropertyDescription(ID);
            App.Current.MainWindow = p;
            this.Close();
            p.Show();
        }

        private void backToMain()
        {
            MainWindow m = new MainWindow();
            App.Current.MainWindow = m;
            this.Close();
            m.Show();
        }

        #endregion


    }
}
