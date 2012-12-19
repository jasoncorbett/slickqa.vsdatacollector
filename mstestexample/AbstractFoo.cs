using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SlickQA.SlickTL;

namespace mstestexample
{
    [TestClass]
    public class AbstractFoo : SlickScreenshotEnabledBaseTest
    {
        [Import]
        public DirectoryManager Directories { get; set; }

        [TestInitialize]
        public void Setup()
        {
            TestLog.Debug("Directories is null: {0}", Directories == null);
            Directories.Should().NotBeNull();
        }
    }
}
