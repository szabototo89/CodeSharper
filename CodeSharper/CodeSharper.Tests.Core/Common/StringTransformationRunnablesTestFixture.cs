using System;
using CodeSharper.Core.Common.Runnables.StringTransformation;
using CodeSharper.Core.Texts;
using CodeSharper.Tests.Core.TestHelpers;
using Ninject;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    internal class StringTransformationRunnablesTestFixture : TextRangeTestBase
    {
        public StandardKernel Kernel { get; private set; }

        [SetUp]
        public override void Setup()
        {
            Kernel = new StandardKernel();
            Kernel.Bind<TextDocument>().ToConstant(new TextDocument("Hello World!"));
            Kernel
                .Bind<TextRange>()
                .ToConstructor(ctor => new TextRange(0, 12, ctor.Inject<TextDocument>()))
                .Named("full");

        }

        [TearDown]
        public override void Teardown()
        {
            if (!Kernel.IsDisposed)
                Kernel.Dispose();
        }

        [Test(Description = "ToUpperCaseRunnable should return uppercase text range")]
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

        [Test(Description = "ToLowerCaseRunnable should return lowercase text range")]
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

        [Test(Description = "ReplaceTextRunnable should able to replace given text")]
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

        [Test(Description = "ReplaceTextRunnable should able to replace array of given values")]
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

        [Test(Description = "InsertTextRangeRunnable should insert new value to text range")]
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

        [Test(Description = "InsertTextRangeRunnable should insert new value into middle of text range")]
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

        [Test(Description = "TrimTextRangeRunnable should trim text")]
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

        [Test(Description = "TrimTextRangeRunnable should trim beginning of text")]
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

        [Test(Description = "TrimTextRangeRunnable should trim end of text")]
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