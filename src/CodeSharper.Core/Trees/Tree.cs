using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Trees
{
    public class Tree<TRootNode> where TRootNode : Node
    {
        /// <summary>
        /// Gets or sets the text document of tree
        /// </summary>
        public TextDocument TextDocument { get; protected set; }

        /// <summary>
        /// Gets or sets the root of tree
        /// </summary>
        public TRootNode Root { get; protected set; }
    }
}
