public class NecessaryFilter : IFilter
{
    private readonly IInvertedIndexSearch _indexSearch;

    public NecessaryFilter(IInvertedIndexSearch indexSearch)
    {
        _indexSearch = indexSearch;
    }
    public IEnumerable<string> ApplyFilter(IQuery query, InvertedIndex invertedIndex)
    {
        var words = query.getWordsOfType("");
        var result = invertedIndex.documentNames;
        foreach (var word in words)
        {
            var docs = new HashSet<string>(_indexSearch.Search(word, invertedIndex));
            result.IntersectWith(docs);
        }
        return result;
    }
}