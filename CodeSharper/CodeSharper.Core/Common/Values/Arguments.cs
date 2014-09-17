namespace CodeSharper.Core.Common.Values
{
    public static class Arguments
    {
        public static ValueArgument<T> Value<T>(T value)
        {
            return new ValueArgument<T>(value);
        } 
    }
}