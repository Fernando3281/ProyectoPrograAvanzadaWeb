using Backend.Services.Implementations;
using Backend.Services.Interfaces;
using DAL.Implementations;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region BD

builder.Services.AddDbContext<DbGoshStoreContext>(options =>
                        options.UseSqlServer(
                            builder.
                            Configuration.
                            GetConnectionString("DefaultConnection")
                            ));

builder.Services.AddDbContext<AuthDbContext>(options =>
                        options.UseSqlServer(
                            builder.
                            Configuration.
                            GetConnectionString("DefaultConnection")
                            ));



#endregion

#region DI
builder.Services.AddDbContext<DbGoshStoreContext>();
builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();
builder.Services.AddScoped<IUsuarioDAL, UsuarioDALImpl>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ICarritoService, CarritoService>();

//
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ICategoriaDAL, CategoriaDALImpl>();
builder.Services.AddScoped<IDireccioneDAL, DireccioneDALImpl>();
builder.Services.AddScoped<IDireccioneService, DireccioneService>();
builder.Services.AddScoped<IProductoService, ProductService>();
builder.Services.AddScoped<IProductoDAL, ProductoDALImpl>();
builder.Services.AddScoped<ICarritoDAL, CarritoDALImpl>();
//
builder.Services.AddScoped<IPedidoDAL, PedidoDALImpl>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IDetallePedidoDAL, DetallePedidoDALImpl>();
builder.Services.AddScoped<IDetallePedidoService, DetallePedidoService>();
builder.Services.AddScoped<ITokenService, TokenService>();
#endregion

#region Identity

builder.Services.AddIdentityCore<IdentityUser>()
             .AddRoles<IdentityRole>()
            .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("fide")
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;

});

#endregion

#region Serilog 

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.File("logs/logsbackend.txt", rollingInterval: RollingInterval.Minute).MinimumLevel.Debug());

#endregion

#region JWT

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

})

    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();


app.UseMiddleware<ApiKeyManager>(); 

app.MapControllers();

app.Run();
