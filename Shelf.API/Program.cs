using Microsoft.EntityFrameworkCore;
using Shelf.API.Data;
using Shelf.Core.Models;

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

builder.Services.AddTransient<Handler>();
    
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
//app.MapGet("/", () => "Hello World!");
//app.MapPost("/", () => "Hello World!");
//app.MapPut("/", () => "Hello World!");
//app.MapDelete("/", () => "Hello World!");
 
app.MapPost("/v1/categories",
        (Request request, Handler handler) =>  handler.Handle(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response>();

app.Run();

public class Request 
{
    public string Title { get; set; } = string.Empty;
 
    public string Description { get; set; } = string.Empty;
    
}

public class Response
{
    public long  ID { get; set; }
    public string Title { get; set; }  = string.Empty;
}

public class Handler(AppDbContext context)
{
    public Response Handle(Request request)
    {
        var category = new Category
        {
            Title = request.Title,
            Description = request.Description
        };

        context.Categories.Add(category);
        context.SaveChanges();

        return new Response
        {       
            ID = category.ID,
            Title = category.Title    
        };
    }
}
 