using Auth0.AspNetCore.Authentication;
using EShop.Web.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
{
  services.AddRazorPages();

  services.AddAuth0WebAppAuthentication(options =>
      {
        options.Domain = configuration["Auth0:Domain"];
        options.ClientId = configuration["Auth0:ClientId"];
      });
  services.AddRazorPages();


  services.AddHttpClient<ICatalogService, CatalogService>(
    c => c.BaseAddress = new Uri(configuration["ApiSettings:GatewayAddress"]));

  services.AddHttpClient<IBasketService, BasketService>(
 c => c.BaseAddress = new Uri(configuration["ApiSettings:GatewayAddress"]));

}
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseExceptionHandler("/Error");
  app.UseHsts();
}

{
  app.UseHttpsRedirection();
  app.UseStaticFiles();
  app.UseRouting();
  app.UseAuthentication();
  app.UseAuthorization();
  app.MapRazorPages();
}

app.Run();
