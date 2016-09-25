using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpfulExtensions
{
  


        public static class HelperExtensions //helper functions for determining a possible language, finding the lowest value, generating square roots, deep copys, building character maps, etc.
        {

       

            public static T MinItem<T, TCompare>(this IEnumerable<T> sequence, Func<T, TCompare> comparatorSelector) where TCompare : IComparable<TCompare>
            {
                var itemarray = sequence as T[] ?? sequence.ToArray();
                var result = itemarray.First();
                return itemarray.Aggregate(result, (current, item) => comparatorSelector(current).CompareTo(comparatorSelector(item)) < 0 ? current : item);
            }

            public static double Sqrt(this double value)
            {
                return Math.Sqrt(value);
            }

       

          

            public static Dictionary<char, char> CharacterMapDeepCopy(this Dictionary<char, char> charactermap)
            {
                return charactermap.ToDictionary(x => x.Key, x => x.Value);
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


