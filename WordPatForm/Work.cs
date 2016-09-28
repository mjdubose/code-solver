﻿using System;
using System.Collections.Generic;
using System.Linq;
using PatternDictionary;
using static WordPatForm.HelperExtensions;
namespace WordPatForm
{
  

    internal class Work
    {

        //this is where the changes must occur for depth first search and character frequency stuff.

        private readonly string _ciphertext;
        private  List<Codeword> _codelist;
       private readonly char[] LETTERS = new char[] { 'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
        private readonly IPatternDictionary _pd; //new PatternDictionary.PatternDictionary(@"dictionary.txt");



        public Work(string ciphertext)
        {


            //  pd.Edit();
            //   pd.WriteToTextFile();
             _pd = new PatternDictionary.PatternDictionary();
            _ciphertext = ciphertext.ToUpper();
          
          
        }

        public List<string> Go()
        {
           


          var letterMapping = HackSimpleSub(_ciphertext);


            var answer = new List<string> {DecryptWithCipherletterMapping(_ciphertext, letterMapping)};
            return answer;

        }

        private string DecryptWithCipherletterMapping(string ciphertext, Dictionary<char, List<char>> letterMapping)
        {
            foreach (var letter in LETTERS)
            {
                
                if (letterMapping[letter].Count == 1)
                {
                    ciphertext = ciphertext.Replace(letter, char.ToLower( letterMapping[letter][0]));
                    Console.WriteLine(ciphertext + " " + letter +" "+ letterMapping[letter][0]);
                }
                else
                {
                   ciphertext = ciphertext.Replace(letter, '_');
                }
                Console.WriteLine(ciphertext);
            }
     
            return ciphertext;
        }

        private static Dictionary<char, List<char>>  AddLettersToMapping(Dictionary<char, List<char>> letterMapping, Codeword codeword)
        {
           
            letterMapping = letterMapping.Copy();
            var possibilities = codeword.GetPossibleList();
        
            if (possibilities.Count == 0)
            {
                return letterMapping;
            }
            for (var i = 0; i < codeword.Text.Length; i++)
            {
                foreach (var word in possibilities)
                {
                    var list = letterMapping[codeword.Plaintext[i]];
                    if (!list.Contains(word[i]))
                    {
                        list.Add(word[i]);
                    }
                }
            }
         
            return letterMapping;
        }

        private Dictionary<char, List<char>> IntersectMapping(Dictionary<char, List<char>> mapA,
            Dictionary<char, List<char>> mapB)
        {
            var intersectedMapping = GetCharactermap();
            foreach (var letter in LETTERS)
            {
                if ((mapA[letter]).Count == 0)
                {
                    intersectedMapping[letter] = mapB[letter].Copy();
                }
                else if (mapB[letter].Count == 0)
                {
                    intersectedMapping[letter] = mapA[letter].Copy();
                }
                else
                {  
                    intersectedMapping[letter] =  mapA[letter].Intersect(mapB[letter]).ToList();
                }
            }
            return intersectedMapping;
        }


        private Dictionary<char,List<char>> HackSimpleSub( string text)
        {

            var intersectMap = GetCharactermap();
            _codelist = FormatTextForProcessing(text).Select(x => new Codeword(x, _pd)).ToList();
            foreach (var word in _codelist)
            {
                var newmap = GetCharactermap();
                 newmap = AddLettersToMapping(newmap, word);
                intersectMap = IntersectMapping(intersectMap, newmap);

            }

            return RemovedSolvedLettersFrommapping(intersectMap);

        }

        private Dictionary<char,List<char>> RemovedSolvedLettersFrommapping(Dictionary<char, List<char>> intersectMap)
        {
            intersectMap = intersectMap.Copy();

            var loopAgain = true;
            while (loopAgain)
            {
                loopAgain = false;
                var solvedLetters = (from letter in LETTERS where intersectMap[letter].Count == 1 select intersectMap[letter][0]).ToList();
                foreach (var letter in LETTERS)
                {
                    foreach (var s in solvedLetters)
                    {
                        if (intersectMap[letter].Count != 1 && intersectMap[letter].Contains(s))
                        {
                            intersectMap[letter].Remove(s);
                            if (intersectMap[letter].Count == 1)
                            {
                                loopAgain = true;
                            }
                        }
                    }
                }
            }
            
          return intersectMap;
        }
    }
}