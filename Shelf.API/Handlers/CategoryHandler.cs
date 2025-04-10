using Shelf.API.Data;
using Shelf.Core.Handlers;
using Shelf.Core.Models;
using Shelf.Core.Requests.Categories;
using Shelf.Core.Responses;

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
         public async Task<Response<Category>> CreateAsync(CreateCategoryRequest request)
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

                return new Response<Category>(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);                 
            }
        }

        public Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetAllAsync(GetAllCategoriesRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Category>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
