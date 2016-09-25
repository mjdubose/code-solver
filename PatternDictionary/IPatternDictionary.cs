using System.Collections.Generic;

namespace PatternDictionary
{
    public interface IPatternDictionary
    {
        
        void WriteToTextFile();
        List<string> ReturnKeys(string value);
        List<string> ReturnValues(string key);
    }
}