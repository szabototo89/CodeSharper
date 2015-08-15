using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using CodeSharper.Core.Common.Runnables.Attributes;
using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Experimental;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;
using CodeSharper.Interpreter.Common;
using CodeSharper.Interpreter.Compiler;
using CodeSharper.Languages.Csv.Compiler;
using CodeSharper.Languages.Csv.Nodes.Selectors;
using CodeSharper.Languages.Json.Compiler;
using TextPosition = CodeSharper.Core.Texts.TextPosition;

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


        public static void Main(String[] args)
        {
            runTest(1000);

            /*initializeApplication();

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
                    // var textDocument = new TextDocument(content);
                    root = CsvCompiler.Parse(content);
                    Console.Write("> ");
                    response = Console.ReadLine();
                    response += " | @convert-to-string";
                    var controlFlowDescriptor = Compiler.Parse(response);
                    var controlFlow = ControlFlowFactory.Create(controlFlowDescriptor);
                    var result = controlFlow.Execute(new[] { root }) as String;
                    content = root.TextRange.GetText();
                    // content = textDocument.Text;

                    Console.WriteLine("{0}", result);
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error message: {0}", exception.Message);
                    Console.WriteLine("Stacktrace: ");
                    Console.WriteLine(exception.StackTrace);
                }

            } while (response != "exit");
             */
        }

        private static void runTest(Int32 lines)
        {
            // Given
            var textBuilder = new StringBuilder();
            for (var i = 0; i < lines; i++)
            {
                textBuilder.AppendLine("one,two,three,four");
            }

            var underTest = new DefaultTextManager(textBuilder.ToString());
            var spans = new List<TextSpan>();

            for (var i = 0; i < lines; i++)
            {
                spans.Add(underTest.CreateOrGetTextSpan(new Core.Experimental.TextPosition(i, 0), new Core.Experimental.TextPosition(i, 3)));
                underTest.CreateOrGetTextSpan(new Core.Experimental.TextPosition(i, 4), new Core.Experimental.TextPosition(i, 7));
            }

            // When
            var watch = Stopwatch.StartNew();
            foreach (var span in spans)
            {
                underTest.SetValue("onetwo", span);
            }
            watch.Stop();

            // Then
            Console.WriteLine(watch.Elapsed);

        }
    }
}
