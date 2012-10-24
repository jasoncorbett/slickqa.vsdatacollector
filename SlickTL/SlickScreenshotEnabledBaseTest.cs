using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SlickQA.SlickTL
{
    public abstract class SlickScreenshotEnabledBaseTest : SlickBaseTest
    {
        public bool AlwaysTakeScreenshot { get; set; }

        [Import]
        public DirectoryManager TestDirectories { get; set; }

        [Import]
        public ITestingContext Context { get; set; }

        [TestInitialize]
        public void InitializeTakeScreenshot()
        {
            AlwaysTakeScreenshot = false;
        }

        public virtual void AttachScreenshotToResult(string filename, ImageFormat format = null)
        {
            if (format == null)
                format = ImageFormat.Png;

            Bitmap bmp = null;
            try
            {
                Rectangle totalSize = Rectangle.Empty;

                foreach (Screen s in Screen.AllScreens)
                    totalSize = Rectangle.Union(totalSize, s.Bounds);

                bmp = new Bitmap(totalSize.Width, totalSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                Graphics screenShotGraphics = Graphics.FromImage(bmp);

                screenShotGraphics.CopyFromScreen(totalSize.X, totalSize.Y,
                    0, 0, totalSize.Size, CopyPixelOperation.SourceCopy);

                screenShotGraphics.Dispose();

            }
            catch (Exception ex)
            {
                TestLog.Error("Unable to take screenshot '{0}' with format '{1}' because of exception '{2}': {3}", filename, format, ex.GetType().FullName, ex.Message);
                TestLog.ErrorException("Error", ex);
                return;
            }

            if (bmp != null)
            {
                if(!filename.Contains(Path.DirectorySeparatorChar))
                {
                    filename = Path.Combine(TestDirectories.CurrentTestOutputDirectory, filename);
                }
                if(string.IsNullOrWhiteSpace(Path.GetExtension(filename)))
                {
                    filename = String.Format("{0}.{1}", filename, format.ToString().ToLower());
                }
                try
                {
                    bmp.Save(filename, format);
                    Context.AddResultFile(filename);
                }
                catch (Exception e)
                {
                    TestLog.Error("Unable to take screenshot '{0}' with format '{1}' because of exception '{2}': {3}", filename, format, e.GetType().FullName, e.Message);
                    TestLog.ErrorException("Error", e);
                }
            }
        }
        
        [TestCleanup]
        public void TakeResultScreenshot()
        {
            if(Context.CurrentTestOutcome != UnitTestOutcome.Passed || AlwaysTakeScreenshot)
            {
                AttachScreenshotToResult("EndResult");
            }
        }
    }
}
