using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.SemanticTrees
{
    public abstract class SemanticNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SemanticNode"/> class.
        /// </summary>
        protected SemanticNode(TextRange textRange)
            : base(textRange)
        {
        }
    }
}
