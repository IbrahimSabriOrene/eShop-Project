using Catalog.Api.Repositories;

namespace Catalog.Api.Services
{
    public class CatalogService 
    {
        
        public CatalogRepository Repository { get; }
        public CatalogService(CatalogRepository repository)
        {
            this.Repository = repository;
        }
    }
}