using Shelf.API.Common.Api;
using Shelf.Core.Handlers;
using Shelf.Core.Models;
using Shelf.Core.Requests.Categories;
using Shelf.Core.Responses;

namespace Shelf.API.Endpoints.Categories
{
    public class DeleteCategoryEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("{id}", HandleAsync)
            .WithName("Categories: Delete")
            .WithSummary("Deleta uma categoria")
            .WithDescription("Deleta uma Categoria")
            .WithOrder(3)
            .Produces<Response<Category?>>();

        public static async Task<IResult> HandleAsync(long id, ICategoryHandler handle)
        {
            var request = new DeleteCategoryRequest 
                {Id = id};

            var result = await handle.DeleteAsync(request);

            return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);   
        }
    }
}
