using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.ControlFlows;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.Services;
using CodeSharper.Interpreter.Common;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace CodeSharper.Tests.Interpreter.Common
{
    [TestFixture]
    internal class DefaultControlFlowFactoryTests : TestFixtureBase
    {
        #region Stubs for initializing

        protected class DummyExecutor : IExecutor
        {
            /// <summary>
            /// Executes the specified runnable with given parameter.
            /// </summary>
            public Object Execute(IRunnable runnable, Object parameter)
            {
                return parameter;
            }
        }

        protected class DummyCommandCallResolver : ICommandCallResolver
        {
            private readonly Fixture _fixture;

            /// <summary>
            /// Initializes a new instance of the <see cref="DummyCommandCallResolver"/> class.
            /// </summary>
            public DummyCommandCallResolver()
            {
                _fixture = new Fixture();
            }

            /// <summary>
            /// Creates the command.
            /// </summary>
            public Command CreateCommand(CommandCallDescriptor descriptor)
            {
                return new Command(new DummyRunnable(), _fixture.Create<CommandDescriptor>());
            }
        }

        protected class DummyRunnable : IRunnable
        {
            /// <summary>
            /// Runs an algorithm with the specified parameter.
            /// </summary>
            public Object Run(Object parameter)
            {
                return parameter;
            }
        }


        #endregion

        #region Initializing test fixture

        protected DummyExecutor Executor { get; set; }

        protected DefaultControlFlowFactory UnderTest { get; set; }

        protected Fixture Fixture { get; set; }

        protected ICommandCallResolver CommandCallResolver { get; set; }

        public ISelectorResolver SelectorResolver { get; set; }

        public ISelectorFactory SelectorFactory { get; set; }

        public override void Setup()
        {
            base.Setup();
            Fixture = new Fixture();
            Executor = new DummyExecutor();
            CommandCallResolver = new DummyCommandCallResolver();
            SelectorFactory = new DefaultSelectorFactory(new[]{ typeof(UniversalSelector) }, Type.EmptyTypes);
            SelectorResolver = new DefaultSelectorResolver(SelectorFactory, new FileDescriptorRepository(@"D:\Development\Projects\C#\CodeSharper\master-refactoring\CodeSharper\tests\Configurations\descriptors.json"));
            UnderTest = new DefaultControlFlowFactory(CommandCallResolver, SelectorResolver, Executor);
        }

        #endregion

        #region Unit tests

        [Test(Description = "Create should return a CommandCallControlFlow instance when a CommandCallControlFlowElement instance is passed")]
        public void Create_ShouldReturnCommandCallControlFlow_WhenCommandCallDescriptorIsPassed()
        {
            // Given in setup
            var sequence = Fixture.Create<CommandCallControlFlowElement>();

            // When
            var result = UnderTest.Create(sequence) as CommandCallControlFlow;

            // Then
            Assert.That(result, Is.Not.Null);
        }

        [Test(Description = "Create should return PipelineControlFlow instance when a PipelineControlFlowElement instance is passed")]
        public void Create_ShouldReturnPipelineOfDummyControlFlowDescriptors_WhenSequenceDescriptorIsPassed()
        {
            // Given in setup
            var sequence = new PipelineControlFlowElement(Fixture.CreateMany<CommandCallControlFlowElement>(3));

            // When
            var result = UnderTest.Create(sequence) as PipelineControlFlow;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Children, Is.Not.Null);

            var children = result.Children.ToArray();
            Assert.That(children.Count(), Is.EqualTo(3));
            Assert.That(children.All(child => child is CommandCallControlFlow), Is.True);
        }

        [Test(Description = "Create should return a sequence of dummy ControlFlowDescriptors when a SequenceDescriptor instance is passed")]
        public void Create_ShouldReturnSequenceOfDummyControlFlowDescriptors_WhenSequenceDescriptorIsPassed()
        {
            // Given in setup
            var sequence = new SequenceControlFlowElement(Fixture.CreateMany<CommandCallControlFlowElement>(3));

            // When
            var result = UnderTest.Create(sequence) as SequenceControlFlow;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Children, Is.Not.Null);

            var children = result.Children.ToArray();
            Assert.That(children.Count(), Is.EqualTo(3));
            Assert.That(children.All(child => child is CommandCallControlFlow), Is.True);
        }

        #endregion
    }
}
