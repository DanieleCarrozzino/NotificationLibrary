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
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Callback(string dbid)
        {
            Trace.WriteLine(dbid);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Notification.SetCallBack(Callback);

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("pack://application:,,,/NotificationLibrary;component/images/chat.png", UriKind.Absolute);
            bitmapImage.Freeze();
            bitmapImage.EndInit();

            Notification.InsertNotification(bitmapImage, "Application", "Daniele Carrozzino", "Hi luke, how are you?", "DC", "1234", Colors.MediumVioletRed);
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            border_effect(true);

            Notification.SetCallBack(Callback);

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("pack://application:,,,/NotificationLibrary;component/images/chat.png", UriKind.Absolute);
            bitmapImage.EndInit();
            bitmapImage.Freeze();

            Notification.InsertNotification(bitmapImage, "Application", "Daniele Carrozzino", "Hi luke, how are you?", "DC", "1234", Colors.MediumVioletRed);
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
