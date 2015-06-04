using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.CollectionOperations;
using CodeSharper.Core.Common.Runnables.CollectionRunnables;
using CodeSharper.Core.Common.Runnables.ConversionOperations;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Common.Runnables.TextRangeOperations;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.Services;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;
using CodeSharper.Interpreter.Common;
using CodeSharper.Interpreter.Compiler;
using CodeSharper.Languages.Csv.Compiler;
using CodeSharper.Languages.Csv.Nodes.Selectors;
using CodeSharper.Languages.Json.Compiler;

namespace CodeSharper.Playground.CLI
{
    class Program
    {
        /// <summary>
        /// Gets or sets the compiler.
        /// </summary>
        public static CodeQueryCompiler Compiler { get; protected set; }

        /// <summary>
        /// Gets or sets the CSV compiler.
        /// </summary>
        public static CsvCompiler CsvCompiler { get; protected set; }

        /// <summary>
        /// Gets or sets the control flow factory.
        /// </summary>
        public static DefaultControlFlowFactory ControlFlowFactory { get; protected set; }

        /// <summary>
        /// Gets or sets the json compiler.
        /// </summary>
        public static JsonCompiler JsonCompiler { get; protected set; }


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
                typeof(ConvertToTextRangeRunnable), typeof(ReplaceTextRunnable), typeof(ReplaceTextInteractiveRunnable),
                typeof(TakeRunnable), typeof(SkipRunnable), typeof(ElementAtRunnable), typeof(RangeRunnable), typeof(LengthRunnable)
            });
            var assemblies = new[] { Assembly.Load("CodeSharper.Core"), Assembly.GetExecutingAssembly(), Assembly.Load("CodeSharper.Languages") };
            var repository = new FileDescriptorRepository(@"D:\Development\Projects\C#\CodeSharper\master\demos\CodeSharper.App.CLI\descriptors.json", assemblies);
            var commandCallResolver = new DefaultCommandCallResolver(repository, runnableFactory);
            var selectorManager = new DefaultSelectorFactory();
            var nodeSelectorResolver = new DefaultSelectorResolver(selectorManager, repository);
            var runnableManager = new DefaultRunnableManager();
            var executor = new StandardExecutor(runnableManager);

            // initialize compiler and control flow factory
            ControlFlowFactory = new DefaultControlFlowFactory(commandCallResolver, nodeSelectorResolver, executor);
            Compiler = new CodeQueryCompiler();
            CsvCompiler = new CsvCompiler();
            JsonCompiler = new JsonCompiler();
        }


        public static void Main(String[] args)
        {
            initializeApplication();

            String response = String.Empty;
            Node root = null;

            String content = String.Empty;

            if (args.Length > 0)
            {
                var stream = File.OpenRead(args[0]);
                var reader = new StreamReader(stream);
                content = reader.ReadToEnd();
            }

            do
            {
                try
                {
                    var textDocument = new TextDocument(content);
                    // root = CsvCompiler.Parse(content);
                    Console.Write("> ");
                    response = Console.ReadLine();
                    response += " | @convert-to-string";
                    var controlFlowDescriptor = Compiler.Parse(response);
                    var controlFlow = ControlFlowFactory.Create(controlFlowDescriptor);
                    var result = controlFlow.Execute(new[] { textDocument.TextRange }) as String;
                    // content = root.TextRange.GetText();
                    content = textDocument.Text;

                    Console.WriteLine("{0}", result);
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
