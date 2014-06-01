namespace CodeSharper.Core.Csv.Nodes
{
    public class CommaNode : DelimiterNode
    {
        #region Public properties

        public override string Delimiter
        {
            get { return ","; }
        }

        #endregion
    }
}