using Infrastructure.Persintense;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();
using var scope = app.Services.CreateScope();


var context = scope.ServiceProvider
                   .GetRequiredService<LibraryDbContext>();

var canConnect = context.Database.CanConnect();

Console.WriteLine($"Conexão: {canConnect}");


app.MapGet("/", () => "Hello World!");


app.Run();
