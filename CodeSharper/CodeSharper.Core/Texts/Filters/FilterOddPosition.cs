namespace CodeSharper.Core.Texts.Filters
{
    public class FilterOddPosition : FilterPosition
    {
        public override bool Filter(int position)
        {
            return (position + 1) % 2 != 0;
        }
    }
}