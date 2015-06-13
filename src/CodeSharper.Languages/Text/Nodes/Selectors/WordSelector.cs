using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CodeSharper.Core.Nodes;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Languages.Text.Nodes.Selectors
{
    public class WordSelector : TextSelectorBase
    {
        private readonly Regex lowerCaseRegularExpression = new Regex(@"^[a-z]+$");
        private readonly Regex upperCaseRegularExpression = new Regex(@"^[A-Z]+$");
        private readonly Regex alphabeticRegularExpression = new Regex(@"^[a-zA-Z]+$");
        private readonly Regex camelCaseRegularExpression = new Regex(@"^[a-z]+[\w_]*$");
        private readonly Regex titleCaseRegularExpression = new Regex(@"^[A-Z]+[\w_]*$");
        private readonly Regex numericRegularExpression = new Regex(@"^[0-9]+$");

        private readonly String LOWERCASE_ATTRIBUTE = "lowercase";
        private readonly String UPPERCASE_ATTRIBUTE = "uppercase";
        private readonly String ALPHABETIC_ATTRIBUTE = "alphabetic";
        private readonly String NUMERIC_ATTRIBUTE = "numeric";
        private readonly String TITLE_CASE_ATTRIBUTE = "title-case";
        private readonly String CAMEL_CASE_ATTRIBUTE = "camel-case";

        private readonly Char[] separators = {' ', '.', ',', '!', '?', '\n', '\r', '\t', '(', ')'};

        /// <summary>
        /// Applys the attributes to specified selector
        /// </summary>
        public override void ApplyAttributes(IEnumerable<SelectorAttribute> attributes)
        {
            base.ApplyAttributes(attributes);

            IsFilteringAlphabetic = GetAttributeBooleanValue(ALPHABETIC_ATTRIBUTE);
            IsFilteringNumeric = GetAttributeBooleanValue(NUMERIC_ATTRIBUTE);
            IsLowerCase = GetAttributeBooleanValue(LOWERCASE_ATTRIBUTE);
            IsUpperCase = GetAttributeBooleanValue(UPPERCASE_ATTRIBUTE);
            IsCamelCase = GetAttributeBooleanValue(CAMEL_CASE_ATTRIBUTE);
            IsTitleCase = GetAttributeBooleanValue(TITLE_CASE_ATTRIBUTE);

            IsAnyAttributeSpecified = IsFilteringAlphabetic ||
                                      IsFilteringNumeric ||
                                      IsLowerCase ||
                                      IsUpperCase ||
                                      IsTitleCase ||
                                      IsCamelCase;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is titel case.
        /// </summary>
        public Boolean IsTitleCase { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is camel case.
        /// </summary>
        public Boolean IsCamelCase { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance filters lower case alphabetic text ranges.
        /// </summary>
        public Boolean IsLowerCase { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance filters upper case alphabetic text ranges.
        /// </summary>
        public Boolean IsUpperCase { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is attribute specified.
        /// </summary>
        public Boolean IsAnyAttributeSpecified { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is filtering numeric.
        /// </summary>
        public Boolean IsFilteringNumeric { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is filtering alphabetic.
        /// </summary>
        public Boolean IsFilteringAlphabetic { get; private set; }

        /// <summary>
        /// Selects the element.
        /// </summary>
        public override IEnumerable<TextRange> SelectElement(TextRange textRange)
        {
            var textDocument = textRange.TextDocument;
            var text = textRange.GetText();
            var index = 0;
            var firstWhitespacePosition = 0;

            do
            {
                firstWhitespacePosition = text.IndexOfAny(separators, index);
                if (firstWhitespacePosition == -1) break;
                var result = text.Substring(index, firstWhitespacePosition - index);
                if (isWord(result))
                    yield return textDocument.CreateOrGetTextRange(textRange.Start + index, textRange.Start + firstWhitespacePosition);
                index = firstWhitespacePosition + 1;
            } while (firstWhitespacePosition != -1);

            var startLastWord = textRange.Start + index;
            var stopLastWord = textRange.Start + text.Length;
            var lastWord = text.Substring(index, text.Length - index);

            if (Math.Abs(stopLastWord - startLastWord) != 0 && isWord(lastWord))
                yield return textDocument.CreateOrGetTextRange(startLastWord, stopLastWord);
        }

        private Boolean isWord(String text)
        {
            return (!IsAnyAttributeSpecified && !String.IsNullOrWhiteSpace(text)) ||
                   isAlphabetic(text) || isNumber(text) || isCamelCase(text) || isTitleCase(text);
        }

        private Boolean isTitleCase(String text)
        {
            return IsTitleCase && titleCaseRegularExpression.IsMatch(text);
        }

        private Boolean isCamelCase(String text)
        {
            return IsCamelCase && camelCaseRegularExpression.IsMatch(text);
        }

        /// <summary>
        /// Determines whether the specified text is number.
        /// </summary>
        private Boolean isNumber(String text)
        {
            return IsFilteringNumeric && numericRegularExpression.IsMatch(text);
        }

        /// <summary>
        /// Determines whether the specified text is alphabetic.
        /// </summary>
        private Boolean isAlphabetic(String text)
        {
            var isLowerCase = IsLowerCase && lowerCaseRegularExpression.IsMatch(text);
            var isUpperCase = IsUpperCase && upperCaseRegularExpression.IsMatch(text);
            var isAlphabetic = IsFilteringAlphabetic && alphabeticRegularExpression.IsMatch(text);

            return isLowerCase || isUpperCase || isAlphabetic;
        }
    }
}