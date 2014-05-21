namespace CodeSharper.Core.Csv.Nodes
{
    public abstract class DelimiterNode : CsvMutableNode
    {
        public abstract string Delimiter { get; }

        protected override void InitializeNodeTypeDescriptor(CsvNodeTypeDescriptor nodeTypeDescriptor)
        {
            nodeTypeDescriptor.Type = CsvNodeType.Delimiter;
        }
    }
}