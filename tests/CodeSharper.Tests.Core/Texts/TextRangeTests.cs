using System;
using System.Text;
using CodeSharper.Core.Texts;
using Moq;
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

        [Test(Description = "Dispose should unregister TextRange in TextDocument text document when it is called")]
        public void Dispose_ShouldUnregisterTextRangeInTextDocument_WhenItIsCalled()
        {
            // Given
            var textDocumentMock = MakeTextDocumentMock("Hello World!");

            IDisposable underTest = new TextRange(0, 5, textDocumentMock.Object);

            // When
            underTest.Dispose();

            // Then
            textDocumentMock
                .Verify(document => document.Unregister(It.Is<TextRange>(value => Equals(value, underTest))),
                        Times.Once());
        }

        private static Mock<ITextDocument> MakeTextDocumentMock(String text)
        {
            var mock = new Mock<ITextDocument>();
            mock.SetupAllProperties();

            mock.SetupGet(document => document.Text)
                .Returns(new StringBuilder(text));

            return mock;
        }
    }
}
