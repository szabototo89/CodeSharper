using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Common
{
    /// <summary>
    /// Optional(T) class which is representing that value is initialized or not.
    /// </summary>
    /// <typeparam name="TValue">The type of the type.</typeparam>
    public struct Optional<TValue>
    {
        private static Optional<TValue> _none;

        #region Private fields

        private TValue _value;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes the <see cref="Optional{TType}"/> struct.
        /// </summary>
        static Optional()
        {
            _none = new Optional<TValue>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Optional{TType}"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public Optional(TValue value)
            : this()
        {
            _value = value;
            IsInitalized = true;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets a value indicating whether the object is initialized with a value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is initalized]; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsInitalized { get; private set; }

        /// <summary>
        /// Gets or sets the value of optional object.
        /// </summary>
        public TValue Value
        {
            get { return _value; }
            set { _value = value; }
        }

        #endregion

        #region Public implicit operators

        public static implicit operator Boolean(Optional<TValue> optional)
        {
            return optional.IsInitalized;
        }

        public static implicit operator TValue(Optional<TValue> optional)
        {
            if (!optional.IsInitalized)
                throw new Exception("Optional is not initialized!");

            return optional.Value;
        }

        public static implicit operator Optional<TValue>(TValue value)
        {
            return new Optional<TValue>(value);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Represents an uninitialized Option(TType) object
        /// </summary>
        public static Optional<TValue> None
        {
            get { return _none; }
        }

        /// <summary>
        /// Initializes an Optional(TType) instance with some value. 
        /// </summary>
        public static Optional<TValue> Some(TValue value)
        {
            return new Optional<TValue>(value);
        }

        #endregion
    }
}