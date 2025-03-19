using Examine;
using FacetBlog.Search;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSingleton<IPersonRepository, PersonRepository>();
builder.Services.AddSingleton<SearchService>();
// Adds Examine Core services
builder.Services.AddExamine();

// Create a Lucene based index
builder.Services.AddExamineLuceneIndex("MyIndex");
builder.Services.ConfigureOptions<ConfigureExternalIndexOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();