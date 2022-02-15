using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject_Nurlan.Data.Entities
{
    public class Genre:BaseEntity
    {
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
