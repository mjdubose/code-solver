namespace Wordpattern
{

    public class Wordpattern
        {
            private readonly string _theString;
            private readonly string _pattern;
            private readonly char[] _workingstring;
          
            public Wordpattern(string String)
            {
                _theString = String.Trim().ToLower();
               
                if (_theString.Length <= 0) return;
               _workingstring = _theString.ToCharArray(0, _theString.Length);
               _pattern = GenerateWordPattern();
           
        }

            public string GetPattern()
            {
                return _pattern;
            }

            public string GetWord()
            {
                return _theString;
            }
            public string GenerateWordPattern()  //Generates the AABACA type pattern for each word (to map character repetition) 
            {
                var workingstring =  new string(_workingstring);
                var validLetters = new[] {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
                var i = 0;
                var validLettersIndex = 0;
                while (i < workingstring.Length)
                {
                    if (char.IsLower(workingstring[i]))
                    {
                        workingstring = workingstring.Replace(workingstring[i], validLetters[validLettersIndex]);
                        validLettersIndex++;
                    }

                    i++;
                }

            return workingstring;
            }

        }
    }

