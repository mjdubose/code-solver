using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WordPatForm
{
  

    internal class Work
    {

        //this is where the changes must occur for depth first search and character frequency stuff.

        private readonly string _ciphertext;
        private readonly List<Codeword> _codelist;
        private List<string> _solutions = new List<string>();
        private readonly TextBox _text;

        public Work(string ciphertext, List<Codeword> codelist, TextBox txt)
        {
            _ciphertext = ciphertext;
            _text = txt;
            _codelist = codelist;
        }

        public List<string> Go()
        {

            var charactermap = NumericExtensions.GetCharactermap();

            var results = GetCipherLetterFrequency(_ciphertext);

            foreach (var x in results)
            {
                _text.Text += x.Character + @" " + x.Frequency+ Environment.NewLine;
            }



            var checkstringvalue = _ciphertext;

            return Solve(charactermap, checkstringvalue);


        }

        private static IEnumerable<CharacterFrequency> GetCipherLetterFrequency(string ciphertext)
        {
            var wordCount =
                ciphertext.Where(char.IsLetter)
                    .GroupBy(character => character)
                    .Select(g => new CharacterFrequency(g.Key,(double) g.Count()/ciphertext.Length));

            return wordCount.OrderByDescending(x => x.Frequency).ToList();



        }

        private List<string> Solve(Dictionary<char, char> charactermap, string checkstringvalue)
        {
            _solutions = new List<string>();

            var displayplaintext = SolveLoop(charactermap, checkstringvalue, _codelist);
            if (displayplaintext.AllCaps())
            {
                _solutions.Add(displayplaintext);
            }
            else
            {
                var stringlist = WordSearch(charactermap, checkstringvalue);
                foreach (var x in stringlist)
                {
                    _solutions.Add(x);
                }
            }


            return _solutions;
        }


        private IEnumerable<string> WordSearch(Dictionary<char, char> charactermap, string checkstringvalue)
        {

            var codewordwithfewestpossiblities = GetCodewordWithFewestPossiblities(_codelist.GetAllNonCapitalizedCodewords());

            if (codewordwithfewestpossiblities.GetPossibleList() == null) return new List<string>();

            var temp = codewordwithfewestpossiblities.GetPossibleList();
            var tempstring = codewordwithfewestpossiblities.Text;


            var tobereturnedList = new List<string>();
            while (temp.Count > 0)
            {

                var reducedplaintextlist = new List<string> { temp[temp.Count - 1] };
                codewordwithfewestpossiblities.SetPossiblities(reducedplaintextlist);
                codewordwithfewestpossiblities.Text = tempstring;

                temp.Remove(temp[temp.Count - 1]);

                _codelist.Add(codewordwithfewestpossiblities);


                var displayplaintext = SolveLoop(charactermap.CharacterMapDeepCopy(), checkstringvalue, _codelist.Copy());
                tobereturnedList.Add(displayplaintext);
                if (displayplaintext.AllCaps())
                {
                    if (tobereturnedList.Contains(displayplaintext)) continue;
                    tobereturnedList.Add(displayplaintext);
                    _codelist.Remove(codewordwithfewestpossiblities);
                }
                else
                {
                    _codelist.Remove(codewordwithfewestpossiblities);


                }


            }
            return tobereturnedList;
        }

        private static Codeword GetCodewordWithFewestPossiblities(IEnumerable<Codeword> wordstobetried, int startingthreshold = 5000)
        {
            var trythisone = new Codeword();
            foreach (var t in
                wordstobetried.Select(t => new { t, x = t.GetPossibleList().Count })
                    .Where(@t1 => @t1.x < startingthreshold && @t1.x > 0)
                    .Select(@t1 => @t1.t))
            {
                startingthreshold = t.GetPossibleList().Count;
                trythisone = t;
            }

            var tobereturned = trythisone.Copy();
            return tobereturned;
        }




        private string SolveLoop(Dictionary<char, char> charactermap, string checkstringvalue, List<Codeword> codeList)
        {
            string displayplaintext1 = string.Empty;
            bool loopcontinue = true;

            int loopcounter = 0;
            while (loopcontinue)
            {
                NumericExtensions.RebuildPlaintextString(charactermap, codeList, _ciphertext);


                displayplaintext1 = charactermap.Aggregate(_ciphertext, (current, x) => current.Replace(x.Value, x.Key));
                if (displayplaintext1 == checkstringvalue || loopcounter > 5)
                {
                    loopcontinue = false;
                }
                checkstringvalue = displayplaintext1;
                loopcounter++;

            }

            return displayplaintext1;
        }
    }
}