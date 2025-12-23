using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Dima.Api.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
    {
        try
        {
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 201, "Categoria cadastrada com sucesso");
        }
        catch (Exception ex)
        {
            return new Response<Category?>(null, 500, "Categoria pode ser cadastrada"); 
        }
    }

    public Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<List<Category>>> GetAllAsync(GetAllCategoryRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var categoria = await context.Categories
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (categoria == null)
                return new Response<Category?>(null, 404, "Categoria não encontrada");
            
            categoria.Title = request.Title;
            categoria.Description = request.Description;

            context.Categories.Update(categoria);
            await context.SaveChangesAsync();

            return new Response<Category?>(categoria, message: "Categoria atualizada com sucesso");
        }
        catch
        {
            return new Response<Category?>(null, 500, "Não foi possivel alterar a categoria");
        }
    }
}
