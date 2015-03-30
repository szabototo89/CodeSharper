using System;
using System.Text.RegularExpressions;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Csv.SyntaxTrees
{
    public class FieldDeclarationSyntax : CsvNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldDeclarationSyntax"/> class.
        /// </summary>
        /// <param name="textRange"></param>
        public FieldDeclarationSyntax(TextRange textRange)
            : base(textRange)
        {
        }

        /// <summary>
        /// Gets the text of text range
        /// </summary>
        protected String Text
        {
            get { return TextDocument.GetText(TextRange); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is text field.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is text field; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsStringField
        {
            get { return Regex.IsMatch(Text, "^\"(\"\"|([^\"]))\"$"); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is empty field.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is empty field; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsEmptyField
        {
            get { return Regex.IsMatch(Text, @"^\s*$"); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is String field.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is String field; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsTextField
        {
            get
            {
                return !IsStringField && Regex.IsMatch(Text, @"^[^,\n\r]+$");
            }
        }
    }
}