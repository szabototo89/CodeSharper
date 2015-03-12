using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.SyntaxTrees
{
    public interface ISyntaxTreeVisitor<out TResult, in TTree>
    {
        TResult Visit(String input, TTree tree);
    }
}
