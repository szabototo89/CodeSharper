using System;
using System.Runtime.InteropServices;

namespace CodeSharper.DemoRunner.DemoApplications.CodeREPL
{
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