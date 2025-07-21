using System.Collections.Generic;

public class Query : IQuery
{
    public LinkedList<string> RequiredWords { get; private set; } = new();
    public LinkedList<string> OptionalWords { get; private set; } = new();
    public LinkedList<string> ForbiddenWords { get; private set; } = new();

    public void ParseInput(string input)
    {
        RequiredWords.Clear();
        OptionalWords.Clear();
        ForbiddenWords.Clear();
        
        var tokens = input.ToUpper().Split(' ', System.StringSplitOptions.RemoveEmptyEntries);

        foreach (var token in tokens)
        {
            if (token.StartsWith("+"))
                OptionalWords.AddLast(token[1..]);
            else if (token.StartsWith("-"))
                ForbiddenWords.AddLast(token[1..]);
            else
                RequiredWords.AddLast(token);
        }
    }
}
