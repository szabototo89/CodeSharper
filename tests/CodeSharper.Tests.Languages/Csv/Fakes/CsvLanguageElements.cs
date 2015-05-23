using System;

namespace CodeSharper.Tests.Languages.Csv.Fakes
{
    [Flags]
    public enum CsvLanguageElements
    {
        Row = 1,
        TextField = 2,
        StringField = 4,
        Field = TextField | StringField
    }
}