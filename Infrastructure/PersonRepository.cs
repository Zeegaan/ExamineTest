using Bogus;

namespace Infrastructure;

public class PersonRepository : IPersonRepository
{
    private List<Person> _people;

    public PersonRepository()
    {
        // Create a new Faker instance for a person
        var personFaker = new Faker<Person>()
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.Email, f => f.Internet.Email())
            .RuleFor(p => p.Age, f => f.Random.Number(1, 99));

        // Generate a single fake person
        _people = personFaker.Generate(1000);
    }
    public List<Person> All()
    {
        return _people;
    }

    public void Create(Person person)
    {
        _people.Add(person);
    }

    public Person? Get(Guid id)
    {
        return _people.FirstOrDefault(x => x.Id == id);
    }

    public void Delete(Guid id)
    {
        var person = Get(id);
        if (person is not null)
        {
            _people.Remove(person);
        }
    }
}