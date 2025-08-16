using System.Reflection;
using InvertedIndexIR.DTO;
using InvertedIndexIR.Filters.Abstraction;
using InvertedIndexIR.QueryGetWordsOfType.Abstraction;

namespace InvertedIndexIR.Search.Abstraction;
public interface IExtendedSearch
{
    public IReadOnlyCollection<string> Search(Query query, InvertedIndex index);
    public void AddFilter(IFilter filter);
}