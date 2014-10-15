using CodeSharper.Core.Texts;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CodeSharper.Tests.Core.Texts
{
    [TestFixture]
    internal class TextNodeTestFixture
    {
        private TextDocument TextDocument { get; set; }

        [SetUp]
        public void Setup()
        {
            TextDocument = new TextDocument("Hello World!");
        }
    }
}