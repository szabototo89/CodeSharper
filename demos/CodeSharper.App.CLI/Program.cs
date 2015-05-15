using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.Services;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;
using CodeSharper.Interpreter.Common;
using CodeSharper.Interpreter.Compiler;
using CodeSharper.Languages.Csv.Compiler;
using CodeSharper.Languages.Csv.Nodes.Selectors;

namespace CodeSharper.Playground.CLI
{
    class Program
    {
        public static CodeQueryCompiler Compiler { get; protected set; }

        public static CsvCompiler CsvCompiler { get; protected set; }

        public static DefaultControlFlowFactory ControlFlowFactory { get; protected set; }

        [Consumes(typeof(MultiValueConsumer<Node>))]
        public class ConvertToTextRangeRunnable : RunnableBase<Node, TextRange>
        {
            /// <summary>
            /// Runs an algorithm with the specified parameter.
            /// </summary>
            public override TextRange Run(Node parameter)
            {
                if (parameter == null) return null;
                return parameter.TextRange;
            }
        }

        private static void initializeApplication()
        {
            var commandDescriptorManager = new DefaultCommandDescriptorManager();
            commandDescriptorManager.Register(new CommandDescriptor {
                CommandNames = new[] { "repeat" },
                Arguments = new[] {
                    new ArgumentDescriptor {
                        ArgumentType = typeof(Int32),
                        ArgumentName = "count",
                        DefaultValue = 1,
                        IsOptional = false,
                        Position = 0
                    }
                },
                Name = "RepeatRunnable"
            });

            commandDescriptorManager.Register(new CommandDescriptor {
                CommandNames = new[] { "convert-to-textrange" },
                Arguments = Enumerable.Empty<ArgumentDescriptor>(),
                Name = "ConvertToTextRangeRunnable"
            });

            commandDescriptorManager.Register(new CommandDescriptor {
                CommandNames = new[] { "convert-to-string" },
                Arguments = Enumerable.Empty<ArgumentDescriptor>(),
                Name = "ConvertToStringRunnable"
            });

            commandDescriptorManager.Register(new CommandDescriptor {
                CommandNames = new[] { "to-upper-case" },
                Arguments = Enumerable.Empty<ArgumentDescriptor>(),
                Name = "ConvertCaseRunnable"
            });

            commandDescriptorManager.Register(new CommandDescriptor {
                CommandNames = new[] { "filter" },
                Arguments = new[] {
                    new ArgumentDescriptor {
                        ArgumentType = typeof(String),
                        ArgumentName = "pattern",
                        DefaultValue = 1,
                        IsOptional = false,
                        Position = 0
                    }
                },
                Name = "FilterRunnable"
            });

            var runnableFactory = new DefaultRunnableFactory(new[]
            {
                typeof(RepeatRunnable), typeof(ConvertToStringRunnable), typeof(FilterRunnable), typeof(ConvertCaseRunnable),
                typeof(ConvertToTextRangeRunnable)
            });
            var commandCallResolver = new DefaultCommandCallResolver(commandDescriptorManager, runnableFactory);
            var selectorManager = new DefaultSelectorFactory();
            var assemblies = new[] { Assembly.Load("CodeSharper.Core"), Assembly.GetExecutingAssembly(), Assembly.Load("CodeSharper.Languages") };
            var repository = new FileDescriptorRepository(@"D:\Development\Projects\C#\CodeSharper\master-refactoring\CodeSharper\demos\CodeSharper.App.CLI\descriptors.json", assemblies);
            var nodeSelectorResolver = new DefaultSelectorResolver(selectorManager, repository);
            var runnableManager = new DefaultRunnableManager();
            var executor = new StandardExecutor(runnableManager);

            // initialize compiler and control flow factory
            ControlFlowFactory = new DefaultControlFlowFactory(commandCallResolver, nodeSelectorResolver, executor);
            Compiler = new CodeQueryCompiler();
            CsvCompiler = new CsvCompiler();
        }

        public static void Main(String[] args)
        {
            initializeApplication();

            String response = String.Empty;
            Node root = null;

            String content = String.Empty;

            if (args.Length > 0)
            {
                content = File.OpenText(args[0]).ReadToEnd();
            }

            do
            {
                try
                {
                    root = CsvCompiler.Parse(content);
                    Console.Write("> ");
                    response = Console.ReadLine();
                    response += " | @convert-to-string";
                    var controlFlowDescriptor = Compiler.Parse(response);
                    var controlFlow = ControlFlowFactory.Create(controlFlowDescriptor);
                    var result = controlFlow.Execute(root) as String;
                    content = root.TextRange.GetText();

                    Console.WriteLine("Result: {0}", result);
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error message: {0}", exception.Message);
                    Console.WriteLine("Stacktrace: ");
                    Console.WriteLine(exception.StackTrace);
                }

            } while (response != "exit");
        }
    }
}
