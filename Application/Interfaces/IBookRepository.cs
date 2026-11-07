using System;
using System.Collections.Generic;

    using Domain.Entities;

    namespace Application.Interfaces
    {
        public interface IBookRepository
        {

            void Create(Book book);
            Task<bool> Delete(Guid id);
            Task<IEnumerable<Book>> GetAll(int page, int PageSize);
            Task<Book?> GetById(Guid id);

        }
    }


