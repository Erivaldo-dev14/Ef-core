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
            Price = price;
            CreatedAt = DateTime.UtcNow;
            LastUpdate = CreatedAt;

        }

        public Guid Id { get; private set; }
        public string Title { get;private set; }
        public string Author { get;private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdate { get; private set; }
        public Decimal Price { get ; private set; }


        public void ChangeTitle(string title)
        {
            Title = !string.IsNullOrWhiteSpace(title) ? title : throw new InvalidDataException("Title");
            LastUpdate = DateTime.UtcNow;

        }
        public void ChangeAuthor(string author)
        {
            Author = !string.IsNullOrWhiteSpace(author)? author :  throw new InvalidDataException("Author");
            LastUpdate = DateTime.UtcNow;

        }
        public void ChangePrice(Decimal price)
        {
            Price = price >= 0 ? price : throw new ArgumentOutOfRangeException("Price");
            LastUpdate = DateTime.UtcNow;
        }
    }
}
