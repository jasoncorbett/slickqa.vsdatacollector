using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using SlickQA.SlickTL.Config;

namespace SlickQA.SlickTL
{
    public interface ITestConfiguration : IFrameworkInitializePart
    {
        string ConfigurationValue(string key, string defaultValue = null);
    }

    public class TestConfigurationAttribute : Attribute
    {
        public string ConfigurationName { get; set; }

        public string DefaultValue { get; set; }

        public TestConfigurationAttribute(string name)
        {
            ConfigurationName = name;
            DefaultValue = null;
        }
    }

    public static class TestConfigurationAttributeExtension
    {
        public static TestConfigurationAttribute GetTestConfigurationAttributeFromProperty(this PropertyInfo property)
        {
            var attributes = property.GetCustomAttributes(typeof(TestConfigurationAttribute), true).ToList();
            if (attributes.Count > 0)
            {
                return (TestConfigurationAttribute) attributes[0];
            }
            return null;
        }
    }

    [Export(typeof(ITestConfiguration))]
    [Export(typeof(IFrameworkInitializePart))]
    class TestConfiguration : ITestConfiguration
    {
        public static Logger Log = LogManager.GetLogger("framework.TestConfiguration");

        [Import]
        public DirectoryManager Directories { get; set; }

        public string Name { get { return "TestConfiguration"; } }

        public IDictionary<Type, IConfigSource> TestConfigurations { get; set; }

        public IDictionary<string, IConfigSource> DllConfigurations { get; set; }

        public IConfigSource DefaultConfiguration { get; set; }

        public IConfigSource RuntimeConfiguration { get; set; }

        public IDictionary<string, string> CurrentConfiguration { get; set; }

        public Type LastTestType { get; set; }

        public TestConfiguration()
        {
            TestConfigurations = new Dictionary<Type, IConfigSource>();
            DllConfigurations = new Dictionary<string, IConfigSource>();
            CurrentConfiguration = null;
            DefaultConfiguration = null;
            RuntimeConfiguration = null;
            LastTestType = null;
        }

        public void initialize(object instance, TestContext context)
        {
            // Only load all the configuration if we have a different test type from the last
            // test run.  Otherwise we know they will be the same.
            if (LastTestType == null || LastTestType != instance.GetType())
            {

                // first load the default configuration (if necessary)
                if (DefaultConfiguration == null)
                    DefaultConfiguration = LoadDefaultConfiguration();

                // next load the dll configuration (if necessary)
                var dllName = DllNameFromObjectInstance(instance);
                IConfigSource dllConfig = null;

                if (DllConfigurations.ContainsKey(dllName))
                {
                    dllConfig = DllConfigurations[dllName];
                }
                else
                {
                    // only put it in the dictionary if it's not null (meaning it exists)
                    dllConfig = LoadDllConfiguration(instance);
                    if (dllConfig != null)
                        DllConfigurations.Add(dllName, dllConfig);
                }

                // next load the test configuration source (if necessary)
                var testType = instance.GetType();
                IConfigSource testConfig = null;

                if (TestConfigurations.ContainsKey(testType))
                {
                    testConfig = TestConfigurations[testType];
                }
                else
                {
                    testConfig = LoadTestConfiguration(instance);
                    if (testConfig != null)
                        TestConfigurations.Add(testType, testConfig);
                }

                // finally load any runtime source (if necessary and it exists)
                if (RuntimeConfiguration == null)
                    RuntimeConfiguration = LoadRuntimeConfiguration();

                // merge all the sources
                var currentSource = new IniConfigSource();
                foreach (var source in
                         new IConfigSource[] {DefaultConfiguration, dllConfig, testConfig, RuntimeConfiguration})
                {
                    if (source != null)
                        currentSource.Merge(source);
                }

                // replace the variables in the values
                currentSource.ExpandKeyValues();

                // flatten the configuration into a dictionary
                CurrentConfiguration = new Dictionary<string, string>();
                foreach (IConfig section in currentSource.Configs)
                {
                    var sectionName = section.Name;
                    foreach (var key in section.GetKeys())
                    {
                        var configName = String.Join(".", sectionName, key);
                        CurrentConfiguration.Add(configName, section.GetString(key));
                    }
                }

                LastTestType = testType;
            }
            InjectConfiguration(instance);
        }

        public string ConfigurationValue(string key, string defaultValue = null)
        {
            // in this method it's vital to log a message and throw an exception for an error.  See
            // the InjectConfiguration method for a reason why.
            
            if (key == null)
            {
                var message = "Invalid configuration key: null";
                Log.Error(message);
                throw new KeyNotFoundException(message);
            }

            if (CurrentConfiguration != null)
            {
                if (CurrentConfiguration.ContainsKey(key))
                {
                    var value = CurrentConfiguration[key];
                    Log.Info("Using Test Configuration '{0}'=>'{1}'", key, value);
                    return value;
                }
                else if(defaultValue != null)
                {
                    Log.Info("Test Configuration not found, using default value '{0}'=>'{1}'.", key, defaultValue);
                    return defaultValue;
                }
                else
                {
                    var message = String.Format("No Test Configuration found for '{0}'!", key);
                    Log.Error(message);
                    throw new KeyNotFoundException(message);
                }
            }
            else
            {
                // This shouldn't happen, but just in case we don't want to blow up without debugging information.
                Log.Error("Unable to check for configuration '{0}', CurrentConfiguration is null!", key);

                // I don't want to check for empty / or whitespace.  I specifically want to just check for null.
                if (defaultValue != null)
                {
                    Log.Info("Even though current configuration is null, using default value '{1}' for key '{0}'.", key, defaultValue);
                    return defaultValue;
                }
                else
                {
                    throw new KeyNotFoundException(String.Format("Unable to find Test Configuration key '{0}', as current configuration is null!", key));
                }

            }
        }

        public static string DllNameFromObjectInstance(object instance)
        {
            return Path.GetFileNameWithoutExtension(instance.GetType().Assembly.Location);
        }

        public IConfigSource LoadRuntimeConfiguration()
        {
            IConfigSource retval = null;
            var envfilename = Environment.GetEnvironmentVariable("TEST_RUNTIME_CONFIGURATION");
            if (String.IsNullOrWhiteSpace(envfilename))
                envfilename = Path.Combine(Directories.ConfigurationDirectory, "env.ini");
            if (File.Exists(envfilename))
                retval = new IniConfigSource(envfilename);
            return retval;
        }

        public IConfigSource LoadDefaultConfiguration()
        {
            IConfigSource retval = null;
            var filename = Path.Combine(Directories.ConfigurationDirectory, "default.ini");
            Log.Debug("Looking for default configuration in {0}", filename);
            if (File.Exists(filename))
                retval = new IniConfigSource(filename);
            return retval;
        }

        public IConfigSource LoadDllConfiguration(object instance)
        {
            IConfigSource retval = null;
            var filename = Path.Combine(Directories.ConfigurationDirectory,
                                        TestConfiguration.DllNameFromObjectInstance(instance) + ".ini");
            if(File.Exists(filename))
                retval = new IniConfigSource(filename);
            return retval;
        }

        public IConfigSource LoadTestConfiguration(object instance)
        {
            IConfigSource retval = null;
            var filename = Path.Combine(Directories.ConfigurationDirectory, instance.GetType().FullName + ".ini");
            if(File.Exists(filename))
                retval = new IniConfigSource(filename);
            return retval;
        }

        public void InjectConfiguration(object instance)
        {
            Exception error = null;
            foreach (var property in instance.GetType().GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance))
            {
                var configAttr = property.GetTestConfigurationAttributeFromProperty();
                if (configAttr != null)
                {
                    try
                    {
                        // by putting this in a try, and storing the first exception, we'll log 
                        // every missing configuration for a test, not just the first one
                        property.SetValue(instance, ConfigurationValue(configAttr.ConfigurationName, configAttr.DefaultValue), null);
                    }
                    catch (Exception e)
                    {
                        if (error == null)
                            error = e;
                    }
                }
            }
            // if we encountered an error with any of the configurations, throw the error
            if (error != null)
                throw error;
        }
    }
}
