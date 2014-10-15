using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Commands;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;
using CodeSharper.Tests.Core.Utilities;
using Moq;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    class CommandTestFixture
    {
        private TextDocument TextDocument { get; set; }

        [SetUp]
        public void Setup()
        {
            TextDocument = new TextDocument("Hello World!");
        }

        [Test(Description = "IdentityCommand should return with passed value.")]
        public void IdentityCommandShouldReturnWithPassedValue()
        {
            // Given
            var parameter = Arguments.Value("Hello World!");
            var underTest = new IdentityCommand();

            // When
            var result = underTest.Execute(parameter);

            // Then
            Assert.That(result, Is.EqualTo(parameter));
        }

        [Test(Description = "StringCommand should return string of value")]
        public void StringCommandShouldReturnStringOfValue()
        {
            // Given
            var underTest = new ToStringCommand();

            // When
            var result = underTest.Execute(Arguments.Value(45 + 5));

            // Then
            Assert.That(result, Is.EqualTo(Arguments.Value("50")));
        }


        [Test(Description = "WriteToConsoleCommand should print value to console output")]
        public void WriteToConsoleCommandShouldPrintValueToConsoleOutput()
        {
            // Given
            var underTest = new WriteToConsoleCommand();

            // When
            var value = Arguments.Value("Hello World!");
            var result = underTest.Execute(value);

            // Then
            Assert.That(value, Is.EqualTo(result));
        }

        [Test(Description = "ToLowerCaseCommand should convert text of TextRange to lower case.")]
        [TestCase("Hello World!", "hello world!")]
        public void ToLowerCaseCommandShouldConvertTextOfTextNodeToLowerCase(string parameter, string expected)
        {
            // Given
            var underTest = new ToLowerCaseCommand();
            var textDocument = new TextDocument(parameter);

            // When
            var value = Arguments.Value(textDocument.TextRange);
            var result = underTest.Execute(value) as ValueArgument<TextRange>;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.Text, Is.EqualTo(expected));
        }

        [Test(Description = "ToUpperCaseCommand should convert text of TextRange to upper case")]
        [TestCase("Hello World!", "HELLO WORLD!")]
        public void ToUpperCaseCommandShouldConvertTextOfTextNodeToUpperCase(string parameter, string expected)
        {
            // Given
            var underTest = new ToUpperCaseCommand();
            var textDocument = new TextDocument(parameter);

            // When
            var value = Arguments.Value(textDocument.TextRange);
            var result = underTest.Execute(value) as ValueArgument<TextRange>;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.Text, Is.EqualTo(expected));
        }

        [Test(Description = "FillStringCommand should fill with given character in string")]
        [TestCase("Hello World!", "************")]
        public void FillStringCommandShouldFillWithGivenCharacterInString(string parameter, string expected)
        {
            // Given
            var underTest = new FillStringCommand('*');
            var textDocument = new TextDocument(parameter);

            // When
            var value = Arguments.Value(textDocument.TextRange);
            var result = underTest.Execute(value) as ValueArgument<TextRange>;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.Text, Is.EqualTo(expected));
        }

        [Test(Description = "FillStringCommand should fill with given character in string")]
        [TestCase("Hello World!", "hihihihihihi")]
        public void FillStringCommandShouldFillWithGivenTextPatternInString(string parameter, string expected)
        {
            // Given
            var underTest = new FillStringCommand("hi");
            var textDocument = new TextDocument(parameter);

            // When
            var value = Arguments.Value(textDocument.TextRange);
            var result = underTest.Execute(value) as ValueArgument<TextRange>;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.Text, Is.EqualTo(expected));
        }

        [Test(Description = "ReplaceTextCommand should replace text of range.")]
        [TestCase("Hello World!", "hello")]
        public void ReplaceTextCommandShouldReplaceTextOfNode(string parameter, string expected)
        {
            // Given
            var underTest = new ReplaceTextCommand(expected);
            var textDocument = new TextDocument(parameter);

            // When
            var value = Arguments.Value(textDocument.TextRange);
            var result = underTest.Execute(value) as ValueArgument<TextRange>;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.Text, Is.EqualTo(expected));
        }

        [Test]
        public void FindAndReplaceCommandsShouldBeAbleToCombine()
        {
            // Given
            var findCommand = new FindTextCommand("ipsum");
            var replaceCommand = new ReplaceTextCommand("IPSUUUM");
            var underTest = new TextDocument(TestHelper.LoremIpsum.TakeWords(3)).TextRange;

            // When
            var result = replaceCommand.Execute(
                findCommand.Execute(Arguments.Value(underTest))
            ) as MultiValueArgument<TextRange>;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Values, Is.Not.Empty);
            Assert.That(underTest.Text, Is.EqualTo("Lorem IPSUUUM dolor"));
        }

        [Test]
        public void FindAndReplaceCommandsShouldBeAbleToCombineAndHandleMultipleValues()
        {
            // Given
            var findCommand = new FindTextCommand("o");
            var replaceCommand = new ReplaceTextCommand("_o_");
            var underTest = new TextDocument(TestHelper.LoremIpsum.TakeWords(3)).TextRange;

            // When
            var result = replaceCommand.Execute(
                findCommand.Execute(Arguments.Values(new[] { underTest }))
            ).ToArray();

            // Then
            Assert.That(result, Is.Not.Null.And.Not.Empty);
            Assert.That(underTest.Text, Is.EqualTo("L_o_rem ipsum d_o_l_o_r"));
        }

        [Test]
        public void ComposeCommandShouldBeAbleToExecuteCommandsSequentially()
        {
            // Given
            var textDocument = new TextDocument("Hello World!");
            var mockCommand = new Mock<ICommand>();
            mockCommand
                .Setup(command => command.Execute(It.IsAny<ValueArgument<Int32>>()))
                .Returns((ValueArgument<Int32> r) => Arguments.Value(r.Value + 1));

            var underTest = new ComposeCommand(
                mockCommand.Object,
                mockCommand.Object,
                mockCommand.Object
            );

            // When
            var argument = Arguments.Value(0);
            var result = underTest.Execute(argument) as ValueArgument<Int32>;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(3));
            mockCommand.Verify(command => command.Execute(It.IsAny<Argument>()), Times.Exactly(3));
        }

        [Test]
        public void FindTextCommandShouldReturnFoundSubStringsOfText()
        {
            // Given
            var textDocument = new TextDocument("This string contains 'abc' twice. This was the first, and the second is here: abc.");
            var underTest = new FindTextCommand("abc");

            // When
            var value = Arguments.Value(textDocument.TextRange);
            var result = underTest.Execute(value) as MultiValueArgument<TextRange>;

            // Then
            Assert.That(result.Values, Is.Not.Null.And.Not.Empty);
            Assert.That(result.Values, Has.Count.Or.Length.EqualTo(2));
        }

        [Test]
        public void FindTextCommandShouldHandleMultipleValues()
        {
            // Given
            var firstFindCommand = new FindTextCommand("abcdef");
            var secondFindCommand = new FindTextCommand("cde");
            var underTest = new TextDocument("abcdef abcdef abcdef").TextRange;

            // When
            var result = secondFindCommand.Execute(
                firstFindCommand.Execute(Arguments.Value(underTest))
            ) as MultiValueArgument<TextRange>;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Values, Is.Not.Empty);
            Assert.That(result.Values.Select(value => value.Text), Is.All.EqualTo("cde"));
        }


        [Test]
        public void CommandsShouldBeAbleToHandleMultipleArgumentsToo()
        {
            // Given
            ISupportMultipleArgumentsCommand underTest = new IdentityCommand();

            // When
            var result = underTest.Execute(new[] { "a", "b", "c" }
                                  .Select(Arguments.Value)).ToArray();

            // Then
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.EquivalentTo(new[]
            {
                Arguments.Value("a"),
                Arguments.Value("b"),
                Arguments.Value("c")
            }));
            Assert.That(result, Has.Length.EqualTo(3));
        }

        [Test]
        public void RegularExpressionCommandShouldBeAbleToFindTextWithRegularExpressions()
        {
            // Given
            var argument = Arguments.Value(new TextDocument(TestHelper.LoremIpsum.TakeWords(4)).TextRange);
            var underTest = new RegularExpressionCommand(@"\w+");

            // When
            var result = underTest.Execute(argument);

            // Then
            Assert.That(result, Is.Not.Null.And.InstanceOf<MultiValueArgument<TextRange>>());

            var expectedValues = (result as MultiValueArgument<TextRange>).Values;
            Assert.That(expectedValues, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void RegularExpressionCommandShouldHandleMultipleValueArguments()
        {
            // Given
            var argument = Arguments.Value(new TextDocument(TestHelper.LoremIpsum.TakeWords(4)).TextRange);
            var selectWords = new RegularExpressionCommand(@"\w+");
            var selectLowerCaseWords = new RegularExpressionCommand(@"[a-z]+");

            // When
            var result = selectLowerCaseWords.Execute(
                selectWords.Execute(argument)
            );

            // Then
            throw new NotImplementedException();
        }

        [Test]
        public void SplitStringCommandShouldSplitAnyStringToMultipleValues()
        {
            // Given
            var argument = Arguments.Value(new TextDocument("a b c d").TextRange);
            var underTest = new SplitStringCommand(" ");

            // When
            var result = underTest.Execute(argument) as MultiValueArgument<TextRange>;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Values, Is.Not.Empty);
            Assert.That(result.Values.Select(range => range.Text), Is.EquivalentTo(new[] { "a", "b", "c", "d" }));
        }

        [Test]
        public void SplitStringCommandShouldHandleMultipleValueArguments()
        {
            // Given
            var argument = Arguments.Value(new TextDocument("a_A b_B c_C").TextRange);
            var first = new SplitStringCommand(" ");
            var second = new SplitStringCommand("_");

            // When
            var subResult = second.Execute(first.Execute(argument));
            var result = subResult as MultiValueArgument<TextRange>;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Values, Is.Not.Empty);
            Assert.That(result.Values.Select(range => range.Text), Is.EquivalentTo(new[] { "a", "A", "b", "B", "c", "C" }));
            throw new NotImplementedException();
        }

    }
}
