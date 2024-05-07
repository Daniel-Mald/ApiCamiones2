#region Program
#region Usings
using CamionesAPI.Models.Entities;
using CamionesAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
#endregion
var builder = WebApplication.CreateBuilder(args);
// Por favor no le muevan a la configuracion de la aplicacion 
#region Services
builder.Services.AddMvc().AddJsonOptions(
    x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);//Para ignorar los ciclos
builder.Services.AddControllers();
builder.Services.AddCors();//Para los recursos
#endregion
#region Agregar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CamionesAPI", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingresa el Token Bearer creado por la aplicacion",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
#endregion
#region Conexion a la base de datos
string? DB = builder.Configuration.GetConnectionString("CamionesDBConnectionString");
builder.Services.AddMySql<Sistem21TestdbContext>(DB, ServerVersion.AutoDetect(DB));
#endregion
#region JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
   jwt =>
        jwt.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audiance"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"] ?? "")),
            ValidateAudience = true,
            ValidateIssuer = true
        });
#endregion
#region Transient`s
builder.Services.AddTransient<UsuariosRepository>();
builder.Services.AddTransient<ChoferRepository>();
builder.Services.AddTransient<UnidadRepository>();
builder.Services.AddTransient<ViajesRepository>();
builder.Services.AddTransient<GenericRepository<Vwvistageneral>>();
builder.Services.AddTransient<GastoRepository>();
#endregion
var app = builder.Build();
app.UseCors();
app.MapControllers();
#region Implementacion de swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion
app.Run();
#endregion