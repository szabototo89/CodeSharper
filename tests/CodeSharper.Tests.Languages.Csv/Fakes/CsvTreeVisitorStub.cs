using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using CodeSharper.Languages.Csv.Visitors;
using Grammar;

namespace CodeSharper.Tests.Languages.Csv.Fakes
{
    [Flags]
    public enum CsvLanguageElements
    {
        Row = 1,
        Field = 2
    }

    public class CsvTreeVisitorStub : CsvBaseVisitor<CsvTreeVisitorStub>
    {
        private readonly List<String> _visitedRules;

        public CsvLanguageElements SupportedLanguageElements { get; set; }

        public CsvTreeVisitorStub()
        {
            _visitedRules = new List<String>();
        }

        public IEnumerable<String> GetVisitedRules()
        {
            return _visitedRules.AsReadOnly();
        }

        public CsvTreeVisitorStub(CsvLanguageElements supportedLanguageElements)
        {
            SupportedLanguageElements = supportedLanguageElements;
        }

        public override CsvTreeVisitorStub VisitField(CsvParser.FieldContext context)
        {
            updateVisitedRules(context, CsvLanguageElements.Field);
            return this;
        }

        public override CsvTreeVisitorStub VisitRow(CsvParser.RowContext context)
        {
            updateVisitedRules(context, CsvLanguageElements.Row);
            return this;
        }

        private void updateVisitedRules(RuleContext context, CsvLanguageElements languageElement)
        {
            if (SupportedLanguageElements.HasFlag(languageElement))
            {
                _visitedRules.Add(String.Format("{0}({1})", languageElement, context.GetText()));
            }
        }
    }
}
