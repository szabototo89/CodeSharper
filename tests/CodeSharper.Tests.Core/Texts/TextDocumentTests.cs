using System;
using CodeSharper.Core.Texts;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Texts
{
    [TestFixture]
    internal class TextDocumentTests : TestFixtureBase
    {
        [Test(Description = "Constructor should create TextRange from passed text after initializing itself")]
        public void Constructor_ShouldCreateTextRangeFromPassedText_AfterInitializingItself()
        {
            // Given
            var text = "Hello World!";
            var underTest = new TextDocument(text);

            // When
            var result = underTest.TextRange;

            // Then
            Assert.That(result, Is.EqualTo(new TextRange(0, 12, underTest)));
        }

        [Test(Description = "TextRanges should always contain the main TextRange when it is called")]
        public void TextRanges_ShouldAlwaysContainMainTextRange_WhenItIsCalled()
        {
            // Given
            var text = "Hello World!";
            var underTest = new TextDocument(text);

            // When
            var result = underTest.TextRanges;

            // Then
            Assert.That(result, Has.Member(underTest.TextRange));
        }

        [Test(Description = "CreateTextRange should instantiate new TextRange when start and stop values are passed")]
        public void CreateTextRange_ShouldInstantiateNewTextRange_WhenStartAndStopValuesArePassed()
        {
            // Given
            String text = "Hello World!";
            var underTest = new TextDocument(text);

            // When
            var result = underTest.CreateTextRange(1, 5);

            // Then
            Assert.That(result, Is.EqualTo(new TextRange(1, 5, underTest)));
        }
    }
}
