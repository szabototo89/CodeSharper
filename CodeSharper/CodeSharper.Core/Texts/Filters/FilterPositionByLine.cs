using System;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Texts.Filters
{
    public class FilterPositionByLine : FilterPosition
    {
        public int Line { get; protected set; }

        public FilterPositionByLine(int line)
        {
            Constraints.IsGreaterThan(() => line, -1);
            Line = line;
        }

        public override bool Filter(Int32 position)
        {
            return position == Line;
        }
    }
}