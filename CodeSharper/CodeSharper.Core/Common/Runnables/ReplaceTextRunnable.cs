using System;
using System.Collections;
using System.Data;

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