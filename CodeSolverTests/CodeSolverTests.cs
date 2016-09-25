using NUnit.Framework;

namespace CodeSolverTests
{
    [TestFixture]
    public class CodeSolverTests
    {

        [Test]
        public void WordPatternReturnsAPattern()
        {
            var testString = "Mississippi";
            var pattern = "ABCCBCCBDDB";

            var patternTester = new Wordpattern.Wordpattern(testString);
            Assert.AreEqual(pattern,patternTester.GenerateWordPattern());
        }
        [Test]
        public void WordPatternReturnsAPatternAsUpperCase()
        {
            var testString = "Mississippi";
            var pattern = "ABCCBCCBDDB";

            var patternTester = new Wordpattern.Wordpattern(testString);
            Assert.AreEqual(pattern, patternTester.GenerateWordPattern());
        }

        [Test]
        public void WordPatternReturnsATextValue()
        {
            var testString = "mississippi";

            var patternTester = new Wordpattern.Wordpattern(testString);
          
            Assert.AreEqual(testString, patternTester.GetWord());
        }
        [Test]
        public void WordPatternReturnsATextValueAsLowerCase()
        {
            var testString = "MISSISSIPPI";

            var patternTester = new Wordpattern.Wordpattern(testString);

            Assert.AreEqual(testString.ToLower(), patternTester.GetWord());
        }


    }
}
