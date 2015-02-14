using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.ErrorHandling
{
    public static class ThrowHelper
    {
        public static Exception Exception(String message)
        {
            return new Exception(message);
        }

        public static NotImplementedException NotImplementedMethod()
        {
            return new NotImplementedException();
        }
    }
}
