using Presto.Plugin.YouTube.Supports;
using System;
using System.Windows;

namespace Presto.Plugin.YouTube.Converters
{
    class BoolToVisibilityConverter : BaseValueConverter<bool, Visibility>
    {
        public bool Condition { get; set; }

        public override Visibility Convert(bool value, object parameter)
        {
            if (value == Condition)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public override bool ConvertBack(Visibility value, object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
