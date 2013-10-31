using System.CodeDom;

namespace CodeSharper.Json
{
    public interface IJsonId : IJsonNode
    {
        JsonIdType IdType { get; }
    }
}