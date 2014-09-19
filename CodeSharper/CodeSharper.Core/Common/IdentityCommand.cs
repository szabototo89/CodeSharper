using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.Values;

namespace CodeSharper.Core.Common
{
    public class IdentityCommand : ICommand
    {
        public Argument Execute(Argument parameter)
        {
            return parameter;
        }
    }
}
