using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Interpreter.Bootstrappers;
using CodeSharper.Languages.CSharp.Compiler;
using Microsoft.CodeAnalysis;

namespace CodeSharper.Playground.GUI.Modules
{
    public class CSharpCompilerModule : CompilerModuleBase
    {
        private readonly CSharpCompiler compiler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvCompilerModule"/> class.
        /// </summary>
        public CSharpCompilerModule(Bootstrapper bootstrapper)
            : base(bootstrapper)
        {
            compiler = new CSharpCompiler();
        }

        public override DocumentResults? ExecuteQuery(String input, String text)
        {
            try
            {
                var root = this.compiler.Parse(text);
                var response = input; // + " | convert-to-string";
                var controlFlowDescriptor = bootstrapper.Compiler.Parse(response);
                var controlFlow = bootstrapper.ControlFlowFactory.Create(controlFlowDescriptor);
                var result = controlFlow.Execute(new[] { root }) as IEnumerable<Object>;

                var documentResults = new DocumentResults() {
                    Source = text,
                    Results = result.OfType<SyntaxNode>().First().SyntaxTree.ToString()
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