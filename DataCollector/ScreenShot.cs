using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using SlickQA.SlickSharp.Logging;

namespace SlickQA.DataCollector
{
	static class ScreenShot
	{	
		public static Bitmap GetScreen()
		{
			return ScreenBitmap();
		}

		//TODO: Return screenshot bytes to put in datasink
		public static void CaptureScreenShot(string fileName)
		{
			var ms = new MemoryStream();
			GetScreen().Save(ms, ImageFormat.Png);
			byte[] screenBytes = ms.GetBuffer();

			var slickScreenshot = new StoredFile
			{
				FileName = fileName,
				Mimetype = "image/png"
			};
			slickScreenshot.Post();
			slickScreenshot.PostContent(screenBytes);
		}
		private static Bitmap ScreenBitmap()
		{
			Rectangle totalSize = Rectangle.Empty;

			foreach ( Screen s in Screen.AllScreens )
				totalSize = Rectangle.Union( totalSize, s.Bounds );

			Bitmap screenShotBMP = new Bitmap( totalSize.Width, totalSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb );

			Graphics screenShotGraphics = Graphics.FromImage( screenShotBMP );

			screenShotGraphics.CopyFromScreen( totalSize.X, totalSize.Y,
				0, 0, totalSize.Size, CopyPixelOperation.SourceCopy );

			screenShotGraphics.Dispose();

			return screenShotBMP;
		}
	}
}

	}
}
