using EShop.Web.Models.Catalog;
using EShop.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace EShop.Web.Pages.Admin
{
    public class AdminPanel : PageModel
    {
        private readonly ILogger<AdminPanel> _logger;
        private readonly ICatalogService _catalogService;
        

        public AdminPanel(ILogger<AdminPanel> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));

        }
        public IEnumerable<CatalogItemModel> ProductList { get; set; } = new List<CatalogItemModel>();
        public IEnumerable<CatalogBrandModel> BrandList { get; set; } = new List<CatalogBrandModel>();
        public IEnumerable<CatalogTypeModel> CategoryList { get; set; } = new List<CatalogTypeModel>();
        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await _catalogService.GetCatalog();
            CategoryList = await _catalogService.GetCategories();
            BrandList = await _catalogService.GetBrands();

            return Page();
        }
    }
}