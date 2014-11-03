using System;
using CodeSharper.Core.Commands;

namespace CodeSharper.Core.Utilities
{
    public interface ICommandDescriptorParser
    {
        CommandDescriptor Parse(String source);
    }
}