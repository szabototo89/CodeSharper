using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using CodeSharper.Core.Nodes;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Languages.Text.Nodes.Selectors
{
    public class LineSelector : TextSeparatorSelector
    {
        private readonly String SEPARATOR_ATTRIBUTE = "separator";
        private readonly String INCLUDE_EMPTY_LINE = "include-empty-lines";

        /// <summary>
        /// Applys the attributes to specified selector
        /// </summary>
        public override void ApplyAttributes(IEnumerable<SelectorAttribute> attributes)
        {
            base.ApplyAttributes(attributes);

            IsEmptyLineIncluded = GetAttributeBooleanValue(INCLUDE_EMPTY_LINE);
            IsSeparatorOverriden = IsAttributeDefined(SEPARATOR_ATTRIBUTE);

            if (IsSeparatorOverriden)
                OverridenSeparator = GetAttributeValueOrDefault(SEPARATOR_ATTRIBUTE, String.Empty);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is empty line included.
        /// </summary>
        public Boolean IsEmptyLineIncluded { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is separator overriden.
        /// </summary>
        protected Boolean IsSeparatorOverriden { get; private set; }

        /// <summary>
        /// Gets the overriden separator.
        /// </summary>
        protected String OverridenSeparator { get; private set; }

        /// <summary>
        /// Gets the pattern.
        /// </summary>
        protected override String Pattern
        {
            get { return OverridenSeparator ?? "\r?\n"; }
        }

        /// <summary>
        /// Selects the element.
        /// </summary>
        public override IEnumerable<TextRange> SelectElement(TextRange textRange)
        {
            return base.SelectElement(textRange).Where(line => {
                var text = TextRangeExtensions.GetText(line);
                return IsEmptyLineIncluded || !String.IsNullOrWhiteSpace(text);
            });
        }
    }
}