using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Languages.Csv.Grammar;
using CodeSharper.Languages.Csv.SyntaxTrees;
using CodeSharper.Languages.Csv.Visitors;

namespace CodeSharper.Tests.Languages.Csv.Fakes
{
    public class CsvTreeVisitorMock : CsvBaseVisitor<CsvNode>,
                                      ISyntaxTreeVisitor<CsvNode, IParseTree>
    {
        private readonly List<String> _visitedRules;

        public String Result { get; protected set; }

        public CsvLanguageElements SupportedLanguageElements { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTreeVisitorMock"/> class.
        /// </summary>
        public CsvTreeVisitorMock()
        {
            _visitedRules  = new List<String>();
        }

        public CsvNode Visit(String input, IParseTree tree)
        {
            var result = this.Visit(tree);
            return result;
        }

        public override CsvNode VisitField(CsvParser.FieldContext context)
        {
            updateVisitedRules(context, CsvLanguageElements.Field);
            return null;
        }

        public override CsvNode VisitRow(CsvParser.RowContext context)
        {
            updateVisitedRules(context, CsvLanguageElements.Row);

            foreach (var field in context.field())
            {
                field.Accept(this);
            }

            return null;
        }

        private void updateVisitedRules(RuleContext context, CsvLanguageElements languageElement)
        {
            if (SupportedLanguageElements.HasFlag(languageElement))
            {
                _visitedRules.Add(String.Format("{0}({1})", languageElement, context.GetText()));
            }
        }
    }

    public class CsvTreeVisitorStub : CsvBaseVisitor<CsvTreeVisitorStub>, 
                                      ISyntaxTreeVisitor<CsvTreeVisitorStub, IParseTree>
    {
        private readonly List<String> _visitedRules;

        public CsvLanguageElements SupportedLanguageElements { get; set; }

        public String Result { get; protected set; }

        public IEnumerable<String> GetVisitedRules()
        {
            return _visitedRules.AsReadOnly();
        }

        public CsvTreeVisitorStub()
            : this(CsvLanguageElements.Field | CsvLanguageElements.Row)
        {
        }

        public CsvTreeVisitorStub(CsvLanguageElements supportedLanguageElements)
        {
            _visitedRules = new List<String>();
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

            foreach (var field in context.field())
            {
                field.Accept(this);
            }

            return this;
        }

        private void updateVisitedRules(RuleContext context, CsvLanguageElements languageElement)
        {
            if (SupportedLanguageElements.HasFlag(languageElement))
            {
                _visitedRules.Add(String.Format("{0}({1})", languageElement, context.GetText()));
            }
        }

        public CsvTreeVisitorStub Visit(String input, IParseTree tree)
        {
            Visit(tree);
            Result = String.Join(Environment.NewLine, _visitedRules);
            return this;
        }
    }
}
