using Shelf.API.Common.Api;
using Shelf.Core.Handlers;
using Shelf.Core.Models;
using Shelf.Core.Requests.Categories;
using Shelf.Core.Responses;

namespace Shelf.API.Endpoints.Categories
{
    public class GetByIdCategoryEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("{id}", HandleAsync)
            .WithName("Categories: Get By Id")
            .WithSummary("Busca uma categoria pelo ID")
            .WithDescription("Busca uma categoria pelo ID")
            .WithOrder(4)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(long id, ICategoryHandler handler)
        {
            var request = new GetCategoryByIdRequest
            {  
                Id = id
            };

            var result = await handler.GetByIdAsync(request);

            return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
      
    }
}
