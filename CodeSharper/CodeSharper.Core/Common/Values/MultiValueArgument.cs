using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodeSharper.Core.Common.Values
{
    public class MultiValueArgument : Argument
    {
        private readonly IEnumerable<Argument> _source;

        public IEnumerable<Argument> Source { get { return _source; } }

        public MultiValueArgument(IEnumerable<Argument> source)
        {
            _source = source ?? Enumerable.Empty<Argument>();
        }
    }
}