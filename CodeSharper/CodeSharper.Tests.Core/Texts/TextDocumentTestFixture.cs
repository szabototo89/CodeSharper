using CodeSharper.Core.Texts;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Texts
{
    [TestFixture]
    internal class TextDocumentTestFixture
    {
        [Test]
        public void TextDocumentShouldBeRepresentedRawText()
        {
            // Given
            var underTest = new TextDocument("Hello World!");

            // When
            var result = underTest.Text;

            // Then
            Assert.That(result, Is.EqualTo("Hello World!"));
        }

    }
}