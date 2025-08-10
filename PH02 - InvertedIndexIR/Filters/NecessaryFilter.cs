using System.Collections.Generic;
using InvertedIndexIR.DTO;
using InvertedIndexIR.Filters.Abstraction;
using InvertedIndexIR.InvertedIndexSearch;
using InvertedIndexIR.InvertedIndexSearch.Abstracion;
using InvertedIndexIR.QueryGetWordsOfType;
using InvertedIndexIR.QueryGetWordsOfType.Abstraction;

namespace InvertedIndexIR.Filters;
public class NecessaryFilter : IFilter
{
    private readonly IInvertedIndexSearch _indexSearch;
    private readonly IQueryWordsOfTypeGetter _queryWordsOfTypeGetter;

    public NecessaryFilter(IInvertedIndexSearch indexSearch,  IQueryWordsOfTypeGetter queryWordsOfTypeGetter)
    {
        _indexSearch = indexSearch ?? throw new ArgumentNullException(nameof(indexSearch));
        _queryWordsOfTypeGetter = queryWordsOfTypeGetter ??
                                  throw new ArgumentNullException(nameof(queryWordsOfTypeGetter));
    }
    public IReadOnlyCollection<string> ApplyFilter(Query query, InvertedIndex invertedIndex)
    {
        var words = _queryWordsOfTypeGetter.GetWordsOfType(query, "");
        var result = invertedIndex.DocumentNames;
        foreach (var word in words)
        {
            var docs = new HashSet<string>(_indexSearch.Search(word, invertedIndex));
            result.IntersectWith(docs);
        }
        return result;
    }
}