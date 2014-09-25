using System;
using System.Linq.Expressions;

namespace CodeSharper.Core.Common.ConstraintUtils
{
    public interface IConstraint
    {
        IConstraint Check();
    }

    public interface IConstraint<TPropertyType> : IConstraint
    {
        Expression<Func<TPropertyType>> Expression { get; }
    }

    public static class Constraints
    {
        public static IConstraint NotNull(Expression<Func<Object>> func)
        {
            return new NotNullConstraint(func).Check();
        }

        public static IConstraint NotBlank(Expression<Func<String>> func)
        {
            return new NotBlankConstraint(func).Check();
        }
    }
}
