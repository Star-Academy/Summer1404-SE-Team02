using System.Text.RegularExpressions;

public class BasicNormalizer : INormalizer
{
    public string Normalize(string token)
    {
        if (string.IsNullOrEmpty(token))
            return token;

        return Regex.Replace(token.ToLowerInvariant(), @"[^\w\s]", "");
    }
}
