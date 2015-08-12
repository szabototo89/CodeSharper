using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        /// Gets or sets the actual arguments.
        /// </summary>
        public ReadOnlyDictionary<String, Object> ActualArguments { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        public Command(IRunnable runnable, CommandDescriptor commandDescriptor)
            : this(runnable, commandDescriptor, new Dictionary<String, Object>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        public Command(IRunnable runnable, CommandDescriptor commandDescriptor, IDictionary<String, Object> actualArguments)
        {
            Assume.NotNull(runnable, nameof(runnable));
            Assume.NotNull(commandDescriptor, nameof(commandDescriptor));
            Assume.NotNull(actualArguments, nameof(actualArguments));

            Runnable = runnable;
            CommandDescriptor = commandDescriptor;
            ActualArguments = new ReadOnlyDictionary<String, Object>(actualArguments);
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
                   CommandDescriptor.Equals(other.CommandDescriptor) &&
                   ActualArguments.SequenceEqual(other.ActualArguments);
        }
    }
}