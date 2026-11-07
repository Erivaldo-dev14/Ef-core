using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class UpdateBookDto
    {
        public  string? Title { get; set; }
        public  string? Author { get; set; }
        public  Decimal? Price { get; set; }

    }
}
