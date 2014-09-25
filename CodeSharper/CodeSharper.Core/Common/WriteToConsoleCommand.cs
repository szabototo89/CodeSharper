using System;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common
{
    public class WriteToConsoleCommand : ValueCommand<ValueArgument<string>>
    {
        protected override ValueArgument<string> Execute(ValueArgument<string> parameter)
        {
            Console.WriteLine(parameter.Value);
            return parameter;
        }
    }
}