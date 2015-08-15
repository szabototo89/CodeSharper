using System;

namespace CodeSharper.Core.Services
{
    public class CommandDescriptorAttribute : Attribute
    {
        public String CommandName { get; set; }

        public String Description { get; set; }

        public CommandDescriptorAttribute(String commandName)
        {
            CommandName = commandName;
        }
    }
}