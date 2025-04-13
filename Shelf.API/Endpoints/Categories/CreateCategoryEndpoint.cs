using Microsoft.AspNetCore.Mvc;
using Shelf.API.Common.Api;
using Shelf.Core.Handlers;
using Shelf.Core.Models;
using Shelf.Core.Requests.Categories;
using Shelf.Core.Responses;

namespace Shelf.API.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("", HandleAsync)
            .WithName("Categories: Created")
            .WithSummary("Cria uma nova categoria")
            .WithDescription("Cria uma nova categoria")
            .WithOrder(1)
            .Produces<Response<Category?>>();
 
        
        private static async Task<IResult> HandleAsync([FromBody] CreateCategoryRequest request, ICategoryHandler handler)
        {
            var result = await handler.CreateAsync(request);

            return result.IsSucess ?
                 TypedResults.Created($"/{result.Data?.ID}", result) :
                 TypedResults.BadRequest(result);
        }
    }
}
