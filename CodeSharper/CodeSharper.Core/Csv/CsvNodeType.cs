namespace CodeSharper.Core.Csv
{
    public class CsvNodeType : NodeType
    {
        public static readonly NodeType Column;
        
        public static readonly NodeType Field;

        public static readonly NodeType Record;

        public static readonly NodeType Delimiter;

        static CsvNodeType()
        {
            Record = new NodeType();
            Delimiter = new NodeType();
            Field = new NodeType();
            Column = new NodeType();
        }
    }
}