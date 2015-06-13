using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Nodes;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Languages.Json.SyntaxTrees.Constants;
using CodeSharper.Languages.Json.SyntaxTrees.Literals;

namespace CodeSharper.Languages.Json.Nodes.Selectors
{
    public class KeyValueSelector : TypedSelectorBase<KeyValueDeclaration>
    {
        private readonly String KEY_ATTRIBUTE = "key";
        private readonly String VALUE_ATTRIBUTE = "value";

        /// <summary>
        /// Applys the attributes to specified selector
        /// </summary>
        public override void ApplyAttributes(IEnumerable<SelectorAttribute> attributes)
        {
            base.ApplyAttributes(attributes);

            FilterByKey = GetAttributeValueOrDefault<String>(KEY_ATTRIBUTE, null);
            FilterByValue = GetAttributeValueOrDefault<String>(VALUE_ATTRIBUTE, null);
        }

        /// <summary>
        /// Gets the filter by value.
        /// </summary>
        public String FilterByValue { get; private set; }

        /// <summary>
        /// Gets the filter by key.
        /// </summary>
        public String FilterByKey { get; private set; }

        /// <summary>
        /// Filters the specified element. Returns true if specified element is in the selection otherwise false.
        /// </summary>
        public override IEnumerable<Object> SelectElement(Object element)
        {
            var elements = base.SelectElement(element).OfType<KeyValueDeclaration>();

            if (FilterByKey == null && FilterByValue == null)
                return elements;

            return elements.Where(keyValue => isFilteringByKey(keyValue) ||
                                              isFilteringStringByValue(keyValue));
        }

        private Boolean isFilteringByKey(KeyValueDeclaration keyValue)
        {
            return keyValue.Key.Value == FilterByKey;
        }

        private Boolean isFilteringStringByValue(KeyValueDeclaration keyValue)
        {
            var value = keyValue.Value;
            return value.IsConstant &&
                   value.Value is StringConstant &&
                   ((StringConstant) value.Value).Value == FilterByValue;
        }
    }
}