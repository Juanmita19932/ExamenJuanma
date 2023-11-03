using ExamenJuanManuelMu�iz.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DemoContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "MiCookie"; // Nombre de la cookie
        options.Cookie.HttpOnly = true; // Solo accesible a trav�s de HTTP
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Tiempo de expiraci�n de la cookie
        options.LoginPath = "/Account/Login"; // Ruta de inicio de sesi�n     
        options.AccessDeniedPath = "/Account/AccessDenied"; // Ruta de acceso denegado
        options.SlidingExpiration = true; // Renovar la cookie autom�ticamente en cada solicitud
    });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
