using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.ControlFlow;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;
using CodeSharper.DemoRunner.Common;
using CodeSharper.Languages.Compilers.CodeSharper;

namespace CodeSharper.DemoRunner.DemoApplications.CodeREPL
{
    [Demo("CodeREPL", Description = "Read-Evaluate-Print-Loop like application for CodeSharper")]
    public class CodeReadEvaluatePrintLoopApplication : IDemoApplication
    {
        private StandardControlFlow _controlFlow;
        private CodeSharperCompiler _compiler;

        private String _text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum non mollis nulla. Suspendisse vestibulum neque vel ante sagittis, in ultricies magna varius. Vestibulum a nunc efficitur, congue tortor sed, tincidunt urna. Donec condimentum orci diam, vehicula pulvinar est sodales id.";

        private TextDocument _textDocument;

        public void Run(String[] args = null)
        {
            args = args ?? Enumerable.Empty<String>().ToArray();

            ConsoleHelper.OpenConsoleWindow();

            initializeCodeSharper();

            var input = String.Empty;
            Console.WriteLine(_text);

            do
            {
                Console.Write(">>> ");
                input = Console.ReadLine();

                switch (input)
                {
                    case "show":
                        Console.WriteLine(_textDocument.Text);
                        break;
                    case "exit":
                        ConsoleHelper.CloseConsoleWindow();
                        break;
                    default:
                        try
                        {
                            var result = callCommand(input);
                            Console.WriteLine(result);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine(ex.Message);
                        }
                        break;
                }
            } while (input != "exit");
        }

        private String callCommand(String command)
        {
            var commandCall = _compiler.RunVisitor<CodeSharperGrammarVisitor, ICommandCall>(command);
            var textRange = _textDocument.TextRange;

            var result = _controlFlow
                .ParseCommandCall(commandCall)
                .Execute(Arguments.Value(textRange));

            if (result is ValueArgument<TextRange>)
            {
                var value = result as ValueArgument<TextRange>;
                return value.Value.Text;
            }

            if (result is MultiValueArgument<TextRange>)
            {
                var values = result as MultiValueArgument<TextRange>;
                return String.Format("[{0}]", String.Join(",", values.Values.Select(x => x.Text)));
            }

            return "An error has occured!";
        }

        private void initializeCodeSharper()
        {
            Console.WriteLine("Initializing CodeSharper ...");
            _compiler = new CodeSharperCompiler();

            var emptyValue = Arguments.Value(0);
            _controlFlow = new StandardControlFlow(new StandardCommandManager(), new StandardExecutor(RunnableManager.Instance));

            var dir = "../../DemoApplications/CodeREPL/CommandDescriptions";

            var insertTextRange = new InsertTextRangeCommandFactory() {
                Descriptor = JsonCommandDescriptorParser.ParseFrom(File.ReadAllText(Path.Combine(dir, "insert-text-range-descriptor.json")))
            };

            var toUpper = new ToUpperCaseCommandFactory() {
                Descriptor = JsonCommandDescriptorParser.ParseFrom(File.ReadAllText(Path.Combine(dir, "to-upper-case-descriptor.json")))
            };

            var findText = new FindTextCommandFactory() {
                Descriptor = JsonCommandDescriptorParser.ParseFrom(File.ReadAllText(Path.Combine(dir, "find-text-descriptor.json")))
            };

            _controlFlow.CommandManager
                .RegisterCommandFactory(findText)
                .RegisterCommandFactory(insertTextRange)
                .RegisterCommandFactory(toUpper);

            var textBuilder = new StringBuilder(_text);
            for (int i = 0; i < 100; i++)
                textBuilder.Append(_text);

            _text = textBuilder.ToString();

            _textDocument = new TextDocument(_text);

            Console.WriteLine("CodeSharper is ready!");
        }
    }
}
