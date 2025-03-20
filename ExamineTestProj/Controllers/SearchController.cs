using Core;
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

        var viewModel = MapToSearchViewModel(searchResults, labels);
        
        return Ok(viewModel);
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
    
    
    private SearchViewModel MapToSearchViewModel(ISearchResults results, string[]? labels)
    {
        var facetResult = results.GetFacet("Age");

        var facets = new List<IFacetValue>();
        if (facetResult is not null && labels is not null)
        {
            facets.AddRange(labels.Select(label => facetResult.Facet(label)));
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
}