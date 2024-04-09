using EShop.Web.Models.Catalog;
using EShop.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ICatalogService _catalogService;
    private readonly IBasketService _basketService;



    public IndexModel(ILogger<IndexModel> logger, ICatalogService catalogService, IBasketService basketService)
    {
        _logger = logger;
        _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
    }
    public IEnumerable<CatalogItemModel> ProductList { get; set; } = new List<CatalogItemModel>();
    public IEnumerable<CatalogTypeModel> CategoryList { get; set; } = new List<CatalogTypeModel>();

    public async Task<IActionResult> OnGetAsync()
    {
        ProductList = await _catalogService.GetCatalog();
        CategoryList = await _catalogService.GetCategories();
        
        
        return Page();
    }
}

