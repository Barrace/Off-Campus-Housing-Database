using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace OffCampusHousingDatabase
{
    class DatabaseImage
    {
        public readonly String ID;
        public readonly BitmapImage image;

        public DatabaseImage(String ID, BitmapImage image)
        {
            this.ID = ID;
            this.image = image;
        }
    }
}
