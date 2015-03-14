﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Commands
{
    public class CommandCallDescriptor : IEquatable<CommandCallDescriptor>
    {
        #region Public properties of CommandCallDescriptor

        /// <summary>
        /// Gets or sets the name of <see cref="CommandCallDescriptor"/>
        /// </summary>
        public String Name { get; protected set; }

        /// <summary>
        /// Gets or sets the command call actual parameters of <see cref="CommandCallDescriptor"/>
        /// </summary>
        public IEnumerable<ICommandCallActualArgument> ActualParameters { get; protected set; }

        #endregion

        #region Constructors of CommandCallDescriptor

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandCallDescriptor"/> class.
        /// </summary>
        public CommandCallDescriptor(String name, IEnumerable<ICommandCallActualArgument> actualParameters)
        {
            Assume.NotNull(name, "name");
            Assume.NotNull(actualParameters, "actualParameters");

            Name = name;
            ActualParameters = actualParameters;
        }

        #endregion

        #region Equality checker methods

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        public override Boolean Equals(Object other)
        {
            return Equals(other as CommandCallDescriptor);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        public override Int32 GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Boolean Equals(CommandCallDescriptor other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(other, this)) return true;

            return String.Equals(Name, other.Name);
        }

        #endregion
    }
}   
