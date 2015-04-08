using System;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common
{
    public class Command : IEquatable<Command>
    {
        /// <summary>
        /// Gets or sets the runnable.
        /// </summary>
        public IRunnable Runnable { get; protected set; }

        /// <summary>
        /// Gets or sets the command descriptor.
        /// </summary>
        public CommandDescriptor CommandDescriptor { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        public Command(IRunnable runnable, CommandDescriptor commandDescriptor)
        {
            Assume.NotNull(runnable, "runnable");
            Assume.NotNull(commandDescriptor, "commandDescriptor");

            Runnable = runnable;
            CommandDescriptor = commandDescriptor;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(Command other)
        {
            return EqualityHelper.IsNullOrReferenceEqual(other, this) ??
                   Runnable.Equals(other.Runnable) &&
                   CommandDescriptor.Equals(other.CommandDescriptor);
        }
    }
}