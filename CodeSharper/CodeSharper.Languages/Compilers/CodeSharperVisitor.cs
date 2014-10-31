using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Languages.Grammar;

namespace CodeSharper.Languages.Compilers
{
    public class CodeSharperGrammarVisitor : CodeSharperGrammarBaseVisitor<Either<ICommandFactory, Object>>
    {
    }
}
