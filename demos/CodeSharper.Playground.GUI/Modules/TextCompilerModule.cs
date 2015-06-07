using System;
using CodeSharper.Core.Texts;
using CodeSharper.Interpreter.Bootstrappers;

namespace CodeSharper.Playground.GUI.Modules
{
    public class TextCompilerModule : CompilerModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextCompilerModule"/> class.
        /// </summary>
        public TextCompilerModule(Bootstrapper bootstrapper) : base(bootstrapper)
        {
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        public override DocumentResults ExecuteQuery(String input, String text)
        {
            try
            {
                var textDocument = new TextDocument(text);
                // root = CsvCompiler.Parse(content);
                var response = input + " | @convert-to-string";
                var controlFlowDescriptor = bootstrapper.Compiler.Parse(response);
                var controlFlow = bootstrapper.ControlFlowFactory.Create(controlFlowDescriptor);
                var result = controlFlow.Execute(new[] {textDocument.TextRange}) as String;
                // content = root.TextRange.GetText();
                var documentResults = new DocumentResults()
                {
                    Source = textDocument.Text,
                    Results = result
                };

                return documentResults;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error message: {0}", exception.Message);
                Console.WriteLine("Stacktrace: ");
                Console.WriteLine(exception.StackTrace);

                return new DocumentResults();
            }
        }
    }
}