using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordPatForm
{
    public class PatternDictionary
    {
        private const string Tempfile = @"file.bin";
        private const string SourceFileToBeAddedToDictionary = @"dictionary.txt";
        private Dictionary<string, string> _dictionary;
        //FileStream fileStream = new FileStream(tempfile, FileMode.Open);
        private string _stringtobepatterned;

        public PatternDictionary(string sourcefile)
        {
            ReadSourceFile();

            ChangeStringToPatternDictionary();
            Serialize();
        }

        public PatternDictionary()
        {
            Deserialize();
        }

        private void ReadSourceFile()
        {
            _stringtobepatterned = File.ReadAllText(SourceFileToBeAddedToDictionary);
        }

        private void ChangeStringToPatternDictionary()
        {
            var gen = new PatternGenerator(_stringtobepatterned);
            _dictionary = gen.GetStringDictionary();
        }

        public void Serialize()
        {
            using (var writer = new BinaryWriter(File.Open(Tempfile, FileMode.OpenOrCreate)))
            {
                writer.Write(_dictionary.Count);
                foreach (var kvp in _dictionary)
                {
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
            File.WriteAllLines(@"Dictionary.txt", x);
        }

        public void Edit()
        {
            var quit = false;
            while (quit != true)
            {
                Console.WriteLine(@"Enter Quit to quit. ");
                Console.WriteLine(@"Enter Serialize to write new dictionary file");
                Console.WriteLine(@"Enter Write to write out dictionary as a text file");
                Console.WriteLine(@"Enter Delete to delete a dictionary key.");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "Quit":
                        quit = true;
                        break;
                    case "Serialize":
                        Serialize();
                        break;
                    case "Write":
                        WriteToTextFile();
                        break;
                    case "Delete":
                    {
                        var deleteQuit = false;
                        while (deleteQuit != true)
                        {
                            Console.WriteLine(@"Enter the key to delete");
                            Console.WriteLine(@"Enter Quit to step out one level");
                            choice = Console.ReadLine();
                            if (choice == "Quit")
                            {
                                deleteQuit = true;
                            }
                            if (choice != null) _dictionary.Remove(choice);
                        }
                    }
                        break;
                }
            }
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