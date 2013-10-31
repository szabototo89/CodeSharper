using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Common
{
    public interface IParent<out TParentType>
    {
        TParentType Parent { get; }
    }
}
