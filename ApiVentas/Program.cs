using System.Text;
using ApiVentas;
using ApiVentas.Common;
using ApiVentas.DAO;
using ApiVentas.Models;
using ApiVentas.Repositories;
using ApiVentas.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<VentasContext>(option =>{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Ventas",
        Description = "Tiendita",
        Version = "v1",
        Contact = new OpenApiContact{
            Name = "Api ventas",
            Url = new Uri("https://example.com/ventas")
        }
    });
    var basePath = AppContext.BaseDirectory;
    var assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
    var fileName = System.IO.Path.GetFileName(assemblyName+".xml");
    var xmlPath = Path.Combine(basePath, fileName);
    c.IncludeXmlComments(xmlPath);
});

//Injeccion Dao
builder.Services.AddScoped<IClienteDAO, ClienteDAO>();

//JWT
var jwtAppSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSetting>(jwtAppSettings);

var appSetting = jwtAppSettings.Get<JwtSetting>();
var key = Encoding.ASCII.GetBytes(appSetting.Code);
builder.Services.AddAuthentication(a=>
{
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(a=>
{
    a.RequireHttpsMetadata=false;
    a.SaveToken = true;
    a.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json","Ventas v1");
    });
}

app.UseHttpsRedirection();
//authJwt
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
