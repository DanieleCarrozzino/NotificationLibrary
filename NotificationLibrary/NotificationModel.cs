using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NotificationLibrary
{
    public class NotificationModel
    {
        public BitmapImage ApplicationIcon { get; set; }
        public string ApplicationName { get; set; } = "";
        public string Title { get; set; }
        public string Message { get; set; }
        public string UniqueIdentifier { get; set; }
        public BitmapImage Image { get; set; }
    }
}