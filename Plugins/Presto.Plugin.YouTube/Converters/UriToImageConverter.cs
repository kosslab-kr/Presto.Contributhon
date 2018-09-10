using Presto.Component.Converters;
using System;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace Presto.Plugin.YouTube.Converters
{
    class UriToImageConverter : ValueConverterBase<string, BitmapImage>
    {
        public BitmapCacheOption CacheOption { get; set; } = BitmapCacheOption.Default;

        public override BitmapImage Convert(string value, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(value.ToString());
                bitmap.CacheOption = CacheOption;
                bitmap.EndInit();

                return bitmap;
            }

            return null;
        }

        public override string ConvertBack(BitmapImage value, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
