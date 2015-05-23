using System;

namespace CodeSharper.Languages.Json.SyntaxTrees.Tokens
{
    [Flags]
    public enum ParenthesisType
    {
        ArrayType = 1,
        ObjectType = 2,
        Left = 4,
        Right = 8
    }
}