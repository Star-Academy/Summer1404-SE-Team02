namespace InvertedIndexWebApi.Filters;
using InvertedIndexWebApi.InvertedIndexDocumentSearch;
using InvertedIndexWebApi.InvertedIndexDTO;
using InvertedIndexWebApi.Query;
public class ExcludedFilter : IFilter
{
    private readonly IInvertedIndexSearch _indexSearch;

    public ExcludedFilter(IInvertedIndexSearch indexSearch)
    {
        _indexSearch = indexSearch;
    }
    public IEnumerable<string> ApplyFilter(IQuery query, InvertedIndex invertedIndex)
    {
        var docs = invertedIndex.documentNames.ToList();
        var excludedWords = query.GetWordsOfType("-");
        if (excludedWords.Count == 0)
        {
            return docs; // No exclusions, return all documents
        }

        foreach (var word in excludedWords)
        {
            var docsWithWord = _indexSearch.Search(word, invertedIndex);
            docs = docs.Except(docsWithWord).ToList();
        }

        return docs;
    }
}