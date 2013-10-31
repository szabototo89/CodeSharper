using System;
using System.Reflection.Emit;
using CodeSharper.Common;

namespace CodeSharper.Json
{
    public interface IJsonTokenValue<out TTokenValueType>
    {
        TTokenValueType TokenValue { get; }
    }

    public interface IJsonTokenNode : IJsonNode
    {
        string Value { get; }
    }

    public interface IJsonSeparator : IJsonTokenNode
    {
      
    }

    public interface IJsonComma : IJsonTokenNode { }

    public interface IJsonLiteralToken : IJsonTokenNode
    {
        
    }

    public interface IJsonStringToken : IJsonLiteralToken, IJsonTokenValue<string>
    {

    }

    public interface IJsonNumberToken : IJsonLiteralToken, IJsonTokenValue<double>
    {

    }

    public interface IJsonBooleanToken : IJsonLiteralToken, IJsonTokenValue<bool>
    {

    }
}