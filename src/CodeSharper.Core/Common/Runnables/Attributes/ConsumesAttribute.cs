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
        public Type TypeOfArgumentBefore { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsumesAttribute"/> class.
        /// </summary>
        public ConsumesAttribute(Type typeOfArgumentBefore)
        {
            Assume.NotNull(typeOfArgumentBefore, "typeOfArgumentBefore");

            TypeOfArgumentBefore = typeOfArgumentBefore;
        }
    }
}