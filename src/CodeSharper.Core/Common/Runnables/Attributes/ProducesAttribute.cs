﻿using System;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Common.Runnables.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ProducesAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the type of argument after.
        /// </summary>
        public Type ProducerType { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProducesAttribute"/> class.
        /// </summary>
        public ProducesAttribute(Type producerType)
        {
            Assume.NotNull(producerType, nameof(producerType));

            ProducerType = producerType;
        }
    }
}