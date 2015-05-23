using NUnit.Framework;

namespace CodeSharper.Tests
{
    public class TestFixtureBase
    {
        [SetUp]
        public virtual void Setup() { }

        [TearDown]
        public virtual void Teardown() { }
    }
}