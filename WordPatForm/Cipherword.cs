using System.Collections.Generic;
using System.Linq;
using PatternDictionary;

namespace WordPatForm
{
    internal class Cipherword
    {
        private readonly string _pattern;
        private readonly string _plaintext;
        private readonly Codeword _possibilites;
        private List<string> _plaintextpossibilities;
        private Dictionary<char,char[]> _charList;

        public Cipherword(IPatternDictionary pd, Codeword codeword)
        {
            _plaintext = codeword.Text;
            Ciphertext = codeword.Text;
            _pattern = codeword.Pattern;
            _possibilites = codeword;
            _plaintextpossibilities = pd.ReturnKeys(_pattern);

           
          


        }

        public Cipherword(Codeword codeword)
        {
            _plaintext = codeword.Plaintext;
            Ciphertext = codeword.Text;
            _pattern = codeword.Pattern;
            _possibilites = codeword;
            _plaintextpossibilities = codeword.GetPossibleList();
        }

        public string Ciphertext { get; }

        public int Length => Ciphertext.Length;

        public int HowManyPossibilities()
        {
            return _plaintextpossibilities?.Count ?? 0;
        }


        




        public Dictionary<char, char> SwapMatchingWordCharacters(Dictionary<char, char> dictionary)
        {
            if (Ciphertext.AllCaps())
            {
                // if the cipher text is caps then nothing further needs to be done with it.  Consider it solved.

                return null;
            }


            var tempwordpossibilities = new List<string>();

            foreach (var codewordtext in _plaintextpossibilities)
            {
                var match = true;
                for (var i = 0; i < Ciphertext.Length; i++) //for each character in the ciphertext 
                {
                    if (!char.IsUpper(Ciphertext[i])) // if the cipher text is lowercase
                    {
                        // get the possible character letter and 
                        //check to see if the dictionary value is '1'
                        if (dictionary[codewordtext[i]] != '1')
                        {
                            match = false; //if it's not '1' then match = false;
                        }
                    }
                    if (!char.IsUpper(Ciphertext[i])) continue;
                    if (codewordtext[i] != Ciphertext[i])
                        //if the uppercase letter in this codeword slot does not match the ciphertext character the the match is false;
                    {
                        match = false;
                    }
                }

                if (!match) continue;
                tempwordpossibilities.Add(codewordtext);
            }

            if (tempwordpossibilities.Count == 1) //if the word possibilities are =1 then a cipher word has been solved.
            {
                _plaintextpossibilities = tempwordpossibilities;

                _possibilites.SetPossiblities(null); //set possibilities to null (there are no more.. SOLVED)
                return ReturnSingleListItemCharacterDictionary(); // setup the character Dictionary and return it
            }
            if (tempwordpossibilities.Count > 1) // if possibilities list is greater than one, update the possibilties
            {
                // would be good to remove other possibilities here (will need to pass in all the other 
                // charactermaps to reduce by letters from other words that are already used.
                _possibilites.SetPossiblities(tempwordpossibilities);
            }

            return null;
        }


        private Dictionary<char, char> ReturnSingleListItemCharacterDictionary()
        {
            var dictionary = new Dictionary<char, char>();
            for (var x = 0; x < Length; x++) //for the length of the cipherword text 
            {
                if (!dictionary.ContainsKey(_plaintext[x])) // if the character dictionary does not contain a key
                    dictionary.Add(_plaintext[x], _plaintextpossibilities[0][x]);
                        //at the first element at the specified location add the character 
            }
            return dictionary;
        }

        public override string ToString()  //overrides to string so that it will display a cipherwords possibilities if between 2 and 5 and the pattern of character repetition
        {
            var words = string.Empty;
            var i = HowManyPossibilities();
            if (i < 1)
            {
                return Ciphertext + " " + _pattern;
            }
            if (i == 1)
            {
                words = _plaintextpossibilities[0];
            }
            else
            {
                if ((1 < i) && (i < 5))
                {
                    words = _plaintextpossibilities.Aggregate(words, (current, y) => current + " " + y);
                }
                else
                {
                    return Ciphertext + " " + _pattern;
                }
            }
            return Ciphertext + " " + _pattern + " " + words;
        }
    }
}