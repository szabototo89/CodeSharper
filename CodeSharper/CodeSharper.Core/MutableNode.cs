using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;

namespace CodeSharper.Core
{
    public class MutableNode : IIdentifiable<long>
    {
        private static long Count;

        long IIdentifiable<long>.Id { get { return _Id; } }

        protected readonly List<MutableNode> _Children;

        private MutableNode _Parent;
        private readonly long _Id;

        private void _LinkChildToThis(MutableNode child)
        {
            if (child == null)
                throw ThrowHelper.ArgumentNullException("child");

            child._Parent = this;
        }

        static MutableNode()
        {
            Count = 0;
        }

        public MutableNode()
        {
            _Id = Count++;
            _Children = new List<MutableNode>();
        }

        public IEnumerable<MutableNode> GetChildren()
        {
            return _Children;
        }

        public MutableNode GetParent()
        {
            return _Parent;
        }

        public MutableNode AppendChild(MutableNode child)
        {
            _Children.Add(child);
            _LinkChildToThis(child);

            return this;
        }

        public MutableNode SetParent(MutableNode parent)
        {
            if (parent == null)
                ThrowHelper.ThrowArgumentNullException("parent");

            if (parent == this)
                ThrowHelper.ThrowArgumentException("parent", "The object and parent is the same!");

            _Parent = parent;

            return this;
        }

        public MutableNode ClearChildren()
        {
            _Children.Clear();

            return this;
        }

        public virtual NodeTypeDescriptor GetNodeTypeDescriptor()
        {
            return new NodeTypeDescriptor();
        }
    }
}
