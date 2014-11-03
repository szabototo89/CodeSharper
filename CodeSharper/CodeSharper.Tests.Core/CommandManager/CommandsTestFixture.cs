using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables.StringTransformation;
using CodeSharper.Core.Common.Values;
using Moq;
using Ninject;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.CommandManager
{
    [TestFixture]
    internal class CommandsTestFixture
    {
        private StandardKernel Kernel { get; set; }

        [SetUp]
        public void Setup()
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
            var mock = new Mock<ICommand>();
            mock.SetupProperty(command => command.Descriptor,
                new CommandDescriptor
                {
                    Name = "mock-command",
                    Arguments = Kernel.Get<ArgumentDescriptorBuilder>()
                                      .Argument<String>("first", true, "Hello World!")
                                      .Argument<Int32>("second", false, 0)
                                      .Create()
                });


            var underTest = mock.Object;

            // When
            var result = underTest.Descriptor;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("mock-command"));
            Assert.That(result.Arguments.Select(arg => arg.ArgumentName), Is.EquivalentTo(new[] { "first", "second" }));
            Assert.That(result.Arguments.Select(arg => arg.ArgumentType), Is.EquivalentTo(new[] { typeof(String), typeof(Int32) }));
        }

        [Test]
        public void InsertTextRangeCommandShouldBeAbleToInitialize()
        {
            // Given
            var underTest = new InsertTextRangeCommand();

            // When
            underTest.PassArguments(
                new CommandArgumentCollection()
                  .SetArgument("startIndex", 10)
                  .SetArgument("value", "Hello World!")
            );

            var result = underTest.GetRunnable() as InsertTextRangeRunnable;

            // Then
            Assert.That(result, Is.Not.Null);
        }

    }
}