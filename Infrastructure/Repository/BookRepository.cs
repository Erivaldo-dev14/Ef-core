using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persintense;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository
{
    public class BookRepository : IBookRepository
    {
       private readonly LibraryDbContext context;

        public BookRepository(LibraryDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Create(Book book)
        {
            context.Add(book);
            await context.SaveChangesAsync();
           
        }

        public async Task<Book>? Delete(Guid id)
        {
            var book =  context.Find<Book>(id);
             if(book == null)
                return book;

             context.Remove(book);

            await context.SaveChangesAsync();
            return book;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await context.Books.ToListAsync();
        }

        public async Task<Book>? GetById(Guid id)
        {
            return await context.Books.FindAsync(id);
        }

        public async Task<int> Update(Book book)
        {
            context.Update(book);
            var affected = await context.SaveChangesAsync();

            return affected;
        }
    }
}
