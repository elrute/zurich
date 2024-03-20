/*
 * Alberto Blanco 2024 
 * hola@albertoblanco.dev
 * 
 */

using Zurich.BusinessLogic.Services.Abstract;
using Zurich.BusinessLogic.Services;
using Zurich.DataAccess;
using Microsoft.EntityFrameworkCore;
using DataAccess.Repository;
using Zurich.BusinessLogic.Repository.Abstract;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{    
    options.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.None;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShapesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IShapeService, ShapeService>();
builder.Services.AddScoped<IScreenService, ScreenService>();



var app = builder.Build();

// Create the database if it does not exist
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<ShapesContext>();
        dbContext.Database.EnsureCreated();
        DatabaseInitializer.Seed(dbContext); // Seed the database
    }
    catch (Exception ex)
    {
        // Optionally log errors or handle them as needed
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
