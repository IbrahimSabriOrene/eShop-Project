using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EShop.Pages;
public class ProfileModel : PageModel
{
    public string UserName { get; set; } = null!;
    public string? UserEmailAddress { get; set; }
    public string? UserProfileImage { get; set; }
    public void OnGet()
    {
        if (!User.Identity!.IsAuthenticated)
        {
                Response.Redirect("/Account/Login");
        }

        LoadProfileInfo();

    }

    public void LoadProfileInfo()
    {
        UserName = User.Identity!.Name!;
        UserEmailAddress = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
        UserProfileImage = User.FindFirst(c => c.Type == "picture")?.Value;
    }

}
