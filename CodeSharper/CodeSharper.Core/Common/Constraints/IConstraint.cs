using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Common.Constraints
{
    public interface IConstraint<out TValue>
    {
        TValue Value { get; }

        bool Matches();
    }
}
