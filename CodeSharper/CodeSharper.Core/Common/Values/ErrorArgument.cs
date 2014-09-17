using System;

namespace CodeSharper.Core.Common.Values
{
    public class ErrorArgument : Argument
    {
        public ErrorArgument(String message = "")
        {
            Value = message;
        }

        public String Value { get; private set; }
    }
}