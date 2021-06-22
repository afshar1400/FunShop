using FunShop.Domain.Entity;
using FunShop.Domain.Specifiction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.Domain.IRepository
{
    public interface IProductRepository
    {
        void AddProduct(Product pr);
        void DeleteProduct(int id);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetAllProducts(Paging page);
        Product GetProductById(int id);
        void UpdataProduct(Product product);
    }
}
