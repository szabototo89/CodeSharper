using CodeSharper.Core.Common.Runnables.Converters;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common.Runnables.StringTransformation
{
    [Consumes(typeof(ValueArgumentUnwrapper<TextRange>)), Produces(typeof(ValueArgumentWrapper<TextRange>))]
    [Consumes(typeof(MultiValueArgumentUnwrapper<TextRange>)), Produces(typeof(MultiValueArgumentWrapper<TextRange>))]
    public class ToLowerCaseRunnable : StringTransformationRunnable
    {
        public ToLowerCaseRunnable()
            : base(parameter => parameter.ToLowerInvariant())
        {
            
        }
    }
}