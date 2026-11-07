using Application.Interfaces;
using Application.Exceptions;
using Domain.Dtos;
using Domain.Entities;
using System.Reflection.Metadata.Ecma335;

namespace Application.Services
{
    public class BookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        public async Task<Book> Create(CreateBookDto dto)
        {
            var book = new Book(dto.Title, dto.Author, dto.Price);

            _bookRepository.Create(book);
            await _unitOfWork.SaveChangesAsync();

            return book;
        }

        public async Task<Book> GetById(Guid id) 
        {
            if(id == Guid.Empty)
                throw new ArgumentException(nameof(id)); 
            var Book = await _bookRepository.GetById(id);
            if(Book == null)
                throw new NotFoundException(nameof(Book));
            return Book;

        }

        public async Task Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id));

            var Operation = await _bookRepository.Delete(id);

            if (Operation == false)
                throw new NotFoundException("The Book couldn't be deleted 'cause it wasn't even found.");
            await _unitOfWork.SaveChangesAsync();
        }

       
        public async Task Update(Guid id, UpdateBookDto update)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id));

            var Book = await _bookRepository.GetById(id);
          
            if (Book == null)
                throw new NotFoundException(nameof(Book));

            if (!string.IsNullOrWhiteSpace(update.Author))
                Book.ChangeAuthor(update.Author);
            if (!string.IsNullOrWhiteSpace(update.Title))
                Book.ChangeTitle(update.Title);
            if(update.Price.HasValue)
                Book.ChangePrice(update.Price.Value);

            await _unitOfWork.SaveChangesAsync();

        }

        public async Task<IEnumerable<GetBookDto>> GetAll(int page)
        {
            int pageSize = 3;
            if (page <= 0)
                throw new ArgumentException("The page must be integer");

            var valores = await _bookRepository.GetAll(page, pageSize);

            return valores.Select(b => new GetBookDto
            {

                Id = b.Id,
                Author = b.Author,
                Price = b.Price,
                Title = b.Title


            });
    

        }

    }
}
