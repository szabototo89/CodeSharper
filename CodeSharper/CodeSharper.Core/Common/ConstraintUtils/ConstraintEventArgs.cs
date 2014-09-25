using System;

namespace CodeSharper.Core.Common.ConstraintUtils
{
    public class ConstraintEventArgs : EventArgs
    {
        public static readonly new ConstraintEventArgs Empty = new ConstraintEventArgs();

        public String Expression { get; protected set; }

        public Object Value { get; protected set; }

        protected ConstraintEventArgs() { }

        public ConstraintEventArgs(String expression, Object value)
        {
            Expression = expression;
            Value = value;
        }
    }
}