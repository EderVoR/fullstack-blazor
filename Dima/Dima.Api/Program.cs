using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("PostgresDB");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connection);
});

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

app.MapPost("v1/categories", (CreateCategoryRequest request, ICategoryHandler handler) 
    => handler.CreateAsync(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response<Category>>();

app.MapPut("v1/categories", (UpdateCategoryRequest request, ICategoryHandler handler)
    => handler.UpdateAsync(request))
    .WithName("Categories: Update")
    .WithSummary("Atualiza uma categoria")
    .Produces<Response<Category>>();

app.Run();
