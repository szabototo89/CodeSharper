using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Trees;
using CodeSharper.Languages.Csv.Factories;

namespace CodeSharper.Tests.Languages.Csv.Fakes
{
    internal class CsvTreeFactoryStub : ICsvTreeFactory
    {
        public SyntaxTree GetSyntaxTree()
        {
            return null;
        }

        public ICsvTreeFactory CreateRow(TextRange textRange)
        {
            throw new NotImplementedException();
        }

        public ICsvTreeFactory CreateField(TextRange textRange)
        {
            throw new NotImplementedException();
        }

        public ICsvTreeFactory CreateComma(TextRange textRange)
        {
            throw new NotImplementedException();
        }

        public ICsvTreeFactory CreateDocument(TextRange textRange)
        {
            throw new NotImplementedException();
        }

        public TextDocument GetTextDocument()
        {
            throw new NotImplementedException();
        }
    }
}
