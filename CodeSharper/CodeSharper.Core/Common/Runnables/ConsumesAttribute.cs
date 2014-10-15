using System;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables.Converters;

namespace CodeSharper.Core.Common.Runnables
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ConsumesAttribute : Attribute
    {
        public Type TypeOfArgumentUnwrapper { get; protected set; }

        public ConsumesAttribute(Type typeOfArgumentUnwrapper)
        {
            TypeOfArgumentUnwrapper = typeOfArgumentUnwrapper;
        }
    }
}