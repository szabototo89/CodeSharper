using System;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Interpreter.Common
{
    public class CommandCallControlFlowElement : ControlFlowElementBase, IEquatable<CommandCallControlFlowElement>
    {
        /// <summary>
        /// Gets or sets the method call
        /// </summary>
        public CommandCallElement CommandCallElement { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandCallControlFlowElement"/> class.
        /// </summary>
        public CommandCallControlFlowElement(CommandCallElement commandCallElement) 
            : base(ControlFlowOperationType.CommandCall)
        {
            Assume.NotNull(commandCallElement, nameof(commandCallElement));
            CommandCallElement = commandCallElement;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(CommandCallControlFlowElement other)
        {
            return Equals(other as ControlFlowElementBase) &&
                   CommandCallElement.Equals(other.CommandCallElement);
        }
    }
}