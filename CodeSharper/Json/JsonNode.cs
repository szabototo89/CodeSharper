using System.Collections.Generic;
using System.Text;
using CodeSharper.Common;

namespace CodeSharper.Json
{
    public abstract class JsonNode : IJsonNode
    {
        #region Siblings

        protected readonly List<JsonNode> _Siblings = new List<JsonNode>();

        IEnumerable<INode> INode.Siblings
        {
            get { return Siblings; }
        }

        public IEnumerable<JsonNode> Siblings
        {
            get { return _Siblings; }
        }

        #endregion

        #region Children

        protected readonly List<IJsonNode> _Children = new List<IJsonNode>();

        IEnumerable<INode> INode.Children
        {
            get { return Children; }
        }


        public IEnumerable<IJsonNode> Children
        {
            get { return _Children; }
        }

        #endregion

        public virtual TextSpan Span { get; protected set; }
        public virtual string Text { get; protected set; }

        INode INode.Parent
        {
            get { return Parent; }
        }

        public virtual IJsonNode Parent { get; protected set; }

        protected JsonNode(IJsonNode parent)
            : this(null, TextSpan.Zero, parent)
        {
            
        }

        protected JsonNode(string text, TextSpan span, IJsonNode parent = null)
        {
            Text = text;
            Span = span;
            Parent = parent;
        }
    }
}