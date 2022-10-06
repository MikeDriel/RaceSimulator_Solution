using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Color = System.Drawing.Color;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace WPFApp
{
	public static class PictureController
	{
		public static Dictionary<string, Bitmap> ImageCache = new Dictionary<string, Bitmap>();
		

		public static Bitmap GetBitmapUrl(string url)
		{
			if (ImageCache.ContainsKey(url))
			{
				return ImageCache[url];
			}
			else
			{
				Bitmap bitmap = new Bitmap(url);
				ImageCache.Add(url, bitmap);
				return bitmap;
			}
		}

		public static void EmptyCache()
		{
			ImageCache.Clear();
		}

		public static Bitmap CloneImageFromCache(string image)
		{
			Bitmap newBitmap = GetBitmapUrl(image);
			Bitmap clone = newBitmap.Clone(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height), PixelFormat.Format32bppArgb);
			return (clone);
		}

		public static Bitmap CreateEmptyBitmap(int x, int y)
		{
			if (ImageCache.ContainsKey("empty"))
			{

				Bitmap newBitmap = new Bitmap(x, y);
				Graphics g = Graphics.FromImage(newBitmap);

				{
					g.FillRectangle(new SolidBrush(Color.White), 0, 0, x, y);

					ImageCache.Add("empty", newBitmap);
					return newBitmap.Clone() as Bitmap;
				}
			}
			return null;
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
	}
}