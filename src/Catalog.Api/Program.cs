using Catalog.Api;
using Catalog.Api.Controllers;
using Catalog.Api.Helpers;
using Catalog.Api.Repositories;
using Catalog.Api.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCatalogServices();
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection(nameof(DbSettings)));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DbContext>();
    await context.Init();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapCatalogApi();

app.Run();
