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
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://localhost:7044",
                "http://localhost:5047");
        });
});
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
app.UseCors();

app.Run();