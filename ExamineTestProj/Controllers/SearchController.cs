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
        var people = _searchService.ByName(name);
        return Ok(new ByNameViewModel()
        {
            Count = _searchService.Count(),
            People = people.ToList()
        });
    }

    [HttpGet]
    public IActionResult GetFacets()
    {
        var facets = _searchService.GetFacets();
        var viewModels = new List<FacetsViewModel>();
        foreach (var facet in facets)
        {
            viewModels.Add(new FacetsViewModel { Label = facet.Label, Count = facet.Value });
        }
        return Ok(viewModels);
    }

    public class ByNameViewModel()
    {
        public long Count { get; set; }
        public List<Person> People { get; set; }
    }

    public class FacetsViewModel
    {
        public string Label { get; set; }
        public float Count { get; set; }
    }
}