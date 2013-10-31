using CodeSharper.Common;

namespace CodeSharper.Json
{
    public interface IJsonBracket : IJsonTokenNode
    {
        JsonBracketType BracketType { get; }
    }

    public interface IJsonLeftBracket : IJsonBracket
    {
    }

    public interface IJsonRightBracket : IJsonBracket
    {
    }
}