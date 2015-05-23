using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common.Runnables
{
    [TestFixture]
    internal class RunnableTests : TestFixtureBase
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
