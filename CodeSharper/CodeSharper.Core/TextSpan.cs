using CodeSharper.Core.Common;

namespace CodeSharper.Core
{
    public struct TextSpan
    {
        private readonly TextLocation _start;
        private readonly TextLocation _stop;

        public TextSpan(TextLocation start, TextLocation stop)
        {
            if (start.CompareTo(stop) > 0)
                throw ThrowHelper.ArgumentException(message: "Start must be lesser than stop!");

            _start = start;
            _stop = stop;
        }

        public TextLocation Start
        {
            get { return _start; }
        }

        public TextLocation Stop
        {
            get { return _stop; }
        }

        public TextSpan Offset(TextLocation location)
        {
            return this;
        }
    }
}