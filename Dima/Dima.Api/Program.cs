using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;
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

app.MapPut("v1/categories/{id}", async ([FromRoute] long id, UpdateCategoryRequest request, ICategoryHandler handler)
    =>
    {
        request.Id = id;
        return await handler.UpdateAsync(request);
    })
    .WithName("Categories: Update")
    .WithSummary("Atualiza uma categoria")
    .Produces<Response<Category?>>();

app.MapDelete("v1/categories/{id}", async ([FromRoute] long id, ICategoryHandler handler)
    =>
    {
        var request =  new DeleteCategoryRequest { Id = id };
        return await handler.DeleteAsync(request);
    })
    .WithName("Categories: Delete")
    .WithSummary("Remove uma categoria")
    .Produces<Response<Category?>>();

app.MapGet("v1/categories/{id}", async ([FromRoute] long id, ICategoryHandler handler)
    =>
    {
        var request = new GetCategoryByIdRequest { Id = id };
        return await handler.GetByIdAsync(request);
    })
    .WithName("Categories: Get By ID")
    .WithSummary("Consulta uma categoria")
    .Produces<Response<Category>>();

app.MapGet("v1/categories", async (ICategoryHandler handler)
    =>
    {
        var request = new GetAllCategoryRequest();
        return await handler.GetAllAsync(request);
    })
    .WithName("Categories: Get All")
    .WithSummary("Retorna todas as categorias")
    .Produces<PagedResponse<Category>?>();

app.Run();
