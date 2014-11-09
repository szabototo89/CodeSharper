using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Utilities
{
    public interface IMonadic<out TValue>
    {
        IMonadic<TResult> Map<TResult>(Func<TValue, TResult> transform);

        IMonadic<TValue> Filter(Predicate<TValue> predicate);

    }
}
