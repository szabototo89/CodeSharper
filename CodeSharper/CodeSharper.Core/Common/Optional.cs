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
    /// <typeparam name="TType">The type of the type.</typeparam>
    public struct Optional<TType>
    {
        private static Optional<TType> _none;

        #region Private fields

        private TType _value;

        #endregion

        #region Constructors

        static Optional()
        {
            _none = new Optional<TType>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Optional{TType}"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public Optional(TType value)
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

        public static Optional<TType> None
        {
            get { return _none; }
        }

        /// <summary>
        /// Gets or sets the value of optional object.
        /// </summary>
        public TType Value
        {
            get { return _value; }
            set { _value = value; }
        }

        #endregion

        #region Public implicit operators

        public static implicit operator Boolean(Optional<TType> optional)
        {
            return optional.IsInitalized;
        }

        public static implicit operator TType(Optional<TType> optional)
        {
            if (!optional.IsInitalized)
                throw new Exception("Optional is not initialized!");

            return optional.Value;
        }

        public static implicit operator Optional<TType>(TType value)
        {
            return new Optional<TType>(value);
        }

        #endregion

        #region Public methods

        public static Optional<TType> Some(TType value)
        {
            return new Optional<TType>(value);
        }

        #endregion
    }
}