
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

        public void Create(Book book)
        {
              context.Add(book);
        }

        public async Task<bool> Delete(Guid id)
        {
            var book =  await context.FindAsync<Book>(id);
            
            if(book == null)
                return false;

            context.Remove(book);

           return  true;
        }

        

        public async Task<Book?> GetById(Guid id)
        {
            return await context.Books.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAll( int page,int pageSize)
        {
            return await context.Books
                .OrderBy(b => b.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking().ToListAsync();
        }
    }
}
