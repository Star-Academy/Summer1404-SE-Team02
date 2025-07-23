public class ExcludedFilter : IFilter
{
    public IEnumerable<string> ApplyFilter(IQuery query, IInvertedIndex invertedIndex)
    {
        var docs = invertedIndex.GetDocumentNames().ToList();
        var excludedWords = query.getWordsOfType("-");
        if (excludedWords.Count == 0)
        {
            return docs; // No exclusions, return all documents
        }

        foreach (var word in excludedWords)
        {
            var docsWithWord = invertedIndex.Search(word);
            docs = docs.Except(docsWithWord).ToList();
        }

        return docs;
    }
}