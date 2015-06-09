using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Languages.Text.Nodes.Selectors
{
    public class LineSelector : TextSeparatorSelector
    {
        private readonly String SEPARATOR_ATTRIBUTE = "separator";
        private readonly String INCLUDE_EMPTY_LINE = "include-empty-lines";

        /// <summary>
        /// Gets or sets a value indicating whether this instance is empty line included.
        /// </summary>
        public Boolean IsEmptyLineIncluded
        {
            get { return GetAttributeBooleanValue(INCLUDE_EMPTY_LINE); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is separator overriden.
        /// </summary>
        protected Boolean IsSeparatorOverriden
        {
            get { return IsAttributeDefined(SEPARATOR_ATTRIBUTE); }
        }

        /// <summary>
        /// Gets the overriden separator.
        /// </summary>
        protected String OverridenSeparator
        {
            get { return GetAttributeValue(SEPARATOR_ATTRIBUTE).Safe(value => value.ToString()); }
        }

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