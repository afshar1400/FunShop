using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.MVC.Models
{
    public class ItemCart
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }

        public int Price { get; set; }

        public string Name { get; set; }
        public string ImageName { get; set; }
    }
}
