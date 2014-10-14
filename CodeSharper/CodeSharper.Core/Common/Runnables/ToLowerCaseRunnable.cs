namespace CodeSharper.Core.Common.Runnables
{
    public class ToLowerCaseRunnable : StringTransformationRunnable
    {
        public ToLowerCaseRunnable()
            : base(parameter => parameter.ToLowerInvariant())
        {
            
        }
    }
}