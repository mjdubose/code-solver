using System.Collections.Generic;


namespace WordPatForm
{
    class PatternGenerator
    {
        // private readonly string _string;
        private readonly Dictionary<string, string> _stringdictionary;


        public PatternGenerator(string String)
        {
            _stringdictionary = new Dictionary<string, string>();
            var parsestrings = String.Replace('\n', ' ').Replace('=',' ').Replace('$',' ').Replace('\r', ' ').Replace('-', ' ').StripPunctuation().Trim().Split(' ');

            foreach (var x in parsestrings)
            {
                GeneratePattern(x);
            }
        }
        
      

        private void GeneratePattern(string temp)
        {
            var tempword = new Wordpattern(temp);
            if (!_stringdictionary.ContainsKey(tempword.GetWord().ToUpper()))
            { _stringdictionary.Add(tempword.GetWord().ToUpper(), tempword.GetPattern()); }

        }
        public Dictionary<string, string> GetStringDictionary()
        {
            return _stringdictionary;

        }


       
    }
   
  

}
