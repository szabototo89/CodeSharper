using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Interpreter.Visitors
{
    public class CodeQueryVisitor : CodeQueryBaseVisitor<CodeQueryVisitor>
    {
        private readonly Stack<Object> _constants;

        public CodeQueryVisitor()
        {
            _constants = new Stack<Object>();
        }

        protected override CodeQueryVisitor DefaultResult
        {
            get { return this; }
        }

        public override CodeQueryVisitor VisitExpressionInner(CodeQuery.ExpressionInnerContext context)
        {
            return context.Accept(this);
        }

        public override CodeQueryVisitor VisitMethodCall(CodeQuery.MethodCallContext context)
        {
            var methodName = context.MethodCallName.Text;

            return base.VisitMethodCall(context);
        }

        public override CodeQueryVisitor VisitConstantNumber(CodeQuery.ConstantNumberContext context)
        {
            Double value;
            if (!Double.TryParse(context.NUMBER().GetText(), out value))
            {
                throw new Exception("Invalid number!");
            }
            _constants.Push(value);

            return base.VisitConstantNumber(context);
        }

        public override CodeQueryVisitor VisitConstantString(CodeQuery.ConstantStringContext context)
        {
            var stringValue = context.STRING().GetText().Trim('"');
            _constants.Push(stringValue);
            return base.VisitConstantString(context);
        }

        public override CodeQueryVisitor VisitConstantBoolean(CodeQuery.ConstantBooleanContext context)
        {
            switch (context.BOOLEAN().GetText())
            {
                case "false":
                    _constants.Push(false);
                    break;
                case "true":
                    _constants.Push(true);
                    break;
                default:
                    throw new NotSupportedException("Not supported boolean value!");
            }

            return base.VisitConstantBoolean(context);
        }
    }
}
