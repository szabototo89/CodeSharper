using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CodeSharper.Core.Common.ConstraintChecking
{
    public class ConstraintArgument<TArgumentType> : IConstraintArgument<TArgumentType>
    {
        public IEnumerable<Expression<Func<TArgumentType>>> Arguments { get; protected set; }

        public IEnumerable<TArgumentType> Values { get; protected set; }

        public ConstraintArgument(IEnumerable<Expression<Func<TArgumentType>>> arguments)
        {
            Arguments = arguments;
            Values = Arguments.Select(arg => arg.Compile().Invoke());
        }
    }
}