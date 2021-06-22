using FunShop.Domain.Entity;
using FunShop.Domain.IRepository;
using FunShop.Domain.Specifiction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.Database.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly ShopDbContext ctx;

        public ProductRepository(ShopDbContext ctx)
        {
            this.ctx = ctx;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var prs = ctx.Products.OrderBy(x => x.Created);
            return prs;
        }
        
        public IEnumerable<Product> GetAllProducts(Paging page)
        {
            IEnumerable<Product> prs = ctx.Products.OrderBy(x => x.Created);

            if (!string.IsNullOrEmpty(page.Search))
            {
                prs = prs.Where(s => s.Name.Contains(page.Search));
            }
                
            prs=prs.Skip((page.PageIndex - 1) * page.PageSize).Take(page.PageSize);
            return prs;
        }
        public Product GetProductById(int id)
        {
            var pr = ctx.Products.FirstOrDefault(x => x.Id == id);
            return pr;
        }
        public void AddProduct(Product pr)
        {
             ctx.Products.Add(pr);
            ctx.SaveChanges();
            
        }

        public void DeleteProduct(int id)
        {
            var product = ctx.Products.FirstOrDefault(x => x.Id == id);
            ctx.Products.Remove(product);
            ctx.SaveChanges();
        }
        public void UpdataProduct(Product product)
        {
            ctx.Products.Update(product);
            ctx.SaveChanges();
        }

        
    }
}
