using System;
using System.Collections.Generic;
using System.Linq;
using HelpfulExtensions;
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
        [Test]
        public void WordPatternReturnsAPatternForEveryLetterInReallyLongWord()
        {
            var testString = "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz";

            var patternTester = new Wordpattern.Wordpattern(testString);
            var pattern = patternTester.GetPattern();

            Assert.AreEqual(testString.ToUpper(), pattern);
        }
        [Test]
        public void PatternGeneratorWorksOnOneWord()
        {

            var patternGenerator = new PatternGenerator.PatternGenerator("This");
           var dictionary =   patternGenerator.GetStringDictionary();
            var keys = dictionary.Keys.ToList();
            var values = dictionary.Values.ToList();
            var kvpString = keys[0] + " " + values[0];
            Assert.AreEqual(kvpString,"THIS ABCD");
        }

        [Test]
        public void PatternGeneratorWorksOnMoreThanOneWord()
        {
            var stringList = new List<string>
            {
                "THIS ABCD",
                "IS AB",
                "A A",
                "TEST ABCA",
                "OF AB",
                "THE ABC",
                "BROADCASTING ABCDEFDGHIJK",
                "SYSTEM ABACDE"
            };
            var patternGenerator = new PatternGenerator.PatternGenerator("This is a test of the broadcasting system");
            var dictionary = patternGenerator.GetStringDictionary();
            var keys = dictionary.Keys.ToList();
            var values = dictionary.Values.ToList();
            var test = true;
            for (var i = 0; i < dictionary.Count; i++)
            {
                var kvpString = keys[i] + " " + values[i];
                if (kvpString != stringList[i])
                {
                    test = false;
                }
            }
            if (test)
            {
                Assert.Pass();
            }
            Assert.Fail();
        }
        [Test]
        public void PatternGeneratorGeneratePatternForCipherTextAsInTheWholeThing()
        {
           
            var patternGenerator = new PatternGenerator.PatternGenerator("This is a test of the broadcasting system");
            var dictionary = patternGenerator.GenerateWholeCipherTextPattern("This is a test of the broadcasting system");
            Console.WriteLine(dictionary);
         Assert.AreEqual("ABCD CD E AFDA GH ABF IJGEKLEDACMN DODAFP",dictionary);
        }
        [Test]
        public void PatternGeneratorCapitalizesKeyValue()
        {
            var patternGenerator = new PatternGenerator.PatternGenerator("This");
            var dictionary = patternGenerator.GetStringDictionary();
            var keys = dictionary.Keys.ToList();
            Assert.AreEqual(keys[0],"THIS");
        }

        [Test]
        public void HelpfulExtensionsCheckThatRemovePunctuationAndNumbersWorks()
        {
            Assert.AreEqual("Str,,,ips.;' out1234567890@@#@@#".StripPunctuationAndNumbers(), "Strips out");
        }
        [Test]
        public void HelpfulExtensionsAllCapsWorksWithAllCaps()
        {
            Assert.AreEqual("THISISALLCAPS".AllCaps(),true);
        }
        [Test]
        public void HelpfulExtensionsAllCapsDoesCatchLowerCase()
        {
            Assert.AreEqual("THISISALLCaPS".AllCaps(), false);
        }
        [Test]
        public void PatternDictionary()
        {
            var pd = new PatternDictionary.PatternDictionary();

            var x = pd.ReturnKeys("ABCDC");
            foreach (var y in x)
            {
                Console.WriteLine(y);
            }
            x = pd.ReturnValues("THERE");
            foreach (var y in x)
            {
                Console.WriteLine(y);
            }

            Assert.Pass();
        }
    }
}
