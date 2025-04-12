namespace Shelf.API.Common.Api
{
    public interface IEndPoint
    {
        abstract static void Map(IEndpointRouteBuilder app);
    }
}
