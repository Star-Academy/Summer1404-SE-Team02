using System.Collections.Generic;
using System.Text.RegularExpressions;
using ParseInput;

namespace InvertedIndexIR.InputParser
{
    public class InputParser : IInputParser
    {
        public List<string> ParseInput(string input, string pattern)
        {
            var matches = Regex.Matches(
                input.ToUpper(),
                pattern
            );
            var result = new List<string>();
            foreach (Match match in matches)
            {
                // Remove surrounding quotes if present
                var value = match.Value;
                if (value.StartsWith("\"") && value.EndsWith("\""))
                {
                    value = value.Substring(1, value.Length - 2);
                }
                result.Add(value);
            }
            return result;
        }
    }
}

