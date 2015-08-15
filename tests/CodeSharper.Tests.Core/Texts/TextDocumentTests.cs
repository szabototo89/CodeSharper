using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CodeSharper.Tests.Core.Texts
{
    [TestFixture]
    internal class TextDocumentTests : TestFixtureBase
    {
        internal class InitializeTextDocumentWithHelloWorldText : TestFixtureBase
        {
            protected TextDocument UnderTest { get; set; }

            public override void Setup()
            {
                base.Setup();
                UnderTest = new TextDocument("Hello World!");
            }
        }

        public class CreateOrGetTextRangeMethod : InitializeTextDocumentWithHelloWorldText
        {
            [Test(Description = "CreateOrGetTextRange should return an existing TextRange when it is called")]
            public void ShouldReturnAnExistingTextRange_WhenItIsCalled()
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
            public void ShouldReturnExistingSubTextRange_WhenSubTextRangeIsInitializedPreviously(Int32 start, Int32 stop)
            {
                // Given in setup
                var textRange = UnderTest.CreateOrGetTextRange(start, stop);

                // When
                var result = UnderTest.CreateOrGetTextRange(start, stop);

                // Then
                Assert.That(result, Is.SameAs(textRange));
            }

            [Test(Description = "CreateOrGetTextRange should order text ranges by start and stop positions when it is called")]
            public void ShouldOrderTextRangesByStartAndStopPositions_WhenItIsCalled()
            {
                // Given in setup

                var textRanges = new[]
                {
                    UnderTest.CreateOrGetTextRange(3, 5),
                    UnderTest.CreateOrGetTextRange(1, 5),
                    UnderTest.CreateOrGetTextRange(3, 4),
                    UnderTest.CreateOrGetTextRange(2, 7),
                    UnderTest.CreateOrGetTextRange(0, 7)
                };

                // When
                var result = UnderTest.TextRanges
                                      .Select(textRange => new
                                      {
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

            [Test(Description = "CreateOrGetTextRange should create new TextRange when proper positions are passed and there is no existing TextRange")]
            public void ShouldCreateNewTextRange_WhenProperPositionsArePassedAndThereIsNoExistingTextRange()
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
        }

        public class TextRangeProperty : InitializeTextDocumentWithHelloWorldText
        {
            [Test(Description = "TextRange should return root element of TextDocument when TextDocument is initialized")]
            public void ShouldReturnRootElementOfTextDocument_WhenTextDocumentIsInitialized()
            {
                // Given in setup

                // When
                var result = UnderTest.TextRange;

                // Then
                Assert.That(result, Is.EqualTo(new TextRange(0, 12, UnderTest)));
            }
        }

        public class ChangeTextMethod : InitializeTextDocumentWithHelloWorldText
        {
            [Test(Description = "ChangeText should remove existing substring when insert new value when there is no conflict between text ranges")]
            public void ShouldRemoveExistingSubstringAndInsertNewValue_WhenThereIsNoConflictBetweenTextRanges
                ()
            {
                // Given in setup

                // When
                UnderTest.ChangeText(UnderTest.TextRange, "Hi World!");
                var result = UnderTest.Text.ToString();

                // Then
                Assert.That(result, Is.EqualTo("Hi World!"));
            }

            [Test(Description = "ChangeText should return updated text range when whole text range is passed")]
            public void ShouldReturnUpdatedTextRange_WhenWholeTextRangeIsPassed()
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
            public void ShouldUpdateNonConflictTextRanges_AfterItChangedText()
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
                Assert.That(result, Is.EquivalentTo(new[] {"Hi", "World!"}));
            }

            [Test(Description = "ChangeText should update superset text ranges after it changed text")]
            public void ShouldUpdateSupersetTextRanges_AfterItChangedText()
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
                Assert.That(result, Is.EquivalentTo(new[] {"Me", "Hello Me!"}));
            }

            [Test(Description = "ChangeText should update overlapping text ranges after it changed text")]
            public void ShouldUpdateOverlappingTextRanges_AfterItChangedText()
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
                Assert.That(result, Is.EquivalentTo(new[] {"9", "9456789"}));
            }

            [Test(Description = "EndTransaction should update one TextRange when it is in batch mode")]
            public void ShouldUpdateOneTextRange_WhenItIsInBatchMode()
            {
                // Given
                var textRange = UnderTest.CreateOrGetTextRange(0, 5);

                // When
                UnderTest.BeginTransaction();
                UnderTest.ChangeText(textRange, "Hi");
                UnderTest.EndTransaction();

                // Then
                Assert.That(textRange.GetText(), Is.EqualTo("Hi"));
                Assert.That(UnderTest.TextRange.GetText(), Is.EqualTo("Hi World!"));
                Assert.That(UnderTest.Text, Is.EqualTo("Hi World!"));
            }

            [Test(Description = "EndTransaction should update two TextRange when it is in batch mode")]
            public void ShouldUpdateTwoTextRange_WhenItIsInBatchMode()
            {
                // Given
                var firstWord = UnderTest.CreateOrGetTextRange(0, 5);
                var secondWord = UnderTest.CreateOrGetTextRange(6, 11);

                // When
                UnderTest.BeginTransaction();
                UnderTest.ChangeText(firstWord, "Hi");
                UnderTest.ChangeText(secondWord, "WOOORLD");
                UnderTest.EndTransaction();

                // Then
                Assert.That(UnderTest.Text, Is.EqualTo("Hi WOOORLD!"));
                Assert.That(UnderTest.TextRange.GetText(), Is.EqualTo("Hi WOOORLD!"));
                Assert.That(firstWord.GetText(), Is.EqualTo("Hi"));
                Assert.That(secondWord.GetText(), Is.EqualTo("WOOORLD"));
            }


            [TestCase(100)]
            [TestCase(5000)]
            [TestCase(10000)]
            [TestCase(20000)]
            [Test(Description = "ChangeText should handle huge amount text spans")]
            public void ShouldHandleHugeAmountTextSpans_WhenBatchModeIsInactive(Int32 lines)
            {
                // Given
                var textBuilder = new StringBuilder();
                var line = "one,two,three,four";
                for (var i = 0; i < lines; i++)
                {
                    textBuilder.AppendLine(line);
                }

                var underTest = new TextDocument(textBuilder.ToString());
                var ranges = new List<TextRange>();

                for (var i = 0; i < lines; i++)
                {
                    var offset = (line.Length + Environment.NewLine.Length)*i;
                    ranges.Add(underTest.CreateOrGetTextRange(offset + 0, offset + 3));
                    underTest.CreateOrGetTextRange(offset + 4, offset + 7);
                }

                // When
                var watch = Stopwatch.StartNew();
                foreach (var range in ranges)
                {
                    underTest.ChangeText(range, "onetwo");
                }
                watch.Stop();

                // Then
                Console.WriteLine(watch.Elapsed);
            }

            [TestCase(10)]
            [TestCase(100)]
            [TestCase(5000)]
            [TestCase(10000)]
            [TestCase(20000)]
            [TestCase(100000)]
            [Test(Description = "ChangeText should handle huge amount text spans")]
            public void ShouldHandleHugeAmountTextSpans_WhenBatchModeIsActive(Int32 lines)
            {
                // Given
                var textBuilder = new StringBuilder();
                var line = "one,two,three,four";
                for (var i = 0; i < lines; i++)
                {
                    textBuilder.AppendLine(line);
                }

                var underTest = new TextDocument(textBuilder.ToString());
                var ranges = new List<TextRange>();

                for (var i = 0; i < lines; i++)
                {
                    var offset = (line.Length + Environment.NewLine.Length)*i;
                    ranges.Add(underTest.CreateOrGetTextRange(offset + 0, offset + 3));
                    underTest.CreateOrGetTextRange(offset + 4, offset + 7);
                }

                // When
                var watch = Stopwatch.StartNew();
                underTest.BeginTransaction();
                foreach (var range in ranges)
                {
                    underTest.ChangeText(range, "onetwo");
                }
                underTest.EndTransaction();
                watch.Stop();

                // Then
                Console.WriteLine(watch.Elapsed);
            }
        }

        public class ChangeRawTextMehod : InitializeTextDocumentWithHelloWorldText
        {
            [Test(Description = "ChangeRawText should update text but it does not update any text ranges when it is called")]
            public void ShouldUpdateTextButItDoesNotUpdateAnyTextRanges_WhenItIsCalled()
            {
                // Given in setup
                var textRange = UnderTest.CreateOrGetTextRange(0, 5);

                // When
                UnderTest.changeRawText(textRange, "Hi");
                var result = UnderTest.Text;

                // Then
                Assert.That(result, Is.EqualTo("Hi World!"));

                var textRanges = UnderTest.TextRanges;
                Assert.That(new[] {textRange}, Is.SubsetOf(textRanges));
            }
        }

        public class GetTextMethod : InitializeTextDocumentWithHelloWorldText
        {
            [Test(Description = "GetText should return positioned text when proper TextRange is passed")]
            public void ShouldReturnPositionedText_WhenProperTextRangeIsPassed()
            {
                // Given in setup

                // When
                var result = UnderTest.GetText(UnderTest.TextRange);

                // Then
                Assert.That(result, Is.EqualTo("Hello World!"));
            }
        }

        public class RemoveTextMethod : InitializeTextDocumentWithHelloWorldText
        {
            [Test(Description = "RemoveText should remove selected text when proper text range is passed")]
            public void ShouldRemoveSelectedText_WhenProperTextRangeIsPassed()
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
            public void ShouldRemoveTextRange_WhenPrefixOfTextRangeIsPassed()
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
            public void ShouldRemoveTextRangeFromChain_WhenProperTextRangeIsPassed(Int32 start, Int32 stop)
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

        public class TransactionMode : InitializeTextDocumentWithHelloWorldText
        {
            [Test(Description = "BeginTransaction should set a flag to change several TextRanges when it is called once")]
            public void BeginTransaction_ShouldSetFlagToChangeSeveralTextRange_WhenItIsCalledOnce()
            {
                // Given
                Assume.That(UnderTest.isBatchModeActive, Is.False);
                UnderTest.BeginTransaction();

                // When
                var result = UnderTest.isBatchModeActive;

                // Then
                Assert.That(result, Is.True);
            }

            [Test(Description = "EndTransaction should unset a flag to change several TextRanges when it is called once")]
            public void EndTransaction_ShouldSetFlagToChangeSeveralTextRange_WhenItIsCalledOnce()
            {
                // Given
                UnderTest.BeginTransaction();
                Assume.That(UnderTest.isBatchModeActive, Is.True);

                // When
                UnderTest.EndTransaction();
                var result = UnderTest.isBatchModeActive;

                // Then
                Assert.That(result, Is.False);
            }
        }
    }
}