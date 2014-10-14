using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Texts;
using CodeSharper.Tests.Core.Utilities;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    internal class RunnableTestFixture
    {
        private TextRange _TextRange(String value)
        {
            return new TextDocument(value).TextRange;
        }

        [Test]
        public void ToUpperCaseRunnableShouldReturnUppercaseTextRange()
        {
            // Given
            var parameter = _TextRange("hello world!");
            var underTest = new ToUpperCaseRunnable();

            // When
            var result = underTest.Run(parameter);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Text, Is.EqualTo("HELLO WORLD!"));
        }

        [Test]
        public void ToLowerCaseRunnableShouldReturnLowercaseTextRange()
        {
            // Given
            var parameter = _TextRange("HELLO WORLD!");
            var underTest = new ToLowerCaseRunnable();

            // When
            var result = underTest.Run(parameter);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Text, Is.EqualTo("hello world!"));
        }

        [Test]
        public void ReplaceTextRunnableShouldAbleToReplaceGivenText()
        {
            // Given
            var parameter = _TextRange("hello world!");
            var underTest = new ReplaceTextRunnable("hi world!");

            // When
            var result = underTest.Run(parameter);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Text, Is.EqualTo("hi world!"));
        }

        [Test(Description = "FillStringRunnable should fill with given character in string")]
        [TestCase("Hello World!", "************")]
        public void FillStringRunnableShouldFillWithGivenCharacterInString(String parameter, String expected)
        {
            // Given
            var underTest = new FillStringRunnable('*');
            var textDocument = _TextRange(parameter);

            // When
            var result = underTest.Run(textDocument);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Text, Is.EqualTo(expected));
        }

        [Test(Description = "FillStringRunnable should fill with given character in string")]
        [TestCase("Hello World!", "hihihihihihi")]
        public void FillStringRunnableShouldFillWithGivenTextPatternInString(String parameter, String expected)
        {
            // Given
            var underTest = new FillStringRunnable("hi");
            var textDocument = _TextRange(parameter);

            // When
            var result = underTest.Run(textDocument);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Text, Is.EqualTo(expected));
        }

        [Test]
        public void FindTextRunnableShouldReturnFoundSubStringsOfText()
        {
            // Given
            var value = _TextRange("This string contains 'abc' twice. This was the first, and the second is here: abc.");
            var underTest = new FindTextRunnable("abc");

            // When
            var result = underTest.Run(value);

            // Then
            Assert.That(result, Is.Not.Null.And.Not.Empty);
            Assert.That(result, Has.Count.Or.Length.EqualTo(2));
        }

        [Test(Description = "IdentityCommand should return with passed value.")]
        public void IdentityRunnableShouldReturnWithPassedValue()
        {
            // Given
            var parameter = "Hello World!";
            var underTest = new IdentityRunnable();

            // When
            var result = underTest.Run(parameter);

            // Then
            Assert.That(result, Is.EqualTo(parameter));
        }

        [Test]
        public void SplitStringRunnableShouldSplitAnyStringToMultipleValues()
        {
            // Given
            var argument = _TextRange("a b c d");
            var underTest = new SplitStringRunnable(" ");

            // When
            var result = underTest.Run(argument);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Select(range => range.Text), Is.EquivalentTo(new[] { "a", "b", "c", "d" }));
        }

        [Test]
        public void RegularExpressionCommandShouldBeAbleToFindTextWithRegularExpressions()
        {
            // Given
            var argument = _TextRange(TestHelper.LoremIpsum.TakeWords(4));
            var underTest = new RegularExpressionRunnable(@"\w+");

            // When
            var result = underTest.Run(argument);

            // Then
            Assert.That(result, Is.Not.Null.And.Not.Empty);
        }
    }
}
