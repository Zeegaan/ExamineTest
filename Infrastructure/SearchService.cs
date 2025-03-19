using Examine;
using Examine.Lucene;
using Examine.Search;

namespace Infrastructure;

public class SearchService
{
    private readonly IExamineManager _examineManager;
    private readonly IPersonRepository _personRepository;

    private Int64Range[] _ranges = new[]
    {
        new Int64Range("0-9", 0, true, 9, true),
        new Int64Range("10-19", 10, true, 19, true),
        new Int64Range("20-29", 20, true, 29, true),
        new Int64Range("30-39", 30, true, 39, true),
        new Int64Range("40-49", 40, true, 49, true),
        new Int64Range("50-59", 50, true, 59, true),
        new Int64Range("60-69", 60, true, 69, true),
        new Int64Range("70-79", 70, true, 79, true),
        new Int64Range("80-89", 80, true, 89, true),
        new Int64Range("90+", 90, true, long.MaxValue, true),
    };

    public SearchService(IExamineManager examineManager, IPersonRepository personRepository)
    {
        _examineManager = examineManager;
        _personRepository = personRepository;
    }

    public void Index()
    {
        var index = GetIndex();

        foreach (var person in _personRepository.All())
        {
            index.IndexItem(new ValueSet(
                person.Id.ToString(),  //Give the doc an ID of your choice
                "Person",               //Each doc has a "Category"
                new Dictionary<string, object>()
                {
                    {"FirstName", person.FirstName },
                    {"LastName", person.FirstName },
                    {"Email", person.Email },
                    {"Age", person.Age}
                }));
        }
    }
    
    public ISearchResults Search(string[]? labels = null)
    {
        var index = GetIndex();
        // Create a query
        var queryBuilder = index.Searcher.CreateQuery("Person");

        var rangesToSearch = labels is null ? _ranges : _ranges.Where(x => labels.Contains(x.Label)).ToArray();
        var results = queryBuilder.All()
            .WithFacets(f => f.FacetLongRange("Age", rangesToSearch))
            .Execute();

        return results;
    }

    public IEnumerable<Person> ByName(string name)
    {
        var index = GetIndex();
        
        // Create a query
        var results = index.Searcher.CreateQuery()
            .Field("FirstName", name)
            .Execute();

        foreach (ISearchResult result in results)
        {
            yield return MapToPerson(result);
        }
    }
    
    private Person MapToPerson(ISearchResult result)
    {
        return new Person(
            result.Values["FirstName"],
            result.Values["LastName"],
            result.Values["Email"],
            int.Parse(result.Values["Age"])
        );
    }

    public IEnumerable<IFacetValue> GetFacets()
    {
        return SearchWithFacets();
    }
    
    public IEnumerable<IFacetValue> SelectFacets(string[] labels)
    {
        return SearchWithFacets(labels);
    }

    private IFacetResult SearchWithFacets(string[]? labels = null)
    {
        var index = GetIndex();
        // Create a query
        var queryBuilder = index.Searcher.CreateQuery("Person");

        var rangesToSearch = labels is null ? _ranges : _ranges.Where(x => labels.Contains(x.Label)).ToArray();
        var results = queryBuilder.All()
            .WithFacets(f => f.FacetLongRange("Age", rangesToSearch))
            .Execute();

        return results.GetFacet("Age");
    }

    private IIndex GetIndex()
    {
        if (!_examineManager.TryGetIndex("MyIndex", out IIndex? index))
        {
            throw new InvalidOperationException("No index found by");
        }

        return index;
    }
}