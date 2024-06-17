using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NotificationLibrary
{
    internal class NotificationObject
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Initials { get; set; }
        public string UniqueIdentifier { get; set; }
        public string ApplicationName { get; set; }
        public Color Color { get; set; }
        public string Tag { get; set; }
        public double Height { get; set; }
        public BitmapImage Image { get; set; }
        public BitmapImage ApplicationIcon { get; set; }
        public BitmapImage Avatar { get; set; }
        public bool HasAvatar { get; set; }
        public bool IsLightTheme { get; set; }

        public LinearGradientBrush SolidColor
        {
            get
            {
                LinearGradientBrush gradientBrush = new LinearGradientBrush();
                gradientBrush.StartPoint = new Point(0, 0);
                gradientBrush.EndPoint = new Point(1, 0);
                gradientBrush.GradientStops.Add(new GradientStop(Color, 0));
                gradientBrush.GradientStops.Add(new GradientStop(GetDarkerColor(Color, 0.3), 1));
                gradientBrush.Freeze();
                return gradientBrush;
            }
        }

        private Color GetDarkerColor(Color color, double factor)
        {
            factor = Math.Clamp(factor, 0.0, 1.0);

            byte r = (byte)(color.R * (1.0 - factor));
            byte g = (byte)(color.G * (1.0 - factor));
            byte b = (byte)(color.B * (1.0 - factor));

            return Color.FromRgb(r, g, b);
        }

        public Visibility InitialsVisibility
        {
            get
            {
                if (HasAvatar) return Visibility.Collapsed;
                else return Visibility.Visible;
            }
        }

        public Visibility AvatarVisibility
        {
            get
            {
                if (!HasAvatar) return Visibility.Collapsed;
                else return Visibility.Visible;
            }
        }

        public Visibility ImageVisibility
        {
            get
            {
                if (Image == null) return Visibility.Collapsed;
                else return Visibility.Visible;
            }
        }

        public NotificationObject(BitmapImage applicationIcon, string applicationName, string title, string message, string initials, string uniqueIdentifier, Color color, BitmapImage image = null)
        {
            this.HasAvatar = false;
            this.ApplicationIcon = applicationIcon;
            this.ApplicationName = applicationName;
            this.Avatar = null;
            this.Title = title;
            this.Message = message;
            this.Initials = initials;
            this.Color = color;
            this.UniqueIdentifier = uniqueIdentifier;
            this.Image = image;
            this.Tag = this.GetHashCode().ToString();
        }

        public NotificationObject(BitmapImage applicationIcon, string applicationName, BitmapImage avatar, string title, string message, string uniqueIdentifier, BitmapImage image = null)
        {
            this.HasAvatar = true;
            this.ApplicationIcon = applicationIcon;
            this.ApplicationName = applicationName;
            this.Avatar = avatar;
            this.Title = title;
            this.Message = message;
            this.Initials = "";
            this.Color = Colors.Red;
            this.UniqueIdentifier = uniqueIdentifier;
            this.Image = image;
            this.Tag = this.GetHashCode().ToString();
        }
    }
}