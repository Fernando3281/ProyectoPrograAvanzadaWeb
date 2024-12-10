using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login/login";
    });

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = false;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddHttpContextAccessor();

// Dependency Injection (DI)
builder.Services.AddHttpClient<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IUsuarioHelper, UsuarioHelper>();
builder.Services.AddScoped<IProductoHelper, ProductoHelper>();
builder.Services.AddScoped<ICategoriaHelper, CategoriaHelper>();
builder.Services.AddScoped<IPedidoHelper, PedidoHelper>();
builder.Services.AddScoped<ISecurityHelper, SecurityHelper>();
builder.Services.AddScoped<ICarritoHelper, CarritoHelper>();
builder.Services.AddScoped<IDetallePedidoHelper, DetallePedidoHelper>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Antes de Authentication
app.UseAuthentication(); // Antes de Authorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
