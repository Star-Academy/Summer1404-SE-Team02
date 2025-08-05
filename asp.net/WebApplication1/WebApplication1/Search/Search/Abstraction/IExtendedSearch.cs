using System.Reflection;
using InvertedIndexWebApi.Filters;
using InvertedIndexWebApi.Query;
using InvertedIndexWebApi.InvertedIndexDTO;
namespace InvertedIndexWebApi.ExtendedSearch;

public interface IExtendedSearch
{
    public IEnumerable<string> Search(IQuery query, InvertedIndex index);
    public void AddFilter(IFilter filter);
}