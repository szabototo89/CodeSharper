namespace CodeSharper.Core.Csv.Nodes
{
    public abstract class DelimiterNode : CsvMutableNode
    {
        #region Public properties

        public abstract string Delimiter { get; }

        #endregion

        #region Protected methods

        protected override void InitializeNodeTypeDescriptor(CsvNodeTypeDescriptor nodeTypeDescriptor)
        {
            nodeTypeDescriptor.Type = CsvNodeType.Delimiter;
        }

        #endregion
    }
}