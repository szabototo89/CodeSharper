using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Common;
using NUnit.Framework;

namespace CodeSharper.Tests.Common
{
    [TestFixture]
    class TextPositionTestFixture
    {
        [Test]
        public void OffsetByStringAndCompareToTest()
        {
            var position = new TextPosition(1, 1);
            var newPosition = position.OffsetByString("1234\n" +
                                                      Environment.NewLine +
                                                      "1234");

            Assert.AreEqual(newPosition.CharPosition, 5, "CharPosition");
            Assert.AreEqual(newPosition.Line, 3, "Line");

            Assert.IsTrue(position.CompareTo(newPosition) < 0);
            Assert.IsTrue(position.CompareTo(position) == 0);
            Assert.IsTrue(newPosition.CompareTo(position) > 0);
        }
    }
}
