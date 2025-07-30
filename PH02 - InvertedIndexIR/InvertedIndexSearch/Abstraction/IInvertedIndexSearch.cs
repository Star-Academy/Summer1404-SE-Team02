using System.Collections.Generic;

public interface IInvertedIndexSearch
{
    public IEnumerable<string> Search(string word, InvertedIndex invertedIndex);
}