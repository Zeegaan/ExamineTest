using Examine;
using Examine.Lucene;
using Examine.Search;
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
    public IActionResult Search(string[]? labels = null)
    {
        var searchResults = _searchService.Search(labels);
        
        return Ok(MapToSearchViewModel(searchResults));
    }


    [HttpGet]
    public IActionResult GetFacets()
    {
        var facets = _searchService.GetFacets();
        var viewModels = new List<FacetViewModel>();
        foreach (var facet in facets)
        {
            viewModels.Add(new FacetViewModel { Label = facet.Label, Count = facet.Value });
        }
        return Ok(viewModels);
    }

    [HttpGet]
    public IActionResult SelectFacets(string[] labels)
    {
        var facets = _searchService.SelectFacets(labels);
        var viewModels = new List<FacetViewModel>();
        foreach (var facet in facets)
        {
            viewModels.Add(new FacetViewModel { Label = facet.Label, Count = facet.Value });
        }
        return Ok(viewModels);
    }
    
    
    private SearchViewModel MapToSearchViewModel(ISearchResults results, string[]? labels)
    {
        var facetResult = results.GetFacet("Age");

        var facets = new List<IFacetValue>();
        foreach (var label in labels)
        {
            facets.Add(facetResult.Facet(label));
        }
        
        return new SearchViewModel
        {
            Facets = facets.Select(x => MapToFaceViewModel(x)).ToList(),
            People = results.Select(x => MapToPerson(x)).ToList(),
            TotalCount = results.TotalItemCount,
        };
    }

    private FacetViewModel MapToFaceViewModel(IFacetValue facetResult)
    {
        return new FacetViewModel
        {
            Label = facetResult.Label,
            Count = facetResult.Value
        };
    }
    
    private Person MapToPerson(ISearchResult result)
    {
        return new Person(
            result.Values["FirstName"],
            result.Values["LastName"],
            result.Values["Email"],
            int.Parse(result.Values["Age"])
        );
    }
    
    
    public class SearchViewModel
    {
        public long PeopleCount => People.Count;
        public long TotalCount { get; set; }
        
        public List<FacetViewModel> Facets { get; set; }
        public List<Person> People { get; set; }

    }

    public class FacetViewModel
    {
        public string Label { get; set; }
        public float Count { get; set; }
    }
}