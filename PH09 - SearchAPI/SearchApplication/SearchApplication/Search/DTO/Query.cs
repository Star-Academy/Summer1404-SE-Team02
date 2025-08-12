using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace InvertedIndexIR.DTO;
[ExcludeFromCodeCoverage]

public class Query
{
    public Dictionary<string, List<string>> ParsedWords;
}
