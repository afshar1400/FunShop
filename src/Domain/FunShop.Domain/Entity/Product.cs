using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.Domain.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public int InStock { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public DateTime Created { get; set; } = DateTime.Today;
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
