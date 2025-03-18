using Microsoft.AspNetCore.Mvc;

namespace ExamineTestProj;

[ApiController]
[Route("[controller]/[action]")]
public class PersonController : ControllerBase
{
    private Person[] _people = {
        new("Nikolaj", "Geisle", "nge@ubmraoc.dk", 29),
        new("Nikolaj", "Aaaa", "nny@email.ciom", 15),
    };
    
    [HttpGet]
    public IActionResult All()
    {
        return Ok(_people);
    }
}

public class Person(string firstName, string surName, string email, int age)
{
    public string FirstName { get; init; } = firstName;
    
    public string Surname { get; init; } = surName;

    public string Email { get; init; } = email;

    public int Age { get; init; } = age;
}