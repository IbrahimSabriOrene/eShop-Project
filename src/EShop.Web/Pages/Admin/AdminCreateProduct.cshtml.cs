using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Web.Models.Catalog;
using EShop.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.Extensions.Logging;

namespace EShop.Web.Pages.Admin
{
    public class AdminCreateProduct : PageModel
    {
        private readonly ILogger<AdminCreateProduct> _logger;
        private readonly ICatalogService _catalogService;

        public AdminCreateProduct(ILogger<AdminCreateProduct> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        }
        public IEnumerable<CatalogBrandModel> BrandList { get; set; } = new List<CatalogBrandModel>();
        public IEnumerable<CatalogTypeModel> CategoryList { get; set; } = new List<CatalogTypeModel>();
        public async Task OnGetAsync()
        {
            await Task.CompletedTask;

            BrandList = await _catalogService.GetBrands();
            CategoryList = await _catalogService.GetCategories();
        }

        public async Task OnPostAsync()
        {
            await Task.CompletedTask;
            var model = new CatalogItemModel
            {
                Name = Request.Form["name"],
                ImageFile = Request.Form["image"],
                CatalogTypeId = Convert.ToInt32(Request.Form["category"]),
                CatalogBrandId = Convert.ToInt32(Request.Form["brand"]),
                Summary = Request.Form["summary"],
                Description = Request.Form["description"],
                Price = Convert.ToDecimal(Request.Form["price"])
                
            };
            

            await _catalogService.CreateCatalog(model);


        }
    }
}