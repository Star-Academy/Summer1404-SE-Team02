namespace InvertedIndexIR.InputParser.Abstraction;

public interface IInputParser
{
    List<string> ParseInput(string input, string pattern);
}