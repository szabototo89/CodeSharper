using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Interpreter.Grammar;

namespace CodeSharper.Interpreter.Visitors
{
    public class CodeQueryVisitor : CodeQueryBaseVisitor<CodeQueryVisitor>
    {
        private readonly ISelectorBuilder _selectorBuilder;
        private readonly Stack<Object> _constants;

        public CodeQueryVisitor(ISelectorBuilder selectorBuilder)
        {
            Assume.NotNull(selectorBuilder, "selectorBuilder");

            _constants = new Stack<Object>();
            _selectorBuilder = selectorBuilder;
        }

        protected override CodeQueryVisitor DefaultResult
        {
            get { return this; }
        }

        public override CodeQueryVisitor VisitPseudoSelectorWithConstant(CodeQueryParser.PseudoSelectorWithConstantContext context)
        {
            var name = context.ID().GetText();
            var value = _constants.Pop();

            _selectorBuilder.CreatePseudoSelector(name, value);

            return base.VisitPseudoSelectorWithConstant(context);
        }

        public override CodeQueryVisitor VisitSelectorAttribute(CodeQueryParser.SelectorAttributeContext context)
        {
            context.AttributeValue.Accept(this);
            if (!_constants.Any())
                throw new Exception("Invalid selectorElement attribute!");

            _selectorBuilder.CreateSelectorAttribute(context.AttributeName.Text, _constants.Pop());

            return base.VisitSelectorAttribute(context);
        }

        public override CodeQueryVisitor VisitMethodCall(CodeQueryParser.MethodCallContext context)
        {
            var methodName = context.MethodCallName.Text;

            return base.VisitMethodCall(context);
        }

        public override CodeQueryVisitor VisitConstantNumber(CodeQueryParser.ConstantNumberContext context)
        {
            Double value;
            if (!Double.TryParse(context.NUMBER().GetText(), out value))
            {
                throw new Exception("Invalid number!");
            }
            _constants.Push(value);

            return base.VisitConstantNumber(context);
        }

        public override CodeQueryVisitor VisitConstantString(CodeQueryParser.ConstantStringContext context)
        {
            var stringValue = context.STRING().GetText().Trim('"');
            _constants.Push(stringValue);
            return base.VisitConstantString(context);
        }

        public override CodeQueryVisitor VisitConstantBoolean(CodeQueryParser.ConstantBooleanContext context)
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

    public interface ISelectorBuilder
    {
        void CreateSelectorAttribute(String name, Object value);

        void CreatePseudoSelector(String name, Object value);
    }
}
