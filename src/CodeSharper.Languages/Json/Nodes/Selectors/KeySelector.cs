using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Nodes;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Languages.Json.SyntaxTrees.Literals;

namespace CodeSharper.Languages.Json.Nodes.Selectors
{
    public class KeySelector : TypedSelectorBase<KeyDeclaration>
    {
        private readonly String VALUE_WITH_QUOTES_ATTRIBUTE = "value-with-quotes";

        /// <summary>
        /// Applys the attributes to specified selector
        /// </summary>
        public override void ApplyAttributes(IEnumerable<SelectorAttribute> attributes)
        {
            base.ApplyAttributes(attributes);

            ValueWithQuotes = GetAttributeBooleanValue(VALUE_WITH_QUOTES_ATTRIBUTE);
        }

        /// <summary>
        /// Gets a value indicating whether [value with quotes].
        /// </summary>
        public Boolean ValueWithQuotes { get; private set; }
    }
}