using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.MVC.Models
{
    public class ProductWithFileVM
    {
        public int Id { get; set; }
        public int InStock { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }
}
