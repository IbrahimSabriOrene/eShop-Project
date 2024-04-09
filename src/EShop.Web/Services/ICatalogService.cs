using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Web.Models.Catalog;

namespace EShop.Web.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogItemModel>> GetCatalog();

        Task<IEnumerable<CatalogItemModel>> GetCatalogByCategory(string category);
        Task<IEnumerable<CatalogTypeModel>> GetCategories();
        Task<IEnumerable<CatalogBrandModel>> GetBrands();

        Task<CatalogItemModel> GetCatalog(string id);
        Task <IEnumerable<CatalogTypeModel>> GetType(int id);
        Task CreateCatalog(CatalogItemModel model);
        Task<CatalogItemModel> CreateBrand(CatalogBrandModel model);

    
    }
}