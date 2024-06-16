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
        public static GlobalNotificationSettings Settings { get; set; } = new GlobalNotificationSettings();

        /// <summary>
        /// Create and show the notification inside the vertical
        /// list of the NotificationWindow
        /// </summary>
        /// <param name="notification"></param>
        public static void InsertNotification(NotificationModel notification)
        {
            var manager = NotificationManager.GetInstance();
            var n_object = new NotificationObject(notification.ApplicationIcon,
                notification.ApplicationName,
                notification.Title,
                notification.Message,
                notification.Initials,
                notification.Dbid,
                notification.Color,
                notification.Image);
            manager.AddNotificationObject(n_object);
        }

        /// <summary>
        /// Create and show  a notification with ana avatar
        /// </summary>
        /// <param name="notification"></param>
        public static void InsertNotificationWithAvatar(NotificationModel notification)
        {
            var manager = NotificationManager.GetInstance();
            var n_object = new NotificationObject(notification.ApplicationIcon,
                notification.ApplicationName,
                notification.Avatar,
                notification.Title,
                notification.Message,
                notification.Dbid,
                notification.Image);
            manager.AddNotificationObject(n_object);
        }

        /// <summary>
        /// Define the callback for the click event
        /// </summary>
        /// <param name="Callback"></param>
        public static void SetCallBack(Action<string> Callback)
        {
            var manager = NotificationManager.GetInstance();
            manager.ClickCallBack = Callback;
        }
    }
}