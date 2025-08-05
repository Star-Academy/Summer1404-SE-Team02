using System.Collections.Generic;
namespace InvertedIndexWebApi.InvertedIndexDocumentSearch;
using InvertedIndexWebApi.InvertedIndexDTO;

public interface IInvertedIndexSearch
{
    public IEnumerable<string> Search(string word, InvertedIndex invertedIndex);
}