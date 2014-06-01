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
        private static long _count;

        #region Private fields

        private readonly long _Id;
        private MutableNode _Parent;

        #endregion

        #region Protected fields

        protected readonly List<MutableNode> _Children;

        #endregion

        #region Constructors

        static MutableNode()
        {
            _count = 0;
        }

        public MutableNode()
        {
            _Id = _count++;
            _Children = new List<MutableNode>();
        }

        #endregion

        #region Private methods

        private void _LinkChildToThis(MutableNode child)
        {
            if (child == null)
                throw ThrowHelper.ArgumentNullException("child");

            child._Parent = this;
        }

        #endregion

        #region Public methods

        public MutableNode AppendChild(MutableNode child)
        {
            _Children.Add(child);
            _LinkChildToThis(child);

            return this;
        }

        public MutableNode ClearChildren()
        {
            _Children.Clear();

            return this;
        }

        public IEnumerable<MutableNode> GetChildren()
        {
            return _Children;
        }

        public virtual NodeTypeDescriptor GetNodeTypeDescriptor()
        {
            return new NodeTypeDescriptor();
        }

        public MutableNode GetParent()
        {
            return _Parent;
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

        #endregion

        long IIdentifiable<long>.Id
        {
            get { return _Id; }
        }
    }
}