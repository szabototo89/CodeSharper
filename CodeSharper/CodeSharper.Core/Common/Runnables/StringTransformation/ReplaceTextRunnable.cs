using System;

namespace CodeSharper.Core.Common.Runnables.StringTransformation
{
    public class ReplaceTextRunnable : StringTransformationRunnable
    {
        public ReplaceTextRunnable(String replacedText) 
            : base(parameter => replacedText)
        {
        }
    }
}