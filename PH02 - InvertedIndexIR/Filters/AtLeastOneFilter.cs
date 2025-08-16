public class AtLeastOneFilter : IFilter
{
    public IEnumerable<string> ApplyFilter(IQuery query, IInvertedIndex invertedIndex)
    {
        List<string> words = query.getWordsOfType("+");
        HashSet<string> result = new();
        if (words.Count > 0)
        {
            foreach (var word in words)
            {
                var docs = invertedIndex.Search(word);
                result.UnionWith(docs);
            }
            return result;
        }

        return  invertedIndex.GetDocumentNames();
    }
}