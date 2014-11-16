using System;

namespace CodeSharper.DemoRunner.Models
{
    public struct DemoApplicationDescriptor
    {
        public String  Name { get; set; }

        public String Description { get; set; }

        public Action Run { get; set; }
    }
}