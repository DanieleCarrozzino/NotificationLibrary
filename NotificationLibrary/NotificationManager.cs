using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationLibrary
{
    internal class NotificationManager
    {
        //*************
        //
        //  INSTANCE
        //
        //*************

        private static NotificationManager instance = null;

        public static NotificationManager getInstance()
        {
            instance ??= new NotificationManager();
            return instance;
        }

        private NotificationWindow n_window = null;
        public ObservableCollection<NotificationObject> notificationObjects = new ObservableCollection<NotificationObject>();
        public Action<string> ClickCallBack;

        public void addNotificationObject(NotificationObject n_object)
        {
            n_window ??= new NotificationWindow();
            if (notificationObjects.Count == 0) n_window.Show();

            notificationObjects.Add(n_object);
        }

        public void closeIfEmpty()
        {
            if (notificationObjects.Count == 0)
            {
                n_window.Close();
                n_window = null;
            }
        }

        public void EventClick(string dbid)
        {
            ClickCallBack?.Invoke(dbid);
        }

    }
}
