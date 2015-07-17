using System;
using System.Reflection.Emit;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeSharper.Languages.CSharp.Compiler
{
    public class CSharpCompiler
    {
        public SyntaxNode Parse(String text)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(text);
            return syntaxTree.GetRoot();
        } 
    }
}