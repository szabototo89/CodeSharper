using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Selectors
{
    public abstract class NodeSelectorBase
    {
        public abstract Boolean FilterNode(Node node);
    }
}
