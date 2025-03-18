namespace Infrastructure;


public class Person(string firstName, string surName, string email, int age)
{
    public string FirstName { get; init; } = firstName;

    public string Surname { get; init; } = surName;

    public string Email { get; init; } = email;

    public int Age { get; init; } = age;
    
    public Guid Id { get; init; } = Guid.NewGuid();
}