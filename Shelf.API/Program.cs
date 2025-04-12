using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shelf.API.Data;
using Shelf.API.Handlers;
using Shelf.Core.Handlers;
using Shelf.Core.Models;
using Shelf.Core.Requests.Categories;
using Shelf.Core.Responses;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
}); //Faz com que gere a documentação informando o nome completo da classe.
//Podemos ter Request em todos os projetos, isso causaria uma dificuldade para o Dev entender de qual Request se trata
//Ao adicionar as funções anonimas e colocar para que o tipo seja n.FullName ele retornará, ex:
// Shelf.Shelf.API.Request pois pode ter Shelf.Shelf.Core.Request

var cnnStr = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x => { x.UseSqlServer(cnnStr); }); 

builder.Services.AddSwaggerGen(setupAction => { setupAction.CustomSchemaIds(n => n.FullName); });

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
    
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
 
 
app.MapPost("/v1/categories",
        async (  CreateCategoryRequest request, ICategoryHandler handler) =>
         await handler.CreateAsync(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response<Category?>>();

app.MapPut("/v1/categories/{id}",
        async (long id, UpdateCategoryRequest request, ICategoryHandler handler) =>
        {
            request.ID = id;
            return await handler.UpdateAsync(request);
        }).WithName("Categories: Update")
        .WithSummary("Atualiza uma categoria existente.")
        .Produces<Response<Category?>>();

app.MapDelete("/v1/categories/{id}",
        async (long id, ICategoryHandler handler) =>
        {
            var request = new DeleteCategoryRequest
            {
                Id = id
            };
            return await handler.DeleteAsync(request);
        }).WithName("Categories: Delete")
        .WithSummary("Deleta uma Categoria")
        .Produces<Response<Category?>>();

app.MapGet("/v1/categories/{id}",
        async (long id, ICategoryHandler handler)
        =>
        {
            var request = new GetCategoryByIdRequest
            {
                Id = id
            };
            
            return await handler.GetByIdAsync(request);
        })
        .WithName("Categories: Get by Id")
        .WithSummary("Retorna uma categoria pelo Id")
        .Produces<Response<Category?>>();


app.MapGet("v1/categories",
       async ([FromQuery] int? pageNumber, [FromQuery] int? pageSize, ICategoryHandler handler) =>
       {
           GetAllCategoriesRequest request = new GetAllCategoriesRequest
           {
               PageNumber = pageNumber ?? 0,
               PageSize = pageSize ?? 25,
           };

           return await handler.GetAllAsync(request);
       }
        
       ).WithName("Categories: Get All")
       .WithSummary("Retorna todas as categorias de um usuário")
       .Produces<PagedResponse<List<Category?>?>>();


app.Run();

 
 