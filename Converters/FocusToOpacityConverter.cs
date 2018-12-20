using System;
using System.Globalization;
using System.Windows.Data;

namespace HeroLabExportToPdf.Converters
{
    public class FocusToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isFocused = value != null && (bool) value;
            var opacityArray = (object[]) parameter;

            if (opacityArray != null) return isFocused ? opacityArray[0] : opacityArray[1];
            return 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
