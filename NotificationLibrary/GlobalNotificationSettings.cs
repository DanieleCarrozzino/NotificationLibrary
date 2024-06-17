using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NotificationLibrary
{
    public class GlobalNotificationSettings
    {
        public int Margin { get; set; } = 20;
        public int Duration { get; set; } = 2000;
        public bool PauseOnHover { get; set; } = false;
        public bool LightTheme { get; set; } = false;
    }
}