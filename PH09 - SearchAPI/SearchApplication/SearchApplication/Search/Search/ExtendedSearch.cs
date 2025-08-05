namespace InvertedIndexWebApi.ExtendedSearch;
using InvertedIndexWebApi.Filters;
using InvertedIndexWebApi.Query;
using InvertedIndexWebApi.InvertedIndexDTO;

public class ExtendedSearch : IExtendedSearch
{
    private List<IFilter> _filters = new List<IFilter>();
    
    public IEnumerable<string> Search(IQuery query, InvertedIndex index)
    {
        var result = new HashSet<string>(index.documentNames);
        foreach (var filter in _filters)
        {
            result.IntersectWith(filter.ApplyFilter(query, index));
        }
        return result;
    }

    public void AddFilter(IFilter filter)
    {
        _filters.Add(filter);
    }
}