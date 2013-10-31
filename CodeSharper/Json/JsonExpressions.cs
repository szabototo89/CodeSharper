using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using CodeSharper.Common;
using CodeSharper.Utilities;

namespace CodeSharper.Json
{
    public abstract class JsonBaseExpression : JsonNode, IJsonBaseExpression
    {
        protected JsonBaseExpression(IJsonNode parent = null)
            : base(parent) { }

        protected JsonBaseExpression(string text, TextSpan span, IJsonNode parent = null)
            : base(text, span, parent)
        {
        }

        public override string Text
        {
            get
            {
                return string.Concat(_Children.Select(child => child.Text));
            }
        }
    }

    public class JsonLiteralExpression : JsonBaseExpression, IJsonLiteralExpression
    {
        public JsonLiteralExpression(IJsonLiteralToken literal, IJsonNode parent = null)
            : base(parent)
        {
            if (literal == null)
                throw new ArgumentNullException("literal");

            Literal = literal;
            Span = literal.Span;
            _Children.Add(literal);
        }

        public IJsonLiteralToken Literal { get; protected set; }

        public bool IsString { get { return Literal is IJsonStringToken; } }
        public bool IsBoolean { get { return Literal is IJsonBooleanToken; } }
        public bool IsNumber { get { return Literal is IJsonNumberToken; } }
    }

    public class JsonExpressionItem : JsonNode, IJsonExpressionItem
    {
        public JsonExpressionItem(IJsonId id, IJsonSeparator separator, IJsonBaseExpression expression, IJsonNode parent = null)
            : base(parent)
        {
            Id = id;
            Separator = separator;
            Expression = expression;

            Initialize(id, separator, expression);
        }

        private void Initialize(IJsonId id, IJsonSeparator separator, IJsonBaseExpression expression)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (separator == null) throw new ArgumentNullException("separator");
            if (expression == null) throw new ArgumentNullException("expression");

            _Children.AddRange(id, separator, expression);
            Text = string.Concat(id.Text, separator.Text, expression.Text);
            Span = new TextSpan(id.Span.Start, expression.Span.End);
        }

        public IJsonId Id { get; protected set; }
        public IJsonSeparator Separator { get; protected set; }
        public IJsonBaseExpression Expression { get; protected set; }
    }

    public class JsonArrayExpression : JsonBaseExpression, IJsonArrayExpression
    {
        private readonly List<IJsonBaseExpression> _Items;

        public IJsonLeftBracket LeftBracket { get; protected set; }
        public IJsonRightBracket RightBracket { get; protected set; }

        public IEnumerable<IJsonBaseExpression> Items { get { return _Items; } }

        public JsonArrayExpression(IEnumerable<IJsonNode> items, IJsonNode parent = null)
            : base(parent)
        {
            if (items == null) throw new ArgumentNullException("items");

            _Items = new List<IJsonBaseExpression>();

            Initialize(items);
        }

        private JsonArrayExpression Initialize(IEnumerable<IJsonNode> items)
        {
            _Children.Clear();
            _Children.AddRange(items);

            if (!Children.Any())
                throw new ArgumentException("Cannot be empty!", "items");

            LeftBracket = Children.First() as IJsonLeftBracket;
            RightBracket = Children.Last() as IJsonRightBracket;

            if (new IJsonBracket[] { LeftBracket, RightBracket }
                    .Any(bracket => bracket == null || bracket.BracketType != JsonBracketType.Square)) {
                throw new Exception("Invalid brackets!");
            }

            Text = string.Concat(_Children.Select(item => item.Text));
            Span = new TextSpan(_Children.First().Span.Start, _Children.Last().Span.End);

            _Items.AddRange(Children.OfType<IJsonBaseExpression>());

            return this;
        }
    }

    public class JsonExpression : JsonBaseExpression, IJsonExpression
    {
        public IJsonLeftBracket LeftBracket { get; protected set; }
        public IJsonRightBracket RightBracket { get; protected set; }
        public IEnumerable<IJsonExpressionItem> Items { get; protected set; }

        public JsonExpression(IEnumerable<IJsonNode> items, IJsonNode parent = null)
            : base(parent)
        {
            if (items == null) throw new ArgumentNullException("items");
            Initialize(items);
        }

        private JsonExpression Initialize(IEnumerable<IJsonNode> items)
        {
            _Children.Clear();
            _Children.AddRange(items);

            if (!_Children.Any())
                throw new ArgumentException("Cannot be empty!", "items");

            LeftBracket = _Children.First() as IJsonLeftBracket;
            RightBracket = _Children.Last() as IJsonRightBracket;

            if (new IJsonBracket[] { LeftBracket, RightBracket }.Any(
                    bracket => bracket == null || bracket.BracketType != JsonBracketType.Curly)) {
                throw new Exception("Invalid brackets!");
            }

            Text = string.Concat(_Children.Select(child => child.Text));
            Span = new TextSpan(_Children.First().Span.Start, _Children.Last().Span.End);

            Items = _Children.OfType<IJsonExpressionItem>();

            return this;
        }
    }
}