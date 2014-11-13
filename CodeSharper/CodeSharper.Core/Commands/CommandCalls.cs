using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Commands
{
    public interface ICommandCall
    {
        IEnumerable<ICommandCall> Children { get; }

        void AddCommandCallTree(ICommandCall tree);
    }

    public abstract class CommandCallBase : ICommandCall
    {
        private readonly List<ICommandCall> _children;

        public IEnumerable<ICommandCall> Children
        {
            get { return _children.ToArray(); }
        }

        protected CommandCallBase()
        {
            _children = new List<ICommandCall>();
        }

        public void ClearCommandCallTree()
        {
            _children.Clear();
        }

        public void AddCommandCallTree(ICommandCall tree)
        {
            Constraints.NotNull(() => tree);
            _children.Add(tree);
        }
    }

    public class SequenceCommandCall : CommandCallBase { }

    public class PipeLineCommandCall : CommandCallBase { }

    public class LazyAndCommandCall : CommandCallBase { }

    public class LazyOrCommandCall : CommandCallBase { }
}
