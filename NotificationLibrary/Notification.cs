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
        public static List<(int, String)> InsertNotification(
            BitmapImage applicationIcon, string applicationName, string title, string message, 
            string initials, string dbid, Color color, BitmapImage image = null)
        {
            var manager     = NotificationManager.getInstance();
            var n_object    = new NotificationObject(applicationIcon, applicationName, title, message, initials, dbid, color, image);

            return manager.addNotificationObject(n_object);
        }

        /// <summary>
        /// Create and show  a notification with ana avatar
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="initials"></param>
        /// <param name="color"></param>
        public static List<(int, String)> InsertNotificationWithAvatar(
            BitmapImage applicationIcon, string applicationName, 
            BitmapImage avatar,
            string title, string message, string dbid, BitmapImage image = null)
        {
            var manager = NotificationManager.getInstance();
            var n_object = new NotificationObject(applicationIcon, applicationName, avatar, title, message, dbid, image);

            return manager.addNotificationObject(n_object);
        }

        /// <summary>
        /// Define the callback for the click event
        /// </summary>
        /// <param name="Callback"></param>
        public static void SetCallBack(Action<string> Callback)
        {
            var manager = NotificationManager.getInstance();
            manager.ClickCallBack = Callback;
        }

    }
}
