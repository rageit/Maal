using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Maal.Data;
using Maal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MaalContext>(options => 
    options.UseSqlite(
        builder.Configuration.GetConnectionString("MaalContext")
        ?? throw new InvalidOperationException("Connection string 'MaalContext' not found.")));
builder.Services.AddTransient<IAppSettingsProvider, AppSettingsProvider>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

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
