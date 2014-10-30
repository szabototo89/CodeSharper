using System;
using CodeSharper.Core.Common.Runnables.StringTransformation;
using CodeSharper.Core.Texts;
using CodeSharper.Tests.Core.Utilities;
using Ninject;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    internal class StringTransformationRunnablesTestFixture : TextRangeTestBase
    {
        public StandardKernel Kernel { get; private set; }

        [SetUp]
        public void Setup()
        {
            Kernel = new StandardKernel();
            Kernel.Bind<TextDocument>().ToConstant(new TextDocument("Hello World!"));
            Kernel
                .Bind<TextRange>()
                .ToConstructor(ctor => new TextRange(0, 12, ctor.Inject<TextDocument>()))
                .Named("full");

        }

        [TearDown]
        public void Teardown()
        {
            if (!Kernel.IsDisposed)
                Kernel.Dispose();
        }

        [Test]
        public void ToUpperCaseRunnableShouldReturnUppercaseTextRange()
        {
            // Given
            var parameter = TestHelper.TextRange("hello world!");
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
            var parameter = TestHelper.TextRange("HELLO WORLD!");
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
            var parameter = TestHelper.TextRange("hello world!");
            var underTest = new ReplaceTextRunnable("hi world!");

            // When
            var result = underTest.Run(parameter);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Text, Is.EqualTo("hi world!"));
        }

        [Test]
        public void ReplaceTextRunnableShouldAbleToReplaceArrayOfGivenValues()
        {
            // Given
            var parameter = TestHelper.TextRange("hello world!");
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
            var textDocument = TestHelper.TextRange(parameter);

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
            var textDocument = TestHelper.TextRange(parameter);

            // When
            var result = underTest.Run(textDocument);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Text, Is.EqualTo(expected));
        }

        [Test]
        public void InsertTextRangeRunnableShouldInsertNewValueToTextRange()
        {
            // Given
            var text = "Hello World!";
            var textRange = TextRange(text);
            var underTest = new InsertTextRangeRunnable(0, "Hi, ");

            // When
            var result = underTest.Run(textRange);

            // Then
            Assert.That(result.Text, Is.EqualTo("Hi, Hello World!"));
        }

        [Test]
        public void InsertTextRangeRunnableShouldInsertNewValueIntoMiddleOfTextRange()
        {
            // Given
            var text = "Hello World!";
            var textRange = TextRange(text).SubStringOfText(6, 6);
            var underTest = new InsertTextRangeRunnable(0, "Hi, ");

            // When
            var result = underTest.Run(textRange);

            // Then
            Assert.That(result.Text, Is.EqualTo("Hi, World!"));
            Assert.That(result.TextDocument.Text.ToString(), Is.EqualTo("Hello Hi, World!"));
        }

        [Test]
        public void TrimTextRangeRunnableShouldTrimText()
        {
            // Given
            var text = "   Hello World!   ";
            var textRange = TextRange(text);
            var underTest = new TrimTextRangeRunnable();

            // When
            var result = underTest.Run(textRange);

            // Then
            Assert.That(result.TextDocument.Text.ToString(), Is.EqualTo("Hello World!"));
        }

        [Test]
        public void TrimTextRangeRunnableShouldTrimBeginningOfText()
        {
            // Given
            var text = "   Hello World!   ";
            var textRange = TextRange(text);
            var underTest = new TrimTextRangeRunnable(TrimOptions.TrimStart);

            // When
            var result = underTest.Run(textRange);

            // Then
            Assert.That(result.TextDocument.Text.ToString(), Is.EqualTo("Hello World!   "));
        }

        [Test]
        public void TrimTextRangeRunnableShouldTrimEndOfText()
        {
            // Given
            var text = "   Hello World!   ";
            var textRange = TextRange(text);
            var underTest = new TrimTextRangeRunnable(TrimOptions.TrimEnd);

            // When
            var result = underTest.Run(textRange);

            // Then
            Assert.That(result.TextDocument.Text.ToString(), Is.EqualTo("   Hello World!"));
        }
    }
}