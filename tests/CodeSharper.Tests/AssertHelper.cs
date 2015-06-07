using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeSharper.Tests
{
    public static partial class AssertHelper
    {
        public static void AreEqualByValue<T>(T x, T y)
        {
            Assert.That(x, Is.Not.Null);
            Assert.That(y, Is.Not.Null);
            Assert.That(x, Is.Not.SameAs(y), "x and y objects are the same.");

            Assert.That(x, Is.EqualTo(y), "x and y are not equal.");
            Assert.That(x.GetHashCode(), Is.EqualTo(y.GetHashCode()), "x and y GetHashCode() are not equal.");
        }

        public static void AreNotEqualByValue<T>(T x, T y)
        {
            Assert.That(x, Is.Not.SameAs(y), "x and y objects are the same.");
            Assert.That(x, Is.Not.EqualTo(y), "x and y are equal.");
            Assert.That(x.GetHashCode(), Is.Not.EqualTo(y.GetHashCode()), "x and y GetHashCode() are equal.");
        }
    }
}