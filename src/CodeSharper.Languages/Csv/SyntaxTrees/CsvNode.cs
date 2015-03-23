using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Csv.SyntaxTrees
{
    public abstract class CsvNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvNode"/> class.
        /// </summary>
        protected CsvNode(TextRange textRange) : base(textRange) { }
    }
}
