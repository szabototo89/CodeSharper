using System;
using System.Linq.Expressions;

namespace CodeSharper.Core.Common.ConstraintChecking
{
    public interface IConstraint { }

    public interface IConstraint<TPropertyType> : IConstraint
    {
        IConstraint<TPropertyType> Check(Expression<Func<TPropertyType>> expression);
    }
}
