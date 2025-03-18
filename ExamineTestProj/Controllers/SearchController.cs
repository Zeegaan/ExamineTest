using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ExamineTestProj.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SearchController : ControllerBase
{
    private readonly SearchService _searchService;

    public SearchController(SearchService searchService)
    {
        _searchService = searchService;
    }
        
    [HttpPost]
    public IActionResult Index()
    {
        _searchService.Index();
        return Ok();
    }

    [HttpGet]
    public IActionResult SearchByName(string name)
    {
        _searchService.ByName(name);
        return Ok();
    }
}