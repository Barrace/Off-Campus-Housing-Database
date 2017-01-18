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
using System.IO;
using System.Text.RegularExpressions;

namespace OffCampusHousingDatabase
{
    public partial class UpdateProperty : Window
    {
        #region Variables

        DatabaseHelper dbHelper;
        bool isNewProperty;
        int propertyID;

        Object[] imageArr;
        int selectedImage;

        #endregion

        #region Listeners

        //Allows user to create a new property
        public UpdateProperty()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);
            userEmailTextblock.Text = Globals.email;
            isNewProperty = true;
            submit.Content = "Create";
            addPhotoButton.IsEnabled = false;
            removePhotoButton.IsEnabled = false;
            addressTextbox.Focus();
        }

        //Allows user to update an existing property
        public UpdateProperty(int propertyID)
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);
            userEmailTextblock.Text = Globals.email;
            isNewProperty = false;
            this.propertyID = propertyID;
            loadProperty();
            loadImages();
            addressTextbox.Focus();
        }

        private void email_MouseEnter(object sender, MouseEventArgs e)
        {
            userEmailTextblock.TextDecorations = TextDecorations.Underline;
        }

        private void email_MouseLeave(object sender, MouseEventArgs e)
        {
            userEmailTextblock.TextDecorations = null;
        }

        private void email_MouseDown(object sender, MouseButtonEventArgs e)
        {
            backToUserWindow();
        }

        private void logout_MouseEnter(object sender, MouseEventArgs e)
        {
            logoutTextblock.TextDecorations = TextDecorations.Underline;
        }

        private void logout_MouseLeave(object sender, MouseEventArgs e)
        {
            logoutTextblock.TextDecorations = null;
        }

        private void logout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            logout();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            goBack();
        }

        private void imageBox_MouseDown(object sender, RoutedEventArgs e)
        {
            viewImageZoom();
        }

        private void prevImageButton_Click(object sender, RoutedEventArgs e)
        {
            viewPreviousImage();
        }

        private void nextImageButton_Click(object sender, RoutedEventArgs e)
        {
            viewNextImage();
        }

        private void addPhoto_Click(object sender, RoutedEventArgs e)
        {
            addPhoto();
        }

        private void removePhoto_Click(object sender, RoutedEventArgs e)
        {
            removePhoto();
        }

        private void dateAvailable_GotFocus(object sender, RoutedEventArgs e)
        {
            if (dateAvailableTextbox.Text.Equals("mm/dd/yyyy"))
            {
                dateAvailableTextbox.Text = "";
            }
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            resetAllTextblocks();

            //Converts the contents of the Pets Allowed checkbox to a bit string
            string petsAllowed = "0";
            if ((bool)petsAllowedCheckbox.IsChecked)
            {
                petsAllowed = "1";
            }

            //Creates or updates the property
            if (isNewProperty)
            {
                createProperty(addressTextbox.Text, descriptionTextbox.Text, monthlyRentTextbox.Text,
                               roomsAvailableTextbox.Text, dateAvailableTextbox.Text, petsAllowed);
            }
            else
            {
                updateProperty(addressTextbox.Text, descriptionTextbox.Text, monthlyRentTextbox.Text,
                               roomsAvailableTextbox.Text, dateAvailableTextbox.Text, petsAllowed);
            }
        }

        #endregion

        #region Logic

        private void loadProperty()
        {
            //Loads property information and inserts it into the proper fields
            String[] row = dbHelper.databaseSelectFirst("Property", "`PropertyID` = '" + propertyID + "'");
            addressTextbox.Text = row[2];
            descriptionTextbox.Text = row[3];
            monthlyRentTextbox.Text = row[5];
            roomsAvailableTextbox.Text = row[4];
            if (row[7] == "1")
            {
                petsAllowedCheckbox.IsChecked = true;
            }

            //Formats the date and inserts it into the Date Available textbox
            String date = row[6].Substring(0, 10);
            int slashIndex = date.IndexOf("/");
            if (slashIndex == 1)
            {
                date = date.Insert(0, "0");
                date = date.Remove(10);
            }
            slashIndex = date.IndexOf("/", 4);
            if (slashIndex == 4)
            {
                date = date.Insert(3, "0");
                date = date.Remove(10);
            }
            dateAvailableTextbox.Text = date;
        }

        private void loadImages()
        {
            ArrayList images = dbHelper.databaseSelectImage("Image", "`PropID` = '" + propertyID + "'");
            imageArr = images.ToArray();
            if (imageArr.Length > 1)
            {
                prevImageButton.IsEnabled = true;
                nextImageButton.IsEnabled = true;
            }
            else
            {
                prevImageButton.IsEnabled = false;
                nextImageButton.IsEnabled = false;
            }


            if (imageArr.Length > 0)
            {
                selectedImage = 0;
                removePhotoButton.IsEnabled = true;
            }
            else
            {
                selectedImage = -1;
                removePhotoButton.IsEnabled = false;
            }

            showSelectedImage();
        }

        private void showSelectedImage()
        {
            if (selectedImage >= 0)
            {
                imageBox.Source = ((DatabaseImage)imageArr[selectedImage]).image;
            }
            else
            {
                imageBox.Source = null;
            }
        }

        private void logout()
        {
            //Logs user out and resets all information
            Globals.email = "";
            Globals.loggedOn = false;
            Globals.isManager = false;

            //Returns to main window
            MainWindow mainWindow = new MainWindow();
            App.Current.MainWindow = mainWindow;
            this.Close();
            mainWindow.Show();
        }

        private void backToMain()
        {
            //Add this page onto the window stack
            Globals.windowStack.Push("UpdateProperty|" + propertyID);

            MainWindow mainWindow = new MainWindow();
            App.Current.MainWindow = mainWindow;
            this.Close();
            mainWindow.Show();
        }

        public void goBack()
        {
            Globals.goBack();
            this.Close();
        }

        private void backToUserWindow()
        {
            //Add this page onto the window stack
            Globals.windowStack.Push("PropertyDescription|" + propertyID);

            UserWindow userWindow = new UserWindow(Globals.email);
            App.Current.MainWindow = userWindow;
            this.Close();
            userWindow.Show();
        }


        private void viewImageZoom()
        {
            if (imageArr.Length > 0)
            {
                ImageZoom i = new ImageZoom(imageArr, selectedImage);
                App.Current.MainWindow = i;
                i.Show();
            }
        }

        private void viewPreviousImage()
        {
            selectedImage = selectedImage - 1;

            if (selectedImage < 0)
            {
                selectedImage = imageArr.Length - 1;
            }

            showSelectedImage();
        }

        private void viewNextImage()
        {
            selectedImage = selectedImage + 1;

            if (selectedImage == imageArr.Length)
            {
                selectedImage = 0;
            }

            showSelectedImage();
        }

        private void addPhoto()
        {
            //Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //Set filter for file extension and default file extension
            dlg.DefaultExt = ".jpeg";
            dlg.Filter = "Image files (*.png, *.jpg)|*.png;*.jpg|All files (*.*)|*.*";

            //Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            //Get the selected file name and display in a TextBox
            if (result == true)
            {
                FileInfo f = new FileInfo(dlg.FileName);
                if (f.Length > 1000000)
                {
                    MessageBox.Show("Image Size may not exceed 1MB", "Not Available");
                    return;
                }
                uploadImage(dlg.FileName);
            }

            loadImages();
        }

        private void uploadImage(String fileName)
        {
            if (dbHelper.databaseInsertImageFromFile(fileName, propertyID))
            {
                photoTextblock.Text = "Image Uploaded to Database";
            }

            loadImages();
        }

        private void removePhoto()
        {
            DatabaseImage currImage = (DatabaseImage)imageArr[selectedImage];
            dbHelper.databaseDelete("Image", "`ID` = '" + currImage.ID + "'");
            photoTextblock.Text = "Image Deleted";

            loadImages();
        }

        private void resetAllTextblocks()
        {
            addressErrorTextblock.Text = "";
            descriptionErrorTextblock.Text = "";
            monthlyRentErrorTextbox.Text = "";
            roomsAvailableErrorTextblock.Text = "";
            dateAvailableErrorTextblock.Text = "";
            successTextblock.Text = "";
            photoTextblock.Text = "";
        }

        private void createProperty(string address, string description, string monthlyRent, string numberOfRooms,
                                    string dateAvailable, string petsAllowed)
        {
            if (isValidInformation(address, description, monthlyRent, numberOfRooms, dateAvailable))
            {
                //Formats information so it can be inserted into database without errors
                address = formatApostrophes(address);
                description = formatApostrophes(description);
                dateAvailable = formatDate(dateAvailable);

                //Creates new property in database
                string columns = "`ManagerEmail`, `Address`, `Description`, `MonthlyRent`, `NumberOfRooms`, "
                                 + "`DateAvailable`, `PetsAllowed`";
                string values = "'" + Globals.email + "','" + address + "','" + description + "','" + monthlyRent
                                + "','" + numberOfRooms + "','" + dateAvailable + "',b'" + petsAllowed + "'";
                dbHelper.databaseInsert("Property", columns, values);
                successTextblock.Text = "Property successfully created.";

                //Get the Property ID of the Property that was just added
                String[] res = dbHelper.databaseSelectLast("Property");
                propertyID = Convert.ToInt32(res[0]);

                //Updates window to recognize this property as an existing one
                UpdateProperty updateProperty = new UpdateProperty(propertyID);
                App.Current.MainWindow = updateProperty;
                this.Close();
                updateProperty.Show();
            }
        }

        private void updateProperty(string address, string description, string monthlyRent, string numberOfRooms,
                                    string dateAvailable, string petsAllowed)
        {
            if (isValidInformation(address, description, monthlyRent, numberOfRooms, dateAvailable))
            {
                //Formats information so it can be inserted into database without errors
                address = formatApostrophes(address);
                description = formatApostrophes(description);
                dateAvailable = formatDate(dateAvailable);

                //Updates property in the database
                string update = "Address='" + address + "', Description='" + description + "', MonthlyRent='"
                                + monthlyRent + "', NumberOfRooms='" + numberOfRooms + "', DateAvailable='"
                                + dateAvailable + "', PetsAllowed=b'" + petsAllowed + "'";
                string where = "PropertyID=" + propertyID;
                dbHelper.databaseUpdate("Property", update, where);
                successTextblock.Text = "Property successfully updated.";
            }
        }

        private bool isValidInformation(string address, string description, string monthlyRent, string numberOfRooms,
                                        string dateAvailable)
        {
            bool isValid = true;

            //Checks whether all fields are filled in
            if (address == "")
            {
                addressErrorTextblock.Text = "Please enter an address.";
                isValid = false;
            }
            if (description == "")
            {
                descriptionErrorTextblock.Text = "Please enter a description.";
                isValid = false;
            }
            if (monthlyRent == "")
            {
                monthlyRentErrorTextbox.Text = "Please enter the monthly rent.";
                isValid = false;
            }
            if (numberOfRooms == "")
            {
                roomsAvailableErrorTextblock.Text = "Please enter the number of rooms available.";
                isValid = false;
            }
            if (dateAvailable == "" || dateAvailable == "mm/dd/yyyy")
            {
                dateAvailableErrorTextblock.Text = "Please enter the date available.";
                isValid = false;
            }

            //Checks whether values entered for monthlyRent and numberOfRooms are integers
            int rent, rooms;
            if (monthlyRent != "" && !int.TryParse(monthlyRent, out rent))
            {
                monthlyRentErrorTextbox.Text = "Monthly rent must be an integer.";
                isValid = false;
            }
            if (numberOfRooms != "" && !int.TryParse(numberOfRooms, out rooms))
            {
                roomsAvailableErrorTextblock.Text = "Rooms available must be an integer.";
                isValid = false;
            }

            //Checks whether the date is valid
            if (dateAvailable != "" && !isValidDate(dateAvailable))
            {
                isValid = false;
            }

            return isValid;
        }

        private bool isValidDate(string date)
        {
            bool isValid = true;

            //Checks whether the date has the correct format (mm/dd/yyyy)
            if (!Regex.IsMatch(date, @"\d{2}/\d{2}/\d{4}"))
            {
                dateAvailableErrorTextblock.Text = "Date must be in the format mm/dd/yyyy.";
                isValid = false;
            }
            else
            {
                isValid = dateExists(date);
            }

            return isValid;
        }

        private bool dateExists(string date)
        {
            bool exists = true;

            //Gets month, day, and year as integers and checks whether the date entered exists
            int month, day, year;
            int.TryParse(date.Substring(0, 2), out month);
            int.TryParse(date.Substring(3, 2), out day);
            int.TryParse(date.Substring(6), out year);

            //Checks whether the month and day are valid
            if (month < 1 || month > 12 || day < 1)
            {
                dateAvailableErrorTextblock.Text = date + " is not a valid date.";
                exists = false;
            }
            //Checks whether the day is too large for months with 31 days
            else if ((month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                     && day > 31)
            {
                dateAvailableErrorTextblock.Text = date + " is not a valid date.";
                exists = false;
            }
            //Checks whether the day is too large for months with 30 days
            else if ((month == 4 || month == 6 || month == 9 || month == 11) && day > 30)
            {
                dateAvailableErrorTextblock.Text = date + " is not a valid date.";
                exists = false;
            }
            //Checks whether the day is too large for February in a non-leap year
            else if (month == 2 && (year % 4 != 0 || year % 100 == 0) && day > 28)
            {
                dateAvailableErrorTextblock.Text = date + " is not a valid date.";
                exists = false;
            }
            //Checks whether the day is too large for February in a leap year
            else if (month == 2 && day > 29)
            {
                dateAvailableErrorTextblock.Text = date + " is not a valid date.";
                exists = false;
            }

            return exists;
        }

        private string formatApostrophes(string text)
        {
            int indexOfApostrophe = text.IndexOf('\'');
            while (indexOfApostrophe >= 0)
            {
                text = text.Insert(indexOfApostrophe, "\\");
                indexOfApostrophe = text.IndexOf('\'', indexOfApostrophe + 2);
            }
            return text;
        }

        private string formatDate(string date)
        {
            string month = date.Substring(0, 2);
            string day = date.Substring(3, 2);
            string year = date.Substring(6);
            return year + "-" + month + "-" + day;
        }

        #endregion

    }
}
