using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CodeSharper.Core.Common.ConstraintChecking
{
    [DebuggerStepThrough]
    public static class Constraints
    {
        public static IConstraint NotNull<TArgumentType>(Expression<Func<TArgumentType>> func)
            where TArgumentType : class
        {
            return new NotNullConstraint<TArgumentType>().Check(func);
        }

        public static IConstraint NotBlank(Expression<Func<String>> func)
        {
            return new NotBlankConstraint().Check(func);
        }

        public static IConstraint NotEmpty<TItemType>(Expression<Func<IEnumerable<TItemType>>> func)
        {
            return new NotEmptyConstraint<TItemType>().Check(func);
        }

        public static IConstraint NotNull(this IConstraint constraint, Expression<Func<Object>> func)
        {
            return NotNull(func);
        }

        public static IConstraint NotBlank(this IConstraint constraint, Expression<Func<String>> func)
        {
            return NotBlank(func);
        }

        public static ConstraintArgument<TArgumentType> NotNull<TArgumentType>(this ConstraintArgument<TArgumentType> argument)
            where TArgumentType : class
        {
            Constraints.NotNull(() => argument);

            foreach (var arg in argument.Arguments)
                NotNull(arg);

            return argument;
        }

        public static ConstraintArgument<String> NotBlank(this ConstraintArgument<String> argument)
        {
            Constraints.NotNull(() => argument);

            foreach (var arg in argument.Arguments)
                NotBlank(arg);

            return argument;
        }

        public static ConstraintArgument<IEnumerable<TItemType>> NotEmpty<TItemType>(this ConstraintArgument<IEnumerable<TItemType>> argument)
        {
            Constraints.NotNull(() => argument);

            foreach (var arg in argument.Arguments)
                NotEmpty(arg);

            return argument;
        }


        public static ConstraintArgument<TArgumentType> Argument<TArgumentType>(params Expression<Func<TArgumentType>>[] argumentName)
        {
            return new ConstraintArgument<TArgumentType>(argumentName);
        }

        public static ConstraintArgument<TNewArgumentType> Argument<TArgumentType, TNewArgumentType>(this ConstraintArgument<TArgumentType> previousArgument, params Expression<Func<TNewArgumentType>>[] argumentName)
        {
            return new ConstraintArgument<TNewArgumentType>(argumentName);
        }
    }
}