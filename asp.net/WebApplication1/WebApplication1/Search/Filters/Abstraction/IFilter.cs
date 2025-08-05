namespace InvertedIndexWebApi.Filters;
using InvertedIndexWebApi.InvertedIndexDTO;
using InvertedIndexWebApi.Query;

public interface IFilter
{
    public IEnumerable<string> ApplyFilter(IQuery query, InvertedIndex invertedIndex);
}