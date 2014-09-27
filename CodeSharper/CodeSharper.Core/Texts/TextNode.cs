using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Texts
{
    public class TextNode
    {
        public string Text { get; set; }

        public TextSpan TextSpan { get; set; }

        public IEnumerable<TextNode> Children { get; protected set; }

        public TextNode Parent { get; protected set; }

        /// <summary>
        /// Constructor for cloning of TextNode
        /// </summary>
        private TextNode() { }

        public TextNode(String text, TextNode parent = null)
            : this()
        {
            Constraints.NotNull(() => text);

            Text = text;
            Parent = parent;
            TextSpan = new TextSpan(text);
        }

        public TextNode GetSubText(Int32 start, Int32 end)
        {
            return new TextNode(Text.Substring(start, end), Parent);
        }
    }
}
