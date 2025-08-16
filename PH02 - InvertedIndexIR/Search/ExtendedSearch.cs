public class ExtendedSearch : ISearch
{
    private readonly IInvertedIndex _inverted_index;
    private List<IFilter> _filters = new List<IFilter>();

    public ExtendedSearch(IInvertedIndex invertedIndex)
    {
        _inverted_index = invertedIndex;
    }

    public IEnumerable<string> Search(IQuery query)
    {
        var result = new HashSet<string>(_inverted_index.GetDocumentNames());
        foreach (IFilter filter in _filters)
        {
            result.IntersectWith(filter.ApplyFilter(query, _inverted_index));
        }
        return result;
    }

    public void AddFilter(IFilter filter)
    {
        _filters.Add(filter);
    }
}