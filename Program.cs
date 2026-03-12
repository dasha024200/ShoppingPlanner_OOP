using ShoppingPlanner_OOP.Factory;
using ShoppingPlanner_OOP.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(4);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("uk-UA");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("uk-UA");

builder.Services.AddSingleton<DefaultItemFactory>();
builder.Services.AddSingleton<PriceCalculator>();
builder.Services.AddScoped<BudgetService>();
builder.Services.AddScoped<BudgetObserver>();
builder.Services.AddScoped<SortContext>();
builder.Services.AddScoped<ShoppingSessionService>();
builder.Services.AddSingleton<ShoppingListJsonService>();
builder.Services.AddSingleton<CsvExportService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Shopping}/{action=Index}/{id?}");

app.Run();