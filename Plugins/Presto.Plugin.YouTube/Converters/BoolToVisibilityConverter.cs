using Presto.Component.Converters;
using System;
using System.Globalization;
using System.Windows;

namespace Presto.Plugin.YouTube.Converters
{
    class BoolToVisibilityConverter : ValueConverterBase<bool, Visibility>
    {
        public bool Condition { get; set; }

        public override Visibility Convert(bool value, object parameter, CultureInfo culture)
        {
            if (value == Condition)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public override bool ConvertBack(Visibility value, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
