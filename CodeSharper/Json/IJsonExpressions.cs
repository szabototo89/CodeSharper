using System.Collections.Generic;

namespace CodeSharper.Json
{
    public interface IJsonBaseExpression : IJsonNode
    {
    }

    public interface IJsonArrayExpression : IJsonBaseExpression
    {
        IJsonLeftBracket LeftBracket { get; }
        IJsonRightBracket RightBracket { get; }

        IEnumerable<IJsonBaseExpression> Items { get; }
    }

    public interface IJsonExpressionItem : IJsonNode
    {
        IJsonId Id { get; }
        IJsonSeparator Separator { get; }
        IJsonBaseExpression Expression { get; }
    }

    public interface IJsonExpression : IJsonBaseExpression
    {
        IJsonLeftBracket LeftBracket { get; }
        IJsonRightBracket RightBracket { get; }

        IEnumerable<IJsonExpressionItem> Items { get; } 
    }

    public interface IJsonLiteralExpression : IJsonBaseExpression
    {
        IJsonLiteralToken Literal { get; }
    }
}