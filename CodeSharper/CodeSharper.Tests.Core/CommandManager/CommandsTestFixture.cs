using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Commands.CommandFactories;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables.StringTransformation;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Utilities;
using CodeSharper.Tests.Core.TestHelpers;
using Moq;
using Ninject;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.CommandManager
{
    [TestFixture]
    internal class CommandsTestFixture : TestFixtureBase
    {
        private StandardKernel Kernel { get; set; }

        [SetUp]
        public override void Setup()
        {
            Kernel = new StandardKernel();
            Kernel.Bind<ArgumentDescriptorBuilder>().ToSelf();
        }

        [TearDown]
        public void Teardown()
        {
            Kernel.Dispose();
        }

        [Test]
        public void CommandShouldHaveCommandDescriptor()
        {
            // Given
            var descriptor = new CommandDescriptor {
                Name = "mock-command",
                Arguments = Kernel.Get<ArgumentDescriptorBuilder>()
                    .Argument<String>("first", true, "Hello World!")
                    .Argument<Int32>("second", false, 0)
                    .Create()
            };

            var mock = new Mock<ICommand>();
            mock.SetupGet(cmd => cmd.Descriptor)
                .Returns(() => descriptor);

            var underTest = mock.Object;

            // When
            var result = underTest.Descriptor;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("mock-command"));
            Assert.That(result.Arguments.OfType<ArgumentDescriptor>().Select(arg => arg.ArgumentName), Is.EquivalentTo(new[] { "first", "second" }));
            Assert.That(result.Arguments.Select(arg => arg.ArgumentType), Is.EquivalentTo(new[] { typeof(String), typeof(Int32) }));
        }

        [Test]
        public void InsertTextRangeCommandShouldBeAbleToInitialize()
        {
            // Given
            var descriptor =
                JsonCommandDescriptorParser.ParseFrom(
                    "{\"name\":\"Insert TextRange Command\",\"command-names\":[\"insert\",\"insert-text-range\"],\"arguments\":[{\"name\":\"startIndex\",\"type\":\"System.Int32\",\"optional\":false,\"default-value\":null},{\"name\":\"value\",\"type\":\"System.String\",\"optional\":false,\"default-value\":\"\"}]}");

            var arguments = new CommandArgumentCollection()
                .SetArgument("startIndex", 10)
                .SetArgument("value", "Hello World!");

            var underTest = new InsertTextRangeCommandFactory() { Descriptor = descriptor };

            // When

            var result = underTest.CreateCommand(arguments);

            // Then
            Assert.That(result, Is.Not.Null);
        }

    }
}