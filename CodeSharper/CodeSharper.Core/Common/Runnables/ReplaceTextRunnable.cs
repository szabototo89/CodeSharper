using System;

namespace CodeSharper.Core.Common.Runnables
{
    public class ReplaceTextRunnable : StringTransformationRunnable
    {
        public ReplaceTextRunnable(String replacedText) 
            : base(parameter => replacedText)
        {
        }
    }
}