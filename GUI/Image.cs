using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RaceSimulator;
using Color = System.Drawing.Color;

namespace GUI
{
    public class Image
    {
        private static Dictionary<string, Bitmap> _cache = new Dictionary<string, Bitmap>();

        // krijg image
        public static Bitmap GetImage(string url)
        {
            if (! _cache.ContainsKey(url))
            {
                _cache.Add(url, new Bitmap(System.Drawing.Image.FromFile(url)));
            }

            return _cache[url];
        }

        public static Bitmap CreateEmptyBitmap(int width, int height)
        {
            string key = "empty";
            if (!_cache.ContainsKey(key))
            {
                _cache.Add(key, new Bitmap(width, height));
                // Teken achtergrond
                Graphics graphics = Graphics.FromImage(_cache[key]);
                graphics.Clear(Color.Aqua);
            }

            return (Bitmap) _cache[key].Clone();
        }

        public static Bitmap RotateImage(Bitmap bitmap, Direction direction)
        {
            int maxside = (int)(Math.Sqrt(bitmap.Width * bitmap.Width + bitmap.Height * bitmap.Height));

            //create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(maxside, maxside);
            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(returnBitmap);

            //move rotation point to center of image

            g.TranslateTransform((float)bitmap.Width / 2, (float)bitmap.Height / 2);

            switch (direction)
            {
                case Direction.South:
                    g.RotateTransform(90);
                    break;
                case Direction.West:
                    g.RotateTransform(180);
                    break;
                case Direction.North:
                    g.RotateTransform(270);
                    break;
            }

            //move image back
            g.TranslateTransform(-(float)bitmap.Width / 2, -(float)bitmap.Height / 2);

            g.DrawImage(bitmap, 0, 0, 256, 256);
            return returnBitmap;
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

        // Verwijder de cache
        public static void ClearCache()
        {
            _cache.Clear();
            BuildTrack.ClearMap();
        }
    }
}
