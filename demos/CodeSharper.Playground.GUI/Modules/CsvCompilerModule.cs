using System;
using CodeSharper.Core.Texts;
using CodeSharper.Interpreter.Bootstrappers;
using CodeSharper.Languages.Csv.Compiler;
using Microsoft.CodeAnalysis;

namespace CodeSharper.Playground.GUI.Modules
{
    public class CsvCompilerModule : CompilerModuleBase
    {
        private readonly CsvCompiler csvCompiler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvCompilerModule"/> class.
        /// </summary>
        public CsvCompilerModule(Bootstrapper bootstrapper) : base(bootstrapper)
        {
            csvCompiler = new CsvCompiler();
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        public override DocumentResults? ExecuteQuery(String input, String text)
        {
            try
            {
                var textDocument = new TextDocument(text);
                var root = csvCompiler.Parse(textDocument.Text);
                var response = input + " | convert-to-string";
                var controlFlowDescriptor = bootstrapper.Compiler.Parse(response);
                var controlFlow = bootstrapper.ControlFlowFactory.Create(controlFlowDescriptor);
                var result = controlFlow.Execute(new[] { root }) as String;

                var documentResults = new DocumentResults() {
                    Source = root.TextDocument.Text,
                    Results = result
                };

                return documentResults;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error message: {0}", exception.Message);
                Console.WriteLine("Stacktrace: ");
                Console.WriteLine(exception.StackTrace);

                return null;
            }
        }
    }
}