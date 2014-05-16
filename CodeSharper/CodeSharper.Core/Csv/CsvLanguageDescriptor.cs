namespace CodeSharper.Core.Csv
{
    public class CsvLanguageDescriptor : LanguageDescriptor
    {
        public static LanguageDescriptor Default;

        static CsvLanguageDescriptor()
        {
            Default = new CsvLanguageDescriptor();
        }

        protected CsvLanguageDescriptor() { }

        public override string Name
        {
            get { return "CSV"; }
        }
    }
}