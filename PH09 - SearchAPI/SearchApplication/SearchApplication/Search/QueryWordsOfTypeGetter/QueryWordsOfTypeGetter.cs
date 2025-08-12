using InvertedIndexIR.DTO;
using InvertedIndexIR.QueryGetWordsOfType.Abstraction;

namespace InvertedIndexIR.QueryGetWordsOfType;

public class QueryWordsOfTypeGetter : IQueryWordsOfTypeGetter
{
    public List<string> GetWordsOfType(Query query, string notation)
    {
        if (query.ParsedWords.ContainsKey(notation))
        {
            return query.ParsedWords[notation];
        }
        return new List<string>();
    }
}