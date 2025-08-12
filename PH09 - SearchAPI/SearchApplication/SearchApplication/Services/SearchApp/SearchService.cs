using System.Collections.Generic;
using InvertedIndexIR.DTO;
using InvertedIndexIR.Filters;
using InvertedIndexIR.InputParser;
using InvertedIndexIR.InputParser.Abstraction;
using InvertedIndexIR.InvertedIndexDocumentAdder;
using InvertedIndexIR.InvertedIndexSearch;
using InvertedIndexIR.QueryBuilder;
using InvertedIndexIR.QueryBuilder.Abstraction;
using InvertedIndexIR.QueryGetWordsOfType;
using InvertedIndexIR.Search.Abstraction;
using InvertedIndexIR.Search.Extended;
using SearchApp.Abstraction;

namespace SearchApp;

public class SearchService :ISearchService
{
    private readonly InvertedIndex _index;
    private readonly IExtendedSearch _extendedSearch;
    private readonly IInputParser _parser;
    private readonly IQueryBuilder _queryBuilder;

    public SearchService()
    {
        var tokenizer = new BasicTokenizer();
        var normalizer = new BasicNormalizer();
        _parser = new InputParser();
        _index = new InvertedIndex();
        _queryBuilder = new QueryBuilder();
        var typesOfWordGetter = new QueryWordsOfTypeGetter();

        var adder = new InvertedIndexDocumentAdder(tokenizer, normalizer);
        var filePaths = FileReader.ReadAllFileNames("./Search/EnglishData"); 

        foreach (var path in filePaths)
        {
            var content = File.ReadAllText(path);
            adder.AddDocument(content, path, _index);
        }
        Console.WriteLine(filePaths.Length + "FILES FOUND");
        Console.WriteLine(Directory.GetCurrentDirectory() + " current directory");

        var indexSearch = new InvertedIndexSearch(tokenizer, normalizer);

        _extendedSearch = new ExtendedSearch();
        _extendedSearch.AddFilter(new AtLeastOneFilter(indexSearch, typesOfWordGetter));
        _extendedSearch.AddFilter(new NecessaryFilter(indexSearch, typesOfWordGetter));
        _extendedSearch.AddFilter(new ExcludedFilter(indexSearch, typesOfWordGetter));
    }

    public IEnumerable<string> Search(string rawQuery)
    {
        var parsedWords = _parser.ParseInput(rawQuery, @"[+-]?[\""].+?[\""]|\S+", new List<string> { "+", "-" });
        var query = _queryBuilder.BuildQuery(parsedWords, new List<string> { "+", "-" });
        return _extendedSearch.Search(query, _index);
    }
}
