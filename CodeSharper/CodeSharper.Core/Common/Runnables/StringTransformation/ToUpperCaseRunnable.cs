namespace CodeSharper.Core.Common.Runnables.StringTransformation
{
    public class ToUpperCaseRunnable : StringTransformationRunnable
    {
        public ToUpperCaseRunnable() : base(parameter => parameter.ToUpperInvariant())
        {
            
        }
    }
}