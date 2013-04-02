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
        public static ITestingContext TestingContext { get; set; }
        public static TestFinishedSignaler TestFinishedSignaler { get; set; }
        public static IEnumerable<IFrameworkInitializePart> FrameworkInitializerParts { get; set; }
        public static IEnumerable<IFrameworkCleanupPart> CleanupParts { get; set; }
        public static bool InitializeHasBeenRun { get; set; }

        public static void Initialize(object testInstance, TestContext context)
        {
            if (Container == null)
            {
                // first time only
                InitializeHasBeenRun = false;
                Container = new CompositionContainer(new ApplicationCatalog());
                TestingContext = Container.GetExportedValue<ITestingContext>();
                TestFinishedSignaler = Container.GetExportedValue<TestFinishedSignaler>();
                Directories = Container.GetExportedValue<DirectoryManager>();
                LoggingManager = Container.GetExportedValue<LoggingManager>();
                FrameworkInitializerParts = Container.GetExportedValues<IFrameworkInitializePart>();
                CleanupParts = Container.GetExportedValues<IFrameworkCleanupPart>();
            }

            if(!InitializeHasBeenRun)
            {
                Log.Debug("Running initialize for test '{0}'.", context.TestName);
                // these 3 have to be handled individually first so that everything else can log
                TestingContext.Initialize(testInstance, context);
                Directories.initialize(testInstance, context);
                LoggingManager.initialize(testInstance);
                Log.Debug("Initializing '{0}' framework parts.", FrameworkInitializerParts.Count());

                foreach (var initializer in FrameworkInitializerParts)
                {
                    Log.Debug("Initializing Framework Part '{0}'.", initializer.Name);
                    try
                    {
                        initializer.initialize(testInstance);
                    }
                    catch (Exception e)
                    {
                        Log.Warn("Error occurred while trying to initialize framework part '{0}': {1}", initializer.Name,
                                 e.Message);
                        Log.WarnException("Error from initialize.", e);
                        throw;
                    }
                }

                Log.Debug("Performing composition on test instance.");
                Container.ComposeParts(testInstance);
                Log.Debug("Framework Initialization Complete.");
                InitializeHasBeenRun = true;
            }
            else
            {
                Log.Debug("Initialize has already been run for test '{0}'.", context.TestName);
            }
            
        }

        public static void Cleanup(object testInstance, TestContext context)
        {
            Log.Debug("Performing Framework Cleanup");
            InitializeHasBeenRun = false;
            foreach (var cleanupPart in CleanupParts)
            {
                Log.Debug("Calling cleanup on framework part '{0}'.", cleanupPart.Name);
                try
                {
                    cleanupPart.cleanup(testInstance);
                }
                catch (Exception e)
                {
                    Log.Warn("Error occurred while trying to cleanup framework part '{0}': {1}", cleanupPart.Name, e.Message);
                    Log.WarnException("Error from cleanup.", e);
                }
            }
            Log.Debug("Framework Cleanup Complete.");
            LoggingManager.cleanup();
            TestFinishedSignaler.FrameworkCleanupFinished(testInstance);
        }


    }

}
