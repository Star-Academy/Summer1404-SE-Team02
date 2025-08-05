namespace InvertedIndexWebApi.Tokenizer;

public interface ITokenizer
{
    string[] Tokenize(string text);
}
