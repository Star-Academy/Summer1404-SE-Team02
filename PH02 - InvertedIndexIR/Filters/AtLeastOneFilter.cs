using System.Collections.Generic;
using InvertedIndexIR.DTO;
using InvertedIndexIR.Filters.Abstraction;
using InvertedIndexIR.InvertedIndexSearch;
using InvertedIndexIR.InvertedIndexSearch.Abstracion;
using InvertedIndexIR.QueryGetWordsOfType.Abstraction;

namespace InvertedIndexIR.Filters;

public class AtLeastOneFilter : IFilter
{
    private readonly IInvertedIndexSearch _indexSearch;
    private readonly IQueryWordsOfTypeGetter _queryWordsOfTypeGetter;

    public AtLeastOneFilter(IInvertedIndexSearch indexSearch,  IQueryWordsOfTypeGetter queryWordsOfTypeGetter)
    {
        _indexSearch = indexSearch ?? throw new ArgumentNullException(nameof(indexSearch));
        _queryWordsOfTypeGetter = queryWordsOfTypeGetter ??
                                  throw new ArgumentNullException(nameof(queryWordsOfTypeGetter));
    }
    
    public IReadOnlyCollection<string> ApplyFilter(Query query, InvertedIndex invertedIndex)
    {
        List<string> words = _queryWordsOfTypeGetter.GetWordsOfType(query, "+");
        HashSet<string> result = new();
        if (words.Count > 0)
        {
            foreach (var word in words)
            {
                var docs = _indexSearch.Search(word, invertedIndex);
                result.UnionWith(docs);
            }
            return result;
        }
        return  invertedIndex.DocumentNames;
    }
}