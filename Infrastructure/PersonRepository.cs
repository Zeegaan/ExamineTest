namespace Infrastructure;

public class PersonRepository : IPersonRepository
{
    private List<Person> _people;
    private int IdCount = 1;

    public PersonRepository()
    {
        _people =
        [
            new Person("Nikolaj", "Geisle", "nge@ubmraoc.dk", 29),
            new Person("Nikolaj", "Aaaa", "nny@email.ciom", 15)
        ];
    }
    public List<Person> All()
    {
        return _people;
    }

    public void Create(Person person)
    {
        person.Id = IdCount;
        IdCount++;
        _people.Add(person);
    }

    public Person? Get(int id)
    {
        return _people.FirstOrDefault(x => x.Id == id);
    }

    public void Delete(int id)
    {
        var person = Get(id);
        if (person is not null)
        {
            _people.Remove(person);
        }
    }
}