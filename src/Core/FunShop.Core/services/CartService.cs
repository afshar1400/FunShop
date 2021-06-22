using FunShop.Core.Infrastructrue;
using FunShop.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.Core.services
{
    [Inject]
    public class CartService
    {
        private readonly IProductRepository productRepo;
        private readonly ISessionManager sessionManager;

        public CartService(IProductRepository productRepository,ISessionManager sessionManager)
        {
            this.productRepo = productRepository;
            this.sessionManager = sessionManager;
        }

        public void AddToCart(int id)
        {
            var product = productRepo.GetProductById(id);
            var proItem = new ItemCart {ProductId=product.Id ,ImageName=product.ImageName,Name=product.Name,Price=product.Price};
            sessionManager.AddToSessions(proItem);

        }

        public void RemoveFromCart(int id)
        {
            var product = productRepo.GetProductById(id);
            var proItem = new ItemCart { ProductId = product.Id, ImageName = product.ImageName, Name = product.Name, Price = product.Price };
            sessionManager.RemoveFromSessions(proItem);

        }

        public IEnumerable<ItemCart> GetCartItem()
        {
            return sessionManager.GetCartItem();
        }
    }

    public class ItemCart
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }

        public int Price { get; set; }

        public string Name { get; set; }
        public string ImageName { get; set; }
    }
}
