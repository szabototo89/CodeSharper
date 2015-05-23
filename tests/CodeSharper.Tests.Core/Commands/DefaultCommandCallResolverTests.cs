using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.Services;
using Moq;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Commands
{
    [TestFixture]
    internal class DefaultCommandCallResolverTests : TestFixtureBase
    {
        #region Stubs for initializing

        internal class StubCommandDescriptorRepository : IDescriptorRepository
        {
            public IEnumerable<CombinatorDescriptor> GetCombinators()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<ModifierDescriptor> GetPseudoSelectors()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<SelectorDescriptor> GetSelectors()
            {
                throw new NotImplementedException();
            }

            /// <summary>
            /// Gets the command descriptors.
            /// </summary>
            public IEnumerable<CommandDescriptor> GetCommandDescriptors()
            {
                yield return new CommandDescriptor {
                    Name = "test-command",
                    CommandNames = new[] { "test-command" },
                    Arguments = new[] { 
                                    new ArgumentDescriptor {
                                        ArgumentName = "parameter",
                                        ArgumentType = typeof (Boolean),
                                        Position = 0
                                    }
                                }
                };
            }
        }

        internal class StubRunnableFactory : IRunnableFactory
        {
            /// <summary>
            /// Creates a runnable with the specified name and actual arguments
            /// </summary>
            public IRunnable Create(String runnableName, IEnumerable<KeyValuePair<String, Object>> actualArguments)
            {
                var runnable = new Mock<IRunnable>();
                runnable.Setup(r => r.Run(It.IsAny<Object>()))
                        .Returns<Object>(parameter => parameter);

                return runnable.Object;
            }
        }

        #endregion

        #region Initializing test fixture

        /// <summary>
        /// Gets or sets the under test.
        /// </summary>
        protected DefaultCommandCallResolver UnderTest { get; set; }

        /// <summary>
        /// Gets or sets the runnable factory.
        /// </summary>
        protected IRunnableFactory RunnableFactory { get; set; }

        /// <summary>
        /// Gets or sets the descriptor manager.
        /// </summary>
        protected IDescriptorRepository DescriptorRepository { get; set; }

        public override void Setup()
        {
            base.Setup();

            DescriptorRepository = new StubCommandDescriptorRepository();
            RunnableFactory = new StubRunnableFactory();
            UnderTest = new DefaultCommandCallResolver(DescriptorRepository, RunnableFactory);
        }

        #endregion

        #region Unit tests

        [Test(Description = "CreateCommand should create command when valid command call is passed")]
        public void CreateCommand_ShouldCreateCommand_WhenValidCommandCallIsPassed()
        {
            // Given in setup

            // When
            var result = UnderTest.CreateCommand(
                            new CommandCallDescriptor("test-command",
                            new[] { new PositionedCommandCallActualArgument(0, false) }));

            // Then
            Assert.That(result, Is.Not.Null);

            var expectedArguments = new Dictionary<String, Object> {
                {"parameter", false}
            };
            Assert.That(result.ActualArguments, Is.EquivalentTo(expectedArguments));

            Assert.That(result.CommandDescriptor, Is.EqualTo(DescriptorRepository.GetCommandDescriptors().First()));
        }

        [Test(Description = "CreateCommand should create command when valid command call is passed")]
        public void CreateCommand_ShouldCreateCommand_WhenCommandIsNotFound()
        {
            // Given in setup
            var arguments = new[] {
                new PositionedCommandCallActualArgument(0, false),
                new PositionedCommandCallActualArgument(1, "Hello World")
            };
            var commandCallDescriptor = new CommandCallDescriptor("test-command", arguments);

            // When
            var result = UnderTest.CreateCommand(commandCallDescriptor);

            // Then
            Assert.That(result, Is.Null);
        }

        #endregion
    }
}
