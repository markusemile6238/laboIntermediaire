using Agency.API.Middleware;
using Agency.BLL.Services;
using Agency.DAL.Database;
using Agency.DAL.Repositories;
using Agency.Domaine.Repositories;
using Microsoft.OpenApi.Models;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

//logging
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
    logging.SetMinimumLevel(LogLevel.Debug);
});

// Charger la configuration de l'API
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

//Récupérer la connection string de l'API
var connectionString = builder.Configuration.GetConnectionString("main");


// Ajout de controller
builder.Services.AddControllers();

// configuration du cors
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});

// Add services to the container.
builder.Services.AddScoped<IDbConnectionFactory>(sp =>
{
var configuration = sp.GetRequiredService<IConfiguration>();
return new DbConnectionFactory(configuration);
});
builder.Services.AddScoped<IDbCommandFactory, DbCommandFactory>();

// DALL
builder.Services.AddScoped<IDestinationRepo, DestinationRepository>();

//BLL
builder.Services.AddScoped<IDestinationService, DestinationService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title="Nomadic Trail",
        Version="v1",
        Description="API pour la gestion de l'agence Nomadic Trail",
        Contact=new OpenApiContact
        {
            Name="Markus Emile",
            Email="admin@nomadic-trail.be"
        }
    });
});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nomadic Trail v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<DiTestMiddleware>();


app.Run();
