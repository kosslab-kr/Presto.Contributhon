using System.Drawing;

namespace Presto.Plugin.YouTube.Utilities
{
    static class BitmapUtility
    {
        public static Bitmap CenterCrop(string path)
        {
            using (var source = Image.FromFile(path) as Bitmap)
            {
                var horizontalMargin = (source.Width - source.Height) / 2;
                var cropRect = new Rectangle(horizontalMargin, 0, source.Height, source.Height);

                var target = new Bitmap(cropRect.Width, cropRect.Height);
                using (var g = Graphics.FromImage(target))
                {
                    g.DrawImage(source, new Rectangle(0, 0, target.Width, target.Height), cropRect, GraphicsUnit.Pixel);
                }

                return target;
            }
        }

        public static Bitmap DeleteLetterBox(string path)
        {
            using (var source = Image.FromFile(path) as Bitmap)
            {
                var originalHeight = (source.Width / 16) * 9;
                var letterBoxHeight = (source.Height - originalHeight) / 2;
                var cropRect = new Rectangle(0, letterBoxHeight, source.Width, originalHeight);

                var target = new Bitmap(cropRect.Width, cropRect.Height);
                using (var g = Graphics.FromImage(target))
                {
                    g.DrawImage(source, new Rectangle(0, 0, target.Width, target.Height), cropRect, GraphicsUnit.Pixel);
                }

                return target;
            }
        }
    }
}
