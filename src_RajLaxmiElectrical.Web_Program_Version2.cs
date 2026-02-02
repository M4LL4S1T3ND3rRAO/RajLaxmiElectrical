using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RajLaxmiElectrical.Web.Data;
using Microsoft.EntityFrameworkCore;
using RajLaxmiElectrical.Web.Services;
using Microsoft.AspNetCore.Identity;
using RajLaxmiElectrical.Web.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__DEFAULTCONNECTION");

// Add services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// EF Core + Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();

// App services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBarcodeService, BarcodeService>();
builder.Services.AddHttpClient();

// Configure PayPal credentials in configuration
builder.Services.Configure<PayPalSettings>(builder.Configuration.GetSection("PayPal"));

// Build
var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();