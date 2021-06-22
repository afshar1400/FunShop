using FunShop.Core.services;
using FunShop.Domain.Entity;
using FunShop.Domain.Specifiction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.Core.ViewModel
{
    public class ProductVMPaging
    {
        public IEnumerable<Product> Products { get; set; }

        public Paging Paging { get; set; }
    }
}
