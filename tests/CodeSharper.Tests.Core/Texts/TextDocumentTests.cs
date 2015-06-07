using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CodeSharper.Tests.Core.Texts
{
    [TestFixture]
    internal class TextDocumentTests : TestFixtureBase
    {
        protected TextDocument UnderTest { get; set; }

        public override void Setup()
        {
            base.Setup();
            UnderTest = new TextDocument("Hello World!");
        }

        [Test(Description = "CreateOrGetTextRange should create new TextRange when proper positions are passed and there is no existing TextRange")]
        public void CreateOrGetTextRange_ShouldCreateNewTextRange_WhenProperPositionsArePassedAndThereIsNoExistingTextRange()
        {
            // Given in setup 

            // When
            var result = UnderTest.CreateOrGetTextRange(0, 5);

            // Then
            Assert.That(result.Start, Is.EqualTo(0));
            Assert.That(result.Stop, Is.EqualTo(5));
            Assert.That(result.TextDocument, Is.EqualTo(UnderTest));

            Assert.That(result, Is.EqualTo(new TextRange(0, 5, UnderTest)));
        }

        [Test(Description = "TextRange should return root element of TextDocument when TextDocument is initialized")]
        public void TextRange_ShouldReturnRootElementOfTextDocument_WhenTextDocumentIsInitialized()
        {
            // Given in setup

            // When
            var result = UnderTest.TextRange;

            // Then
            Assert.That(result, Is.EqualTo(new TextRange(0, 12, UnderTest)));
        }

        [Test(Description = "CreateOrGetTextRange should return an existing TextRange when it is called")]
        public void CreateOrGetTextRange_ShouldReturnAnExistingTextRange_WhenItIsCalled()
        {
            // Given in setup


            // When
            var result = UnderTest.CreateOrGetTextRange(0, 12);

            // Then
            Assert.That(result, Is.SameAs(UnderTest.TextRange));
        }

        [TestCase(0, 5)]
        [TestCase(1, 5)]
        [TestCase(5, 5)]
        [Test(Description = "CreateOrGetTextRange should return existing sub TextRange when sub TextRange is initialized previously")]
        public void CreateOrGetTextRange_ShouldReturnExistingSubTextRange_WhenSubTextRangeIsInitializedPreviously(Int32 start, Int32 stop)
        {
            // Given in setup
            var textRange = UnderTest.CreateOrGetTextRange(start, stop);

            // When
            var result = UnderTest.CreateOrGetTextRange(start, stop);

            // Then
            Assert.That(result, Is.SameAs(textRange));
        }

        [Test(Description = "CreateOrGetTextRange should order text ranges by start and stop positions when it is called")]
        public void CreateOrGetTextRange_ShouldOrderTextRangesByStartAndStopPositions_WhenItIsCalled()
        {
            // Given in setup

            var textRanges = new[] {
                UnderTest.CreateOrGetTextRange(3, 5),
                UnderTest.CreateOrGetTextRange(1, 5),
                UnderTest.CreateOrGetTextRange(3, 4),
                UnderTest.CreateOrGetTextRange(2, 7),
                UnderTest.CreateOrGetTextRange(0, 7)
            };

            // When
            var result = UnderTest.TextRanges
                                  .Select(textRange => new {
                                      Start = textRange.Start,
                                      Stop = textRange.Stop
                                  })
                                  .ToArray();

            // Then
            Assert.That(result, Is.EquivalentTo(new[]
            {
                new {Start = 0, Stop = 7},
                new {Start = 0, Stop = 12},
                new {Start = 1, Stop = 5},
                new {Start = 2, Stop = 7},
                new {Start = 3, Stop = 4},
                new {Start = 3, Stop = 5},
            }));
        }

        [Test(Description = "ChangeText should remove existing substring when insert new value when there is no conflict between text ranges")]
        public void ChangeText_ShouldRemoveExistingSubstringAndInsertNewValue_WhenThereIsNoConflictBetweenTextRanges()
        {
            // Given in setup


            // When
            UnderTest.ChangeText(UnderTest.TextRange, "Hi World!");
            var result = UnderTest.Text.ToString();

            // Then
            Assert.That(result, Is.EqualTo("Hi World!"));
        }

        [Test(Description = "ChangeText should return updated text range when whole text range is passed")]
        public void ChangeText_ShouldReturnUpdatedTextRange_WhenWholeTextRangeIsPassed()
        {
            // Given in setup

            // When
            UnderTest.ChangeText(UnderTest.TextRange, "Hi World!");
            var result = UnderTest.GetText(UnderTest.TextRange);

            // Then
            Assert.That(UnderTest.TextRange, Is.EqualTo(new TextRange(0, 9, UnderTest)));
            Assert.That(result, Is.EqualTo("Hi World!"));
        }

        [Test(Description = "ChangeText should update non-conflict text ranges after it changed text")]
        public void ChangeText_ShouldUpdateNonConflictTextRanges_AfterItChangedText()
        {
            // Given in setup
            var words = new[]
            {
                UnderTest.CreateOrGetTextRange(0, 5),
                UnderTest.CreateOrGetTextRange(6, 12)
            };

            // When
            UnderTest.ChangeText(words[0], "Hi");
            var result = UnderTest.TextRanges
                                  .Where(range => range != UnderTest.TextRange)
                                  .Select(textRange => UnderTest.GetText(textRange)).ToArray();

            // Then
            Assert.That(result, Is.EquivalentTo(new[] { "Hi", "World!" }));
        }

        [Test(Description = "ChangeText should update superset text ranges after it changed text")]
        public void ChangeText_ShouldUpdateSupersetTextRanges_AfterItChangedText()
        {
            // Given in setup
            var words = new[]
            {
                UnderTest.CreateOrGetTextRange(6, 11),
                UnderTest.CreateOrGetTextRange(0, 12),
            };

            // When
            UnderTest.ChangeText(words[0], "Me");
            var result = UnderTest.TextRanges.Select(textRange => UnderTest.GetText(textRange)).ToArray();

            // Then
            Assert.That(UnderTest.Text, Is.EqualTo("Hello Me!"));
            Assert.That(result, Is.EquivalentTo(new[] { "Me", "Hello Me!" }));
        }

        [Test(Description = "ChangeText should update overlapping text ranges after it changed text")]
        public void ChangeText_ShouldUpdateOverlappingTextRanges_AfterItChangedText()
        {
            // Given 
            var underTest = new TextDocument("0123456789");

            var words = new[]
            {
                underTest.CreateOrGetTextRange(0, 4),
                underTest.CreateOrGetTextRange(2, 10),
            };

            // When
            underTest.ChangeText(words[0], "9");
            var result = underTest.TextRanges
                                  .Where(range => range != underTest.TextRange)
                                  .Select(textRange => underTest.GetText(textRange)).ToArray();

            // Then
            Assert.That(result, Is.EquivalentTo(new[] { "9", "9456789" }));
        }


        [Test(Description = "ChangeRawText should update text but it does not update any text ranges when it is called")]
        public void ChangeRawText_ShouldUpdateTextButItDoesNotUpdateAnyTextRanges_WhenItIsCalled()
        {
            // Given in setup
            var textRange = UnderTest.CreateOrGetTextRange(0, 5);

            // When
            UnderTest.ChangeRawText(textRange, "Hi");
            var result = UnderTest.Text;

            // Then
            Assert.That(result, Is.EqualTo("Hi World!"));

            var textRanges = UnderTest.TextRanges;
            Assert.That(new[] { textRange }, Is.SubsetOf(textRanges));
        }

        [Test(Description = "GetText should return positioned text when proper TextRange is passed")]
        public void GetText_ShouldReturnPositionedText_WhenProperTextRangeIsPassed()
        {
            // Given in setup

            // When
            var result = UnderTest.GetText(UnderTest.TextRange);

            // Then
            Assert.That(result, Is.EqualTo("Hello World!"));
        }

        [Test(Description = "RemoveText should remove selected text when proper text range is passed")]
        public void RemoveText_ShouldRemoveSelectedText_WhenProperTextRangeIsPassed()
        {
            // Given
            var textRange = UnderTest.CreateOrGetTextRange(0, 5);

            // When
            UnderTest.RemoveText(textRange);
            var result = UnderTest.Text;

            // Then
            Assert.That(result, Is.EqualTo(" World!"));
        }

        [Test(Description = "RemoveText should remove text range when prefix of text range is passed")]
        public void RemoveText_ShouldRemoveTextRange_WhenPrefixOfTextRangeIsPassed()
        {
            // Given
            var textRange = UnderTest.CreateOrGetTextRange(0, 5);

            // When
            UnderTest.RemoveText(textRange);
            var result = UnderTest.TextRange;

            // Then
            // Assert.That(result, Is.EqualTo(textRange.Next));
        }

        [TestCase(0, 5)]
        [TestCase(1, 5)]
        [TestCase(6, 11)]
        [Test(Description = "RemoveText should remove text range from chain when proper text range is passed")]
        public void RemoveText_ShouldRemoveTextRangeFromChain_WhenProperTextRangeIsPassed(Int32 start, Int32 stop)
        {
            // Given
            var textRange = UnderTest.CreateOrGetTextRange(start, stop);

            // When
            UnderTest.RemoveText(textRange);
            var textRanges = UnderTest.TextRanges.ToArray();
            var result = textRanges.FirstOrDefault(range => textRange.Equals(range));

            // Then
            Assert.That(result, Is.Null);
        }
    }
}
