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

namespace OffCampusHousingDatabase
{
    public partial class UpdateProperty : Window
    {
        #region Variables

        DatabaseHelper dbHelper;
        bool isNewProperty;
        int ID;
        String managerEmail;

        Object[] imageArr;
        int selectedImage;

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

            managerEmail = "maxbeauchemin@gmail.com";

            addPhoto.IsEnabled = false;
            removePhoto.IsEnabled = false;
        }

        //Allows user to update an existing property
        public UpdateProperty(int propertyID)
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);
            isNewProperty = false;
            ID = propertyID;
            loadProperty();
            loadImages();
            addressTextbox.Focus();
        }

        private void addPhoto_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".jpeg";
            dlg.Filter = "Image files (*.png, *.jpg)|*.png;*.jpg|All files (*.*)|*.*";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
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
        }

        private void removePhoto_Click(object sender, RoutedEventArgs e)
        {
            DatabaseImage currImage = (DatabaseImage)imageArr[selectedImage];
            dbHelper.databaseDelete("Image", "`ID` = '" + currImage.ID + "'");
            statusLabel.Text = "Image Deleted";

            loadImages();
        }

        private void nextImageButton_Click(object sender, RoutedEventArgs e)
        {
            selectedImage = selectedImage + 1;

            if (selectedImage == imageArr.Length)
                selectedImage = 0;

            showSelectedImage();
        }

        private void prevImageButton_Click(object sender, RoutedEventArgs e)
        {
            selectedImage = selectedImage - 1;

            if (selectedImage < 0)
                selectedImage = imageArr.Length - 1;

            showSelectedImage();
        }

        private void ImageBox_MouseDown(object sender, RoutedEventArgs e)
        {
            if (imageArr.Length > 0)
            {
                ImageZoom i = new ImageZoom(imageArr, selectedImage);
                App.Current.MainWindow = i;
                i.Show();
            }
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

        public void loadProperty()
        {
            String[] row = dbHelper.databaseSelectFirst("Property", "`PropertyID` = '" + ID + "'");
            addressTextbox.Text = row[2];
            descriptionTextbox.Text = row[3];
            monthlyRentTextbox.Text = row[5];
            roomsAvailableTextbox.Text = row[4];
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

                //Get the Property ID of the Property that was just added
                String[] res  = dbHelper.databaseSelectLast("Property");
                ID = Convert.ToInt32(res[0]);

                //Will update window to recognize this property as an existing one
                UpdateProperty p = new UpdateProperty(ID);
                App.Current.MainWindow = p;
                this.Close();
                p.Show();
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

        private void uploadImage(String fileName)
        {
            if(dbHelper.databaseInsertImageFromFile(fileName, ID))
            { 
                statusLabel.Text = "Image Uploaded to Database";
            }
            loadImages();
        }

        public void loadImages()
        {
            //Currently set to load a single image
            ArrayList images = dbHelper.databaseSelectImage("Image", "`PropID` = '" + ID + "'");
            imageArr = images.ToArray();
            if (imageArr.Length > 1)
            {
                nextImageButton.IsEnabled = true;
                prevImageButton.IsEnabled = true;
            }


            if (imageArr.Length > 0)
            {
                selectedImage = 0;
                showSelectedImage();
            }
            else
            {
                nextImageButton.IsEnabled = false;
                prevImageButton.IsEnabled = false;
                removePhoto.IsEnabled = false;
            }
        }

        public void showSelectedImage()
        {
            ImageBox.Source = ((DatabaseImage)imageArr[selectedImage]).image;
        }

        #endregion

    }
}
