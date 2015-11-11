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

        int ID;
        String email = "";

        #endregion

        #region Listeners

        public PropertyDescription(int propertyID)
        {
            InitializeComponent();

            ID = propertyID;
            dbHelper = new DatabaseHelper(ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString);

            loadProperty();
            loadComments();
            loadImages();
            loadRatings();
            leaserEmail.Text = "database hit here";
        }

        private void backButtonClick(object sender, RoutedEventArgs e)
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

        private void leaveCommentButton_Click(object sender, RoutedEventArgs e)
        {
            newComment(TypeCommentBox.Text);
            TypeCommentBox.Text = "";
        }

        public void leaveCommentGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = "";
            tb.GotFocus -= leaveCommentGotFocus;
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
            //Add code that will transition to the user's profile page
            UserWindow user = new UserWindow(email, false);
            App.Current.MainWindow = user;
            this.Close();
            user.Show();
        }

        #endregion

        #region Logic

        public void setEmail(String email)
        {
            this.email = email;
        }

        public void loadProperty()
        {
            String[] row = dbHelper.databaseSelectFirst("Property", "`PropertyID` = '" + ID + "'");
            AddrTextBlock.Text = row[2];
            DesTextBlock.Text = row[3];
            RentTextBlock.Text = row[5];

        }

        public void loadComments()
        {
            commentListView.Items.Clear();

            ArrayList rows = dbHelper.databaseSelect("Comment", "`PropID` = '" + ID + "'");

            foreach (String[] row in rows)
            {
                commentListView.Items.Add(new Comment { CommentID = Convert.ToInt32(row[0]), UserEmail = row[2], Text = row[3] });
            }
        }

        public void newComment(String comment)
        {
            dbHelper.databaseInsert("Comment", "`PropID`, `UserEmail`, `Text`", "'" + ID + "','" + email + "','" + comment + "'");
            loadComments();
        }

        public void loadImages()
        {

        }

        public void loadRatings()
        {
            int ratingCount = 0;
            int totalRating = 0;
            double averageRating = 0.0;

            ArrayList rows = dbHelper.databaseSelect("Rating");

            foreach (String[] row in rows)
            {
                totalRating += Int32.Parse(row[3]);
                ratingCount++;
            }

            //averageRating = (totalRating / ratingCount);

            AverageRatingBlock.Text = averageRating.ToString() ;
        }

        private class Comment
        {
            public int CommentID { get; set; }
            public string UserEmail { get; set; }
            public int PropID { get; set; }
            public string Text { get; set; }
        }

        private class Image
        {
            public int ImageID { get; set; }
            public int PropID { get; set; }
            public string image { get; set; }
            public Boolean isThumnail { get; set; }
        }

        private class Rating
        {
            public int RatingID { get; set; }
            public string UserEmail { get; set; }
            public int PropID { get; set; }
            public int rating { get; set; }
        }

        #endregion


        
    }
}
