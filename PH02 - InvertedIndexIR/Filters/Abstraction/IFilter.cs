using System.Collections.Generic;
using InvertedIndexIR.Query;
using InvertedIndexIR.Query.Abstraction;

namespace InvertedIndexIR.Filters.Abstraction;
public interface IFilter
{
    public IReadOnlyCollection<string> ApplyFilter(IQuery query, InvertedIndex invertedIndex);
}