using System;
using System.Collections.Generic;
using CodeSharper.Common;
using CodeSharper.Utilities;

namespace CodeSharper.Json
{
    public abstract class JsonTokenNode : JsonNode, IJsonTokenNode
    {
        public string Value { get; protected set; }

        protected JsonTokenNode(string text, IJsonNode parent)
            : base(parent)
        {
            Text = text;
            Span = TextSpan.GetFromString(text);
        }

        protected JsonTokenNode(string text, TextSpan span, IJsonNode parent = null)
            : base(text, span, parent)
        {
            Value = text.Trim();
        }
    }

    public class JsonComma : JsonTokenNode, IJsonComma
    {
        public JsonComma(string text, IJsonNode parent = null)
            : base(text, parent)
        {

        }

        public JsonComma(string text, TextSpan span, IJsonNode parent = null)
            : base(text, span, parent)
        {
            Value = ",";
        }
    }

    public class JsonSeparator : JsonTokenNode, IJsonSeparator
    {
        public JsonSeparator(string text, IJsonNode parent = null)
            : base(text, parent)
        {

        }

        public JsonSeparator(string text, TextSpan span, IJsonNode parent = null)
            : base(text, span, parent)
        {
            Value = ":";
        }
    }

    public class JsonLiteralToken : JsonTokenNode, IJsonLiteralToken
    {
        public JsonLiteralToken(string text, TextSpan span, IJsonNode parent = null)
            : base(text, span, parent)
        {
        }
    }

    public class JsonStringToken : JsonLiteralToken, IJsonStringToken
    {
        public string TokenValue { get; protected set; }

        public JsonStringToken(string text, TextSpan span, IJsonNode parent = null)
            : base(text, span, parent)
        {
            TokenValue = text.Trim();
        }
    }

    public class JsonNumberToken : JsonLiteralToken, IJsonNumberToken
    {
        public double TokenValue { get; protected set; }

        public JsonNumberToken(string text, TextSpan span, IJsonNode parent = null)
            : base(text, span, parent)
        {
            double number;

            if (double.TryParse(text, out number))
                TokenValue = number;
        }
    }

    public class JsonBooleanToken : JsonLiteralToken, IJsonBooleanToken
    {
        public bool TokenValue { get; protected set; }

        public JsonBooleanToken(string text, TextSpan span, IJsonNode parent = null)
            : base(text, span, parent)
        {
            bool value;
            if (bool.TryParse(text, out value))
                TokenValue = value;
        }
    }
}