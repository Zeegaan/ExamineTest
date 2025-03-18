namespace Infrastructure;

public interface IPersonRepository
{
    List<Person> All();
    void Create(Person person);
    Person? Get(int id);
    void Delete(int id);
}