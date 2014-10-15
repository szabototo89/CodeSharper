using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Values;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Commands
{
    public class ToUpperCaseCommand : StringTransformationCommand
    {
        public ToUpperCaseCommand() 
            : base(text => text.ToUpperInvariant())
        {
        }
    }
}
