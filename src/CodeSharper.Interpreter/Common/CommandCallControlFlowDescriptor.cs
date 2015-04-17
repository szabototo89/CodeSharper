using System;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Interpreter.Common
{
    public class CommandCallControlFlowDescriptor : ControlFlowDescriptorBase, IEquatable<CommandCallControlFlowDescriptor>
    {
        /// <summary>
        /// Gets or sets the method call
        /// </summary>
        public CommandCall CommandCall { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandCallControlFlowDescriptor"/> class.
        /// </summary>
        public CommandCallControlFlowDescriptor(CommandCall commandCall) 
            : base(ControlFlowOperationType.CommandCall)
        {
            Assume.NotNull(commandCall, "CommandCall");
            CommandCall = commandCall;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(CommandCallControlFlowDescriptor other)
        {
            return Equals(other as ControlFlowDescriptorBase) &&
                   CommandCall.Equals(other.CommandCall);
        }
    }
}