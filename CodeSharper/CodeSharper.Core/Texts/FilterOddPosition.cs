namespace CodeSharper.Core.Texts
{
    public class FilterOddPosition : FilterPosition
    {
        public override bool Filter(int position)
        {
            return (position + 1) % 2 != 0;
        }
    }
}