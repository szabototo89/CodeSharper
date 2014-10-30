using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.StringTransformation;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Texts.Filters;
using CodeSharper.Tests.Core.Utilities;
using Ninject;
using Ninject.Activation;
using Ninject.Parameters;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    internal class RunnableTestFixture : TextRangeTestBase
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
        public void FindTextRunnableShouldReturnFoundSubStringsOfText()
        {
            // Given
            var value =
                TestHelper.TextRange(
                    "This string contains 'abc' twice. This was the first, and the second is here: abc.");
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
        public void RepeatRunnableShouldReturnARepeatedValuesOfPassedValue()
        {
            // Given
            var parameter = 10;
            var underTest = new RepeatRunnable(3);

            // When
            var result = underTest.Run(parameter);

            // Then
            Assert.That(result, Is.EquivalentTo(new[] {10, 10, 10}));
        }

        [Test]
        public void SplitStringRunnableShouldSplitAnyStringToMultipleValues()
        {
            // Given
            var argument = TestHelper.TextRange("a b c d");
            var underTest = new SplitStringRunnable(" ");

            // When
            var result = underTest.Run(argument);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Select(range => range.Text), Is.EquivalentTo(new[] {"a", "b", "c", "d"}));
        }

        [Test]
        public void RegularExpressionCommandShouldBeAbleToFindTextWithRegularExpressions()
        {
            // Given
            var argument = TestHelper.TextRange(TestHelper.LoremIpsum.TakeWords(4));
            var underTest = new RegularExpressionRunnable(@"\w+");

            // When
            var result = underTest.Run(argument);

            // Then
            Assert.That(result, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void RunnableManagerShouldHandleArgumentWrappersAndUnwrappers()
        {
            // Given
            var underTest = RunnableManager.Instance;
            var runnableType = typeof (FindTextRunnable);
            underTest.Register(runnableType);

            // When
            var result = underTest.GetRunnableDescriptor(runnableType);

            // Then
            Assert.That(result.Type, Is.EqualTo(runnableType));
        }

        [Test]
        public void FilterByLineShouldFilterTextRanges()
        {
            // Given
            var text = String.Join(Environment.NewLine, Enumerable.Range(0, 15));
            var textRange = TextRange(text);
            var underTest = new FilterTextByLineRunnable(5);

            // When
            var result = underTest.Run(textRange).Single();

            // Then
            Assert.That(result.Text, Is.EqualTo("5"));
        }

        [Test]
        public void FilterByLineShouldFilterByEvenPositionsTextRanges()
        {
            // Given
            var text = String.Join(Environment.NewLine, Enumerable.Range(1, 6));
            var textRange = TextRange(text);
            var underTest = new FilterTextByLineRunnable(FilterPositions.Even);

            // When
            var result = underTest.Run(textRange);

            // Then
            Assert.That(result.Select(range => range.Text), Is.EquivalentTo(new[] {"2", "4", "6"}));
        }

        [Test]
        public void FilterByLineShouldFilterByOddPositionsTextRanges()
        {
            // Given
            var text = String.Join(Environment.NewLine, Enumerable.Range(1, 6));
            var textRange = TextRange(text);
            var underTest = new FilterTextByLineRunnable(FilterPositions.Odd);

            // When
            var result = underTest.Run(textRange);

            // Then
            Assert.That(result.Select(range => range.Text), Is.EquivalentTo(new[] {"1", "3", "5"}));
        }

        [Test]
        public void FilterByColumnShouldFilterTextRanges()
        {
            // Given
            var text = String.Join(Environment.NewLine, Enumerable.Repeat("a b c d e f", 3));
            var textRange = TextRange(text);
            var underTest = new FilterTextByColumnRunnable(2);

            // When
            var result = underTest.Run(textRange);

            // Then
            var expected = Enumerable.Repeat("c", 3);
            Assert.That(result.Select(range => range.Text), Is.EquivalentTo(expected));
        }

        [Test]
        public void FilterByColumnShouldFilterVariableLengthOfLinesTextRange()
        {
            // Given
            var text = String.Join(Environment.NewLine, new[]
            {
                "a b c d e",
                "aaa b c d e",
                "hello world c again",
                "hi"
            });
            var textRange = TextRange(text);
            var underTest = new FilterTextByColumnRunnable(2);

            // When
            var result = underTest.Run(textRange);

            // Then
            var expected = Enumerable.Repeat("c", 3);
            Assert.That(result.Select(range => range.Text), Is.EquivalentTo(expected));
        }

        [Test]
        public void CountTextRangeLengthShouldReturnLengthOfTextRange()
        {
            // Given
            var text = "Hello World!";
            var textRange = TextRange(text);
            var underTest = new CountTextRangeLengthRunnable();

            // When
            var result = underTest.Run(textRange);

            // Then
            Assert.That(result, Is.EqualTo(12));
        }
    }
}
