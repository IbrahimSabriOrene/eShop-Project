using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShop.Web.Pages.Admin
{
    public class AdminPanel : PageModel
    {
        private readonly ILogger<AdminPanel> _logger;

        public AdminPanel(ILogger<AdminPanel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}