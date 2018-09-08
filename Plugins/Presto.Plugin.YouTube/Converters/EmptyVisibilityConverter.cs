using Presto.Plugin.YouTube.Supports;
using System;
using System.Windows;

namespace Presto.Plugin.YouTube.Converters
{
    class EmptyVisibilityConverter : BaseValueConverter<string, Visibility>
    {
        public override Visibility Convert(string value, object parameter)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public override string ConvertBack(Visibility value, object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
