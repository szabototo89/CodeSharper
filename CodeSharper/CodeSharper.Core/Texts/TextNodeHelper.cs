﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Texts
{
    public static class TextNodeHelper
    {
        public static Boolean IsRoot(this TextNode node)
        {
            Constraints.NotNull(() => node);

            return node.Parent == null;
        }
    }
}