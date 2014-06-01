namespace CodeSharper.Core.Csv
{
    public class CsvLanguageDescriptor : LanguageDescriptor
    {
        public static LanguageDescriptor Default;

        #region Constructors

        static CsvLanguageDescriptor()
        {
            Default = new CsvLanguageDescriptor();
        }

        protected CsvLanguageDescriptor() { }

        #endregion

        #region Public properties

        public override string Name
        {
            get { return "CSV"; }
        }

        #endregion
    }
}