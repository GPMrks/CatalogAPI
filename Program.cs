using System.Text.Json.Serialization;
using CatalogAPI.Context;
using CatalogAPI.Filters;
using CatalogAPI.Logging;
using CatalogAPI.Repositories;
using CatalogAPI.Repositories.Impl;
using CatalogAPI.Services;
using CatalogAPI.Services.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<ApiLoggingFilter>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v5.0.1",
        Title = "Catalog API",
        Description = "Catalog API for Products and Categories of Products - ASP.NET Core",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Guilherme Marques",
            Email = "guilhermepereiramarques@hotmail.com",
            Url = new Uri("https://linkedin.com/in/guilherme-p-marques")
        }
        // License = new OpenApiLicense
        // {
        //     Name = "Use under LICX",
        //     Url = new Uri("https://example.com/license"),
        // }
    });
    // var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var connectionString = builder.Configuration.GetConnectionString("CatalogApiContext");

builder.Services.AddDbContext<CatalogApiContext>(optionsAction =>
    optionsAction.UseNpgsql(connectionString,
        npgsqlDbContextOptionsBuilder => npgsqlDbContextOptionsBuilder.MigrationsAssembly("CatalogAPI")));

// builder.Logging.ClearProviders();
builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(endpoint =>
    {
        endpoint.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API");
        endpoint.RoutePrefix = "";
        endpoint.DocExpansion(DocExpansion.List);
    });
}

DatabaseManagementService.MigrationInitialization(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();