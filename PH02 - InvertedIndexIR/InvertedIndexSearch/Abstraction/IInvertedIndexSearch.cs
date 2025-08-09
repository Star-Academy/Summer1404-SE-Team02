namespace InvertedIndexIR.InvertedIndexSearch.Abstracion;
public interface IInvertedIndexSearch
{
    public IReadOnlyCollection<string> Search(string word, InvertedIndex invertedIndex);
}