namespace InvertedIndexWebApi.Query;

public interface IQuery
{
   // void ParseInput(string input);
   List<string> GetWordsOfType(string notation);
}