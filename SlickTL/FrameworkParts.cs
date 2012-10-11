using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SlickQA.SlickTL
{
    public interface INamablePart
    {
        string Name { get; }
    }

    public interface IFrameworkInitializePart : INamablePart
    {
        void initialize(object instance, TestContext context);
    }

    public interface IFrameworkCleanupPart : INamablePart
    {
        void cleanup(object instance, TestContext context);
    }
}
