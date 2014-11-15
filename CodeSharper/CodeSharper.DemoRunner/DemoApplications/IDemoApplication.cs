using System;
using System.Security.Cryptography.X509Certificates;

namespace CodeSharper.DemoRunner.DemoApplications
{
    public interface IDemoApplication
    {
        void Run(String[] args);
    }
}