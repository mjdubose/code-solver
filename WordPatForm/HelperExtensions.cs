using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordPatForm
{
    public static class HelperExtensions //helper functions for determining a possible language, finding the lowest value, generating square roots, deep copys, building character maps, etc.
    {
        public static string[] FormatTextForProcessing(string thestring)
        {
            return thestring.ToUpper().Replace('\n', ' ')
                .Replace('\r', ' ')
                .Replace('-', ' ')
                .Replace("   "," ")
                .StripPunctuation()
                .Trim().Split(' '); 
        }

        public static LanguageSignature ReturnEnglishLanguage()
        {
            return new LanguageSignature("English", new Dictionary<char, double>
            {
                {'a', 0.08167},
                {'b', 0.01492},
                {'c', 0.02782},
                {'d', 0.04253},
                {'e', 0.12702},
                {'f', 0.02228},
                {'g', 0.02015},
                {'h', 0.06094},
                {'i', 0.06966},
                {'j', 0.00153},
                {'k', 0.00772},
                {'l', 0.04025},
                {'m', 0.02406},
                {'n', 0.06749},
                {'o', 0.07507},
                {'p', 0.01929},
                {'q', 0.00095},
                {'r', 0.05987},
                {'s', 0.06327},
                {'t', 0.09056},
                {'u', 0.02758},
                {'v', 0.00978},
                {'w', 0.02361},
                {'x', 0.00150},
                {'y', 0.01974},
                {'z', 0.00074}
            });
        }
     public   static LanguageSignature DetermineMostLikelyLanguage(string sample, IEnumerable<LanguageSignature> signatures)
        {
            var characterFrequencies = CalculateCharacterFrequencies(sample);
            var closestLanguage = signatures.Select(signature => new
            {
                Language = signature,
                Distance = CalculateDistanceFromSignature(signature, characterFrequencies)
            })
            .MinItem(languageDistance => languageDistance.Distance);

            return closestLanguage.Language;
        }

        public static T MinItem<T, TCompare>(this IEnumerable<T> sequence, Func<T, TCompare> comparatorSelector) where TCompare : IComparable<TCompare>
        {
            var itemarray= sequence as T[] ?? sequence.ToArray();
            var result = itemarray.First();
            return itemarray.Aggregate(result, (current, item) => comparatorSelector(current).CompareTo(comparatorSelector(item)) < 0 ? current : item);
        }

        public static double Sqrt(this double value)
        {
            return Math.Sqrt(value);
        }

       public static double CalculateDistanceFromSignature(LanguageSignature signature, IEnumerable<CharacterFrequency> characterFrequencies)
        {
            return characterFrequencies
                .Where(characterFrequency => signature.GetFrequency(characterFrequency.Character) > 0)
                .Select(characterFrequency
                    => Math.Pow(characterFrequency.Frequency - signature.GetFrequency(characterFrequency.Character), 2))
                .Sum().Sqrt();
        }

        public static  IEnumerable<CharacterFrequency> CalculateCharacterFrequencies(string sample)
        {
            return sample
                .Select(char.ToLower)
                .GroupBy(c => c)
                .Select(group => new CharacterFrequency
                {
                    Character = group.Key,
                    Frequency = (double)group.Count() / sample.Length
                });
        }

        public static Dictionary<char, List<char>> CharacterMapDeepCopy(this Dictionary<char, List<char>> charactermap)
        {
            return charactermap.ToDictionary(x => x.Key, x => x.Value);
        }

        public static List<Codeword> GetAllNonCapitalizedCodewords(this List<Codeword> codewordList)
        {
            return codewordList.Where(x => !x.Text.AllCaps()).Select(x => new Codeword(x)).ToList();
        }


        public static Dictionary<char, List<char>> GetCharactermap()
        {
            return new Dictionary<char, List<char>>
            {
                {'A', new List<char>()},
                {'B', new List<char>()},
                {'C', new List<char>()},
                {'D', new List<char>()},
                {'E',new List<char>()},
                {'F',new List<char>()},
                {'G', new List<char>()},
                {'H',new List<char>()},
                {'I', new List<char>()},
                {'J', new List<char>()},
                {'K', new List<char>()},
                {'L', new List<char>()},
                {'M',new List<char>()},
                {'N', new List<char>()},
                {'O',new List<char>()},
                {'P',new List<char>()},
                {'Q', new List<char>()},
                {'R', new List<char>()},
                {'S', new List<char>()},
                {'T', new List<char>()},
                {'U',new List<char>()},
                {'V', new List<char>()},
                {'W', new List<char>()},
                {'X',new List<char>()},
                {'Y', new List<char>()},
                {'Z', new List<char>()}
            };
           
        }
    
       

        public static bool AllCaps(this string s)
        {
            return s != null && s.All(t => !char.IsLower(t));
        }

        public static string StripPunctuation(this string s)
        {
            var sb = new StringBuilder();
            var ab = new StringBuilder();
            foreach (var c in s.Where(c => !char.IsPunctuation(c)))
            {
                sb.Append(c);
            }
            foreach (var c in sb.ToString().Where(c => !char.IsNumber(c)))
            {
                ab.Append(c);
            }
            return ab.ToString();
        }
    }
}
