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

        [Test(Description = "TextRange should have Text and Position property")]
        public void TextNodeShouldHaveTextAndPositionProperty()
        {
            // Given
            var underTest = new TextRange("Hello World!", TextDocument);

            // When
            var result = new
            {
                underTest.Text,
                TextRange = underTest
            };

            // Then
            var expected = new
            {
                Text = "Hello World!",
                TextRange = new TextRange(0, "Hello World!")
            };

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}