using System.Collections.Generic;

namespace WordPatForm
{
    public class Codeword
    {
        private string _pattern;
         private List<string> _possibilities;

        public string Plaintext { get; set; }
        public string Text { get; set; }

        public string Pattern
        {
            get { return _pattern; }
            set { _pattern = value; }
        }
        public List<string> GetPossibleList()
        {
            if (Text.AllCaps())
                _possibilities = null;
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
   
        public Codeword(string text,PatternDictionary pd)
        {
            Text = text;
            Plaintext = text;
            var x = new Wordpattern(text);
            _pattern = x.GetPattern();
            _possibilities = pd.ReturnKeys(_pattern);
        }
    }
}
