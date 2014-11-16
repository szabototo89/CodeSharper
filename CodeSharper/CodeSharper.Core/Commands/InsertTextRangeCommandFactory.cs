using System;
using System.Reflection;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.StringTransformation;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Commands
{
    public class InsertTextRangeCommandFactory : CommandFactoryBase
    {
        private String _value;
        private Int32 _startIndex;
        private readonly string ARGUMENT_VALUE = "value";
        private readonly string ARGUMENT_START_INDEX = "startIndex";

        protected override void MapArguments(CommandArgumentCollection arguments)
        {
            base.MapArguments(arguments);

            var resolver = new CommandArgumentResolver(Descriptor, arguments);
            resolver
                .UpdateArgument(ref _value, ARGUMENT_VALUE)
                .UpdateArgument(ref _startIndex, ARGUMENT_START_INDEX);
        }

        protected override IRunnable CreateRunnable()
        {
            return new InsertTextRangeRunnable(_startIndex, _value);
        }
    }
}