var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ParseInput.IInputParser, InputParser>();
builder.Services.AddSingleton<ITokenizer, BasicTokenizer>();
builder.Services.AddSingleton<INormalizer, BasicNormalizer>();
builder.Services.AddSingleton<IInvertedIndexSearch, InvertedIndexSearch>();
builder.Services.AddSingleton<IInvertedIndexDocumentAdder, InvertedIndexDocumentAdder>();
builder.Services.AddSingleton<IExtendedSearch, ExtendedSearch>();
builder.Services.AddSingleton<IFilter, AtLeastOneFilter>();
builder.Services.AddSingleton<IFilter, ExcludedFilter>();
builder.Services.AddSingleton<IFilter, NecessaryFilter>();


var app = builder.Build();

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
