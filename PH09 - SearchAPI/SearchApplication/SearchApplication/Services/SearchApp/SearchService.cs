using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using InvertedIndexIR.DTO;
using InvertedIndexIR.Filters;
using InvertedIndexIR.InputParser;
using InvertedIndexIR.InputParser.Abstraction;
using InvertedIndexIR.InvertedIndexDocumentAdder;
using InvertedIndexIR.InvertedIndexDocumentAdder.Abstraction;
using InvertedIndexIR.InvertedIndexSearch;
using InvertedIndexIR.QueryBuilder;
using InvertedIndexIR.QueryBuilder.Abstraction;
using InvertedIndexIR.QueryGetWordsOfType;
using InvertedIndexIR.Search.Abstraction;
using InvertedIndexIR.Search.Extended;
using SearchApp.Abstraction;
using SearchApplication.ActivityResources;

namespace SearchApp;
[ExcludeFromCodeCoverage]


public class SearchService :ISearchService
{
    private readonly InvertedIndex _index;
    private readonly IExtendedSearch _extendedSearch;
    private readonly IInputParser _parser;
    private readonly IQueryBuilder _queryBuilder;
    private Instrumentation _instrumentation;

    public SearchService(IExtendedSearch extendedSearch, IInputParser parser, IQueryBuilder queryBuilder,
        IInvertedIndexDocumentAdder  invertedIndexDocumentAdder, Instrumentation instrumentation)
    {
        _parser = parser;
        _queryBuilder = queryBuilder;
        _extendedSearch = extendedSearch;
        _index = new InvertedIndex();
        _instrumentation = instrumentation;
        var filePaths = FileReader.ReadAllFileNames("./Search/EnglishData"); 

        foreach (var path in filePaths)
        {
            var content = File.ReadAllText(path);
            invertedIndexDocumentAdder.AddDocument(content, path, _index);
        }
        Console.WriteLine(filePaths.Length + "FILES FOUND");
        Console.WriteLine(Directory.GetCurrentDirectory() + " current directory");
    }

    public IEnumerable<string> Search(string rawQuery)
    {
        using var activity = _instrumentation.ActivitySource.StartActivity("SearchService.Search");
        activity?.SetTag("query.raw", rawQuery);
        activity?.SetTag("query.length", rawQuery.Length);

        var parsedWords = _parser.ParseInput(rawQuery, @"[+-]?[\""].+?[\""]|\S+");
        activity?.AddEvent(new ActivityEvent("ParsedWords", 
            tags: new ActivityTagsCollection { { "parsed.count", parsedWords.Count } }));

        var query = _queryBuilder.BuildQuery(parsedWords, new List<string> { "+", "-" });
        activity?.AddEvent(new ActivityEvent("BuiltQuery"));

        var results = _extendedSearch.Search(query, _index);
        activity?.SetTag("results.count", results.Count());

        return results;
    }
}
