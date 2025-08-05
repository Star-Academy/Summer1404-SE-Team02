namespace InvertedIndexWebApi.Tokenizer;

public class BasicTokenizer : ITokenizer
{
    public string[] Tokenize(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {

            return Array.Empty<string>();
        }

        return text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

    }
}
