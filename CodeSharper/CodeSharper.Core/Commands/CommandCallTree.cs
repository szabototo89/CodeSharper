using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Commands
{
    public interface ICommandCallTree
    {
        IEnumerable<ICommandCallTree> Children { get; }

        void AddCommandCallTree(ICommandCallTree tree);
    }

    public abstract class CommandCallTreeBase : ICommandCallTree
    {
        private readonly List<ICommandCallTree> _children;

        public IEnumerable<ICommandCallTree> Children
        {
            get { return _children.ToArray(); }
        }

        protected CommandCallTreeBase()
        {
            _children = new List<ICommandCallTree>();
        }

        public void ClearCommandCallTree()
        {
            _children.Clear();
        }

        public void AddCommandCallTree(ICommandCallTree tree)
        {
            Constraints.NotNull(() => tree);
            _children.Add(tree);
        }
    }

    public class SequenceCommandCallTree : CommandCallTreeBase { }

    public class PipeLineCommandCallTree : CommandCallTreeBase { }

    public class LazyAndCommandCallTree : CommandCallTreeBase { }

    public class LazyOrCommandCallTree : CommandCallTreeBase { }
}
