using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NotificationLibrary
{
    internal class NotificationObject
    {
        public string title { get; set; }
        public string message { get; set; }
        public string initials { get; set; }
        public string dbid { get; set; }

        public string applicationName { get; set; }

        public Color color { get; set; }

        public string tag { get; set; }

        public double Height { get; set; }

        public BitmapImage applicationIcon { get; set; }

        public SolidColorBrush solidColor { 
            get { 
                var solid = new SolidColorBrush(color);
                solid.Freeze();
                return solid;
            }
        }

        public NotificationObject(BitmapImage applicationIcon, string applicationName, string title, string message, string initials, string dbid, Color color)
        {
            this.applicationIcon    = applicationIcon;
            this.applicationName    = applicationName;
            this.title              = title;
            this.message            = message;
            this.initials           = initials;
            this.color              = color;
            this.dbid               = dbid;
            this.tag                = this.GetHashCode().ToString();
        }
    }
}
