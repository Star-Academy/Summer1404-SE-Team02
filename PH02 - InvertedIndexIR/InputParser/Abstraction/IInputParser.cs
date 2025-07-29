namespace ParseInput;

public interface IInputParser
{
    public Dictionary<string, List<string>> ParseInput(string input, string pattern, List<string> notations);
}