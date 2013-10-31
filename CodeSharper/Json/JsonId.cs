using System;
using CodeSharper.Common;

namespace CodeSharper.Json
{
    public class JsonId : JsonNode, IJsonId
    {
        public JsonIdType IdType { get; protected set; }

        public JsonId(JsonIdType type, string text, TextSpan span, JsonNode parent)
            : base(text, span, parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            IdType = type;
        }

        public JsonId(string text, TextSpan span, JsonNode parent)
            : base(text, span, parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            IdType = GetTypeByString(text.Trim());
        }

        private static JsonIdType GetTypeByString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new NotSupportedException("Value cannot be blank string!");

            int number = 0;

            if (int.TryParse(value, out number))
                return JsonIdType.Integer;

            foreach (var stringLiteral in new[] {"'", "\""})
            {
                if (value.StartsWith(stringLiteral) && value.EndsWith(stringLiteral))
                    return JsonIdType.String;
            }
            
            return JsonIdType.Identifier;
        }
    }
}