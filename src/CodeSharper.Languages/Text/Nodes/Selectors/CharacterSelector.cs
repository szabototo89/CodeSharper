using System;
using System.Collections.Generic;
using CodeSharper.Core.Nodes;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Languages.Text.Nodes.Selectors
{
    public class CharacterSelector : TextSelectorBase
    {
        private readonly String UPPERCASE_ATTRIBUTE = "uppercase";
        private readonly String LOWERCASE_ATTRIBUTE = "lowercase";
        private readonly String PUNCTUATION_ATTRIBUTE = "punctuation";
        private readonly String DIGIT_ATTRIBUTE = "digit";

        /// <summary>
        /// Applys the attributes to specified selector
        /// </summary>
        public override void ApplyAttributes(IEnumerable<SelectorAttribute> attributes)
        {
            base.ApplyAttributes(attributes);

            IsUpperCase = GetAttributeBooleanValue(UPPERCASE_ATTRIBUTE);
            IsLowerCase = GetAttributeBooleanValue(LOWERCASE_ATTRIBUTE);
            IsPunctuation = GetAttributeBooleanValue(PUNCTUATION_ATTRIBUTE);
            IsDigit = GetAttributeBooleanValue(DIGIT_ATTRIBUTE);
        }

        /// <summary>
        /// Gets a value indicating whether this instance is digit.
        /// </summary>
        public Boolean IsDigit { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is punctuation.
        /// </summary>
        public Boolean IsPunctuation { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is upper case.
        /// </summary>
        public Boolean IsUpperCase { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is upper case.
        /// </summary>
        public Boolean IsLowerCase { get; private set; }

        /// <summary>
        /// Filters the specified element. Returns true if specified element is in the selection otherwise false.
        /// </summary>
        public override IEnumerable<TextRange> SelectElement(TextRange textRange)
        {
            var textDocument = textRange.TextDocument;
            var text = textRange.GetText();

            for (var i = 0; i < text.Length; i++)
            {
                var start = textRange.Start + i;

                var character = text[i];

                var isLowerCaseAllowed = (IsLowerCase && Char.IsLower(character));
                var isUpperCaseAllowed = (IsUpperCase && Char.IsUpper(character));
                var isPunctuationAllowed = (IsPunctuation && Char.IsPunctuation(character));
                var isDigitAllowed = (IsDigit && Char.IsDigit(character));

                if (isLowerCaseAllowed || isUpperCaseAllowed ||
                    isDigitAllowed || isPunctuationAllowed)
                    yield return textDocument.CreateOrGetTextRange(start, start + 1);
            }
        }
    }
}