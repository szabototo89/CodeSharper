using System.Globalization;
using System.Net.Mime;
using System.Reflection;

namespace CodeSharper.Core.Common.Runnables
{
    public class ToUpperCaseRunnable : StringTransformationRunnable
    {
        public ToUpperCaseRunnable() : base(parameter => parameter.ToUpperInvariant())
        {
            
        }
    }
}