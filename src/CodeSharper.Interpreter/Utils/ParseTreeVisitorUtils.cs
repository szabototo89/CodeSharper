using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace CodeSharper.Interpreter.Utils
{
    public static class ParseTreeVisitorUtils
    {
        public static TValue[] AcceptAll<TContext, TValue>(this TContext[] contexts, IParseTreeVisitor<TValue> treeVisitor)
            where TContext : ParserRuleContext
        {
            return contexts.Select(context => context.Accept(treeVisitor)).ToArray();
        }

    }
}
