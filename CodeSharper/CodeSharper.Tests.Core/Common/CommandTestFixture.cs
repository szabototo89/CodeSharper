using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Commands;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    class CommandTestFixture
    {
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

        [Test(Description = "ToLowerCaseCommand should convert text of TextNode to lower case.")]
        [TestCase("Hello World!", "hello world!")]
        public void ToLowerCaseCommandShouldConvertTextOfTextNodeToLowerCase(string parameter, string expected)
        {
            // Given
            var underTest = new ToLowerCaseCommand();

            // When
            var value = Arguments.Value(new TextNode(parameter));
            var result = underTest.Execute(value) as ValueArgument<TextNode>;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.Text, Is.EqualTo(expected));
        } 

        [Test(Description = "ToUpperCaseCommand should convert text of TextNode to upper case")]
        [TestCase("Hello World!", "HELLO WORLD!")]
        public void ToUpperCaseCommandShouldConvertTextOfTextNodeToUpperCase(string parameter, string expected)
        {
            // Given
            var underTest = new ToUpperCaseCommand();

            // When
            var value = Arguments.Value(new TextNode(parameter));
            var result = underTest.Execute(value) as ValueArgument<TextNode>;

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
            var value = Arguments.Value(new TextNode(parameter));
            var result = underTest.Execute(value) as ValueArgument<TextNode>;

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
            var value = Arguments.Value(new TextNode( parameter));
            var result = underTest.Execute(value) as ValueArgument<TextNode>;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value.Text, Is.EqualTo(expected));
        } 

    }
}
