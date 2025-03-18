using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ExamineTestProj.Controllers;

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
    public IActionResult ById(Guid id)
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
    public IActionResult Delete(Guid id)
    {
        _personRepository.Delete(id);
        return Ok();
    }
}