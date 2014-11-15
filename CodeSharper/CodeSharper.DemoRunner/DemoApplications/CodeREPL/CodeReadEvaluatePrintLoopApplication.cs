using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.DemoRunner.Common;
using System.Runtime.InteropServices;

namespace CodeSharper.DemoRunner.DemoApplications.CodeREPL
{
    [Demo("CodeREPL", Description = "Read-Evaluate-Print-Loop like application for CodeSharper")]
    public class CodeReadEvaluatePrintLoopApplication : IDemoApplication
    {
        public void Run(String[] args = null)
        {
            args = args ?? Enumerable.Empty<String>().ToArray();

            ConsoleHelper.OpenConsoleWindow();
        }
    }

    public class ConsoleHelper
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern Boolean AttachConsole(int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern Boolean FreeConsole();

        public static Boolean OpenConsoleWindow()
        {
            AttachConsole(-1);
        }
    }
}
