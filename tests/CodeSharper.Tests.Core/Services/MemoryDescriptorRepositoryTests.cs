using System;
using System.Linq;
using CodeSharper.Core.Commands.Selectors;
using CodeSharper.Core.Common;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Services;
using CodeSharper.Core.Utilities;
using NUnit.Framework;
using SelectorDescriptor = CodeSharper.Core.Nodes.Selectors.SelectorDescriptor;

namespace CodeSharper.Tests.Core.Services
{
    [TestFixture]
    public class MemoryDescriptorRepositoryTests : TestFixtureBase
    {
        private MemoryDescriptorRepository underTest;

        [Test(Description = "GetCombinator should return defined combinator descriptors after calling it")]
        public void GetCombinators_ShouldReturnDefinedCombinatorDescriptors_AfterCallingIt()
        {
            // Given in setup
            var combinators = new[]
            {
                new CombinatorDescriptor("test-combinator", "test", typeof (Object))
            };
            underTest = new MemoryDescriptorRepository(combinatorDescriptors: combinators);

            // When
            var result = underTest.GetCombinatorDescriptors();

            // Then
            Assert.That(result, Is.EquivalentTo(new[]
            {
                new CombinatorDescriptor("test-combinator", "test", typeof (Object))
            }));
        }

        [Test(Description = "GetModifierDescriptors should return defined pseudo-selector descriptors after calling it")]
        public void GetPseudoSelectors_ShouldReturnDefinedPseudoSelectorDescriptors_AfterCallingIt()
        {
            // Given in setup
            var pseudoSelectors = new[]
            {
                new ModifierDescriptor("test-pseudo", "test-pseudo", Enumerable.Empty<String>(), typeof (Object), false),
            };
            underTest = new MemoryDescriptorRepository(modifierDescriptors: pseudoSelectors);

            // When
            var result = underTest.GetModifierDescriptors();

            // Then
            Assert.That(result, Is.EquivalentTo(new[]
            {
                new ModifierDescriptor("test-pseudo", "test-pseudo", Enumerable.Empty<String>(), typeof (Object), false)
            }));
        }

        [Test(Description = "GetSelectorDescriptors should return defined selector descriptors after calling it")]
        public void GetSelectors_ShouldReturnDefinedSelectorDescriptors_AfterCallingIt()
        {
            // Given
            var selectors = new[]
            {
                new SelectorDescriptor("test-selector", "test-selector", typeof (Object))
            };

            underTest = new MemoryDescriptorRepository(selectorDescriptors: selectors);

            // When
            var result = underTest.GetSelectorDescriptors();

            // Then
            Assert.That(result, Is.EquivalentTo(new[]
            {
                new SelectorDescriptor("test-selector", "test-selector", typeof (Object)),
            }));
        }

        [Test(Description = "GetCommandDescriptors should return defined command descriptors after calling it")]
        public void GetCommandDescriptors_ShouldReturnDefinedCommandDescriptors_AfterCallingIt()
        {
            // Given
            var commandDescriptors = new[]
            {
                new CommandDescriptor()
                {
                    CommandNames = new[] {"test"},
                    Arguments = Enumerable.Empty<ArgumentDescriptor>(),
                    Description = "test description",
                    Name = "test-command"
                }
            };
            underTest = new MemoryDescriptorRepository(commandDescriptors: commandDescriptors);
            // When
            var result = underTest.GetCommandDescriptors();

            // Then
            Assert.That(result, Is.EquivalentTo(new[]
            {
                new CommandDescriptor()
                {
                    CommandNames = new[] {"test"},
                    Arguments = Enumerable.Empty<ArgumentDescriptor>(),
                    Description = "test description",
                    Name = "test-command"
                }
            }));
        }
    }
}