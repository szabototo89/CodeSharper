using System;

namespace CodeSharper.Core.Texts
{
    public class FilterEvenPosition : FilterPosition
    {
        public override bool Filter(Int32 position)
        {
            return (position + 1) % 2 == 0;
        }
    }
}