using System.Collections.Generic;
using InvertedIndexIR.Filters.Abstraction;
using InvertedIndexIR.InvertedIndexSearch;
using InvertedIndexIR.Query;
using InvertedIndexIR.InvertedIndexSearch.Abstracion;
using InvertedIndexIR.Query.Abstraction;

namespace InvertedIndexIR.Filters;

public class AtLeastOneFilter : IFilter
{
    private readonly IInvertedIndexSearch _indexSearch;

    public AtLeastOneFilter(IInvertedIndexSearch indexSearch)
    {
        if(indexSearch == null) throw new ArgumentNullException(nameof(indexSearch));
        _indexSearch = indexSearch;
    }
    
    public IReadOnlyCollection<string> ApplyFilter(IQuery query, InvertedIndex invertedIndex)
    {
        List<string> words = query.GetWordsOfType("+");
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
        return  invertedIndex.documentNames;
    }
}