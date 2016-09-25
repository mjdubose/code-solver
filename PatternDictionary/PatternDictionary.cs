using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PatternDictionary
{
    public class PatternDictionary
    {


        private const string Tempfile = @"file.bin";
        // private const string SourceFileToBeAddedToDictionary = @"dictionary.txt";
        private Dictionary<string, string> _dictionary;
        //FileStream fileStream = new FileStream(tempfile, FileMode.Open);
        private string _stringtobepatterned;

        public PatternDictionary(string sourcefile)
        {
            ReadSourceFile(sourcefile);

            ChangeStringToPatternDictionary();
            Serialize();
            Deserialize();
            WriteToTextFile();
        }

        public PatternDictionary()
        {
            Deserialize();
        }

        private void ReadSourceFile(string sourcefile)
        {
            _stringtobepatterned = File.ReadAllText(sourcefile);
        }

        private void ChangeStringToPatternDictionary()
        {
            var gen = new PatternGenerator.PatternGenerator(_stringtobepatterned);
            _dictionary = gen.GetStringDictionary();
        }

        public void Serialize()
        {
            using (var writer = new BinaryWriter(File.Open(Tempfile, FileMode.OpenOrCreate)))
            {
                writer.Write(_dictionary.Count);
                foreach (var kvp in _dictionary)
                {
                    if (kvp.Value == null) continue;
                    writer.Write(kvp.Key);
                    writer.Write(kvp.Value);
                }
                writer.Flush();
            }
        }

        public void WriteToTextFile()
        {
            var items = from k in _dictionary.Keys
                        orderby _dictionary[k] ascending
                        select k;

            var x = items.ToList();
            File.Delete(@"Dictionary.txt");

            File.WriteAllLines(@"Dictionary.txt", x);
        }



        private void Deserialize()
        {
            if (!File.Exists(Tempfile)) return;
            using (var reader = new BinaryReader(File.Open(Tempfile, FileMode.Open)))
            {
                var count = reader.ReadInt32();
                _dictionary = new Dictionary<string, string>(count);
                for (var n = 0; n < count; n++)
                {
                    var key = reader.ReadString();
                    var value = reader.ReadString();
                    _dictionary.Add(key, value);
                }
            }
        }

        public List<string> ReturnKeys(string value)
        {
            return _dictionary.Where(pair => pair.Value.Equals(value)).Select(pair => pair.Key).ToList();
        }

        public List<string> ReturnValues(string key)
        {
            return _dictionary.Where(pair => pair.Key.Equals(key)).Select(pair => pair.Value).ToList();
        }
    }
}
