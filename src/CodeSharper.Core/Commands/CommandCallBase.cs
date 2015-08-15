using System.Collections.Generic;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Commands
{
    public abstract class CommandCallBase : ICommandCall
    {
        /// <summary>
        /// Gets or sets children of command call
        /// </summary>
        public IEnumerable<ICommandCall> Children { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandCallBase"/> class.
        /// </summary>
        protected CommandCallBase(IEnumerable<ICommandCall> commands)
        {
            Assume.NotNull(commands, nameof(commands));

            Children = commands;
        }
    }

    public class PipelineCommandCall : CommandCallBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineCommandCall"/> class.
        /// </summary>
        public PipelineCommandCall(IEnumerable<ICommandCall> children)
            : base(children)
        {
        }
    }

    public class SequenceCommandCall : CommandCallBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceCommandCall"/> class.
        /// </summary>
        public SequenceCommandCall(IEnumerable<ICommandCall> commands)
            : base(commands)
        {
        }
    }

    public class LazyAndCommandCall : CommandCallBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LazyAndCommandCall"/> class.
        /// </summary>
        public LazyAndCommandCall(IEnumerable<ICommandCall> commands)
            : base(commands)
        {
        }
    }

    public class LazyOrCommandCall : CommandCallBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LazyOrCommandCall"/> class.
        /// </summary>
        public LazyOrCommandCall(IEnumerable<ICommandCall> commands)
            : base(commands)
        {
        }
    }
}
