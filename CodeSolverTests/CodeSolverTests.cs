using System;
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
            Console.WriteLine(patternTester.GetPattern());
            Assert.AreEqual(pattern,patternTester.GetPattern());
        }





    }
}
