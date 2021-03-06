﻿using System;
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
using CodeSharper.Interpreter.Compiler;
using CodeSharper.Tests.Interpreter.Stubs;
using NUnit.Framework;

namespace CodeSharper.Tests.Interpreter.Visitors
{
    [TestFixture]
    internal class CodeQueryVisitorIntegratedTests : TestFixtureBase
    {
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

            var commands = new[]
            {
                new CommandDescriptor("IdentityRunnable", String.Empty, Enumerable.Empty<ArgumentDescriptor>(), new[] {"IdentityRunnable"}),
                new CommandDescriptor("IncrementRunnable", String.Empty, new[]
                {
                    new ArgumentDescriptor
                    {
                        ArgumentType = typeof (Double),
                        ArgumentName = "value",
                        DefaultValue = 0,
                        IsOptional = false,
                        Position = 0
                    }
                }, new[] {"IncrementRunnable", "increment", "inc"})
            };

            var runnableFactory = new DefaultRunnableFactory(new[] {typeof (IdentityRunnable), typeof (IncrementRunnable)});

            var repository = new MemoryDescriptorRepository(commandDescriptors: commands);
            var commandCallResolver = new DefaultCommandCallResolver(repository, runnableFactory);
            var selectorManager = new DefaultSelectorFactory();
            var nodeSelectorResolver = new DefaultSelectorResolver(selectorManager, repository);
            var runnableManager = new DefaultRunnableManager();
            var executor = new StandardExecutor(runnableManager);

            // initialize compiler and control flow factory
            ControlFlowFactory = new DefaultControlFlowFactory(commandCallResolver, nodeSelectorResolver, executor);
            Compiler = new CodeQueryCompiler();
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

        [Test(Description = "IncrementRunnable should increment its value by one when proper value is passed")]
        public void IncrementRunnable_ShouldIncrementValueByOne_WhenProperValueIsPassed()
        {
            // Given in setup
            var input = "@inc 1";

            // When
            var commandCall = Compiler.Parse(input);
            var controlFlow = ControlFlowFactory.Create(commandCall);
            var result = controlFlow.Execute(10.0);

            // Then
            Assert.That(result, Is.EqualTo(11));
        }

        [TestCase("increment 2")]
        [TestCase("increment 1; increment 2")]
        [TestCase("increment 1 | inc 1")]
        [TestCase("increment 2 | IdentityRunnable")]
        [Test(Description = "IncrementRunnable should increment its value by one when proper value is passed")]
        public void IncrementRunnable_ShouldIncrementValueByTwo_WhenProperValueIsPassed(String input)
        {
            // Given in setup

            // When
            var commandCall = Compiler.Parse(input);
            var controlFlow = ControlFlowFactory.Create(commandCall);
            var result = controlFlow.Execute(10.0);

            // Then
            Assert.That(result, Is.EqualTo(12));
        }

        [Test(Description = "IncrementRunnable should increment its value by one when proper value is passed")]
        public void IncrementRunnable_ShouldIncrementValueByThree_WhenProperValueIsPassed()
        {
            // Given in setup
            var input = "@IncrementRunnable 1 | @IncrementRunnable 2";

            // When
            var commandCall = Compiler.Parse(input);
            var controlFlow = ControlFlowFactory.Create(commandCall);
            var result = controlFlow.Execute(10.0);

            // Then
            Assert.That(result, Is.EqualTo(13));
        }

        [Test(Description = "IncrementRunnable with MultiValueProducer should increment its value by one when proper value is passed")]
        public void IncrementRunnableWithMultiValueProducer_ShouldIncrementValueByThree_WhenProperValueIsPassed()
        {
            // Given in setup
            var input = "@IncrementRunnable 1 | @IncrementRunnable 2";

            // When
            var commandCall = Compiler.Parse(input);
            var controlFlow = ControlFlowFactory.Create(commandCall);
            var result = controlFlow.Execute(new[] {10.0, 1.0});

            // Then
            Assert.That(result, Is.EqualTo(new[] {13.0, 4.0}));
        }

        #endregion
    }
}