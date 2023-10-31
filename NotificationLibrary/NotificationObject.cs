﻿using System;
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
        public string title { get; set; }
        public string message { get; set; }
        public string initials { get; set; }
        public string dbid { get; set; }
        public string applicationName { get; set; }
        public Color color { get; set; }
        public string tag { get; set; }
        public double Height { get; set; }
        public BitmapImage applicationIcon { get; set; }
        public LinearGradientBrush solidColor {
            get
            {
                LinearGradientBrush gradientBrush = new LinearGradientBrush();
                gradientBrush.StartPoint = new Point(0, 0);
                gradientBrush.EndPoint = new Point(1, 0);
                gradientBrush.GradientStops.Add(new GradientStop(color, 0));
                gradientBrush.GradientStops.Add(new GradientStop(GetDarkerColor(color, 0.3), 1));
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

        public NotificationObject(BitmapImage applicationIcon, string applicationName, string title, string message, string initials, string dbid, Color color)
        {
            this.applicationIcon    = applicationIcon;
            this.applicationName    = applicationName;
            this.title              = title;
            this.message            = message;
            this.initials           = initials;
            this.color              = color;
            this.dbid               = dbid;
            this.tag                = this.GetHashCode().ToString();
        }
    }
}
