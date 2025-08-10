namespace InvertedIndexIR.Query.Abstraction;
public interface IQuery
{ 
    List<string> GetWordsOfType(string notation);
}