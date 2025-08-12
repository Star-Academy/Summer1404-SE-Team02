using System.Collections.Generic;
using InvertedIndexIR.DTO;
using InvertedIndexIR.QueryGetWordsOfType;
using InvertedIndexIR.QueryGetWordsOfType.Abstraction;

namespace InvertedIndexIR.Filters.Abstraction;
public interface IFilter
{
    public IReadOnlyCollection<string> ApplyFilter(Query query, InvertedIndex invertedIndex);
}