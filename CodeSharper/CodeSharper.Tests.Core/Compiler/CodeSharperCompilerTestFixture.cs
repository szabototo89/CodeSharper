using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Commands;
using CodeSharper.Languages.Compilers;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Compiler
{
    [TestFixture]
    internal class CodeSharperCompilerTestFixture
    {
        [SetUp]
        public void Setup()
        {
            // TODO: (optional) not implemented
        }

        [TearDown]
        public void Teardown()
        {
            // TODO: (optional) not implemented
        }

        [Test]
        public void CodeSharperCompilerShouldParseACommandWithoutArgument()
        {
            // Given
            var underTest = new CodeSharperCompiler();
            var command = "test";

            // When
            var result = underTest
                .RunVisitor<CodeSharperGrammarVisitor, ICommandCallTree>(command + Environment.NewLine) as SingleCommandCallTree;

            // Then
            Assert.That(result, Is.Not.Null);

            var descriptor = result.CommandCallDescriptor;
            Assert.That(descriptor.Name, Is.EqualTo("test"));
            Assert.That(descriptor.Arguments.Any(), Is.False);
            Assert.That(descriptor.NamedArguments.Any(), Is.False);
        }

        [Test]
        public void CodeSharperCompilerShouldParseCommandCallWithNamedArgument()
        {
            // Given
            var underTest = new CodeSharperCompiler();
            var command = "test value=15";

            // When
            var result = underTest
                .RunVisitor<CodeSharperGrammarVisitor, ICommandCallTree>(command + Environment.NewLine) as SingleCommandCallTree;

            // Then
            Assert.That(result, Is.Not.Null);

            var descriptor = result.CommandCallDescriptor;
            Assert.That(descriptor.Name, Is.EqualTo("test"));
            Assert.That(descriptor.Arguments.Any(), Is.False);

            var argument = descriptor.NamedArguments.SingleOrDefault();
            Assert.That(argument.Key, Is.EqualTo("value"));
            Assert.That(argument.Value, Is.EqualTo(15));
        }

        [Test]
        public void CodeSharperCompilerShouldAbleToParseMultipleArguments()
        {
            // Given
            var underTest = new CodeSharperCompiler();
            var command = "test 1 2 'a' value=false";

            // When
            var result = (underTest
                .RunVisitor<CodeSharperGrammarVisitor, ICommandCallTree>(command) as SingleCommandCallTree);

            // Then
            Assert.That(result, Is.Not.Null);

            var descriptor = result.CommandCallDescriptor;
            Assert.That(descriptor.Name, Is.EqualTo("test"));
            Assert.That(descriptor.Arguments, Is.EquivalentTo(new Object[] { 1, 2, "a" }));

            var argument = descriptor.NamedArguments.SingleOrDefault();
            Assert.That(argument.Key, Is.EqualTo("value"));
            Assert.That(argument.Value, Is.EqualTo(false));
        }

        [Test]
        public void CodeSharperCompilerShouldAbleToParseMultipleCommands()
        {
            // Given
            var underTest = new CodeSharperCompiler();
            var command = "test-1 1 | test-2 2";

            // When
            var result = underTest
                .RunVisitor<CodeSharperGrammarVisitor, ICommandCallTree>(command)
                .Children.OfType<SingleCommandCallTree>()
                .Select(x => x.CommandCallDescriptor);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Select(call => call.Name), Is.EquivalentTo(new[] { "test-1", "test-2" }));

            var arguments = result.SelectMany(call => call.Arguments);
            Assert.That(arguments, Is.EquivalentTo(new[] { 1, 2 }));
        }

        [Test]
        public void CodeSharperCompilerShouldAbleToParseAndOperatorCommands()
        {
            // Given
            var underTest = new CodeSharperCompiler();
            var command = "test-1 1 && test-2 2";

            // When
            var result = underTest
                .RunVisitor<CodeSharperGrammarVisitor, ICommandCallTree>(command);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<LazyAndCommandCallTree>());

            var descriptors = result.Children.OfType<SingleCommandCallTree>().Select(x => x.CommandCallDescriptor);
            Assert.That(descriptors, Is.Not.Null);
            Assert.That(descriptors.Select(call => call.Name), Is.EquivalentTo(new[] { "test-1", "test-2" }));

            var arguments = descriptors.SelectMany(call => call.Arguments);
            Assert.That(arguments, Is.EquivalentTo(new[] { 1, 2 }));
        }

        [Test]
        public void CodeSharperCompilerShouldAbleToParseComplicatedCommandCall()
        {
            // Given
            var underTest = new CodeSharperCompiler();
            var command = "test-1 :line(120) | {\r\n    test-2 && test-3\r\n}";

            // When
            var result = underTest
                .RunVisitor<CodeSharperGrammarVisitor, ICommandCallTree>(command);

            // Then
            Assert.That(result, Is.Not.Null);
        }
    }
}