using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace InvertedIndexWebApi.ParseInput;

public class InputParser : IInputParser
{
    public Dictionary<string, List<string>> ParseInput(string input, string pattern, List<string> notations)
    {
        var matches = Regex.Matches(
            input.ToUpper(),
            pattern
        );
        List<string> parsedWords = new List<string>();
        
        foreach (Match match in matches)
        {
            parsedWords.Add(ExtractPhrase(match.Value));
        }
        return AddToDictionary(parsedWords, notations);
    }

    private string ExtractPhrase(string phrase)
    {
        return phrase.Replace("\"", "");
    }

    private Dictionary<string, List<string>> AddToDictionary(List<string> words, List<string> notations)
    {
        var result = new Dictionary<string, List<string>>();
        result[""] = new List<string>();
        foreach (var notation in notations)
        {
            result.Add(notation, new List<string>());
        }
        foreach (var word in words)
        {
            bool added = false;
            foreach (var notation in notations)
            {
                if (word.StartsWith(notation))
                {
                    result[notation].Add(word.Substring(notation.Length));
                    added = true;
                }
            }
            if(!added) result[""].Add(word);
        }
        return result;
    }
}


