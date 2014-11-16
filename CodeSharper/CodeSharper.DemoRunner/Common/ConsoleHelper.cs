using System;
using System.Runtime.InteropServices;

namespace CodeSharper.DemoRunner.Common
{
    public class ConsoleHelper
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern Boolean AttachConsole(int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern Boolean FreeConsole();

        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        public static void OpenConsoleWindow()
        {
            if (!AttachConsole(-1))
                AllocConsole();
        }

        public static void CloseConsoleWindow()
        {
            FreeConsole();
        }
    }
}