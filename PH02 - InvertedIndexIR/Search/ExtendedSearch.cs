public class ExtendedSearch : ISearch
{
    private readonly InvertedIndex _inverted_index;
    private List<IFilter> _filters = new List<IFilter>();

    public ExtendedSearch(InvertedIndex invertedIndex)
    {
        _inverted_index = invertedIndex;
    }

    public IEnumerable<string> Search(IQuery query)
    {
        var result = new HashSet<string>(_inverted_index.GetDocumentNames());
        foreach (IFilter filter in _filters)
        {
            
            result.IntersectWith(filter.ApplyFilter(query, _inverted_index));
            Console.WriteLine(filter.ApplyFilter(query, _inverted_index).Count() + " documents after applying filter: " + filter.GetType().Name);
        }
        return result;
    }

    public void AddFilter(IFilter filter)
    {
        _filters.Add(filter);
    }
}