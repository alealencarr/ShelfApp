using Microsoft.EntityFrameworkCore;
using Shelf.API.Data;
using Shelf.Core.Handlers;
using Shelf.Core.Models;
using Shelf.Core.Requests.Categories;
using Shelf.Core.Responses;
using System.Net;

namespace Shelf.API.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        // Ao criar a classe como CategoryHandler(AppDbContext context) é o mesmo que criar um ctor com readonly e passar como param o contexto

        //private readonly AppDbContext _context;

        //public CategoryHandler(AppDbContext context)
        //{
        //    context = _context;
        //}
         public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            try
            {
                var category = new Category
                {
                    UserID = request.UserId,
                    Title = request.Title,
                    Description = request.Description
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(data: category, code: HttpStatusCode.Created, "Categoria criada com sucesso!");
            }
            catch 
            {
                return new Response<Category?>(data: null, code: HttpStatusCode.InternalServerError, "Não foi possível criar a categoria.");
            }
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(cat => cat.UserID == request.UserId && cat.ID == request.Id);

                if (category is null) return new Response<Category?>(data: null, code: HttpStatusCode.NotFound, "Categoria não encontrada.");

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(data: category, code: HttpStatusCode.OK, "Categoria excluída com sucesso!");
            }
            catch
            {
                return new Response<Category?>(data: null, code: HttpStatusCode.InternalServerError, "Não foi possível excluir a categoria.");
            }
        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
        {
            try
            {
                var query = context
                    .Categories
                    .AsNoTracking()
                    .Where(category  => category.UserID == request.UserId)
                    .OrderBy(category => category.Title);

                var categories = await
                    query
                    .Skip(request.PageSize * request.PageNumber)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return categories is null ? new PagedResponse<List<Category>>(data: null, count, request.PageNumber, request.PageSize) :
                 new PagedResponse<List<Category>>(data:categories,count,request.PageNumber, request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Category>>(data: null, HttpStatusCode.InternalServerError, "Categorias não localizadas.");
            }
        }

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(cat => cat.UserID == request.UserId && cat.ID == request.Id);

                return (category is null) ? new Response<Category?>(data: null, code: HttpStatusCode.NotFound, "Categoria não encontrada.") : 
                 new Response<Category?>(data: category, code: HttpStatusCode.OK, "Categoria localizada!");
            }
            catch
            {
                return new Response<Category?>(data: null, code: HttpStatusCode.InternalServerError, "Não foi possível localizar a categoria.");
            }
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            var category = await context.
                           Categories.
                           FirstOrDefaultAsync(cat => (cat.ID == request.ID && cat.UserID == request.UserId) );

            if (category is null) return new Response<Category?>(data: null, code: HttpStatusCode.NotFound, "Categoria não encontrada.");

            category.Title = request.Title;
            category.Description = request.Description;

            try
            {
                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, message:"Categoria atualizada com sucesso!");
            }
            catch
            {
                return new Response<Category?>(data: null, code: HttpStatusCode.InternalServerError, "Não foi possível atualizar a Categoria.");
            }
        }
    }
}
