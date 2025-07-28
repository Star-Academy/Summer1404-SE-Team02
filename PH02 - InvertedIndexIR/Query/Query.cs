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

        var matches = System.Text.RegularExpressions.Regex.Matches(
            input.ToUpper(),
            @"([+-]?""[^""]+""|[+-]?\S+)"
        );

        foreach (System.Text.RegularExpressions.Match match in matches)
        {
            var token = match.Value;
            string word = token;

            if (token.StartsWith("+"))
            {
                word = token[1..];
                if (word.StartsWith("\"") && word.EndsWith("\""))
                    word = word[1..^1];
                wordsByType["+"].AddLast(word);
            }
            else if (token.StartsWith("-"))
            {
                word = token[1..];
                if (word.StartsWith("\"") && word.EndsWith("\""))
                    word = word[1..^1];
                wordsByType["-"].AddLast(word);
            }
            else
            {
                if (word.StartsWith("\"") && word.EndsWith("\""))
                    word = word[1..^1];
                wordsByType[""].AddLast(word);
            }
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
