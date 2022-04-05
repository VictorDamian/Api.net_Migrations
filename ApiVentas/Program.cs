using ApiVentas;
using ApiVentas.Models;
using ApiVentas.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        Title = "Libreria",
        Description = "Libros chidos",
        Version = "v1"
    });
});
//Mapper
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Perfiles a ligar
//Inyeccion mapper
//agregar al servicio
var mapperCfg = new MapperConfiguration(m=>
{
    m.AddProfile(new MappingProfile());
});

IMapper mapper = mapperCfg.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json","Libreria v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
