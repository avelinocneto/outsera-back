using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using outsera_back.Context;
using outsera_back.Helpers;
using outsera_back.Services;
using outsera_back.Services.Interfaces;
using outsera_back.Transformers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MoviePrizeContext>(options =>
    options.UseSqlite("Data Source=movieprizes.db"));

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new LowercaseParameterTransformer()));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "outsera-back API",
        Description = "ASP.NET Core Web API para gerenciamento de Premiações de Filmes",
        Contact = new OpenApiContact
        {
            Name = "Avelino C Neto",
        },
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddScoped<CsvImporter>();
builder.Services.AddScoped<IMoviePrizeService, MoviePrizeService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MoviePrizeContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
    var csvImporter = scope.ServiceProvider.GetRequiredService<CsvImporter>();
    var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "movielist.csv");
    csvImporter.ImportCsvData(filePath);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();
app.UseCors();
app.Run();