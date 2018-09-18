using Presto.Component.Converters;
using System;
using System.Globalization;
using System.Windows;

namespace Presto.Plugin.YouTube.Converters
{
    class EmptyToVisibilityConverter : ValueConverterBase<string, Visibility>
    {
        public bool Condition { get; set; }

        public override Visibility Convert(string value, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value) == Condition)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public override string ConvertBack(Visibility value, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
