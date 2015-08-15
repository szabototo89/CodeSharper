using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common
{
    public class CommandDescriptor : IEquatable<CommandDescriptor>
    {
        public static readonly CommandDescriptor Empty;

        /// <summary>
        /// Gets or sets the name of command
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Gets or sets the formal arguments of command
        /// </summary>
        public IEnumerable<ArgumentDescriptor> Arguments { get; }

        /// <summary>
        /// Gets or sets the alias names of command
        /// </summary>
        public IEnumerable<String> CommandNames { get; }

        /// <summary>
        /// Gets or sets the description of command
        /// </summary>
        public String Description { get; }

        /// <summary>
        /// Initializes the <see cref="CommandDescriptor"/> class.
        /// </summary>
        static CommandDescriptor()
        {
            Empty = new CommandDescriptor();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDescriptor"/> class.
        /// </summary>
        public CommandDescriptor()
        {
            Arguments = Enumerable.Empty<ArgumentDescriptor>();
            CommandNames = Enumerable.Empty<String>();
        }

        public CommandDescriptor(String name, String description, IEnumerable<ArgumentDescriptor> arguments, IEnumerable<String> commandNames)
        {
            Assume.NotNull(name, nameof(name));
            Assume.NotNull(description, nameof(description));

            Name = name;
            Description = description;
            Arguments = arguments ?? Enumerable.Empty<ArgumentDescriptor>();
            CommandNames = commandNames ?? Enumerable.Empty<String>();
        }

        #region Equality members

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode*397) ^ (Arguments?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (CommandNames?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (Description?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override Boolean Equals(Object obj)
        {
            return Equals(obj as CommandDescriptor);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(CommandDescriptor other)
        {
            return EqualityHelper.IsNullOrReferenceEqual(other, this) ??
                   String.Equals(Name, other.Name) &&
                   String.Equals(Description, other.Description) &&
                   Enumerable.SequenceEqual(Arguments, other.Arguments) &&
                   Enumerable.SequenceEqual(CommandNames, other.CommandNames);
        }

        #endregion
    }
}