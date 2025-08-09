using System.Collections.Generic;
using InvertedIndexIR.Filters.Abstraction;
using InvertedIndexIR.InvertedIndexSearch;
using InvertedIndexIR.InvertedIndexSearch.Abstracion;
using InvertedIndexIR.Query;
using InvertedIndexIR.Query.Abstraction;

namespace InvertedIndexIR.Filters;

public class ExcludedFilter : IFilter
{
    private readonly IInvertedIndexSearch _indexSearch;

    public ExcludedFilter(IInvertedIndexSearch indexSearch)
    {
        if(indexSearch == null) throw new ArgumentNullException(nameof(indexSearch));
        _indexSearch = indexSearch;
    }
    public IReadOnlyCollection<string> ApplyFilter(IQuery query, InvertedIndex invertedIndex)
    {
        var docs = invertedIndex.documentNames.ToList();
        var excludedWords = query.GetWordsOfType("-");
        if (excludedWords.Count == 0)
        {
            return docs;
        }

        foreach (var word in excludedWords)
        {
            var docsWithWord = _indexSearch.Search(word, invertedIndex);
            docs = docs.Except(docsWithWord).ToList();
        }

        return docs;
    }
}