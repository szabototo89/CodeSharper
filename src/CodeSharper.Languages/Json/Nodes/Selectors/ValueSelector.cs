using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Nodes;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Languages.Json.SyntaxTrees.Constants;
using CodeSharper.Languages.Json.SyntaxTrees.Literals;

namespace CodeSharper.Languages.Json.Nodes.Selectors
{
    public class ValueSelector : TypedSelectorBase<ValueDeclaration>
    {
        private readonly String CONSTANT_ATTRIBUTE = "constant";
        private readonly String LITERAL_ATTRIBUTE = "literal";
        private readonly String STRING_ATTRIBUTE = "string";

        /// <summary>
        /// Applys the attributes to specified selector
        /// </summary>
        public override void ApplyAttributes(IEnumerable<SelectorAttribute> attributes)
        {
            base.ApplyAttributes(attributes);

            IsConstant = GetAttributeBooleanValue(CONSTANT_ATTRIBUTE);
            IsString = GetAttributeBooleanValue(STRING_ATTRIBUTE);
            IsLiteral = GetAttributeBooleanValue(LITERAL_ATTRIBUTE);
        }

        /// <summary>
        /// Gets a value indicating whether this instance is string.
        /// </summary>
        public Boolean IsString { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is literal.
        /// </summary>
        public Boolean IsLiteral { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is constant.
        /// </summary>
        public Boolean IsConstant { get; private set; }

        /// <summary>
        /// Filters the specified element. Returns true if specified element is in the selection otherwise false.
        /// </summary>
        public override IEnumerable<Object> SelectElement(Object element)
        {
            var elements = base.SelectElement(element).OfType<ValueDeclaration>();

            if (IsLiteral || IsConstant || IsString)
            {
                foreach (var value in elements)
                {
                    if (isConstant(value) || isLiteral(value) || isString(value))
                        yield return value;
                }

                yield break;
            }

            foreach (var value in elements)
            {
                yield return value;
            }
        }

        private Boolean isString(ValueDeclaration value)
        {
            return IsString && value.Value is StringConstant;
        }

        private Boolean isLiteral(ValueDeclaration value)
        {
            return IsLiteral && value.IsLiteral;
        }

        private Boolean isConstant(ValueDeclaration value)
        {
            return IsConstant && value.IsConstant;
        }
    }
}