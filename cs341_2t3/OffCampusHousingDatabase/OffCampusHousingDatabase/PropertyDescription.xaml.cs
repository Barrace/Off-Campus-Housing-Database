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
        #region Variables

        DatabaseHelper dbHelper;

        int iD;
        int selectedImage;
        Boolean ratedProperty = false;
        Object[] imageArr;

        #endregion

        #region Listeners

        public PropertyDescription(int propertyID)
        {
            InitializeComponent();

            iD = propertyID;
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);
            imageArr = new Object[4];

            if (Globals.loggedOn)
            {
                //Show on the UI that the user is logged on, and hide the Login, Or, and Signup textblocks
                loginTextBlock.Text = "Log Out";
                OrTextBlock.Text = "";
                signupTextBlock.Text = "";

                //Display user email on page
                userEmailTextBlock.Text = Globals.email;

                if (!Globals.isManager)
                {
                    watchButton.IsEnabled = true;
                }
            }

            loadProperty();
            loadComments();
            loadImages();
            loadRatings();
            loadWatched();
        }

        private void loginMouse_Down(object sender, MouseButtonEventArgs e)
        {
            if (Globals.loggedOn == false)
            {
                //Add this page onto the window stack
                Globals.windowStack.Push("PropertyDescription|" + iD);

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

                loginTextBlock.Text = "Login";
                OrTextBlock.Text = "or";
                signupTextBlock.Text = "Sign Up";

                MainWindow main = new MainWindow();
                App.Current.MainWindow = main;
                this.Close();
                main.Show();
            }
        }

        private void loginMouse_Enter(object sender, MouseEventArgs e)
        {
            loginTextBlock.TextDecorations = TextDecorations.Underline;
        }

        private void loginMouse_Leave(object sender, MouseEventArgs e)
        {
            loginTextBlock.TextDecorations = null;
        }

        private void signupMouse_Down(object sender, MouseButtonEventArgs e)
        {
            //Add this page onto the window stack
            Globals.windowStack.Push("PropertyDescription|" + iD);

            SignUp signUpPage = new SignUp();
            App.Current.MainWindow = signUpPage;
            this.Close();
            signUpPage.Show();
        }

        private void signupMouse_Enter(object sender, MouseEventArgs e)
        {
            signupTextBlock.TextDecorations = TextDecorations.Underline;
        }

        private void signupMouse_Leave(object sender, MouseEventArgs e)
        {
            signupTextBlock.TextDecorations = null;
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
            //Add this page onto the window stack
            Globals.windowStack.Push("PropertyDescription|" + iD);

            //Add code that will transition to the user's profile page
            UserWindow user = new UserWindow(Globals.email);
            App.Current.MainWindow = user;
            this.Close();
            user.Show();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.goBack();
            this.Close();
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

        private void imageBox_MouseDown(object sender, RoutedEventArgs e)
        {
            if(imageArr.Length>0)
            {
                ImageZoom i = new ImageZoom(imageArr, selectedImage);
                App.Current.MainWindow = i;
                i.Show();
            }
        }

        private void leaveCommentButton_Click(object sender, RoutedEventArgs e)
        {
            newComment(typeCommentBox.Text);
            typeCommentBox.Text = "";
        }

        private void deleteCommentButton_Click(object sender, RoutedEventArgs e)
        {
            if(commentListView.SelectedIndex < 0)
                return;

            Comment c = (Comment)commentListView.SelectedItem;
            deleteComment(c.userEmail, c.commentID);
        }

        private void commentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (commentListView.SelectedIndex < 0)
                return;

            Comment c = (Comment)commentListView.SelectedItem;
            if (Globals.loggedOn && Globals.email.Equals(c.userEmail))
                deleteCommentButton.Visibility = Visibility.Visible;
            else
                deleteCommentButton.Visibility = Visibility.Hidden;
        }

        public void leaveCommentGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = "";
            tb.GotFocus -= leaveCommentGotFocus;
        }

        private void rateProperty_Click(object sender, RoutedEventArgs e)
        {
            if (ratedProperty == false)
            {
                dbHelper.databaseInsert("Rating", "`PropID`, `UserEmail`, `Rating`", "'" + iD + "','" + Globals.email + "','" + ratingSlider.Value + "'");
                loadRatings();
            }
            else
            {
                dbHelper.databaseUpdate("Rating", "`Rating` = '" + ratingSlider.Value + "'", "`UserEmail` = '" + Globals.email + "' AND `PropID` = '" + iD + "'");
                loadRatings();
            }
        }

        private void leaserMouseEnter(object sender, MouseEventArgs e)
        {
            leaserEmail.TextDecorations = TextDecorations.Underline;
        }

        private void leaserMouseLeave(object sender, MouseEventArgs e)
        {
            leaserEmail.TextDecorations = null;
        }

        private void leaserMouseDown(object sender, MouseEventArgs e)
        {
            //Add this page onto the window stack
            Globals.windowStack.Push("PropertyDescription|" + iD);

            //Add code that will transition to the user's profile page
            UserWindow user = new UserWindow(leaserEmail.Text);
            App.Current.MainWindow = user;
            this.Close();
            user.Show();
        }

        private void watchButtonClick(object sender, RoutedEventArgs e)
        {
            if (watchButton.Content.Equals("Watch"))
            {
                addWatchedProperty();
                watchButton.Content = "Unwatch";
            }
            else
            {
                removeWatchedProperty();
                watchButton.Content = "Watch";
            }
        }

        #endregion

        #region Logic

        public void loadProperty()
        {
            String[] row = dbHelper.databaseSelectFirst("Property", "`PropertyID` = '" + iD + "'");
            addressTextBlock.Text = row[2];
            descriptionTextBlock.Text = row[3];
            rentTextBlock.Text = row[5];
            leaserEmail.Text = row[1];
            numberOfRoomsTextBlock.Text = row[4];
            dateAvailableTextBlock.Text = (row[6].Split(' '))[0];

            String petsAllowed = (row[7] == "1") ? "Allowed" : "Not Allowed";
            petsAllowedTextBlock.Text = petsAllowed;
        }

        public void loadComments()
        {
            if (Globals.loggedOn == false || Globals.isManager)
            {
                leaveCommentButton.IsEnabled = false;
                typeCommentBox.IsEnabled = false;
            }
            else
            {
                leaveCommentButton.IsEnabled = true;
            }

            //Special case where the user is a manager and this is their property, then they can comment
            if (Globals.loggedOn & Globals.isManager && Globals.email.Equals(leaserEmail.Text))
            {
                leaveCommentButton.IsEnabled = true;
                typeCommentBox.IsEnabled = true;
            }

            commentListView.Items.Clear();

            ArrayList rows = dbHelper.databaseSelect("Comment", "`PropID` = '" + iD + "'");

            foreach (String[] row in rows)
            {
                commentListView.Items.Add(new Comment { commentID = Convert.ToInt32(row[0]), propID = Convert.ToInt32(row[1]), userEmail = row[2], text = row[3] });
            }
        }

        public void newComment(String comment)
        {
            dbHelper.databaseInsert("Comment", "`PropID`, `UserEmail`, `Text`", "'" + iD + "','" + Globals.email + "','" + comment + "'");
            loadComments();
        }

        public void deleteComment(String commentEmail, int commentID)
        {
            if (Globals.loggedOn && commentEmail.Equals(Globals.email))
            {
                dbHelper.databaseDelete("Comment", "`ID` = '" + commentID + "'");
                loadComments();
                deleteCommentButton.Visibility = Visibility.Hidden;
            }
            else
            {
                //Cannot delete a comment that is not yours
            }
        }

        public void loadImages()
        {
            //Currently set to load a single image
            ArrayList images = dbHelper.databaseSelectImage("Image", "`PropID` = '" + iD + "'");
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
        }

        public void showSelectedImage()
        {
            imageBox.Source = ((DatabaseImage)imageArr[selectedImage]).image;
        }

        public void loadRatings()
        {
            String roundedAverage;
            double ratingCount = 0.0;
            int totalRating = 0;
            double averageRating = 0.0;

            ArrayList arr = dbHelper.databaseSelect("Rating", "`UserEmail` = '" + Globals.email + "' AND `PropID` = '" + iD + "'");
            if (arr.Count == 1)
            {
                ratedProperty = true;
                ratePropertyButton.Content = "Re-rate";
            }

            if (!(Globals.loggedOn) || Globals.isManager)
            {
                ratePropertyButton.IsEnabled = false;
            }
            else
            {
                ratePropertyButton.IsEnabled = true;
            }

            ArrayList rows = dbHelper.databaseSelect("Rating", "`PropID` = '" + iD + "'");

            foreach (String[] row in rows)
            {
                totalRating += Int32.Parse(row[3]);
                ratingCount++;
            }

            if (ratingCount == 0.0)
            {
                averageRating = 0.0;
            }
            else
            {
                averageRating = (totalRating / ratingCount);
            }

            roundedAverage = String.Format("{0:0.00}", averageRating);
            averageRatingBlock.Text = roundedAverage;
            ratingsTextBlock.Text = ratingCount.ToString();
        }

        public void loadWatched(){
            if(Globals.loggedOn){
                String[] arr = getUserWatchedPropertyCSV().Split(',');

                watchButton.Content = arr.Contains(iD.ToString()) ? "Unwatch" : "Watch";
            }
        }

        public String getUserWatchedPropertyCSV()
        {
            String[] user = dbHelper.databaseSelectFirst("User", "`email` = '" + Globals.email + "'");

            return (String)user[4];
        }

        public void updateWatchedProperties(String newCSV)
        {
            dbHelper.databaseUpdate("User", "`WatchedProperties` = '" + newCSV + "'", "`email` = '" + Globals.email + "'");
        }

        public void addWatchedProperty()
        {
            String csv = getUserWatchedPropertyCSV();
            csv = csv + "," + iD;

            updateWatchedProperties(sanitizeCSV(csv));
        }

        public void removeWatchedProperty()
        {
            String csv = getUserWatchedPropertyCSV();
            csv = csv.Replace(iD.ToString(), "");

            updateWatchedProperties(sanitizeCSV(csv));
        }

        public String sanitizeCSV(String csv)
        {
            //Clean CSV by removing leading and trailing commas, and removing double commas
            csv = csv.Replace(",,", ",");
            csv = csv.TrimStart(',');
            csv = csv.TrimEnd(',');

            return csv;
        }

        private class Comment
        {
            public int commentID { get; set; }
            public string userEmail { get; set; }
            public int propID { get; set; }
            public string text { get; set; }
        }

        private class Image
        {
            public int imageID { get; set; }
            public int propID { get; set; }
            public string image { get; set; }
            public Boolean isThumnail { get; set; }
        }

        private class Rating
        {
            public int ratingID { get; set; }
            public string userEmail { get; set; }
            public int propID { get; set; }
            public int rating { get; set; }
        }

        #endregion 

        

        
    }
}
