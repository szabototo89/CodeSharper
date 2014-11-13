using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common;
using CodeSharper.Languages.Compilers;
using CodeSharper.Languages.Compilers.CodeSharper;
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

        [Test(Description = "CodeSharperCompiler should parse command without argument")]
        public void CodeSharperCompilerShouldParseCommandWithoutArgument()
        {
            // Given
            var underTest = new CodeSharperCompiler();
            var command = "test";

            // When
            var result = underTest
                .RunVisitor<CodeSharperGrammarVisitor, ICommandCall>(command + Environment.NewLine) as SingleCommandCall;

            // Then
            Assert.That(result, Is.Not.Null);

            var descriptor = result.CommandCallDescriptor;
            Assert.That(descriptor.Name, Is.EqualTo("test"));
            Assert.That(descriptor.Arguments.Any(), Is.False);
            Assert.That(descriptor.NamedArguments.Any(), Is.False);
        }

        [Test(Description = "CodeSharperCompiler should parse command call with named argument")]
        public void CodeSharperCompilerShouldParseCommandCallWithNamedArgument()
        {
            // Given
            var underTest = new CodeSharperCompiler();
            var command = "test value=15";

            // When
            var result = underTest
                .RunVisitor<CodeSharperGrammarVisitor, ICommandCall>(command + Environment.NewLine) as SingleCommandCall;

            // Then
            Assert.That(result, Is.Not.Null);

            var descriptor = result.CommandCallDescriptor;
            Assert.That(descriptor.Name, Is.EqualTo("test"));
            Assert.That(descriptor.Arguments.Any(), Is.False);

            var argument = descriptor.NamedArguments.SingleOrDefault();
            Assert.That(argument.Key, Is.EqualTo("value"));
            Assert.That(argument.Value, Is.EqualTo(15));
        }

        [Test(Description = "CodeSharperCompiler should be able to parse multiple arguments")]
        public void CodeSharperCompilerShouldBeAbleToParseMultipleArguments()
        {
            // Given
            var underTest = new CodeSharperCompiler();
            var command = "test 1 2 'a' value=false";

            // When
            var result = (underTest
                .RunVisitor<CodeSharperGrammarVisitor, ICommandCall>(command) as SingleCommandCall);

            // Then
            Assert.That(result, Is.Not.Null);

            var descriptor = result.CommandCallDescriptor;
            Assert.That(descriptor.Name, Is.EqualTo("test"));
            Assert.That(descriptor.Arguments, Is.EquivalentTo(new Object[] { 1, 2, "a" }));

            var argument = descriptor.NamedArguments.SingleOrDefault();
            Assert.That(argument.Key, Is.EqualTo("value"));
            Assert.That(argument.Value, Is.EqualTo(false));
        }

        [Test(Description = "CodeSharperCompiler should be able to parse multiple commands")]
        public void CodeSharperCompilerShouldBeAbleToParseMultipleCommands()
        {
            // Given
            var underTest = new CodeSharperCompiler();
            var command = "test-1 1 | test-2 2";

            // When
            var result = underTest
                .RunVisitor<CodeSharperGrammarVisitor, ICommandCall>(command)
                .Children.OfType<SingleCommandCall>()
                .Select(x => x.CommandCallDescriptor);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Select(call => call.Name), Is.EquivalentTo(new[] { "test-1", "test-2" }));

            var arguments = result.SelectMany(call => call.Arguments);
            Assert.That(arguments, Is.EquivalentTo(new[] { 1, 2 }));
        }

        [Test(Description = "CodeSharperCompiler should be able to parse 'and' operator with commands")]
        public void CodeSharperCompilerShouldBeAbleToParseAndOperatorWithCommands()
        {
            // Given
            var underTest = new CodeSharperCompiler();
            var command = "test-1 1 && test-2 2";

            // When
            var result = underTest
                .RunVisitor<CodeSharperGrammarVisitor, ICommandCall>(command);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<LazyAndCommandCall>());

            var descriptors = result.Children.OfType<SingleCommandCall>().Select(x => x.CommandCallDescriptor);
            Assert.That(descriptors, Is.Not.Null);
            Assert.That(descriptors.Select(call => call.Name), Is.EquivalentTo(new[] { "test-1", "test-2" }));

            var arguments = descriptors.SelectMany(call => call.Arguments);
            Assert.That(arguments, Is.EquivalentTo(new[] { 1, 2 }));
        }

        [Test(Description = "CodeSharperCompiler should be able to parse complicated command call")]
        public void CodeSharperCompilerShouldBeAbleToParseComplicatedCommandCall()
        {
            // Given
            var underTest = new CodeSharperCompiler();
            var command = "test-1 :line(120) | {\r\n    test-2 && test-3\r\n}";

            // When
            var result = underTest
                .RunVisitor<CodeSharperGrammarVisitor, ICommandCall>(command);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<PipeLineCommandCall>());
            Assert.That(result.Children, Is.Not.Null.And.Not.Empty);

            var left = result.Children.First(); // test-1 :line(120)
            Assert.That(left, Is.InstanceOf<SingleCommandCall>());

            var right = result.Children.Last(); // (test-2 && test-3)
            Assert.That(right, Is.InstanceOf<LazyAndCommandCall>());
            Assert.That(right.Children, Is.All.InstanceOf<SingleCommandCall>());
        }
    }
}