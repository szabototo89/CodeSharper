using System;
using System.Linq;
using CodeSharper.Core.Texts;
using CodeSharper.Tests.Core.TestHelpers;
using Moq;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Texts
{
    [TestFixture]
    internal class TextRangeTestFixture : TestFixtureBase
    {
        private TextDocument TextDocument;

        [SetUp]
        public override void Setup()
        {
            TextDocument = new TextDocument("Hello World!");
        }

        [Test]
        public void TextRangeShouldBeInitializedTest()
        {
            // Given
            var text = "Hello World!";
            var underTest = new TextDocument(text).TextRange;

            // When
            var result = new {
                Start = underTest.Start,
                Stop = underTest.Stop,
                Length = underTest.Length
            };

            // Then
            Assert.That(result, Is.EqualTo(new {
                Start = 0,
                Stop = text.Length,
                Length = text.Length
            }));
        }

        [Test]
        public void TextRangeHasStartTest()
        {
            // Given
            var underTest = new TextDocument(String.Empty).TextRange;

            // When
            var result = underTest.Start;

            // Then
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void TextRangeHasStopTest()
        {
            // Given
            var underTest = new TextDocument(String.Empty).TextRange;

            // When
            var result = underTest.Stop;

            // Then
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void TextRangeHasLengthTest()
        {
            // Given
            var underTest = new TextDocument(String.Empty).TextRange;

            // When
            var result = underTest.Length;

            // Then
            Assert.That(result, Is.EqualTo(0));
        }

        [Test(Description = "TextRange should throw exception when Offset too much into negative direction")]
        public void TextRangeShouldThrowExceptionWhenOffsetTooMuchIntoNegativeDirectionTest()
        {
            // Given
            const string text = "Hello World!";
            var textDocument = new TextDocument(text);
            var underTest = textDocument.TextRange;

            // When
            TestDelegate result = () => underTest.OffsetBy(-10);

            // Then
            Assert.That(result, Throws.InstanceOf<ArgumentException>());
        }

        [Test]
        public void TextRangeShouldHaveChildren()
        {
            // Given
            var underTest = TextDocument.TextRange;

            // When
            var result = underTest.Children;

            // Then
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void TextRangeShouldBeAbleToDefineSubTextRange()
        {
            // Given
            var textDocument = new TextDocument("Hello");
            var underTest = textDocument.TextRange;

            // When
            var result = underTest.SubStringOfText(1, underTest.Length - 2);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Length, Is.EqualTo(3));
            Assert.That(result.Start, Is.EqualTo(1));
            Assert.That(result.Stop, Is.EqualTo(4));
            Assert.That(result.Parent, Is.EqualTo(underTest));
            Assert.That(underTest.Children, Has.Member(result));
        }

    }
}
