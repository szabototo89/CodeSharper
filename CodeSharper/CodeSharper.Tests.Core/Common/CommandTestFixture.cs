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

            // When
            var value = Arguments.Value(new TextRange(parameter, TextDocument));
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

            // When
            var value = Arguments.Value(new TextRange(parameter, TextDocument));
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

            // When
            var value = Arguments.Value(new TextRange(parameter, TextDocument));
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

            // When
            var value = Arguments.Value(new TextRange(parameter, TextDocument));
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

            // When
            var value = Arguments.Value(new TextRange(parameter, TextDocument));
            var result = underTest.Execute(value) as ValueArgument<TextRange>;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.Text, Is.EqualTo(expected));
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
            var result = underTest.Execute(value) as ValueArgument<IEnumerable<TextRange>>;

            // Then
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value, Has.Count.EqualTo(2));
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
    }
}
