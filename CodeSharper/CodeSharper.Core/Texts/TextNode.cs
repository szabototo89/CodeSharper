using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

        public TextDocument Parent { get; protected set; }

        /// <summary>
        /// Constructor for cloning of TextNode
        /// </summary>
        private TextNode() { }

        public TextNode(String text, TextDocument parent = null)
            : this(0, text, parent) { }

        public TextNode(Int32 start, String text, TextDocument parent = null)
            : this()
        {
            Constraints.NotNull(() => text);

            Text = text;
            Parent = parent;

            TextSpan = new TextSpan(start, text);
            if (parent != null)
                parent.AppendChild(this);
        }

        public TextNode Detach()
        {
            if (Parent != null)
                Parent.RemoveChild(this);

            Parent = null;

            return this;
        }

        public TextNode AttachTo(TextDocument parent)
        {
            Constraints
                .NotNull(() => parent);

            Parent = parent;
            Parent.AppendChild(this);

            return this;
        }
    }
}
