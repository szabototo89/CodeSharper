using System;
using CodeSharper.Core.Common.Runnables.TextRangeOperations;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common.Runnables.TextRangeOperations
{
    [TestFixture]
    internal class JoinLinesTextRunnableTests : RunnableTestFixtureBase<JoinLinesTextRunnable>
    {
        private TextRange createTextRange(String text)
        {
            var document = new TextDocument(text);
            var textRange = document.TextRange;
            return textRange;
        }

        [Test(Description = "JoinLinesTextRunnable should join lines when multiple lines are passed")]
        public void JoinLinesTextRunnable_ShouldJoinLines_WhenMultipleLinesArePassed()
        {
            // Given
            var text = String.Join(Environment.NewLine, "Hello ", "World", "!");
            var textRange = createTextRange(text);
            var underTest = new JoinLinesTextRunnable();

            // When
            var result = underTest.Run(textRange);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.GetText(), Is.EqualTo("Hello World!"));
        }
    }
}
