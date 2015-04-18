using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.ControlFlows;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Interpreter.Common;
using CodeSharper.Interpreter.Compiler;
using CodeSharper.Tests.Interpreter.Stubs;
using NUnit.Framework;

namespace CodeSharper.Tests.Interpreter.Visitors
{
    [TestFixture]
    internal class CodeQueryVisitorIntegratedTests : TestFixtureBase
    {
        #region Stubs for initializing

        #endregion

        #region Initializing test fixture

        /// <summary>
        /// Gets or sets the compiler.
        /// </summary>
        public CodeQueryCompiler Compiler { get; set; }

        /// <summary>
        /// Gets or sets the control flow factory.
        /// </summary>
        public IControlFlowFactory<ControlFlowBase> ControlFlowFactory { get; set; }

        public override void Setup()
        {
            base.Setup();

            // initialize compiler
            Compiler = new CodeQueryCompiler();

            var commandDescriptorManager = new DefaultCommandDescriptorManager();
            commandDescriptorManager.Register(new CommandDescriptor {
                CommandNames = new[] { "IdentityRunnable" },
                Arguments = Enumerable.Empty<ArgumentDescriptor>(),
                Name = "IdentityRunnable"
            });
            var runnableFactory = new DefaultRunnableFactory(new[] { typeof(IdentityRunnable) });
            var commandCallResolver = new DefaultCommandCallResolver(commandDescriptorManager, runnableFactory);
            var runnableManager = new DefaultRunnableManager();
            var executor = new StandardExecutor(runnableManager);
            // initialize control flow factory
            ControlFlowFactory = new DefaultControlFlowFactory(commandCallResolver, executor);
        }

        #endregion

        #region Unit tests

        [Test(Description = "DefaultControlFlowFactory should create and run control flow when every default dependency is initialized")]
        public void DefaultControlFlowFactory_ShouldCreateAndRunControlFlow_WhenEveryDefaultDependencyIsInitialized()
        {
            // Given
            var input = "@IdentityRunnable";

            // When
            var commandCall = Compiler.Parse(input);
            var controlFlow = ControlFlowFactory.Create(commandCall);
            var result = controlFlow.Execute(10);

            // Then
            Assert.That(result, Is.EqualTo(10));
        }

        #endregion
    }
}
