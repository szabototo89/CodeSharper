using System;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Common.Runnables.StringTransformation;

namespace CodeSharper.Core.Commands.CommandFactories
{
    public class InsertTextRangeCommandFactory : CommandFactoryBase
    {
        private readonly String ARGUMENT_VALUE = "value";
        private readonly String ARGUMENT_START_INDEX = "startIndex";
        private String _value;
        private Int32 _startIndex;

        [BindTo("value")]
        public String Value
        {
            get { return _value; }
            set { _value = value; }
        }

        [BindTo("startIndex")]
        public Int32 StartIndex
        {
            get { return _startIndex; }
            set { _startIndex = value; }
        }

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
            return new InsertTextRangeRunnable(StartIndex, Value);
        }
    }
}