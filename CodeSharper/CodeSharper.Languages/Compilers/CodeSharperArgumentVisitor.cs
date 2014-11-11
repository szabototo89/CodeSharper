using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CodeSharper.Core.Common;
using CodeSharper.Core.Utilities;
using CodeSharper.Languages.Grammar;

namespace CodeSharper.Languages.Compilers
{
    public class CodeSharperArgumentVisitor : CodeSharperGrammarBaseVisitor<Object>, IVisitor<Object>
    {
        public Object Visit(RuleContext context)
        {
            return VisitParameter(context as CodeSharperGrammarParser.ParameterContext);
        }
    }
}