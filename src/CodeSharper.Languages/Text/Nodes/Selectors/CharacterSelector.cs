using System;
using System.Collections.Generic;
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
        /// Gets or sets a value indicating whether this instance is attribute specified.
        /// </summary>
        public Boolean IsAttributeSpecified
        {
            get
            {
                return IsAttributeDefined(UPPERCASE_ATTRIBUTE) ||
                       IsAttributeDefined(LOWERCASE_ATTRIBUTE) ||
                       IsAttributeDefined(PUNCTUATION_ATTRIBUTE) ||
                       IsAttributeDefined(DIGIT_ATTRIBUTE);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is digit.
        /// </summary>
        public Boolean IsDigit
        {
            get { return GetAttributeBooleanValue(DIGIT_ATTRIBUTE); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is punctuation.
        /// </summary>
        public Boolean IsPunctuation
        {
            get { return GetAttributeBooleanValue(PUNCTUATION_ATTRIBUTE); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is upper case.
        /// </summary>
        public Boolean IsUpperCase
        {
            get { return GetAttributeBooleanValue(UPPERCASE_ATTRIBUTE); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is upper case.
        /// </summary>
        public Boolean IsLowerCase
        {
            get { return GetAttributeBooleanValue(LOWERCASE_ATTRIBUTE); }
        }

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

                if (!IsAttributeSpecified ||
                    isLowerCaseAllowed || isUpperCaseAllowed ||
                    isDigitAllowed || isPunctuationAllowed)
                {
                    yield return textDocument.CreateOrGetTextRange(start, start + 1);
                }
            }
        }
    }
}