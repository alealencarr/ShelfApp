using Microsoft.AspNetCore.Mvc;
using Shelf.API.Common.Api;
using Shelf.Core.Handlers;
using Shelf.Core.Models;
using Shelf.Core.Requests.Categories;
using Shelf.Core.Responses;

namespace Shelf.API.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        =>
            app.MapGet("/", HandleAsync)
                .WithName("Categories: Get All Categories")
                .WithSummary("Retorna todas as categorias")
                .WithDescription("Retorna todas as categorias")
                .WithOrder(5)
                .Produces<PagedResponse<List<Category>>> ();
         

        public static async Task<IResult> HandleAsync([FromQuery] int? pageNumber, [FromQuery] int? pageSize, ICategoryHandler handler)
        {
            var request = new GetAllCategoriesRequest
            {
                PageNumber = pageNumber ?? 0,
                PageSize = pageSize ?? 25
            };

            var result = await handler.GetAllAsync(request);

            return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
