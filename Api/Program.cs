using Application.Exceptions;
using Application.Interfaces;
using Application.Services;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Persintense;
using Infrastructure.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<BookService>();
var app = builder.Build();



app.MapGet("/", () => "Hello World!");


app.MapPost("/Books", async (BookService service, CreateBookDto dto) =>
{
    try
    {
        var book = await service.Create(dto);
        return Results.Created($"/Books/{book.Id}", book);
    }
    catch (DbUpdateException ex) when (ex.InnerException is SqlException sql
                                       && (sql.Number == 2601 || sql.Number == 2627))
    {
        return Results.Conflict("Este livro já existe.");   // 409
    }
});

app.MapGet("/Books", async (BookService service, int page) =>
{    
    var  books = await service.GetAll(page);

    return Results.Ok(books);

});

app.MapGet("/Books/{id:guid}", async (BookService service, Guid id) =>
{
    Book book;
    try
    {
       book = await service.GetById(id);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
    catch(NotFoundException ex)
    {
        return Results.NotFound(ex.Message);
    }

    return Results.Ok(book);
});

app.MapPatch("/Books/{id:guid}", async(BookService service, Guid id, UpdateBookDto dto) => 
{
    try
    {
        await service.Update(id, dto);

    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
    catch (NotFoundException ex) 
    {
        return Results.NotFound(ex.Message);
    }

    return Results.Ok();
});

app.MapPut("/Books/{id:guid}", async (BookService service, Guid id, UpdateBookDto dto) =>
{
    try
    {
        await service.Update(id, dto);

    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
    catch (NotFoundException ex)
    {
        return Results.NotFound(ex.Message);
    }

    return Results.Ok();
});

app.MapDelete("/Books/{id:guid}", async (BookService service, Guid id) =>
{
    try
    {
        await service.Delete(id);
    }
    catch(NotFoundException ex)
    {
        return Results.NotFound(ex.Message);
    }
    catch(ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }

    return Results.NoContent();
});




app.Run();
