using System.Linq;

namespace WordPatForm
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
            GenerateWordPattern();
        }

        public string GetPattern()
        {
            return _pattern;
        }

        public string GetWord()
        {
            return _theString;
        }
        public void GenerateWordPattern()  //Generates the AABACA type pattern for each word (to map character repetition) 
        {
            var y = 65;  // using ASCII numberic values that can be incremented every time a new character is introduced
            foreach (var character in _workingstring.Where(char.IsLetter))
            {
                for (var t = 0; t < _workingpattern.Length; t++)
                {
                    if (_workingpattern[t] == character)
                    {
                        _workingpattern[t] = (char) y;
                    }
                }
                y++;
            }
            _pattern = new string(_workingpattern);
       }
      
    }
}
