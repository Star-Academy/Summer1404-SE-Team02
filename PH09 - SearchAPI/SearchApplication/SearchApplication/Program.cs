using System.Diagnostics.CodeAnalysis;
using InvertedIndexIR.Filters;
using InvertedIndexIR.Filters.Abstraction;
using InvertedIndexIR.InputParser;
using InvertedIndexIR.InputParser.Abstraction;
using InvertedIndexIR.InvertedIndexDocumentAdder;
using InvertedIndexIR.InvertedIndexDocumentAdder.Abstraction;
using InvertedIndexIR.InvertedIndexSearch;
using InvertedIndexIR.InvertedIndexSearch.Abstracion;
using InvertedIndexIR.QueryBuilder;
using InvertedIndexIR.QueryBuilder.Abstraction;
using InvertedIndexIR.QueryGetWordsOfType;
using SearchApp;
using SearchApp.Abstraction;
using InvertedIndexIR.QueryGetWordsOfType.Abstraction;
using InvertedIndexIR.Search.Abstraction;
using InvertedIndexIR.Search.Extended;
using OpenTelemetry.Trace;
using Prometheus;
using SearchApplication.ActivityResources;

[ExcludeFromCodeCoverage]
class Program
{
    public static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<ITokenizer, BasicTokenizer>();
        builder.Services.AddSingleton<INormalizer, BasicNormalizer>();
        builder.Services.AddSingleton<IInvertedIndexDocumentAdder,InvertedIndexDocumentAdder>();
        builder.Services.AddSingleton<IInvertedIndexSearch, InvertedIndexSearch>();
        builder.Services.AddSingleton<IInputParser, InputParser>();
        builder.Services.AddSingleton<IQueryBuilder, QueryBuilder>();
        builder.Services.AddSingleton<IQueryWordsOfTypeGetter, QueryWordsOfTypeGetter>();
        builder.Services.AddSingleton<IExtendedSearch, ExtendedSearch>();
        builder.Services.AddSingleton<IFilter, NecessaryFilter>();
        builder.Services.AddSingleton<IFilter, AtLeastOneFilter>();
        builder.Services.AddSingleton<IFilter, ExcludedFilter>();

        builder.Services.AddSingleton<ISearchService, SearchService>();
        builder.Services.AddSingleton<Instrumentation>();
        
        builder.Services.AddOpenTelemetry()
            .WithTracing(tracerProviderBuilder =>
            {
                tracerProviderBuilder
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSource("SearchActivity")
                    .AddJaegerExporter(o =>
                    {
                        o.AgentHost = "localhost";
                        o.AgentPort = 6831;
                    });
            });

        var app = builder.Build();
        // app.UseMetricServer(9184);
        app.UseHttpMetrics(); 
        app.MapMetrics();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}
