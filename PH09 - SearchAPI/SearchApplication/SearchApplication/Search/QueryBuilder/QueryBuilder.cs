using InvertedIndexIR.DTO;
using InvertedIndexIR.QueryBuilder.Abstraction;

namespace InvertedIndexIR.QueryBuilder;

public class QueryBuilder : IQueryBuilder
{
    public Query BuildQuery(List<string> parsedWords, List<string> notations)
    {
        var result = new Dictionary<string, List<string>>();
        result[""] = new List<string>();
        foreach (var notation in notations)
        {
            result.Add(notation, new List<string>());
        }
        foreach (var word in parsedWords)
        {
            bool added = false;
            foreach (var notation in notations)
            {
                if (word.StartsWith(notation))
                {
                    result[notation].Add(word.Substring(notation.Length).ToUpper());
                    added = true;
                }
            }
            if(!added) result[""].Add(word.ToUpper());
        }
        var query = new Query();
        query.ParsedWords = result;
        return query;
    }
}