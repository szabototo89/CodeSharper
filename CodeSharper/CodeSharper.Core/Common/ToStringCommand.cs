﻿using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common
{
    public class ToStringCommand : ICommand
    {
        public Argument Execute(Argument parameter)
        {
            if (parameter is IValueArgument)
                return Execute(parameter as IValueArgument);

            return null;
        }

        protected ValueArgument<string> Execute(IValueArgument parameter)
        {
            return Arguments.Value(parameter.Value.ToString());
        }
    }
}