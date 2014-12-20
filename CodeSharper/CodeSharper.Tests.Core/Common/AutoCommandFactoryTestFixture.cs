using System;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Commands.CommandFactories;
using CodeSharper.Tests.Core.Mocks;
using CodeSharper.Tests.Core.TestHelpers;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Common
{
    [TestFixture]
    internal class AutoCommandFactoryTestFixture : TestFixtureBase
    {
        [Test(Description = "AutoCommandFactory should be able to bind arguments automatically")]
        public void AutoCommandFactoryShouldBeAbleToBindArgumentsAutomatically()
        {
            // Given
            var underTest = new AutoCommandFactory<RunnableMocks.TestRunnable>() {
                Descriptor = new CommandDescriptor() {
                    Name = "Repeat Command",
                    CommandNames = new[] { "repeat" },
                    Arguments = new[] { 
                        new ArgumentDescriptor {
                            ArgumentName = "count",
                            ArgumentType = typeof(Int32),
                            DefaultValue = 0,
                            IsOptional = false
                        } 
                    }
                }
            };

            // When
            var result = underTest.CreateCommand(new CommandArgumentCollection().SetArgument("count", 3));

            // Then
            Assert.That(result.Runnable, Is.Not.Null);
            Assert.That(result.Runnable, Is.InstanceOf<RunnableMocks.TestRunnable>());
            Assert.That((result.Runnable as RunnableMocks.TestRunnable).Count, Is.EqualTo(3));
        }

    }
}