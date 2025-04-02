using Microsoft.EntityFrameworkCore;
using Shelf.API.Data;

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
 
app.MapPost("/v1/transactions", 
        (Request request, Handler handler) =>  handler.Handle(request))
    .WithName("Transactions: Create")
    .WithSummary("Cria uma nova transação")
    .Produces<Response>();

app.Run();

public class Request 
{
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int Type { get; set; } 
    public decimal Amount { get; set; }
    public long CategoryID { get; set; }
    public string UserID { get; set; } = string.Empty;
    
}

public class Response
{
    public long  ID { get; set; }
    public string Title { get; set; }  = string.Empty;
}

public class Handler
{
    public Response Handle(Request request)
    {
        return new Response
        {       
            ID = 4,
            Title = request.Title    
        };
    }
}
 