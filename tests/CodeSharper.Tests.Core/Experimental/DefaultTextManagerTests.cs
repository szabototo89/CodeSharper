using System;
using CodeSharper.Core.Experimental;
using NUnit.Framework;

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
            var stop = new TextPosition(0, 4);
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

        [Test(Description = "SetValue should set selected text by span to given value when span Start and Stop are in the different lines and start column starts from zero and there is no conflict between spans.")]
        public void SetValue_ShouldSetSelectedTextBySpanToGivenValue_WhenSpanStartAndStopAreInDifferentLinesAndStartColumnStartsFromZeroAndThereIsNoConflictBetweenSpans()
        {
            // Given
            var text = multiLineText("Hello World!",
                                     "How is it going?",
                                     "We are working on a very exciting project.",
                                     "Come and join us!");

            var underTest = new DefaultTextManager(text);
            var start = new TextPosition(0, 0);
            var stop = new TextPosition(2, 5);
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
            var stop = new TextPosition(2, 5);
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
    }
}