using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.ConstraintChecking;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Common
{
    public static class Executors
    {
        private static readonly StandardExecutor _standardExecutor;

        static Executors()
        {
            _standardExecutor = new StandardExecutor(RunnableManager.Instance);
        }

        public static StandardExecutor StandardExecutor
        {
            get { return _standardExecutor; }
        }
    }
}
