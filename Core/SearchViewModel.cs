namespace Core;

public class SearchViewModel
{
    public long PeopleCount => People.Count;
    public long TotalCount { get; set; }
        
    public List<FacetViewModel> Facets { get; set; }
    public List<Person> People { get; set; }

}