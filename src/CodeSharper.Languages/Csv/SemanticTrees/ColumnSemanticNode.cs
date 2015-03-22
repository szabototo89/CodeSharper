using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.SemanticTrees;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Csv.SemanticTrees
{
    public class ColumnSemanticNode : SemanticNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnSemanticNode"/> class.
        /// </summary>
        public ColumnSemanticNode(String name, String index, TextRange textRange) : base(textRange) { }
    }
}
