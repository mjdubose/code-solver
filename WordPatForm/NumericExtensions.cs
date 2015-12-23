using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordPatForm
{
    public static class NumericExtensions
    {

     static   public LanguageSignature DetermineMostLikelyLanguage(string sample, IEnumerable<LanguageSignature> signatures)
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
            var minItem = sequence.Aggregate(
                sequence.First(),
                (current, min) => comparatorSelector(current).CompareTo(comparatorSelector(min)) < 0 ? current : min);

            return minItem;
        }

        static public double Sqrt(this double value)
        {
            return Math.Sqrt(value);
        }

       public static double CalculateDistanceFromSignature(LanguageSignature signature, IEnumerable<CharacterFrequency> characterFrequencies)
        {
            var distance = characterFrequencies
                .Where(characterFrequency => signature.GetFrequency(characterFrequency.Character) > 0)
                .Select(characterFrequency
                    => Math.Pow(characterFrequency.Frequency - signature.GetFrequency(characterFrequency.Character), 2))
                .Sum().Sqrt();
                
            return distance;
        }




        public static  IEnumerable<CharacterFrequency> CalculateCharacterFrequencies(string sample)
        {
            var characterFrequencies = sample
                .Select(char.ToLower)
                .GroupBy(c => c)
                .Select(group => new CharacterFrequency
                {
                    Character = group.Key,
                    Frequency = (double)group.Count() / sample.Length
                });

            return characterFrequencies;
        }

        public static Dictionary<char, char> CharacterMapDeepCopy(this Dictionary<char, char> charactermap)
        {
            var tempcharactermap = charactermap.ToDictionary(x => x.Key, x => x.Value);
            return tempcharactermap;
        }

        public static List<Codeword> GetAllNonCapitalizedCodewords(this List<Codeword> codewordList)
        {
            return codewordList.Where(x => !x.Text.AllCaps()).Select(x => new Codeword(x)).ToList();
        }


        public static Dictionary<char, char> GetCharactermap()
        {
            var charactermap = new Dictionary<char, char>
            {
                {'A', '1'},
                {'B', '1'},
                {'C', '1'},
                {'D', '1'},
                {'E', '1'},
                {'F', '1'},
                {'G', '1'},
                {'H', '1'},
                {'I', '1'},
                {'J', '1'},
                {'K', '1'},
                {'L', '1'},
                {'M', '1'},
                {'N', '1'},
                {'O', '1'},
                {'P', '1'},
                {'Q', '1'},
                {'R', '1'},
                {'S', '1'},
                {'T', '1'},
                {'U', '1'},
                {'V', '1'},
                {'W', '1'},
                {'X', '1'},
                {'Y', '1'},
                {'Z', '1'}
            };
            return charactermap;
        }
    
        public static void RebuildPlaintextString(Dictionary<char, char> charactermap, List<Codeword> codelist, string ciphertext)
        {
            foreach (var keyValuePair in codelist.Select(x => new Cipherword(x))
                .Select(tempCipherword => tempCipherword.SwapMatchingWordCharacters(charactermap))
                .Where(tempDictionary => tempDictionary != null).SelectMany(tempDictionary => tempDictionary))
            {
                foreach (var codeword in codelist)
                {
                    codeword.Text = codeword.Text.Replace(keyValuePair.Key, keyValuePair.Value);
                }
                charactermap[keyValuePair.Value] = keyValuePair.Key;
            }
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
