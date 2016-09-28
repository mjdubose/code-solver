using System.Collections.Generic;
using HelpfulExtensions;

namespace PatternGenerator
{
    public class PatternGenerator
    {
        private readonly Dictionary<string, string> _stringdictionary;

        public PatternGenerator(string String)
        {
            _stringdictionary = new Dictionary<string, string>();
            var parsestrings =
                String.Replace('\n', ' ')
                    .Replace('=', ' ')
                    .Replace('$', ' ')
                    .Replace('\r', ' ')
                    .Replace('-', ' ')
                    .StripPunctuationAndNumbers()
                    .Trim()
                    .Split(' ');

            foreach (var x in parsestrings)
            {
                GeneratePattern(x);
            }
        }

        public string GenerateWholeCipherTextPattern(string temp)
        {
            var parsestrings =
                temp.Replace('\n', ' ')
                    .Replace('=', ' ')
                    .Replace('$', ' ')
                    .Replace('\r', ' ')
                    .Replace('-', ' ')
                    .StripPunctuationAndNumbers()
                    .Trim();
            var tempword = new Wordpattern.Wordpattern(parsestrings);
            return tempword.GetPattern();

        }




        private void GeneratePattern(string temp)
        {
            var tempword = new Wordpattern.Wordpattern(temp);
            if (!_stringdictionary.ContainsKey(tempword.GetWord().ToUpper()))
            {
                _stringdictionary.Add(tempword.GetWord().ToUpper(), tempword.GetPattern()); //Adds a plaintext word and the pattern for that word into a Wordpattern object list of possibilities
            }
        }
     
        public Dictionary<string, string> GetStringDictionary()
        {
            return _stringdictionary;
        }
    }
}

