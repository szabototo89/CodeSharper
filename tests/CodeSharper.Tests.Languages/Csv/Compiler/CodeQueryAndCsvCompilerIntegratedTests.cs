﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.ControlFlows;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.Services;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;
using CodeSharper.Interpreter.Common;
using CodeSharper.Interpreter.Compiler;
using CodeSharper.Languages.Csv.Compiler;
using CodeSharper.Languages.Csv.Nodes.Selectors;
using NUnit.Framework;

namespace CodeSharper.Tests.Languages.Csv.Compiler
{
    [TestFixture]
    internal class CodeQueryAndCsvCompilerIntegratedTests : TestFixtureBase
    {
        #region Stubs for testing

        [Consumes(typeof (MultiValueConsumer<Node>))]
        public class GetTextRunnable : RunnableBase<Node, TextRange>
        {
            /// <summary>
            /// Runs an algorithm with the specified parameter.
            /// </summary>
            public override TextRange Run(Node parameter)
            {
                if (parameter == null)
                    return null;

                return parameter.TextRange;
            }
        }

        [Consumes(typeof (MultiValueConsumer<TextRange>))]
        public class ToUpperCaseRunnable : RunnableBase<TextRange, TextRange>
        {
            /// <summary>
            /// Runs an algorithm with the specified parameter.
            /// </summary>
            public override TextRange Run(TextRange parameter)
            {
                if (parameter == null)
                    return null;
                return parameter.ChangeText(parameter.GetText().ToUpper());
            }
        }

        [Consumes(typeof (MultiValueConsumer<TextRange>))]
        public class ToStringRunnable : RunnableBase<TextRange, String>
        {
            /// <summary>
            /// Runs an algorithm with the specified parameter.
            /// </summary>
            public override String Run(TextRange parameter)
            {
                return parameter.GetText();
            }
        }

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

            var commands = new[]
            {
                new CommandDescriptor
                {
                    CommandNames = new[] {"to-upper-case"},
                    Arguments = Enumerable.Empty<ArgumentDescriptor>(),
                    Name = "ToUpperCaseRunnable"
                },
                new CommandDescriptor
                {
                    CommandNames = new[] {"get-text"},
                    Arguments = Enumerable.Empty<ArgumentDescriptor>(),
                    Name = "GetTextRunnable"
                },
                new CommandDescriptor
                {
                    CommandNames = new[] {"to-string"},
                    Arguments = Enumerable.Empty<ArgumentDescriptor>(),
                    Name = "ToStringRunnable"
                }
            };

            var runnableFactory = new DefaultRunnableFactory(new[] {typeof (ToUpperCaseRunnable), typeof (GetTextRunnable), typeof (ToStringRunnable)});

            var selectors = new[]
            {
                new SelectorDescriptor("UniversalSelector", "UniversalSelector", typeof (UniversalSelector)),
                new SelectorDescriptor("FieldNodeSelector", "FieldNodeSelector", typeof (FieldSelector)),
            };

            var combinators = new[]
            {
                new CombinatorDescriptor("RelativeNodeCombinator", "", typeof (RelativeNodeCombinator)),
            };

            var descriptorRepository = new MemoryDescriptorRepository(selectors, combinators, commandDescriptors: commands);

            var commandCallResolver = new DefaultCommandCallResolver(descriptorRepository, runnableFactory);
            var selectorManager = new DefaultSelectorFactory();
            var nodeSelectorResolver = new DefaultSelectorResolver(selectorManager, descriptorRepository);
            var runnableManager = new DefaultRunnableManager();
            var executor = new StandardExecutor(runnableManager);

            // initialize compiler and control flow factory
            ControlFlowFactory = new DefaultControlFlowFactory(commandCallResolver, nodeSelectorResolver, executor);
            Compiler = new CodeQueryCompiler();
        }

        #endregion

        #region Tests

        [Test(Description = "Compilation should compile and execute CSV input when proper query is passed")]
        public void Compilation_ShouldCompileAndExecuteCsvInput_WhenProperQueryIsPassed()
        {
            // Given
            var csvInput = "first,second,third,fourth";
            var csvCompiler = new CsvCompiler();

            var query = "UniversalSelector FieldNodeSelector | @get-text | @to-upper-case | @to-string";

            // When
            var csvSyntaxTree = csvCompiler.Parse(csvInput);
            var controlFlowDescriptor = Compiler.Parse(query);
            var controlFlow = ControlFlowFactory.Create(controlFlowDescriptor);
            var result = controlFlow.Execute(csvSyntaxTree);

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EquivalentTo(new[] {"FIRST", "SECOND", "THIRD", "FOURTH"}));
        }

        #endregion
    }
}