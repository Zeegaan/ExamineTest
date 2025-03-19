using Examine;
using Examine.Lucene;
using Microsoft.Extensions.Options;

namespace FacetBlog.Search;

public class ConfigureExternalIndexOptions : IConfigureNamedOptions<LuceneDirectoryIndexOptions>
{
    private readonly IServiceProvider _serviceProvider;

    public ConfigureExternalIndexOptions(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Configure(string name, LuceneDirectoryIndexOptions options)
    {
        if (name.Equals("MyIndex"))
        {
            // Index the price field as a facet of the type long (int64)
            options.FieldDefinitions.AddOrUpdate(new FieldDefinition("Age", FieldDefinitionTypes.FacetTaxonomyInteger));
            options.UseTaxonomyIndex = true;

            // The standard directory factory does not work with the taxonomi index.
            // If running on azure it should use the syncedTemp factory
            options.DirectoryFactory = _serviceProvider.GetRequiredService<global::Examine.Lucene.Directories.TempEnvFileSystemDirectoryFactory>();
        }
    }

    public void Configure(LuceneDirectoryIndexOptions options)
    {
        throw new System.NotImplementedException();
    }
}