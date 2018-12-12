using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace HeroLabExportToPdf.Converters
{
    public class PageToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var pages = values.Select(v => (int) v).ToArray();
            return pages[0] == pages[1] ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
