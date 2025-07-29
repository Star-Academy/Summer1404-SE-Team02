using System.Collections.Generic;
using InvertedIndexIR.InputParser;

public class Query : IQuery
{
    private readonly InputParser inputParser;
    private readonly string rawInput;
    private readonly string defaultPattern;

    public Query(InputParser inputParser, string rawInput, string defaultPattern)
    {
        this.inputParser = inputParser;
        this.rawInput = rawInput;
        this.defaultPattern = defaultPattern;
    }

    public List<string> GetWordsOfType(string notation)
    {
        var parsedWords = inputParser.ParseInput(rawInput, defaultPattern);
        var result = new List<string>();

        foreach (var word in parsedWords)
        {
            if (word.StartsWith(notation))
            {
                result.Add(word);
            }
        }

        return result;
    }
}
