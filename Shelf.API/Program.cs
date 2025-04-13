using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shelf.API.Data;
using Shelf.API.Endpoints;
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

app.MapGet("/", () => new { message = "OK" });
app.MapEndpoints();
 
 


app.Run();

 
 