namespace CodeSharper.Core.Common
{
    public interface IIdentifiable<out TIdType>
    {
        TIdType Id { get; }
    }
}