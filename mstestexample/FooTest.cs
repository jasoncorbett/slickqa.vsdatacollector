using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mstestexample
{
    [TestClass]
    public class FooTest : AbstractFoo
    {
        [TestMethod]
        public void Footest()
        {
            true.Should().BeTrue();
        }
    }
}
