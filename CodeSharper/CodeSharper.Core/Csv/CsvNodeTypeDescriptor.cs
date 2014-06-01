using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core;

namespace CodeSharper.Core.Csv
{
    public class CsvNodeTypeDescriptor : NodeTypeDescriptor
    {
        #region Constructors

        public CsvNodeTypeDescriptor()
        {
            Language = CsvLanguageDescriptor.Default;
        }

        #endregion
    }
}
