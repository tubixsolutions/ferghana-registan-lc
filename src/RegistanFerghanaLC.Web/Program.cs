using Microsoft.Extensions.DependencyInjection;
using RegistanFerghanaLC.Web.Configuration.LayerConfigurations;
using RegistanFerghanaLC.Web.Midllewares;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureDataAccess(builder.Configuration);
builder.Services.AddWeb(builder.Configuration);
builder.Services.AddService();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseMiddleware<TokenRedirectMiddleware>();

//app.UseStatusCodePages(async context =>
//{
//    if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
//    {
//        context.HttpContext.Response.Redirect("accounts/login");
//    }
//});

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
