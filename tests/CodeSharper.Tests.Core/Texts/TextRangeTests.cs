using CodeSharper.Core.Texts;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Texts
{
    [TestFixture]
    internal class TextRangeTestFixture : TestFixtureBase
    {
        protected TextDocument TextDocument { get; set; }

        public override void Setup()
        {
            base.Setup();
            TextDocument = new TextDocument("Hello World!");
        }

        [Test(Description = "Constructor should take start position and text when it is called")]
        public void Constructor_ShouldTakeStartPositionAndText_WhenItIsCalled()
        {
            // Given
            var start = 0;
            var stop = 5;
            var underTest = new TextRange(start, stop, TextDocument);

            // When
            var result = underTest.Text;

            // Then
            Assert.That(result, Is.EqualTo("Hello"));
        }

        [Test(Description = "Length should return length of text range when it is called")]
        public void Length_ShouldReturnLengthOfTextRange_WhenItIsCalled()
        {
            // Given
            var start = 1;
            var stop = 6;
            var underTest = new TextRange(start, stop, TextDocument);

            // When
            var result = underTest.Length;

            // Then
            Assert.That(result, Is.EqualTo(5));
        }
    }
}
