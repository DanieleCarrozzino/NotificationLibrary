using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NotificationLibrary
{
    internal class NotificationObject
    {
        public string title { get; set; }
        public string message { get; set; }
        public string initials { get; set; }
        public string dbid { get; set; }

        public Color color { get; set; }

        public string tag { get; set; }

        public double Height { get; set; }

        public NotificationObject(string title, string message, string initials, string dbid, Color color)
        {
            this.title      = title;
            this.message    = message;
            this.initials   = initials;
            this.color      = color;
            this.dbid       = dbid;
            this.tag        = this.GetHashCode().ToString();
        }
    }
}
