using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Mocks
{
    internal class GeneralObjectMocks
    {
        public class Person
        {
            public String Name { get; set; }

            public Int32 Age { get; set; }

            public static Person JohnDoe
            {
                get { return new Person { Name = "John Doe", Age = 24 }; }
            }
        }

        public class Employee : Person
        {
            public Address Address { get; set; }
        }

        public class Address
        {
            public String City { get; set; }

            public String Street { get; set; }
        }
    }
}
