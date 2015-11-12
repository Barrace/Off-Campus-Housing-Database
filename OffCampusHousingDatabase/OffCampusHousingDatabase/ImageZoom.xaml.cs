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

namespace OffCampusHousingDatabase
{
    /// <summary>
    /// Interaction logic for ImageZoom.xaml
    /// </summary>
    public partial class ImageZoom : Window
    {
        #region variables
        Object[] imageArr;
        int selectedImage;

        #endregion

        #region listeners

        public ImageZoom(Object[] imageArr, int selectedImage)
        {
            InitializeComponent();
            this.imageArr = imageArr;
            this.selectedImage = selectedImage;

            if (imageArr.Length > 1)
            {
                nextImageButton.IsEnabled = true;
                prevImageButton.IsEnabled = true;
            }

            showSelectedImage();
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

        #endregion


        #region logic

        public void showSelectedImage()
        {
            ImageBox.Source = (BitmapImage)imageArr[selectedImage];
        }

        #endregion
    }
}
