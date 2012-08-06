// Copyright 2012 AccessData Group, LLC.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//  http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SlickQA.SlickSharp.Logging;

namespace SlickQA.DataCollector
{
	//TODO: Need Unit Test Coverage Here
	internal static class ScreenShot
	{
		//TODO: Return screenshot bytes to put in datasink?
		public static StoredFile CaptureScreenShot(string fileName)
		{
			var ms = new MemoryStream();
			ScreenBitmap().Save(ms, ImageFormat.Png);
			byte[] screenBytes = ms.GetBuffer();

			var slickScreenshot = new StoredFile {FileName = fileName, Mimetype = "image/png"};
			slickScreenshot.Post();
			slickScreenshot.PostContent(screenBytes);
			return slickScreenshot;
		}

		private static Bitmap ScreenBitmap()
		{
			Rectangle totalSize = Screen.AllScreens.Aggregate(Rectangle.Empty, (current, s) => Rectangle.Union(current, s.Bounds));

			var screenShotBmp = new Bitmap(totalSize.Width, totalSize.Height, PixelFormat.Format32bppArgb);

			Graphics screenShotGraphics = Graphics.FromImage(screenShotBmp);

			screenShotGraphics.CopyFromScreen(totalSize.X, totalSize.Y, 0, 0, totalSize.Size, CopyPixelOperation.SourceCopy);

			screenShotGraphics.Dispose();

			return screenShotBmp;
		}
	}
}
