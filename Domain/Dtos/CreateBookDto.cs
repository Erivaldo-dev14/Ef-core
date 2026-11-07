using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class CreateBookDto
    {
        public required string Title { get;  set; }
        public required string Author { get;  set; }
        public required Decimal Price { get;  set; }
    }
}
