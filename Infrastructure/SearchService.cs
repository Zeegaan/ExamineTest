using Examine;

namespace Infrastructure;

public class SearchService
{
    private readonly IExamineManager _examineManager;
    private readonly IPersonRepository _personRepository;

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

    public List<Person> ByName(string name)
    {
        var index = GetIndex();
        
        // Create a query
        var results = index.Searcher.CreateQuery()
            .Field("FirstName", name)
            .Execute();

        return [];
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