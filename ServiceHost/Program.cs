using _0_Framework.Application;
using DiscountManagement.Configuration;
using InventoryManagement.Configuration;
using ServiceHost.Requirements;
using ShopManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("SogandShopConnectionString");

ShopManagementBootstrapper.Configure(services,connectionString);
DiscountManagementBootstrapper.Configure(services,connectionString);
InventoryManagementBootstrapper.Configure(services,connectionString);


services.AddTransient<IFileUploader,FileUploader>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
