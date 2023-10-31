using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace NotificationLibrary
{
    /// <summary>
    /// Logica di interazione per Notification.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        NotificationManager notificationManager = NotificationManager.getInstance();

        const int MARGIN = 20;
        const int DURATION = 6500;

        double screenWidth;
        double screenHeight;

        public NotificationWindow()
        {
            InitializeComponent();

            Closed += NotificationWindow_Closed;

            listView.ItemsSource = notificationManager.notificationObjects;

            // Get the working area of the primary screen
            Rect workArea = SystemParameters.WorkArea;
            screenWidth  = workArea.Width;
            screenHeight = workArea.Height;
        }

        private void NotificationWindow_Closed(object? sender, EventArgs e)
        {
            notificationManager.clearWindow();
        }

        private void border_Loaded(object sender, RoutedEventArgs e)
        {
            var width = ((Border)sender).ActualWidth;
            var height = ((Border)sender).ActualHeight;

            var index = 0;
            foreach (var n_object in notificationManager.notificationObjects)
            {
                if (n_object.tag.Equals(((Border)sender).Tag))
                {
                    notificationManager.notificationObjects[index].Height = height;
                }
                index++;
            }

            // Position the window at the bottom right corner
            Left = screenWidth - width - MARGIN;
            Top  = screenHeight - ((height + 4) * notificationManager.notificationObjects.Count) - MARGIN;

            startAnimation((Border)sender);
            removeNotification((Border)sender);
        }

        private void startAnimation(Border border)
        {
            // Opacity Animation
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                To = 1,
                Duration = TimeSpan.FromMilliseconds(100),
            };

            // Movement Animation (X-axis)
            DoubleAnimation translateXAnimation = new DoubleAnimation
            {
                From = 90,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(100),
            };

            // Apply the animations to your elements
            border.BeginAnimation(Border.OpacityProperty, opacityAnimation);

            TranslateTransform translateTransform = new TranslateTransform();
            border.RenderTransform = translateTransform;
            translateTransform.BeginAnimation(TranslateTransform.XProperty, translateXAnimation);
        }

        private void closeAnimation(Border border)
        {
            // Opacity Animation
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.FromMilliseconds(240),
            };

            // Apply the animations to your elements
            border.BeginAnimation(Border.OpacityProperty, opacityAnimation);
        }

        async private void removeNotification(Border border)
        {
            await Task.Delay(DURATION);
            await this.Dispatcher.InvokeAsync(async () =>
            {
                closeAnimation(border);
                await Task.Delay(250);

                // remove item from the listview
                if (notificationManager.notificationObjects.First().tag.Equals(border.Tag))
                {
                    notificationManager.notificationObjects
                        .RemoveAt(0);
                    Top += border.ActualHeight + 4;
                }
                notificationManager.closeIfEmpty();

            });
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach(var n_object in notificationManager.notificationObjects)
            {
                if (n_object.tag.Equals(((Image)sender).Tag))
                {
                    notificationManager.notificationObjects.Remove(n_object);
                    Top += n_object.Height + 4;
                    break;
                }
            }
            notificationManager.closeIfEmpty();
        }

        private void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void ListViewItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Manage the click over the notification
            var item = listView.SelectedItem;
            if (item == null) return;
            notificationManager.EventClick((item as NotificationObject).dbid);
            notificationManager.notificationObjects.Remove((item as NotificationObject));

            notificationManager.closeIfEmpty();

            Top += (item as NotificationObject).Height + 4;
            e.Handled = true;
        }
    }
}
