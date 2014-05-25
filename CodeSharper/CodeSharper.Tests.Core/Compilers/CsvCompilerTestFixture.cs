using CodeSharper.Core.Csv.Nodes;
using CodeSharper.Languages.Compilers;
using NUnit.Framework;

namespace CodeSharper.Tests.Core.Compilers
{
    [TestFixture]
    class CsvCompilerTestFixture
    {
        [Test]
        public void CsvCompilerShouldCompileSourceCodeFromStringLiteralTest()
        {
            // GIVEN
            var underTest = new CsvCompiler();

            // WHEN
            var result = underTest.Compile("");

            // THEN
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<CsvCompilationUnit>());
        }

        [Test]
        public void CsvCompilerShouldCompileOneRecordTest()
        {
            // GIVEN
            var underTest = new CsvCompiler();

            // WHEN
            var result = underTest.Compile("one,two,three")
                                  .Root;

            // THEN
            Assert.That(result, Is.Not.Null);
            Assert.That(result.GetChildren(), Is.Not.Empty);
        }
    }
}