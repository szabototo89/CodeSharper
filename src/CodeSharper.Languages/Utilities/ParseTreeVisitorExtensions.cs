using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Utilities
{
    internal static class ParseTreeVisitorExtensions
    {
        public static TValue[] AcceptAll<TContext, TValue>(this TContext[] contexts, IParseTreeVisitor<TValue> treeVisitor)
            where TContext : ParserRuleContext
        {
            return contexts.Select(context => context.Accept(treeVisitor)).ToArray();
        }

        public static TextRange CreateTextRange(this ITerminalNode comma, TextDocument textDocument)
        {
            return textDocument.CreateOrGetTextRange(comma.Symbol.StartIndex, comma.Symbol.StopIndex + 1);
        }

        public static TextRange CreateTextRange(this ParserRuleContext context, TextDocument textDocument)
        {
            TextRange textRange = textDocument.CreateOrGetTextRange(context.Start.StartIndex, context.Stop.StopIndex + 1);
            return textRange;
        }
    }
}
