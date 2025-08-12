using InvertedIndexIR.DTO;
using InvertedIndexIR.Filters.Abstraction;
using InvertedIndexIR.Search.Abstraction;
using InvertedIndexIR.QueryGetWordsOfType.Abstraction;
namespace InvertedIndexIR.Search.Extended;

public class ExtendedSearch : IExtendedSearch
{
    private List<IFilter> _filters = new List<IFilter>();
    

    public IReadOnlyCollection<string> Search(Query query, InvertedIndex index)
    {
        var result = new HashSet<string>(index.DocumentNames);
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