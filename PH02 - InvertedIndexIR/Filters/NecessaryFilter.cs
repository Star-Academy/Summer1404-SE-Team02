using System.Collections.Generic;
using InvertedIndexIR.Filters.Abstraction;
using InvertedIndexIR.InvertedIndexSearch;
using InvertedIndexIR.InvertedIndexSearch.Abstracion;
using InvertedIndexIR.Query;
using InvertedIndexIR.Query.Abstraction;

namespace InvertedIndexIR.Filters;
public class NecessaryFilter : IFilter
{
    private readonly IInvertedIndexSearch _indexSearch;

    public NecessaryFilter(IInvertedIndexSearch indexSearch)
    {
        if(indexSearch == null) throw new ArgumentNullException(nameof(indexSearch));
        _indexSearch = indexSearch;
    }
    public IReadOnlyCollection<string> ApplyFilter(IQuery query, InvertedIndex invertedIndex)
    {
        var words = query.GetWordsOfType("");
        var result = invertedIndex.documentNames;
        foreach (var word in words)
        {
            var docs = new HashSet<string>(_indexSearch.Search(word, invertedIndex));
            result.IntersectWith(docs);
        }
        return result;
    }
}