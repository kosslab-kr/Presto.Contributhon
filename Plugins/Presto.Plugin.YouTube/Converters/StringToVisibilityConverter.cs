using Presto.Component.Converters;
using System;
using System.Globalization;
using System.Windows;

namespace Presto.Plugin.YouTube.Converters
{
    class StringToVisibilityConverter : ValueConverterBase<string, Visibility>
    {
        public override Visibility Convert(string value, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public override string ConvertBack(Visibility value, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
