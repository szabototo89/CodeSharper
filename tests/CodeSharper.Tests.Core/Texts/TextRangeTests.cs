using System;
using System.Collections;
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

        #region Helper methods for testing

        private static Mock<ITextDocument> MakeTextDocumentMock(String text)
        {
            var mock = new Mock<ITextDocument>();
            mock.SetupAllProperties();

            mock.SetupGet(document => document.Text).Returns(new StringBuilder(text));

            return mock;
        }

        #endregion

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

        [Test(Description = "SubRange should instantiate sub text-range when relative positions are passed")]
        public void SubRange_ShouldInstantiateSubTextRange_WhenRelativePositionsArePassed()
        {
            // Given
            var underTest = TextDocument.GetOrCreateTextRange(1, 5);

            // When
            var result = underTest.SubRange(0, 3, TextPosition.Relative);

            // Then
            Assert.That(result.Text, Is.EqualTo("ell"));
            Assert.That(result, Is.EqualTo(new TextRange(1, 4, TextDocument)));
        }

        [Test(Description = "SubRange should instantiate sub text range when absolute positions are passed")]
        public void SubRange_ShouldInstantiateSubText_WhenAbsolutePositionsArePassed()
        {
            // Given
            var underTest = TextDocument.GetOrCreateTextRange(1, 5);

            // When
            var result = underTest.SubRange(1, 4, TextPosition.Absolute);

            // Then
            Assert.That(result.Text, Is.EqualTo("ell"));
            Assert.That(result, Is.EqualTo(new TextRange(1, 4, TextDocument)));
        }

        [Test(Description = "SubRange should throw an Exception when relative positions are not in the specified text range")]
        public void SubRange_ShouldThrowException_WhenRelativePositionsAreNotInTheSpecifiedTextRange()
        {
            // Given
            var underTest = TextDocument.GetOrCreateTextRange(1, 5);

            // When
            TestDelegate subRangeWithTooLargePositions = () => underTest.SubRange(1, 10, TextPosition.Relative);

            // Then
            Assert.That(subRangeWithTooLargePositions, Throws.Exception);
        }

        [Test(Description = "SubRange should throw an Exception when absolute positions are not in the specified text range")]
        public void SubRange_ShouldThrowAnException_WhenAbsolutePositionsAreNotInTheSpecifiedTextRange()
        {
            // Given
            var underTest = TextDocument.GetOrCreateTextRange(1, 5);

            // When
            TestDelegate subRangeWithTooLargePositions = () => underTest.SubRange(1, 6, TextPosition.Absolute);

            // Then
            Assert.That(subRangeWithTooLargePositions, Throws.Exception);
        }

        [Test(Description = "SubRange should add newly created sub text-range to its children collection when it is called properly")]
        public void SubRange_ShouldAddSubRangeToItsChildrenCollection_WhenItIsCalledProperly()
        {
            // Given
            var underTest = TextDocument.GetOrCreateTextRange(1, 5);
            var subRange = underTest.SubRange(0, 3, TextPosition.Relative);

            // When
            var result = underTest.Children;

            // Then
            Assert.That(result, Has.Member(subRange));
        }

        [Test(Description = "SubRange should add its child just once when same text ranges are initialized")]
        public void SubRange_ShouldAddItsChildJustOnce_WhenSameTextRangesAreInitialized()
        {
            // Given
            var underTest = TextDocument.GetOrCreateTextRange(1, 6);
            var subRange = underTest.SubRange(0, 4);

            // When
            var result = underTest.SubRange(0, 4);

            // Then
            Assert.That(result, Is.SameAs(subRange));
        }

        [Test(Description = "UpdateText should change value of text range when it has no any child")]
        public void UpdateText_ShouldChangeValueOfTextRange_WhenItHasNoAnyChild()
        {
            // Given
            var underTest = TextDocument.TextRange;

            // When
            var result = underTest.UpdateText("Hi World!");

            // Then
            Assert.That(result, Is.SameAs(underTest));
            Assert.That(result.Text, Is.EqualTo("Hi World!"));
        }

        [Test(Description = "UpdateText should change value of TextDocument when it has no any child and TextRange is the root")]
        public void UpdateText_ShouldChangeValueOfTextDocument_WhenItHasNoAnyChildAndTextRangeIsTheRoot()
        {
            // Given
            var underTest = TextDocument.TextRange;

            // When
            underTest.UpdateText("Hi World!");

            // Then
            Assert.That(TextDocument.Text.ToString(), Is.EqualTo("Hi World!"));
        }

        [Test(Description = "UpdateText should update its children when it has one child and text's length is the same")]
        public void UpdateText_ShouldUpdateItsChild_WhenItHasOneChildAndTextsLengthIsTheSame()
        {
            // Given
            var underTest = TextDocument.TextRange;
            var subTextRange = underTest.SubRange(1, 11, TextPosition.Absolute);

            // When
            underTest.UpdateText("HELLO WORLD!");

            // Then
            Assert.That(subTextRange.Start, Is.EqualTo(1));
            Assert.That(subTextRange.Stop, Is.EqualTo(11));
            Assert.That(subTextRange.Text, Is.EqualTo("ELLO WORLD"));
        }
    }
}
