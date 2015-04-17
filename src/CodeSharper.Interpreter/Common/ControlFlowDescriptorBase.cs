using System;

namespace CodeSharper.Interpreter.Common
{
    public abstract class ControlFlowDescriptorBase : IEquatable<ControlFlowDescriptorBase>
    {
        /// <summary>
        /// Gets or sets the type of the operation.
        /// </summary>
        public ControlFlowOperationType OperationType { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlFlowDescriptorBase"/> class.
        /// </summary>
        protected ControlFlowDescriptorBase(ControlFlowOperationType operationType)
        {
            OperationType = operationType;
        }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(ControlFlowDescriptorBase other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return OperationType == other.OperationType;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        public override Boolean Equals(Object obj)
        {
            return Equals(obj as ControlFlowDescriptorBase);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override Int32 GetHashCode()
        {
            return (Int32) OperationType;
        }

        #endregion
    }
}