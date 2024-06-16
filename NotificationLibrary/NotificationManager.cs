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

        private static NotificationManager _instance = null;

        public static NotificationManager GetInstance()
        {
            _instance ??= new NotificationManager();
            return _instance;
        }

        private NotificationWindow? _nWindow = null;
        public ObservableCollection<NotificationObject> NotificationObjects = new ObservableCollection<NotificationObject>();
        public Action<string> ClickCallBack;

        public void AddNotificationObject(NotificationObject nObject)
        {
            try
            {
                _nWindow ??= new NotificationWindow();


                switch (NotificationObjects.Count)
                {
                    case 0:
                        _nWindow.Show();
                        break;
                    case > 4:
                        NotificationObjects.Remove(NotificationObjects.First());
                        break;
                }

                NotificationObjects.Add(nObject);
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        public void CloseIfEmpty()
        {
            if (NotificationObjects.Count == 0 && _nWindow != null)
            {
                _nWindow.Close();
            }
        }

        public void ClearWindow()
        {
            _nWindow = null;
        }

        public void EventClick(string dbid)
        {
            ClickCallBack?.Invoke(dbid);
        }
    }
}