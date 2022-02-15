using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject_Nurlan.Data.Entities
{
    public class Author:BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Book> Books { get; set; }

    }
}
