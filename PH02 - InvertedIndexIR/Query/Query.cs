using System.Collections.Generic;
// using InvertedIndexIR.InputParser;
using ParseInput;

public class Query : IQuery
{
    private readonly Dictionary<string, List<string>> _parsedWords;

    public Query(Dictionary<string, List<string>> parsedWords)
    {
        _parsedWords = parsedWords;
    }

    public List<string> GetWordsOfType(string notation)
    {
        if (_parsedWords.ContainsKey(notation))
        {
            return _parsedWords[notation];
        }
        return new List<string>();
    }
}
