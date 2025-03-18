using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ExamineTestProj;

[ApiController]
[Route("[controller]/[action]")]
public class PersonController : ControllerBase
{
    private readonly IPersonRepository _personRepository;

    public PersonController(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    [HttpGet]
    public IActionResult All()
    {
        return Ok(_personRepository.All());
    }
    
    [HttpGet]
    public IActionResult ById(int id)
    {
        return Ok(_personRepository.Get(id));
    }

    [HttpPost]
    public IActionResult Create(Person person)
    {
        _personRepository.Create(person);
        return Ok();
    }
    
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        _personRepository.Delete(id);
        return Ok();
    }
}