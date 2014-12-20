using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.Texts;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.TestHelpers
{
    internal abstract class TextTestFixtureBase : TestFixtureBase
    {
        private readonly string _testDirectory;

        protected struct TextTestCaseDescriptor
        {
            public String Example { get; private set; }
            public String Expected { get; private set; }

            public TextTestCaseDescriptor(String example, String expected)
                : this()
            {
                Example = example;
                Expected = expected;
            }
        }

        protected TextTestFixtureBase()
        {
            _testDirectory = @"../../TestData/";
        }

        protected TextTestCaseDescriptor TestCase(String name)
        {
            return TestCase(name, name);
        }

        protected TextTestCaseDescriptor TestCase(String example, String expected)
        {
            return new TextTestCaseDescriptor(example, expected);
        }

        public TextRange TextRange(String value)
        {
            return new TextDocument(value).TextRange;
        }

        protected IEnumerable<String> ReadValues(String name, String searchPattern)
        {
            var files = Directory.GetFiles(Path.Combine(_testDirectory, name));
            var regex = new Regex(searchPattern);

            var values = files.Where(file => regex.IsMatch(file)).ToArray();

            if (!values.Any()) {
                throw new FileNotFoundException(name);
            }

            foreach (var value in values) {
                using (var stream = File.OpenText(value)) {
                    yield return stream.ReadToEnd();
                }
            }
        }

        public String ReadExample(String name)
        {
            return ReadExamples(name).First();
        }

        protected IEnumerable<String> ReadExpectedResults(String name)
        {
            return ReadValues(name, @"expected(-\d+)?");
        }

        public IEnumerable<String> ReadExamples(String name)
        {
            return ReadValues(name, @"example(-\d+)?");
        }

        protected void TestRunnableWithTestCasesOf(IRunnable runnable, params TextTestCaseDescriptor[] testCasesName)
        {
            foreach (var testCase in testCasesName)
                TestRunnableWithTestCaseOf(runnable, testCase);
        }

        protected void TestRunnableWithTestCaseOf(IRunnable runnable, TextTestCaseDescriptor descriptor)
        {
            // Given
            var examples = ReadExamples(descriptor.Example);
            var underTest = runnable;

            // When
            var result = examples
                .Select(example => (TextRange)underTest.Run(TextRange(example)))
                .Select(range => range.Text);

            // Then
            Assert.That(result, Is.Not.Null.And.Not.Empty);
            var expected = ReadExpectedResults(descriptor.Expected);
            Assert.That(result, Is.EquivalentTo(expected));
        }

    }
}
