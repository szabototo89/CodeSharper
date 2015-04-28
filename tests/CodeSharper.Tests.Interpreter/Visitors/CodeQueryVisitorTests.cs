using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Utilities;
using CodeSharper.Interpreter.Common;
using CodeSharper.Interpreter.Compiler;
using CodeSharper.Interpreter.Visitors;
using NUnit.Framework;

namespace CodeSharper.Tests.Interpreter.Visitors
{
    [TestFixture]
    internal class CodeQueryVisitorTests
    {
        #region Integrated tests

        [Test(Description = "Parse should parse command when simple command call is passed")]
        public void Parse_ShouldParseCommand_WhenSimpleCommandCallIsPassed()
        {
            // Given
            var underTest = new CodeQueryCompiler();

            // When
            var result = underTest.Parse("@test-command");

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<CommandCallControlFlowDescriptor>());
        }

        [Test(Description = "Parse should parse command when simple command call with one string parameter is passed")]
        public void Parse_ShouldParseCommand_WhenSimpleCommandCallWithOneStringParameterIsPassed()
        {
            // Given
            var underTest = new CodeQueryCompiler();

            // When
            var result = underTest.Parse("@test-command \"hello world!\"") as CommandCallControlFlowDescriptor;

            // Then
            Assert.That(result, Is.Not.Null);

            var actualParameters = result.CommandCall.ActualParameters.ToArray();
            Assert.That(actualParameters.Length, Is.EqualTo(1));
            Assert.That(actualParameters.Select(parameter => new {
                Position = parameter.Position,
                Value = parameter.Value.Value as String
            }), Is.EquivalentTo(new[] { new { Position = Option.Some(0), Value = "hello world!" } }));
        }

        [Test(Description = "Parse should return sequence control flow when multiple commands are passed")]
        public void Parse_ShouldReturnSequenceControlFlow_WhenMultipleCommandsArePassed()
        {
            // Given
            var underTest = new CodeQueryCompiler();

            // When
            var result = underTest.Parse("@test-command; @test-command-2") as SequenceControlFlowDescriptor;

            // Then
            Assert.That(result, Is.Not.Null);
            var expectedChildren = result.Children
                                         .Cast<CommandCallControlFlowDescriptor>()
                                         .Select(call => call.CommandCall.MethodName);

            Assert.That(expectedChildren, Is.EquivalentTo(new[] { "test-command", "test-command-2" }));
        }

        [TestCase("column", Description = "Unary selection")]
        [TestCase("column[name=\"First Name\"]", Description = "Unary selection with attribute")]
        [TestCase("column > field", Description = "Binary selection")]
        [Test(Description = "Parse should handle input when selectors are defined")]
        public void Parse_ShouldParseHandle_WhenSelectorsAreDefined(String command)
        {
            // Given
            var underTest = new CodeQueryCompiler();

            // When
            var result = underTest.Parse(command);

            // Then
            Assert.That(result, Is.Not.Null);
        }

        #endregion
    }
}
