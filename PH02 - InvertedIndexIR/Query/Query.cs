using System.Collections.Generic;

public class Query : IQuery
{
    private Dictionary<string, LinkedList<string>> wordsByType = new Dictionary<string, LinkedList<string>>
    {
        { "+", new LinkedList<string>() }, // Optional words
        { "-", new LinkedList<string>() }, // Forbidden words
        { "", new LinkedList<string>() }    // Required words
    };
    
    public void ParseInput(string input)
    {
        foreach (var list in wordsByType.Values)
        {
            list.Clear();
        }

        var tokens = input.ToUpper().Split(' ', System.StringSplitOptions.RemoveEmptyEntries);

        foreach (var token in tokens)
        {
            if (token.StartsWith("+"))
                wordsByType["+"].AddLast(token[1..]);
            else if (token.StartsWith("-"))
                wordsByType["-"].AddLast(token[1..]);
            else
                wordsByType[""].AddLast(token);
        }
    }

    public List<string> getWordsOfType(string notation)
    {
        if (!wordsByType.ContainsKey(notation))
        {
            return new List<string>();
        }
        return new List<string>(wordsByType[notation]);
    }
}
