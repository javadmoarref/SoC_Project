using _0_Framework.Application;
using _0_Framework.Application.Sms;
using _0_Framework.Application.ZarinPal;
using _0_Framework.Infrastructure;
using AccountManagement.Configuration;
using BlogManagement.Configuration;
using CommentManagement.Configuration;
using DiscountManagement.Configuration;
using InventoryManagement.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using ServiceHost.Requirements;
using ShopManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("SogandShopConnectionString");

services.AddHttpContextAccessor();
ShopManagementBootstrapper.Configure(services,connectionString);
DiscountManagementBootstrapper.Configure(services,connectionString);
InventoryManagementBootstrapper.Configure(services,connectionString);
BlogManagementBootstrapper.Configure(services,connectionString);
CommentManagementBootstrapper.Configure(services, connectionString);
AccountManagementBootstrapper.Configure(services,connectionString);

services.AddSingleton<IPasswordHasher, PasswordHasher>();
services.AddTransient<IFileUploader,FileUploader>();
services.AddTransient<IAuthHelper, AuthHelper>();
services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
services.AddTransient<ISmsService, SmsService>();

services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
    {
        o.LoginPath = new PathString("/Account");
        o.LogoutPath = new PathString("/Account");
        o.AccessDeniedPath = new PathString("/AccessDenied");
    });

services.AddAuthorization(option => option.AddPolicy("AdminArea",
    x => x.RequireRole(new List<string> { Roles.Administrator })));

builder.Services.AddRazorPages()
    .AddRazorPagesOptions(option=>
        option.Conventions.AuthorizeAreaFolder("Administration","/","AdminArea"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
