using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.DemoRunner.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DemoAttribute : Attribute
    {
        public String Name { get; set; }

        public String Description { get; set; }

        public DemoAttribute(String name)
        {
            Name = name;
        }
    }
}
