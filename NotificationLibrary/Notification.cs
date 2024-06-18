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
        public static GlobalNotificationSettings Settings { get; } = new GlobalNotificationSettings();

        public static void InsertNotification(NotificationInitialsModel notification)
        {
            var manager = NotificationManager.Instance;
            var n_object = new NotificationObject(notification.ApplicationIcon,
                notification.ApplicationName,
                notification.Title,
                notification.Message,
                notification.Initials,
                notification.UniqueIdentifier,
                notification.Color,
                notification.Image);
            manager.AddNotificationObject(n_object);
        }

        /// <summary>
        /// Create and show  a notification with ana avatar
        /// </summary>
        /// <param name="notification"></param>
        public static void InsertNotification(NotificationAvatarModel notification)
        {
            var manager = NotificationManager.Instance;
            var n_object = new NotificationObject(notification.ApplicationIcon,
                notification.ApplicationName,
                notification.Avatar,
                notification.Title,
                notification.Message,
                notification.UniqueIdentifier,
                notification.Image);
            manager.AddNotificationObject(n_object);
        }

        /// <summary>
        /// Define the callback for the click event
        /// </summary>
        /// <param name="Callback"></param>
        public static void SetCallBack(Action<string> Callback)
        {
            var manager = NotificationManager.Instance;
            manager.ClickCallBack = Callback;
        }
    }
}