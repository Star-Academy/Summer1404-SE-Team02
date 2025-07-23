public class NecessaryFilter : IFilter
{
    public IEnumerable<string> ApplyFilter(IQuery query, IInvertedIndex invertedIndex)
    {
        List<string> words = query.getWordsOfType("");
        HashSet<string> result = invertedIndex.GetDocumentNames().ToHashSet();
        foreach (var word in words)
        {
            var docs = new HashSet<string>(invertedIndex.Search(word));
            result.IntersectWith(docs);
        }
        return result;
    }
}