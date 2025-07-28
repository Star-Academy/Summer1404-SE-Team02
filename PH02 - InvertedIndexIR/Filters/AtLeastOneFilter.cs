public class AtLeastOneFilter : IFilter
{
    private readonly IInvertedIndexSearch _indexSearch;

    public AtLeastOneFilter(IInvertedIndexSearch indexSearch)
    {
        _indexSearch = indexSearch;
    }
    
    public IEnumerable<string> ApplyFilter(IQuery query, InvertedIndex invertedIndex)
    {
        List<string> words = query.getWordsOfType("+");
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