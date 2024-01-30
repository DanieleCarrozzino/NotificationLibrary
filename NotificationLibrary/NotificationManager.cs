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

        public List<(int, String)> addNotificationObject(NotificationObject n_object)
        {
            var list = new List<(int, String)>();
            try
            {
                if(n_window == null)
                {
                    list.Add((1, "Create window 1"));
                    n_window = new NotificationWindow();
                    list.Add((2, "Create window 2"));
                }
                //n_window ??= new NotificationWindow();


                if (notificationObjects.Count == 0) 
                {
                    list.Add((2, "Show window 1"));
                    n_window.Show();
                    list.Add((2, "Show window 2"));
                }

                if (notificationObjects.Count > 4)
                {
                    list.Add((2, "Remove not 1"));
                    notificationObjects.RemoveAt(0);
                    list.Add((2, "Remove not 2"));
                }

                // reset the top inside the loaded method
                notificationObjects.Add(n_object);
                list.Add((2, "Add not"));
            }
            catch(Exception ex)
            {
                list.Add((2, ex.Message.ToString()));
            }
            return list;
        }

        public void closeIfEmpty()
        {
            if (notificationObjects.Count == 0 && n_window != null)
            {
                n_window.Close();
            }
        }

        public void clearWindow()
        {
            n_window = null;
        }

        public void EventClick(string dbid)
        {
            ClickCallBack?.Invoke(dbid);
        }

    }
}
