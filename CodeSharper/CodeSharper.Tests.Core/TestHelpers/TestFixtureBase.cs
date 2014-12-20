using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.TestHelpers
{
    /// <summary>
    /// Abstract class for testing
    /// </summary>
    internal abstract class TestFixtureBase
    {
        [SetUp]
        public virtual void Setup() { }

        [TearDown]
        public virtual void Teardown() { }
    }
}
