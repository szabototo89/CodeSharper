﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Csv.Nodes;

namespace CodeSharper.Core.Csv
{
    public class CsvAbstractSyntaxTree : AbstractSyntaxTree
    {
        #region Public properties

        public CsvCompilationUnit Root { get; set; }

        #endregion
    }
}