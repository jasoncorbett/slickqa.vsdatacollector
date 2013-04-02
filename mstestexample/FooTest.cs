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
