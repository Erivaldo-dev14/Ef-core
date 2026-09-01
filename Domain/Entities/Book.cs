using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Book
    {
        public Book(string title, string author, decimal price)
        {
            if (price < 0)
                throw new ArgumentException(nameof(price));
            Id = Guid.NewGuid();
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Author = author ?? throw new ArgumentNullException(nameof(author));
            CreatedAt = DateTime.Now;
            LastUpdate = DateTime.Now;
            Price = price;
        }

        public Guid Id { get; private set; }
        public string Title { get;private set; }
        public string Author { get;private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdate { get; private set; }
        public Decimal Price { get ; private set; }

    }
}
