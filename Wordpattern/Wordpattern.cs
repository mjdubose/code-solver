using System.Linq;

namespace Wordpattern
{
   
        public class Wordpattern
        {
            private readonly string _theString;
            private string _pattern;
            private readonly char[] _workingstring;
            private readonly char[] _workingpattern;
            public Wordpattern(string String)
            {
                String = String.Trim();
                _theString = String.ToLower();
                _pattern = String.ToLower();
                if (_theString.Length <= 0) return;
                _workingstring = _theString.ToCharArray(0, _theString.Length);
                _workingpattern = _theString.ToCharArray(0, _theString.Length);
               _pattern = GenerateWordPattern(_workingstring.ToString());
            }

            public string GetPattern()
            {
                return _pattern;
            }

            public string GetWord()
            {
                return _theString;
            }
            public string GenerateWordPattern(string workingstring)  //Generates the AABACA type pattern for each word (to map character repetition) 
            {   var  pattern = new char[workingstring.Length];
                var y = 65;  // using ASCII numberic values that can be incremented every time a new character is introduced
                foreach (var character in workingstring.Where(char.IsLetter))
                {
                    for (var t = 0; t < pattern.Length; t++)
                    {
                        if (pattern[t] == character)
                        {
                            pattern[t] = (char)y;
                        }
                    }
                    y++;
                }
               return new string(pattern);
            }

        }
    }

