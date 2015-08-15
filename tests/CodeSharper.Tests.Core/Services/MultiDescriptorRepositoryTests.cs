using System;
using System.Collections.Generic;
using CodeSharper.Core.Common;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.Services;
using CodeSharper.Core.Utilities;
using NUnit.Framework;
using static CodeSharper.Core.Utilities.ConstructsHelper;

namespace CodeSharper.Tests.Core.Services
{
    [TestFixture]
    public class MultiDescriptorRepositoryTests : TestFixtureBase
    {
        #region Repository stub

        internal class AdhocDescriptorRepository : IDescriptorRepository
        {
            private readonly String descriptorId;

            public AdhocDescriptorRepository(Int32 id)
            {
                this.descriptorId = $"test-{id}";
            }

            public IEnumerable<CombinatorDescriptor> GetCombinatorDescriptors()
            {
                yield return new CombinatorDescriptor(descriptorId, descriptorId, typeof (Object));
            }

            public IEnumerable<ModifierDescriptor> GetModifierDescriptors()
            {
                yield return new ModifierDescriptor(descriptorId, descriptorId, Array<String>(), typeof (Object), false);
            }

            public IEnumerable<SelectorDescriptor> GetSelectorDescriptors()
            {
                yield return new SelectorDescriptor(descriptorId, descriptorId, typeof (Object));
            }

            public IEnumerable<CommandDescriptor> GetCommandDescriptors()
            {
                yield return new CommandDescriptor(descriptorId, descriptorId, Array<ArgumentDescriptor>(), Array<String>());
            }
        }

        #endregion

        #region Initializer

        public class InitializeWithTwoAdhocDescriptorRepository : TestFixtureBase
        {
            protected MultiDescriptorRepository UnderTest { get; set; }

            public override void Setup()
            {
                base.Setup();
                var repositories = Array(new AdhocDescriptorRepository(1), new AdhocDescriptorRepository(2));
                UnderTest = new MultiDescriptorRepository(repositories);
            }
        }

        #endregion

        public class GetCombinatorDescriptorsMethod : InitializeWithTwoAdhocDescriptorRepository
        {
            [Test(Description = "should return two elements from each repository when two repositories is specified")]
            public void ShouldReturnTwoElementsFromEachRepository_WhenTwoRepositoriesIsSpecified()
            {
                // Act + Arrange
                var result = UnderTest.GetCombinatorDescriptors();

                // Assert
                Assert.That(result, Is.EquivalentTo(Array( 
                    new CombinatorDescriptor("test-1", "test-1", typeof(Object)),
                    new CombinatorDescriptor("test-2", "test-2", typeof(Object))
                )));
            }
        }

        public class GetModifierDescriptorsMethod : InitializeWithTwoAdhocDescriptorRepository
        {
            [Test(Description = "should return two elements from each repository when two repositories is specified")]
            public void ShouldReturnTwoElementsFromEachRepository_WhenTwoRepositoriesIsSpecified()
            {
                // Act + Arrange
                var result = UnderTest.GetModifierDescriptors();

                // Assert
                Assert.That(result, Is.EquivalentTo(Array(
                    new ModifierDescriptor("test-1", "test-1", Array<String>(), typeof(Object), false),
                    new ModifierDescriptor("test-2", "test-2", Array<String>(), typeof(Object), false)
                )));
            }
        }

        public class GetSelectorDescriptorsMethod : InitializeWithTwoAdhocDescriptorRepository
        {
            [Test(Description = "should return two elements from each repository when two repositories is specified")]
            public void ShouldReturnTwoElementsFromEachRepository_WhenTwoRepositoriesIsSpecified()
            {
                // Act + Arrange
                var result = UnderTest.GetSelectorDescriptors();

                // Assert
                Assert.That(result, Is.EquivalentTo(Array(
                    new SelectorDescriptor("test-1", "test-1", typeof(Object)),
                    new SelectorDescriptor("test-2", "test-2", typeof(Object))
                )));
            }
        }

        public class GetCommandDescriptorsMethod : InitializeWithTwoAdhocDescriptorRepository
        {
            [Test(Description = "should return two elements from each repository when two repositories is specified")]
            public void ShouldReturnTwoElementsFromEachRepository_WhenTwoRepositoriesIsSpecified()
            {
                // Act + Arrange
                var result = UnderTest.GetCommandDescriptors();

                // Assert
                Assert.That(result, Is.EquivalentTo(Array(
                    new CommandDescriptor("test-1", "test-1", Array<ArgumentDescriptor>(), Array<String>()),
                    new CommandDescriptor("test-2", "test-2", Array<ArgumentDescriptor>(), Array<String>())
                )));
            }
        }
    }
}