using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Interpreter.Bootstrappers;
using CodeSharper.Languages.CSharp.Common;
using CodeSharper.Languages.CSharp.Compiler;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace CodeSharper.Playground.GUI.Modules
{
    public class CSharpCompilerModule : CompilerModuleBase
    {
        private readonly CSharpCompiler compiler;
        private readonly AdhocWorkspace workspace;
        private CompilationContext context;
        private Project project;
        private Document document;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvCompilerModule"/> class.
        /// </summary>
        public CSharpCompilerModule(Bootstrapper bootstrapper)
            : base(bootstrapper)
        {
            compiler = new CSharpCompiler();
            workspace = new AdhocWorkspace();
            project = workspace.AddProject("CSharpProject", LanguageNames.CSharp);
            document = project.AddDocument("current", "");
            context = new CompilationContext(workspace, document);
        }

        public override DocumentResults? ExecuteQuery(String input, String text)
        {
            try
            {
                context = new CompilationContext(workspace, document.WithText(SourceText.From(text)));

                var csharpBootStrapper = BootstrapperBuilder.Create(bootstrapper)
                    .WithExecutor(new StandardExecutorWithContext(bootstrapper.RunnableManager, context))
                    .Build();

                // var root = this.compiler.Parse(text);
                var response = input; // + " | convert-to-string";
                var controlFlowDescriptor = csharpBootStrapper.Compiler.Parse(response);
                var controlFlow = csharpBootStrapper.ControlFlowFactory.Create(controlFlowDescriptor);
                var result = controlFlow.Execute(new[] { context.CurrentDocument.GetSyntaxRootAsync().Result }) as IEnumerable<Object>;

                var source = context.DocumentEditor.GetChangedDocument().GetSyntaxRootAsync().Result;

                var documentResults = new DocumentResults() {
                    Source = source.ToFullString(),
                    Results = String.Join(Environment.NewLine, result.OfType<SyntaxNode>().Select(node => node.ToFullString()))
                };

                return documentResults;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error message: {exception.Message}");
                Console.WriteLine("Stacktrace: ");
                Console.WriteLine(exception.StackTrace);

                return null;
            }

        }
    }
}