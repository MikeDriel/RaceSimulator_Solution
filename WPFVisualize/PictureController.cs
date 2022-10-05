using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WPFVisualize
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

		public static Bitmap DrawOnBitmap(int x, int y)
		{
			if (ImageCache.ContainsKey("empty"))
			{

				Bitmap newBitmap = new Bitmap(x, y);
				Graphics g = Graphics.FromImage(newBitmap);
				
				{
					g.FillRegion(new SolidBrush(Color.White), new System.Drawing.Region(new Rectangle(0, 0, x, y)));

					ImageCache.Add("empty", newBitmap);
				return newBitmap;
			}
			return null;
		}
	}
}