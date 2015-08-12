using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;
using CodeSharper.Languages.Json.SyntaxTrees;
using CodeSharper.Languages.Json.SyntaxTrees.Constants;
using CodeSharper.Languages.Json.SyntaxTrees.Literals;
using CodeSharper.Languages.Json.SyntaxTrees.Tokens;

namespace CodeSharper.Languages.Json.Factories
{
    public interface IJsonSyntaxTreeFactory
    {
        /// <summary>
        /// Creates a boolean constant.
        /// </summary>
        JsonNode CreateBooleanConstant(TextRange textRange);

        /// <summary>
        /// Creates a number constant.
        /// </summary>
        JsonNode CreateNumberConstant(TextRange textRange);

        /// <summary>
        /// Creates a string constant.
        /// </summary>
        JsonNode CreateStringConstant(TextRange textRange);

        /// <summary>
        /// Creates a key.
        /// </summary>
        JsonNode CreateKey(String value, TextRange textRange);

        /// <summary>
        /// Creates a value.
        /// </summary>
        JsonNode CreateValue(ConstantSyntax value, TextRange textRange);

        /// <summary>
        /// Creates a value.
        /// </summary>
        JsonNode CreateValue(LiteralSyntax value, TextRange textRange);

        /// <summary>
        /// Creates a key value pair.
        /// </summary>
        JsonNode CreateKeyValuePair(TextRange textRange, KeyDeclaration key, ValueDeclaration value);

        /// <summary>
        /// Creates a array literal.
        /// </summary>
        JsonNode CreateArrayLiteral(IEnumerable<ValueDeclaration> values, TextRange textRange);

        /// <summary>
        /// Creates a object literal.
        /// </summary>
        JsonNode CreateObjectLiteral(IEnumerable<KeyValueDeclaration> keyValuePairs, TextRange textRange);
    }

    public class StandardJsonSyntaxTreeFactory : IJsonSyntaxTreeFactory
    {
        /// <summary>
        /// Creates the boolean constant.
        /// </summary>
        public JsonNode CreateBooleanConstant(TextRange textRange)
        {
            Assume.NotNull(textRange, nameof(textRange));
            var text = textRange.GetText();
            Boolean value;
            if (text == "false") value = false;
            else if (text == "true") value = true;
            else throw new NotSupportedException(String.Format("Invalid text of boolean constant: {0}.", text));

            return new BooleanConstant(value, textRange);
        }

        /// <summary>
        /// Creates the number constant.
        /// </summary>
        public JsonNode CreateNumberConstant(TextRange textRange)
        {
            Assume.NotNull(textRange, nameof(textRange));
            var text = textRange.GetText();
            Decimal value;

            if (!Decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
                throw new NotSupportedException(String.Format("Not supported number format: {0}", text));

            return new NumberConstant(value, textRange);
        }

        /// <summary>
        /// Creates the string constant.
        /// </summary>
        public JsonNode CreateStringConstant(TextRange textRange)
        {
            Assume.NotNull(textRange, nameof(textRange));
            var value = textRange.GetText();
            return new StringConstant(value, textRange);
        }

        /// <summary>
        /// Creates the key of key-value pair
        /// </summary>
        public JsonNode CreateKey(String value, TextRange textRange)
        {
            Assume.NotNull(value, nameof(value));
            Assume.NotNull(textRange, nameof(textRange));
            value = value.Trim('\"');
            return new KeyDeclaration(value, textRange);
        }

        /// <summary>
        /// Creates the value.
        /// </summary>
        public JsonNode CreateValue(ConstantSyntax value, TextRange textRange)
        {
            Assume.NotNull(value, nameof(value));
            Assume.NotNull(textRange, nameof(textRange));

            return new ValueDeclaration(value, textRange);
        }

        /// <summary>
        /// Creates the value.
        /// </summary>
        public JsonNode CreateValue(LiteralSyntax value, TextRange textRange)
        {
            Assume.NotNull(value, nameof(value));
            Assume.NotNull(textRange, nameof(textRange));

            return new ValueDeclaration(value, textRange);
        }

        public JsonNode CreateKeyValuePair(TextRange textRange, KeyDeclaration key, ValueDeclaration value)
        {
            Assume.NotNull(textRange, nameof(textRange));
            Assume.NotNull(key, nameof(key));
            Assume.NotNull(value, nameof(value));

            return new KeyValueDeclaration(key, value, textRange);
        }

        /// <summary>
        /// Creates the array literal.
        /// </summary>
        public JsonNode CreateArrayLiteral(IEnumerable<ValueDeclaration> values, TextRange textRange)
        {
            Assume.NotNull(values, nameof(values));        
            Assume.NotNull(textRange, nameof(textRange));
    
            return new ArrayLiteralDeclaration(values.Select(value => value.Value).ToArray(), textRange);
        }

        /// <summary>
        /// Creates a object literal.
        /// </summary>
        public JsonNode CreateObjectLiteral(IEnumerable<KeyValueDeclaration> keyValuePairs, TextRange textRange)
        {
            Assume.NotNull(keyValuePairs, nameof(keyValuePairs));
            Assume.NotNull(textRange, nameof(textRange));

            return new ObjectLiteralDeclaration(keyValuePairs, textRange);
        }
    }
}