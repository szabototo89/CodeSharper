namespace CodeSharper.Core.Common.Runnables.StringTransformation
{
    public class ToLowerCaseRunnable : StringTransformationRunnable
    {
        public ToLowerCaseRunnable()
            : base(parameter => parameter.ToLowerInvariant())
        {
            
        }
    }
}