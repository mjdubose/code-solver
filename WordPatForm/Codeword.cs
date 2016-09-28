using System.Collections.Generic;
using PatternDictionary;

namespace WordPatForm
{
    public class Codeword  //class that holds the ciphertext, pattern for repeating characters, list of possible words that it could be, plaintext thus far
    {
        private List<string> _possibilities;  

        public string Plaintext { get; set; }
        public string Text { get; set; }

        public string Pattern { get; set; }

        public List<string> GetPossibleList()
        {
           
            return _possibilities;
        }

        public void SetPossiblities(List<string> temp )
        {
            _possibilities = temp;
        }

        public Codeword(){}
        public Codeword(Codeword word)
        {
            Text = word.Text;
            Plaintext = word.Plaintext;
            Pattern = word.Pattern;
            _possibilities = new List<string>();
            foreach (var x in word.GetPossibleList())
            {
                _possibilities.Add(x);
            }
        }
   
        public Codeword(string text,IPatternDictionary pd)
        {
            Text = text;
            Plaintext = text;
            var x = new Wordpattern.Wordpattern(text);
            Pattern = x.GetPattern();
            _possibilities = pd.ReturnKeys(Pattern);
        }
    }
}
