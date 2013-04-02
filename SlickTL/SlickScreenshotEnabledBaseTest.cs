using System;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;

namespace SlickQA.SlickTL
{
    [TestClass]
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
            AlwaysTakeScreenshot = true;
        }

        public static void DoScreenshotAndAttach(string filename, ITestingContext context, Logger testLog, DirectoryManager directories, ImageFormat format = null)
        {
            if (format == null)
                format = ImageFormat.Png;

            Bitmap bmp;
            try
            {
                Rectangle totalSize = Rectangle.Empty;

                foreach (Screen s in Screen.AllScreens)
                    totalSize = Rectangle.Union(totalSize, s.Bounds);

                bmp = new Bitmap(totalSize.Width, totalSize.Height, PixelFormat.Format32bppArgb);

                Graphics screenShotGraphics = Graphics.FromImage(bmp);

                screenShotGraphics.CopyFromScreen(totalSize.X, totalSize.Y,
                    0, 0, totalSize.Size, CopyPixelOperation.SourceCopy);

                screenShotGraphics.Dispose();

            }
            catch (Exception ex)
            {
                testLog.Error("Unable to take screenshot '{0}' with format '{1}' because of exception '{2}': {3}", filename, format, ex.GetType().FullName, ex.Message);
                testLog.ErrorException("Error", ex);
                return;
            }

            if(!filename.Contains(Path.DirectorySeparatorChar))
            {
                filename = Path.Combine(directories.CurrentTestOutputDirectory, filename);
            }
            if(string.IsNullOrWhiteSpace(Path.GetExtension(filename)))
            {
                filename = String.Format("{0}.{1}", filename, format.ToString().ToLower());
            }
            try
            {
                bmp.Save(filename, format);
                context.AddResultFile(filename);
            }
            catch (Exception e)
            {
                testLog.Error("Unable to take screenshot '{0}' with format '{1}' because of exception '{2}': {3}", filename, format, e.GetType().FullName, e.Message);
                testLog.ErrorException("Error", e);
            }
        }

        public virtual void AttachScreenshotToResult(string filename, ImageFormat format = null)
        {
            try
            {
                DoScreenshotAndAttach(filename, Context, TestLog, TestDirectories, format);
            }
            catch (Exception e)
            {
                TestLog.Warn("Taking screenshot threw exception!", e);
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
