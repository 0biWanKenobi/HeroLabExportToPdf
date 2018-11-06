using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SampleCode.Converters
{
    /// <inheritdoc />
    /// <summary>
    /// Converts a color value to a brush.
    /// </summary>
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color) value;
            var colorBrush = new SolidColorBrush
            {
                Color = color,
                Opacity = color.ScA
               
            };
            return colorBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
