using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Virtualisatie
{
    public static class CacheImages
    {
        private static Dictionary<string, System.Drawing.Bitmap> Cache = new Dictionary<string, Bitmap>();

        public static Bitmap GetUrl(string url)
        {
            if (Cache.ContainsKey(url))
                return Cache[url];

            Bitmap bmp = new Bitmap(url);
            Cache.Add(url, bmp);
            
            return bmp;
        }

        public static Bitmap CreateBitmap(int width, int height)
        {
            if (!Cache.ContainsKey("empty"))
            {
                Cache.Add("empty", new Bitmap(width, height));
                Graphics g = Graphics.FromImage(Cache["empty"]);
                g.Clear(System.Drawing.Color.FromArgb(0, 100, 0));
            }

            return (Bitmap)Cache["empty"].Clone();
        }

        public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        public static void ClearCache()
        {
            Cache.Clear();
        }
    }
}
