using System.Collections.Generic;
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

namespace SearchApp;
[ExcludeFromCodeCoverage]


public class SearchService :ISearchService
{
    private readonly InvertedIndex _index;
    private readonly IExtendedSearch _extendedSearch;
    private readonly IInputParser _parser;
    private readonly IQueryBuilder _queryBuilder;

    public SearchService(IExtendedSearch extendedSearch, IInputParser parser, IQueryBuilder queryBuilder,
        IInvertedIndexDocumentAdder  invertedIndexDocumentAdder)
    {
        _parser = parser;
        _queryBuilder = queryBuilder;
        _extendedSearch = extendedSearch;
        _index = new InvertedIndex();
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
        var parsedWords = _parser.ParseInput(rawQuery, @"[+-]?[\""].+?[\""]|\S+");
        var query = _queryBuilder.BuildQuery(parsedWords, new List<string> { "+", "-" });
        return _extendedSearch.Search(query, _index);
    }
}
