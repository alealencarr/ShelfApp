using Microsoft.AspNetCore.Mvc;
using Shelf.API.Common.Api;
using Shelf.Core.Handlers;
using Shelf.Core.Models;
using Shelf.Core.Requests.Categories;
using Shelf.Core.Responses;

namespace Shelf.API.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndPoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Categories: Update")
            .WithSummary("Atualiza uma categoria existente.")
            .WithName("Atualiza uma categoria existente.")
            .WithOrder(2)
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(long id, [FromBody] UpdateCategoryRequest request, ICategoryHandler handler)
        {
            request.ID = id;

            var result = await handler.UpdateAsync(request);

            return result.IsSucess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
