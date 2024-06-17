using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
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
        readonly NotificationManager _notificationManager = NotificationManager.Instance;

        private readonly int _margin;
        private readonly int _duration;
        private readonly bool _pauseOnHover;
        private CancellationTokenSource _delayCancellationTokenSource = new CancellationTokenSource();
        private bool _removingOfNotificationPaused = false;
        readonly double _screenWidth;
        readonly double _screenHeight;

        private DateTime _lastClickTime;

        public NotificationWindow()
        {
            ShowInTaskbar = false;
            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;

            var darkThemeUri = new Uri("pack://application:,,,/NotificationLibrary;component/themes/dark.xaml", UriKind.Absolute);
            var darkThemeDict = new ResourceDictionary { Source = darkThemeUri };

            var lightThemeUri = new Uri("pack://application:,,,/NotificationLibrary;component/themes/light.xaml", UriKind.Absolute);
            var lightThemeDict = new ResourceDictionary { Source = lightThemeUri };
            if (Notification.Settings.LightTheme)
            {
                Resources.MergedDictionaries.Add(lightThemeDict);
            }
            else
            {
                Resources.MergedDictionaries.Add(darkThemeDict);
            }

            InitializeComponent();

            Closed += NotificationWindow_Closed;

            // Properties
            _duration = Notification.Settings.Duration;
            _margin = Notification.Settings.Margin;
            _pauseOnHover = Notification.Settings.PauseOnHover;
            this.DataContext = _notificationManager;

            // Get the working area of the primary screen
            Rect workArea = SystemParameters.WorkArea;
            _screenWidth = workArea.Width;
            _screenHeight = workArea.Height;
        }

        private void NotificationWindow_Closed(object? sender, EventArgs e)
        {
            Closed -= NotificationWindow_Closed;
            _notificationManager.ClearWindow();
        }

        private void border_Loaded(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"border_Loaded");

            var width = ((Border)sender).ActualWidth;
            var height = ((Border)sender).ActualHeight;

            var index = 0;
            foreach (var nObject in _notificationManager.NotificationObjects)
            {
                if (nObject.Tag.Equals(((Border)sender).Tag))
                {
                    _notificationManager.NotificationObjects[index].Height = height;
                }

                index++;
            }

            // Position the window in the bottom right corner
            Left = _screenWidth - width - _margin;
            ResetTopHeight();

            StartAnimation((Border)sender);
            RemoveNotification((Border)sender);
        }

        private void StartAnimation(Border border)
        {
            System.Diagnostics.Debug.WriteLine($"startAnimation");

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

        private void CloseAnimation(Border border)
        {
            System.Diagnostics.Debug.WriteLine($"closeAnimation");

            // Opacity Animation
            DoubleAnimation opacityAnimation = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.FromMilliseconds(240),
            };

            // Apply the animations to your elements
            border.BeginAnimation(Border.OpacityProperty, opacityAnimation);
        }


        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!_pauseOnHover) return;
            if (sender is ListViewItem item && listView.ItemContainerGenerator.ItemFromContainer(item) is NotificationObject nObject)
            {
                System.Diagnostics.Debug.WriteLine($"Mouse hovering over: {nObject.Title}");
                _removingOfNotificationPaused = true;
            }
        }

        private void ListViewItem_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!_pauseOnHover) return;

            if (sender is ListViewItem item && listView.ItemContainerGenerator.ItemFromContainer(item) is NotificationObject nObject)
            {
                System.Diagnostics.Debug.WriteLine($"Mouse left: {nObject.Title}");
                _removingOfNotificationPaused = false;
                _delayCancellationTokenSource.Cancel();
                _delayCancellationTokenSource = new CancellationTokenSource();
            }
        }

        private async void RemoveNotification(Border border)
        {
            await Task.Delay(_duration);

            try
            {
                while (_removingOfNotificationPaused)
                {
                    System.Diagnostics.Debug.WriteLine($"Delay started.");
                    await Task.Delay(100, _delayCancellationTokenSource.Token); // Use the token here
                }
            }
            catch (TaskCanceledException)
            {
                System.Diagnostics.Debug.WriteLine("Delay was canceled.");
                await Task.Delay(1000); // Give extra 1s so it doesn't close immediately
            }

            await this.Dispatcher.InvokeAsync(async () =>
            {
                System.Diagnostics.Debug.WriteLine($"Removing item");

                CloseAnimation(border);
                await Task.Delay(250);
                // remove item from the listview
                foreach (var nObject in _notificationManager.NotificationObjects)
                {
                    if (nObject.Tag.Equals(border.Tag))
                    {
                        _notificationManager.NotificationObjects.Remove(nObject);
                        Top += nObject.Height + 4;
                        break;
                    }
                }

                _notificationManager.CloseIfEmpty();
            });
        }


        public void ResetTopHeight()
        {
            double height = 0;
            foreach (var item in _notificationManager.NotificationObjects)
                height += item.Height + 4;
            Top = _screenHeight - (height) - _margin;
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (var nObject in _notificationManager.NotificationObjects)
            {
                if (nObject.Tag.Equals(((Image)sender).Tag))
                {
                    _notificationManager.NotificationObjects.Remove(nObject);
                    Top += nObject.Height + 4;
                    break;
                }
            }

            _notificationManager.CloseIfEmpty();
        }

        private void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void ListViewItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            if ((currentTime - _lastClickTime).TotalMilliseconds < 350) return;
            _lastClickTime = currentTime;

            // Manage the click over the notification
            var item = listView.SelectedItem;
            if (item == null) return;
            _notificationManager.EventClick((item as NotificationObject).UniqueIdentifier);
            _notificationManager.NotificationObjects.Remove((item as NotificationObject));

            _notificationManager.CloseIfEmpty();

            Top += (item as NotificationObject).Height + 4;
            e.Handled = true;
        }
    }
}