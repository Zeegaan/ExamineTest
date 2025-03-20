using Core;

namespace Infrastructure;

public interface IPersonRepository
{
    List<Person> All();
    void Create(Person person);
    Person? Get(Guid id);
    void Delete(Guid id);
}