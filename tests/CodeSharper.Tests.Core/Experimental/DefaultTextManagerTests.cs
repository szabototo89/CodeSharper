using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CodeSharper.Core.Experimental;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace CodeSharper.Tests.Core.Experimental
{
    [TestFixture]
    internal class DefaultTextManagerTests : TestFixtureBase
    {
        private String multiLineText(params String[] lines)
        {
            return String.Join(Environment.NewLine, lines);
        }

        [Test(Description = "CreateOrGetTextSpan should create a new TextSpan when it has not initialized yet.")]
        public void CreateOrGetTextSpan_ShouldCreateANewTextSpan_WhenItHasNotInitializedYet()
        {
            // Given
            var underTest = new DefaultTextManager("Hello World!");
            var start = new TextPosition(0, 0);
            var stop = new TextPosition(0, 12);

            // When
            var result = underTest.CreateOrGetTextSpan(start, stop);

            // Then
            Assert.That(result, Is.EqualTo(new TextSpan(start, stop)));
        }

        [Test(Description = "CreateOrGetTextSpan should get an existing TextSpan when it has already been defined.")]
        public void CreateOrGetTextSpan_ShouldGetAnExistingTextSpan_WhenItHasAlreadyBeenDefined()
        {
            // Given
            var underTest = new DefaultTextManager("Hello World!");
            var start = new TextPosition(0, 0);
            var stop = new TextPosition(0, 12);
            var textSpan = underTest.CreateOrGetTextSpan(start, stop);

            // When
            var result = underTest.CreateOrGetTextSpan(start, stop);

            // Then
            Assert.That(result, Is.SameAs(textSpan));
        }

        [Test(Description = "GetValue should return selected text by span when Span.Start and Stop are in same lines.")]
        public void GetValue_ShouldReturnSelectedTextBySpan_WhenSpanStartAndStopAreInSameLines()
        {
            // Given
            var underTest = new DefaultTextManager("Hello World!");
            var start = new TextPosition(0, 0);
            var stop = new TextPosition(0, 5);
            var textSpan = underTest.CreateOrGetTextSpan(start, stop);

            // When
            var result = underTest.GetValue(textSpan);

            // Then
            Assert.That(result, Is.EqualTo("Hello"));
        }

        [Test(Description = "GetValue should return selected text by span when Span.Start and Stop are in different lines.")]
        public void GetValue_ShouldReturnSelectedTextBySpan_WhenSpanStartAndStopAreInDifferentLines()
        {
            // Given
            var text = multiLineText("Hello World!",
                                     "How is it going?",
                                     "We are programming on a very exciting project.",
                                     "Come and join us!");

            var underTest = new DefaultTextManager(text);
            var start = new TextPosition(0, 6);
            var stop = new TextPosition(1, 6);
            var textSpan = underTest.CreateOrGetTextSpan(start, stop);

            // When
            var result = underTest.GetValue(textSpan);

            // Then
            Assert.That(result, Is.EqualTo(multiLineText("World!", "How is")));
        }

        [Test(Description = "SetValue should set selected text by span to given value when span Start and Stop are in the same lines and there is no conflict between spans.")]
        public void SetValue_ShouldSetSelectedTextBySpanToGivenValue_WhenSpanStartAndStopAreInSameLinesAndThereIsNoConflictBetweenSpans()
        {
            // Given
            var text = multiLineText("Hello World!",
                                     "How is it going?",
                                     "We are working on a very exciting project.",
                                     "Come and join us!");

            var underTest = new DefaultTextManager(text);
            var start = new TextPosition(0, 0);
            var stop = new TextPosition(0, 5);
            var textSpan = underTest.CreateOrGetTextSpan(start, stop);

            // When
            underTest.SetValue("Hi", textSpan);
            var result = new
            {
                Span = underTest.GetValue(textSpan),
                Text = underTest.GetText()
            };

            // Then
            Assert.That(result, Is.EqualTo(new
            {
                Span = "Hi",
                Text = multiLineText("Hi World!",
                                     "How is it going?",
                                     "We are working on a very exciting project.",
                                     "Come and join us!")
            }));
        }

        [Test(
            Description =
                "SetValue should set selected text by span to given value when span Start and Stop are in the different lines and start column starts from zero and there is no conflict between spans."
            )]
        public void SetValue_ShouldSetSelectedTextBySpanToGivenValue_WhenSpanStartAndStopAreInDifferentLinesAndStartColumnStartsFromZeroAndThereIsNoConflictBetweenSpans()
        {
            // Given
            var text = multiLineText("Hello World!",
                                     "How is it going?",
                                     "We are working on a very exciting project.",
                                     "Come and join us!");

            var underTest = new DefaultTextManager(text);
            var start = new TextPosition(0, 0);
            var stop = new TextPosition(2, 6);
            var textSpan = underTest.CreateOrGetTextSpan(start, stop);

            // When
            underTest.SetValue("We are", textSpan);
            var result = new
            {
                Span = underTest.GetValue(textSpan),
                Text = underTest.GetText()
            };

            // Then
            Assert.That(result, Is.EqualTo(new
            {
                Span = "We are",
                Text = multiLineText("We are working on a very exciting project.",
                                     "Come and join us!")
            }));
        }

        [Test(Description = "SetValue should set selected text by span to given value when span Start and Stop are in the different lines and columns and there is no conflict between spans.")]
        public void SetValue_ShouldSetSelectedTextBySpanToGivenValue_WhenSpanStartAndStopAreInDifferentLinesAndColumnsAndThereIsNoConflictBetweenSpans()
        {
            // Given
            var text = multiLineText("Hello World!",
                                     "How is it going?",
                                     "We are working on a very exciting project.",
                                     "Come and join us!");

            var underTest = new DefaultTextManager(text);
            var start = new TextPosition(0, 6);
            var stop = new TextPosition(2, 6);
            var textSpan = underTest.CreateOrGetTextSpan(start, stop);

            // When
            underTest.SetValue("We are", textSpan);
            var result = new
            {
                Span = underTest.GetValue(textSpan),
                Text = underTest.GetText()
            };

            // Then
            Assert.That(result, Is.EqualTo(new
            {
                Span = "We are",
                Text = multiLineText("Hello We are working on a very exciting project.",
                                     "Come and join us!")
            }));
        }

        [Test(Description = "SetValue should set selected text by span to given value when span Start and Stop are in the same lines and columns and there are multiple spans without conflict.")]
        public void SetValue_ShouldSetSelectedTextBySpanToGivenValue_WhenSpanStartAndStopAreInSameLinesAndColumnsAndThereAreMultipleSpansWithoutConflict()
        {
            // Given
            var text = multiLineText("Hello World!",
                                     "How is it going?",
                                     "We are working on a very exciting project.",
                                     "Come and join us!");

            var underTest = new DefaultTextManager(text);

            var helloSpan = underTest.CreateOrGetTextSpan(new TextPosition(0, 0), new TextPosition(0, 5));
            var worldSpan = underTest.CreateOrGetTextSpan(new TextPosition(0, 6), new TextPosition(0, 11));

            // When
            underTest.SetValue("WORLD", worldSpan);
            var result = new
            {
                Spans = new[] {underTest.GetValue(helloSpan), underTest.GetValue(worldSpan)},
                Text = underTest.GetText()
            };

            // Then
            Assert.That(result.Spans, Is.EquivalentTo(new[] {"Hello", "WORLD"}));

            var expectedText = multiLineText("Hello WORLD!",
                                             "How is it going?",
                                             "We are working on a very exciting project.",
                                             "Come and join us!");

            Assert.That(result.Text, Is.EqualTo(expectedText));
        }


        [Test(Description = "SetValue should set selected text by span to given value and update text ranges in the same line when span Start and Stop are in the same lines and columns and there are multiple spans without conflict.")]
        public void SetValue_ShouldSetSelectedTextBySpanToGivenValueAndUpdateTextRangesInTheSameLine_WhenSpanStartAndStopAreInSameLinesAndColumnsAndThereAreMultipleSpansWithoutConflict()
        {
            // Given
            var text = multiLineText("Hello World!",
                                     "How is it going?",
                                     "We are working on a very exciting project.",
                                     "Come and join us!");

            var underTest = new DefaultTextManager(text);

            var firstSpan = underTest.CreateOrGetTextSpan(new TextPosition(1, 0), new TextPosition(1, 3));
            var secondSpan = underTest.CreateOrGetTextSpan(new TextPosition(1, 4), new TextPosition(1, 6));

            // When
            underTest.SetValue("Where", firstSpan);
            var result = new {
                Spans = new[] { underTest.GetValue(firstSpan), underTest.GetValue(secondSpan) },
                Text = underTest.GetText()
            };

            // Then
            Assert.That(result.Spans, Is.EquivalentTo(new[] { "Where", "is" }));

            var expectedText = multiLineText("Hello World!",
                                             "Where is it going?",
                                             "We are working on a very exciting project.",
                                             "Come and join us!");

            Assert.That(result.Text, Is.EqualTo(expectedText));
        }

        [Test(Description = "SetValue should set selected text by span to given value and update text ranges in the same line when span Start and Stop are in the different lines and columns and there are multiple spans without conflict.")]
        public void SetValue_ShouldSetSelectedTextBySpanToGivenValueAndUpdateTextRangesInTheSameLine_WhenSpanStartAndStopAreInDifferentLinesAndColumnsAndThereAreMultipleSpansWithoutConflict()
        {
            // Given
            var text = multiLineText("Hello World!",
                                     "How is it going?",
                                     "We are working on a very exciting project.",
                                     "Come and join us!");

            var underTest = new DefaultTextManager(text);

            var firstSpan = underTest.CreateOrGetTextSpan(new TextPosition(0, 0), new TextPosition(1, 3));
            var secondSpan = underTest.CreateOrGetTextSpan(new TextPosition(1, 4), new TextPosition(1, 6));

            // When
            underTest.SetValue("Where", firstSpan);
            var result = new {
                Spans = new[] { underTest.GetValue(firstSpan), underTest.GetValue(secondSpan) },
                Text = underTest.GetText()
            };

            // Then
            Assert.That(result.Spans, Is.EquivalentTo(new[] { "Where", "is" }));

            var expectedText = multiLineText("Where is it going?",
                                             "We are working on a very exciting project.",
                                             "Come and join us!");

            Assert.That(result.Text, Is.EqualTo(expectedText));
        }

        [TestCase(100)]
        [TestCase(5000)]
        [TestCase(10000)]
        [TestCase(20000)]
        [Test(Description = "SetValue should handle huge amount text spans")]
        [Ignore("Run these manually")]
        public void SetValue_ShouldHandleHugeAmountTextSpans(Int32 lines)
        {
            // Given
            var textBuilder = new StringBuilder();
            for (var i = 0; i < lines; i++)
            {
                textBuilder.AppendLine("one,two,three,four");
            }

            var underTest = new DefaultTextManager(textBuilder.ToString());
            var spans = new List<TextSpan>();
            for (var i = 0; i < lines; i++)
            {
                spans.Add(underTest.CreateOrGetTextSpan(new TextPosition(i, 0), new TextPosition(i, 3)));
                underTest.CreateOrGetTextSpan(new TextPosition(i, 4), new TextPosition(i, 7));
            }

            // When
            var watch = Stopwatch.StartNew();
            foreach (var span in spans)
            {
                underTest.SetValue("onetwo", span);
            }
            watch.Stop();

            // Then
            Console.WriteLine(watch.Elapsed);
        }
    }
}