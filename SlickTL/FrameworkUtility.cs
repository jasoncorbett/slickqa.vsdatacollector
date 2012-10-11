using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;

namespace SlickQA.SlickTL
{
    public class FrameworkUtility
    {
        private static readonly Logger Log = LogManager.GetLogger("framework");
        public static CompositionContainer Container { get; set; }
        public static DirectoryManager Directories { get; set; }
        public static LoggingManager LoggingManager { get; set; }
        public static IEnumerable<IFrameworkInitializePart> FrameworkInitializerParts { get; set; }
        public static IEnumerable<IFrameworkCleanupPart> CleanupParts { get; set; }

        public static void Initialize(object testInstance, TestContext context)
        {
            if (Container == null)
            {
                Container = new CompositionContainer(new ApplicationCatalog());
                Directories = Container.GetExportedValue<DirectoryManager>();
                LoggingManager = Container.GetExportedValue<LoggingManager>();
                FrameworkInitializerParts = Container.GetExportedValues<IFrameworkInitializePart>();
                CleanupParts = Container.GetExportedValues<IFrameworkCleanupPart>();
            }
            // these 2 have to be handled individually first so that everything else can log
            Directories.initialize(testInstance, context);
            LoggingManager.initialize(testInstance, context);

            foreach (var initializer in FrameworkInitializerParts)
            {
                Log.Debug("Initializing Framework Part '{0}'.", initializer.Name);
                try
                {
                    initializer.initialize(testInstance, context);
                }
                catch (Exception e)
                {
                    Log.Warn("Error occurred while trying to initialize framework part '{0}': {1}", initializer.Name, e.Message);
                    Log.WarnException("Error from initialize.", e);
                }
            }

            Log.Debug("Performing composition on test instance.");
            Container.ComposeParts(testInstance);
            Log.Debug("Framework Initialization Complete.");
        }

        public static void Cleanup(object testInstance, TestContext context)
        {
            Log.Debug("Performing Framework Cleanup");
            foreach (var cleanupPart in CleanupParts)
            {
                Log.Debug("Calling cleanup on framework part '{0}'.", cleanupPart.Name);
                try
                {
                    cleanupPart.cleanup(testInstance, context);
                }
                catch (Exception e)
                {
                    Log.Warn("Error occurred while trying to cleanup framework part '{0}': {1}", cleanupPart.Name, e.Message);
                    Log.WarnException("Error from cleanup.", e);
                }
            }
            Log.Debug("Framework Cleanup Complete.");
            LoggingManager.cleanup();
        }


    }
}
