using System.Reflection;
using InvertedIndexIR.Filters.Abstraction;
using InvertedIndexIR.Query.Abstraction;

namespace InvertedIndexIR.Search.Abstraction;
public interface IExtendedSearch
{
    public IReadOnlyCollection<string> Search(IQuery query, InvertedIndex index);
    public void AddFilter(IFilter filter);
}