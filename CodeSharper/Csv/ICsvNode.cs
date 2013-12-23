using System;
using System.Collections.Generic;
using CodeSharper.Common;

namespace CodeSharper.Csv
{
    public interface ICsvNode : INode
    {
    }

    public class CsvNode : ICsvNode
    {
        #region Explicit implementation
        IEnumerable<INode> INode.Children
        {
            get { return _Children; }
        }

        #endregion

        protected List<ICsvNode> _Children;
        protected List<ICsvNode> _Siblings;

        IEnumerable<INode> INode.Siblings
        {
            get { return _Siblings; }
        }

        public virtual INode Parent { get; private set; }

        public IEnumerable<ICsvNode> Children { get { return _Children; } }
        public IEnumerable<ICsvNode> Siblings { get { return _Siblings; } }

        private CsvNode()
        {
            _Children = new List<ICsvNode>();
            _Siblings = new List<ICsvNode>();
        }

        public CsvNode(string text, TextPosition start, ICsvNode parent)
            : this()
        {
            if (text == null)
                throw new ArgumentNullException("text");

            Text = text;
            Span = TextSpan.GetFromString(text, start);
            Parent = parent;
        }

        public virtual TextSpan Span { get; private set; }
        public virtual string Text { get; private set; }
    }

    public class CsvEmptyNode : CsvNode
    {
        public CsvEmptyNode(TextPosition start, ICsvNode parent)
            : base(string.Empty, start, parent)
        {

        }
    }
}