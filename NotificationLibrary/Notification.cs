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
    public static class Notification
    {

        /// <summary>
        /// Create and show the notification inside the vertical
        /// list of the NotificationWindow
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="initials"></param>
        /// <param name="color"></param>
        public static void InsertNotification(BitmapImage applicationIcon, string applicationName, string title, string message, string initials, string dbid, Color color)
        {
            var manager     = NotificationManager.getInstance();
            var n_object    = new NotificationObject(applicationIcon, applicationName, title, message, initials, dbid, color);

            manager.addNotificationObject(n_object);
        }

        public static void SetCallBack(Action<string> Callback)
        {
            var manager = NotificationManager.getInstance();
            manager.ClickCallBack = Callback;
        }

    }
}
