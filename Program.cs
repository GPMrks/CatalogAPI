using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CatalogAPI.Context;
using CatalogAPI.Services;
using CatalogAPI.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Catalog API for Products and Categories of Products",
        Description = "A simple example ASP.NET Core Web API",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Guilherme Marques",
            Email = "guilhermepereiramarques@hotmail.com",
            Url = new Uri("https://linkedin.com/in/guilherme-p-marques"),
        },
        // License = new OpenApiLicense
        // {
        //     Name = "Use under LICX",
        //     Url = new Uri("https://example.com/license"),
        // }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddDbContext<CatalogApiContext>(optionsAction =>
    optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("ProductsApiContext"),
        npgsqlDbContextOptionsBuilder => npgsqlDbContextOptionsBuilder.MigrationsAssembly("CatalogAPI")));

var app = builder.Build();

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