using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Utilities;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Utilities
{
    [TestFixture]
    internal class ObjectCreatorTests : TestFixtureBase
    {
        private class DummyObject
        {
            /// <summary>
            /// Gets or sets the dummy number.
            /// </summary>
            public Int32 DummyNumber { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="DummyObject"/> class.
            /// </summary>
            public DummyObject()
            {
                DummyNumber = 0;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="DummyObject"/> class.
            /// </summary>
            public DummyObject(Int32 dummyNumber)
            {
                DummyNumber = dummyNumber;
            }
        }

        private class LittleBitSmarterObject : DummyObject
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="LittleBitSmarterObject"/> class.
            /// </summary>
            public LittleBitSmarterObject(Int32 dummyNumber) : base(dummyNumber)
            {
            }
        }

        private class DummyObjectDealer
        {
            public DummyObject DummyObject { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="DummyObjectDealer"/> class.
            /// </summary>
            public DummyObjectDealer(Int32 dummyNumber, DummyObject dummyObject)
            {
                DummyObject = dummyObject;
                DummyObject.DummyNumber = dummyNumber;
            }
        }

        [Test(Description = "Create should throw NotSupportedException when specific type is passed")]
        public void Create_ShouldThrowNotSupportedException_WhenSpecificTypeIsPassed()
        {
            // Given
            var underTest = new ObjectCreator();

            // When
            TestDelegate instantiateInt32 = () => underTest.Create(typeof (Int32));

            // Then
            Assert.That(instantiateInt32, Throws.TypeOf<NotSupportedException>());
        }

        [Test(Description = "Create should call matching constructor and return its value when specific type is passed")]
        public void Create_ShouldCallMatchingConstructorAndReturnItsValue_WhenComplexTypeIsPassed()
        {
            // Given
            var underTest = new ObjectCreator();

            // When
            var result = underTest.Create(typeof(DummyObject)) as DummyObject;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.DummyNumber, Is.EqualTo(0));
        }

        [Test(Description = "Create should call matching constructor and return its value when specific type and one number is passed")]
        public void Create_ShouldCallMatchingConstructorAndReturnItsValue_WhenSpecificTypeAndOneNumberArePassed()
        {
            // Given
            var underTest = new ObjectCreator();

            // When
            var result = underTest.Create(typeof (DummyObject), 32) as DummyObject;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.DummyNumber, Is.EqualTo(32));
        }

        [Test(Description = "Create should call matching constructor and return its value when calling constructor with complex type and primitive type")]
        public void Create_ShouldCallMatchingConstructorAndReturnItsValue_WhenCallingConstructorWithComplexTypeAndPrimitiveType()
        {
            // Given
            var underTest = new ObjectCreator();
            var complexType = underTest.Create(typeof (LittleBitSmarterObject), /* old value */ 23);

            // When
            var result = underTest.Create(typeof(DummyObjectDealer), /* expected value */ 32, complexType) as DummyObjectDealer;

            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.DummyObject, Is.Not.Null);
            Assert.That(result.DummyObject.DummyNumber, Is.EqualTo(32));
        }
    }
}
