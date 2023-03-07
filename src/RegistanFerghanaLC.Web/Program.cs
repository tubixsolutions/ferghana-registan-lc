using Microsoft.OpenApi.Models;
using RegistanFerghanaLC.Web.Configuration.LayerConfigurations;
using RegistanFerghanaLC.Web.Midllewares;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureDataAccess(builder.Configuration);
builder.Services.AddWeb(builder.Configuration);
builder.Services.AddService();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Registon API", Version = "v1"});
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Registon API V1");
    c.RoutePrefix = "area/swagger";
});
app.UseMiddleware<TokenRedirectMiddleware>();

app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        context.HttpContext.Response.Redirect("accounts/login");
    }
});

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
