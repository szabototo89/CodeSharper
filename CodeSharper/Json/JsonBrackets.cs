using System;
using CodeSharper.Common;

namespace CodeSharper.Json
{
    public class JsonLeftBracket : JsonTokenNode, IJsonLeftBracket
    {
        public JsonLeftBracket(JsonBracketType bracketType, string text, TextSpan span, IJsonNode parent = null)
            : base(text, span, parent)
        {
            BracketType = bracketType;

            switch (bracketType)
            {
                case JsonBracketType.Curly:
                    Value = "{";
                    break;
                case JsonBracketType.Square:
                    Value = "[";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("bracketType");
            }
        }

        public JsonBracketType BracketType { get; protected set; }
    }

    public class JsonRightBracket : JsonTokenNode, IJsonRightBracket
    {
        public JsonRightBracket(JsonBracketType bracketType, string text, TextSpan span, IJsonNode parent = null)
            : base(text, parent)
        {
            BracketType = bracketType;

            Span = span;
            
            switch (bracketType)
            {
                case JsonBracketType.Curly:
                    Value = "}";
                    break;
                case JsonBracketType.Square:
                    Value = "]";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("bracketType");
            }
        }

        public JsonBracketType BracketType { get; protected set; }
    }

}