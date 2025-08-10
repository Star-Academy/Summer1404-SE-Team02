using System.Collections.Generic;
using InvertedIndexIR.DTO;
using InvertedIndexIR.Filters.Abstraction;
using InvertedIndexIR.InvertedIndexSearch;
using InvertedIndexIR.InvertedIndexSearch.Abstracion;
using InvertedIndexIR.QueryGetWordsOfType;
using InvertedIndexIR.QueryGetWordsOfType.Abstraction;

namespace InvertedIndexIR.Filters;

public class ExcludedFilter : IFilter
{
    private readonly IInvertedIndexSearch _indexSearch;
    private readonly IQueryWordsOfTypeGetter _queryWordsOfTypeGetter;

    public ExcludedFilter(IInvertedIndexSearch indexSearch,  IQueryWordsOfTypeGetter queryWordsOfTypeGetter)
    {
        _indexSearch = indexSearch ?? throw new ArgumentNullException(nameof(indexSearch));
        _queryWordsOfTypeGetter = queryWordsOfTypeGetter ??
                                  throw new ArgumentNullException(nameof(queryWordsOfTypeGetter));;
    }
    public IReadOnlyCollection<string> ApplyFilter(Query query, InvertedIndex invertedIndex)
    {
        var docs = invertedIndex.DocumentNames.ToList();
        var excludedWords = _queryWordsOfTypeGetter.GetWordsOfType(query, "-");
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