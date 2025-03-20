namespace Core;

public class Person
{
    public Person()
    {
        
    }    
    
    public Person(string firstName, string lastName, string email, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Age = age;
    }
    
    
    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Email { get; init; }

    public int Age { get; init; }
    
    public Guid Id { get; init; } = Guid.NewGuid();
}