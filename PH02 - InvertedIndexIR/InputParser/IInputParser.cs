namespace ParseInput;

public interface IInputParser
{
    public List<string> ParseInput(string input, string pattern);
}