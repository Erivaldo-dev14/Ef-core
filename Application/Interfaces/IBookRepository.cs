using System;
using System.Collections.Generic;

    using Domain.Entities;

    namespace Application.Interfaces
    {
        public interface IBookRepository
        {

            Task Create(Book book);
            Task<int> Update(Book book);
            Task<Book> Delete(Guid id);
            Task<IEnumerable<Book>> GetAll();
            Task<Book> GetById(Guid id);

        }
    }


