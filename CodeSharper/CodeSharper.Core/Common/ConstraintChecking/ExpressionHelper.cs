using System;
using System.Linq.Expressions;

namespace CodeSharper.Core.Common.ConstraintChecking
{
    static internal class ExpressionHelper
    {
        public static string GetExpressionName<T>(Expression<Func<T>> expression)
        {
            var lambda = expression as LambdaExpression;
            if (lambda == null)
                throw new Exception("Expression is not lambda expression.");

            MemberExpression memberExpression = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpression = ((UnaryExpression) lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = lambda.Body as MemberExpression;
            }

            if (memberExpression == null)
                throw new ArgumentException("method");

            return memberExpression.Member.Name;
        }
    }
}