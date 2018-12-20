﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HeroLabExportToPdf.Converters
{
    public class IsLessThanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is GridLength v && parameter != null)
                return v.Value <= double.Parse((string)parameter);

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}