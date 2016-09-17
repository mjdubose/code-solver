﻿using System.Collections.Generic;

namespace WordPatForm
{
    public static class LanguageSignatures
    {
        public static LanguageSignature English =
            new LanguageSignature(
                "English",
                new Dictionary<char, double>
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
    public class LanguageSignature
    {
        private readonly IDictionary<char, double> _frequencies;
        public string Language { get; protected set; }

        public LanguageSignature(string language, IDictionary<char, double> characterFrequencies)
        {
            Language = language;
            _frequencies = characterFrequencies;
        }

        public double GetFrequency(char character)
        {
            double frequency;
            return _frequencies.TryGetValue(character, out frequency) ? frequency : 0;
        }
    }
}
