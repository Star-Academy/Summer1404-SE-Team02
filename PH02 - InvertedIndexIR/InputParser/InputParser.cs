using System.Collections.Generic;
using System.Text.RegularExpressions;
using InvertedIndexIR.InputParser.Abstraction;
using InvertedIndexIR.QueryBuilder.Abstraction;

namespace InvertedIndexIR.InputParser;

public class InputParser : IInputParser
{
    public List<string> ParseInput(string input, string pattern, List<string> notations)
    {
        var matches = Regex.Matches(
            input.ToUpper(),
            pattern
        );
        List<string> parsedWords = new List<string>();
        
        foreach (Match match in matches)
        {
            parsedWords.Add(match.Value.Replace("\"", ""));
        }
        return parsedWords;
    }
    
}


