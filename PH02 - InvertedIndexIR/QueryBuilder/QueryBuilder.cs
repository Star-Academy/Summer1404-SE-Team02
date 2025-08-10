using InvertedIndexIR.DTO;
using InvertedIndexIR.QueryBuilder.Abstraction;

namespace InvertedIndexIR.QueryBuilder;

public class QueryBuilder : IQueryBuilder
{
    public Query BuildQuery(List<string> parsedWords, List<string> notations)
    {
        var result = new Query();
        result.ParsedWords[""] = new List<string>();
        foreach (var notation in notations)
        {
            result.ParsedWords.Add(notation, new List<string>());
        }
        foreach (var word in parsedWords)
        {
            bool added = false;
            foreach (var notation in notations)
            {
                if (word.StartsWith(notation))
                {
                    result.ParsedWords[notation].Add(word.Substring(notation.Length).ToUpper());
                    added = true;
                }
            }
            if(!added) result.ParsedWords[""].Add(word.ToUpper());
        }
        return result;
    }
}