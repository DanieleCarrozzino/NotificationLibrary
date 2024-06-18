using NotificationLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<(string, string, string, string, Color)> list = new List<(string, string, string, string, Color)>()
        {
            ("Marco Lombardo", "ML", "Hi lukeòlw ijòl wjgjwò jòlkd fjòlk jwdòlkj òlwjd pòogijw òlkj gòldjf poiwj òlkj òwdfijòiw jòlj wdòlfk jòlw jfòiw jòoij òwl dfjòowi jòlkb gjdòlwj", "1", Colors.DeepPink),
            ("James Smith", "JS", "Can we schedule a meeting?", "2", Colors.BlueViolet),
            ("Fabio Lostrano", "FL", "s hdkjh asdkjh gkajshd gfkjhasg dflgqpiurghf pijuhgò 3hòr-u gh.wàe òorgàw jeròlgk jwdfklnvòhewrpoighè85è9g84èoirjgòlkejrfgòlkmdn ,mvn x,.kn òidhrèoghwoerhjg àòwkergò lkjewhrògkjhw eòrkgh poòwhergòlkewrngòliwhep heorgò lkqeròl gkhòlerhgòoiheòlk ghòlker ", "3", Colors.Orange),
            ("Livia Larina", "LL", "So do I", "4", Colors.ForestGreen),
        };

        int index = 0;

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i <= 100; i++)
            {
                createNotification();
            }

            Notification.SetCallBack(Callback);
        }

        public void Callback(string dbid)
        {
            Trace.WriteLine(dbid);
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            border_effect(true);
            createNotification();
        }

        private void createNotification()
        {
            Notification.Settings.Duration = 1000;
            Notification.Settings.PauseOnHover = true;
            if (index % 3 == 0)
            {
                index++;
                Notification.InsertNotification(new NotificationAvatarModel()
                {
                    ApplicationName = "Application",
                    Avatar = getIcon(),
                    Title = "titolo",
                    Message = "Message",
                    UniqueIdentifier = "dbid",
                });
                return;
            }

            Notification.Settings.LightTheme = true;
            var item = list[index % list.Count];
            index++;
            var notification = new NotificationInitialsModel()
            {
                ApplicationName = "Application",
                ApplicationIcon = getIcon(),
                Title = item.Item1,
                Initials = "ML",
                Message = item.Item3,
                UniqueIdentifier = item.Item4,
                Color = item.Item5
            };
            Notification.InsertNotification(notification);
        }

        private BitmapImage getIcon()
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("pack://application:,,,/NotificationLibrary;component/images/chat.png", UriKind.Absolute);
            bitmapImage.EndInit();
            bitmapImage.Freeze();

            return bitmapImage;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            border_effect(false);
        }

        private void border_effect(bool up)
        {
            DropShadowEffect dropShadowEffect;
            if (up)
            {
                dropShadowEffect = new DropShadowEffect
                {
                    Color = Colors.Gray,
                    Direction = 320,
                    ShadowDepth = 5,
                    Opacity = 0.5,
                };
            }
            else
            {
                dropShadowEffect = new DropShadowEffect
                {
                    Color = Colors.Gray,
                    Direction = 320,
                    ShadowDepth = 3,
                    Opacity = 0.2,
                };
            }

            border.Effect = dropShadowEffect;
        }
    }
}