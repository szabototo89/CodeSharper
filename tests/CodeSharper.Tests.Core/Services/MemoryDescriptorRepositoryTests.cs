using System;
using System.Linq;
using CodeSharper.Core.Commands.Selectors;
using CodeSharper.Core.Common;
using CodeSharper.Core.Nodes.Combinators;
using CodeSharper.Core.Nodes.Modifiers;
using CodeSharper.Core.Services;
using CodeSharper.Core.Utilities;
using CodeSharper.Tests.TestAttributes;
using NUnit.Framework;
using SelectorDescriptor = CodeSharper.Core.Nodes.Selectors.SelectorDescriptor;

namespace CodeSharper.Tests.Core.Services
{
    [TestFixture]
    public class MemoryDescriptorRepositoryTests : TestFixtureBase
    {
        public class GetCombinatorDescriptorsMethod
        {
            [MethodTest(nameof(GetCombinatorDescriptorsMethod), 
                        Description = "should return defined combinator descriptors after calling it")]
            public void ShouldReturnDefinedCombinatorDescriptors_AfterCallingIt()
            {
                // Given in setup
                var combinators = new[]
                {
                    new CombinatorDescriptor("test-combinator", "test", typeof (Object))
                };
                var underTest = new MemoryDescriptorRepository(combinatorDescriptors: combinators);

                // When
                var result = underTest.GetCombinatorDescriptors();

                // Then
                Assert.That(result, Is.EquivalentTo(new[]
                {
                    new CombinatorDescriptor("test-combinator", "test", typeof (Object))
                }));
            }
        }

        public class GetModifierDescriptorsMethod
        {
            [MethodTest(nameof(GetModifierDescriptorsMethod), 
                        Description = "should return defined pseudo-selector descriptors after calling it")]
            public void ShouldReturnDefinedPseudoSelectorDescriptors_AfterCallingIt()
            {
                // Given in setup
                var modifierDescriptors = new[]
                {
                    new ModifierDescriptor("test-pseudo", "test-pseudo", Enumerable.Empty<String>(), typeof (Object),
                        false),
                };
                var underTest = new MemoryDescriptorRepository(modifierDescriptors: modifierDescriptors);

                // When
                var result = underTest.GetModifierDescriptors();

                // Then
                Assert.That(result, Is.EquivalentTo(new[]
                {
                    new ModifierDescriptor("test-pseudo", "test-pseudo", Enumerable.Empty<String>(), typeof (Object),
                        false)
                }));
            }
        }

        public class GetSelectorDescriptorsMethod
        {
            [MethodTest(nameof(GetSelectorDescriptorsMethod),
                        Description = "should return defined selector descriptors after calling it")]
            public void ShouldReturnDefinedSelectorDescriptors_AfterCallingIt()
            {
                // Given
                var selectors = new[]
                {
                    new SelectorDescriptor("test-selector", "test-selector", typeof (Object))
                };

                var underTest = new MemoryDescriptorRepository(selectorDescriptors: selectors);

                // When
                var result = underTest.GetSelectorDescriptors();

                // Then
                Assert.That(result, Is.EquivalentTo(new[]
                {
                    new SelectorDescriptor("test-selector", "test-selector", typeof (Object)),
                }));
            }
        }

        public class GetCommandDescriptorsMethod
        {
            [MethodTest(nameof(GetCommandDescriptorsMethod),
                        Description = "should return defined command descriptors after calling it")]
            public void ShouldReturnDefinedCommandDescriptors_AfterCallingIt()
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
                var underTest = new MemoryDescriptorRepository(commandDescriptors: commandDescriptors);

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
}