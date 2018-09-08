using Presto.Plugin.YouTube.Supports;
using System;
using System.Windows.Media.Imaging;

namespace Presto.Plugin.YouTube.Converters
{
    class UriToImageConverter : BaseValueConverter<string, BitmapImage>
    {
        public BitmapCacheOption CacheOption { get; set; } = BitmapCacheOption.Default;

        public override BitmapImage Convert(string value, object parameter)
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

        public override string ConvertBack(BitmapImage value, object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
