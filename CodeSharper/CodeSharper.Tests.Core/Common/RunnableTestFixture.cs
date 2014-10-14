using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Texts;
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
    }
}
