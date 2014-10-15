using System;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables.Converters;

namespace CodeSharper.Core.Common.Runnables
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ProducesAttribute : Attribute
    {
        public Type TypeOfArgumentWrapper { get; protected set; }

        public ProducesAttribute(Type typeOfArgumentWrapper)
        {
            TypeOfArgumentWrapper = typeOfArgumentWrapper;
        }
    }
}