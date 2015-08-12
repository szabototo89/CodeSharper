using System;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Common.Runnables.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ConsumesAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the type of argument before.
        /// </summary>
        public Type ConsumerType { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumesAttribute"/> class.
        /// </summary>
        public ConsumesAttribute(Type consumerType)
        {
            Assume.NotNull(consumerType, nameof(consumerType));

            ConsumerType = consumerType;
        }
    }
}