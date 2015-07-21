using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Nodes.Modifiers
{
    public class ValueModifier : ModifierBase
    {
        private readonly String value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueModifier"/> class.
        /// </summary>
        public ValueModifier(Object value)
        {
            this.value = (value ?? String.Empty).ToString();
        }

        /// <summary>
        /// Modifies the selection of node 
        /// </summary>
        public override IEnumerable<Object> ModifySelection(Object value)
        {
            if (value is IHasTextRange)
            {
                var valueWithTextRange = (IHasTextRange) value;
                if (valueWithTextRange.TextRange.GetText() == this.value)
                    return new[] {value};
            }
            else if (value is TextRange)
            {
                var textRange = (TextRange) value;
                if (textRange.GetText() == this.value)
                    return new[] {value};
            }

            return Enumerable.Empty<Object>();
        }
    }
}