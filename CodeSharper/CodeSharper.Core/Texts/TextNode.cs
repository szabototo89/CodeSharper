using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Texts
{
    public class TextNode
    {
        public string Text { get; protected set; }

        public TextSpan TextSpan { get; protected set; }

        public TextNode Parent { get; protected set; }



        public TextNode(String text, TextNode parent = null)
        {
            Constraints.NotNull(() => text);

            Text = text;
            Parent = parent;
            TextSpan = new TextSpan(text);
        }

        public TextNode GetSubText(Int32 start, Int32 end)
        {
            throw new NotImplementedException();
        }
    }
}
