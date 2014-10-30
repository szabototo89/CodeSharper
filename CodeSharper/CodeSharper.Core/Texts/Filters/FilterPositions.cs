using System;

namespace CodeSharper.Core.Texts.Filters
{
    public static class FilterPositions
    {
        public static readonly FilterPosition Even = new FilterEvenPosition();
        public static readonly FilterPosition Odd = new FilterOddPosition();

        public static FilterPosition ByLine(Int32 line)
        {
            return new FilterPositionByLine(line);
        }
    }
}